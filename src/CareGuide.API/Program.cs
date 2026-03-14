using CareGuide.API.Middlewares;
using CareGuide.Infra;
using CareGuide.Security;
using CareGuide.Security.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.OpenApi;
using Scalar.AspNetCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

CommonStartupMethods.ConfigureServices(builder.Configuration, builder.Services);
builder.Services.AddScoped<IUserSessionContext, UserSessionContext>();

if (builder.Environment.IsProduction())
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("https://careguide.dev")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}
else
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("CorsPolicy", policy =>
        {
            policy.WithOrigins("http://localhost:8080")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
    });
}

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddOpenApi("v1", options =>
{
    options.AddDocumentTransformer(async (document, context, cancellationToken) =>
    {
        document.Info = new OpenApiInfo
        {
            Title = "CareGuideAPI",
            Version = "v1"
        };

        var authenticationSchemeProvider = context.ApplicationServices.GetRequiredService<IAuthenticationSchemeProvider>();
        var authenticationSchemes = await authenticationSchemeProvider.GetAllSchemesAsync();

        if (authenticationSchemes.Any(scheme => scheme.Name == "Bearer"))
        {
            document.Components ??= new OpenApiComponents();
            document.Components.SecuritySchemes = new Dictionary<string, IOpenApiSecurityScheme>
            {
                ["Bearer"] = new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. Example: \"Authorization: Bearer {token}\""
                }
            };

            foreach (var operation in document.Paths.Values.SelectMany(path => path.Operations ?? Enumerable.Empty<KeyValuePair<HttpMethod, OpenApiOperation>>()))
            {
                operation.Value.Security ??= [];

                operation.Value.Security.Add(new OpenApiSecurityRequirement
                {
                    [new OpenApiSecuritySchemeReference("Bearer", document)] = []
                });
            }
        }
    });
});

builder.Services.AddControllers().AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });
builder.Services.AddTransient<ErrorHandlerMiddleware>();
builder.Services.AddTransient<SessionMiddleware>();

var app = builder.Build();

ConfigurePipeline(app);

app.MapControllers();

app.Run();

static void ConfigurePipeline(WebApplication app)
{
    app.MapOpenApi("/openapi/{documentName}.json");

    if (app.Environment.IsDevelopment())
    {
        app.MapScalarApiReference();
    }
    else
    {
        app.UseHsts();
        app.UseHttpsRedirection();
    }

    app.UseMiddleware<SecurityHeadersMiddleware>();
    app.UseMiddleware<ErrorHandlerMiddleware>();
    app.UseMiddleware<SessionMiddleware>();

    app.UseCors("CorsPolicy");
    app.UseAuthentication();
    app.UseAuthorization();
    app.UseRateLimiter();
}