using CareGuide.API.Middlewares;
using CareGuide.Infra;
using CareGuide.Infra.CrossCutting;
using CareGuide.Security;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;

namespace CareGuide.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            CommomStartupMethods.ConfigureServices(Configuration, services);

            ConfigureSecuritySettings(services);

            ConfigureCors(services);

            ConfigureSwagger(services);

            ConfigureJsonSerializer(services);

            ConfigureMiddlewares(services);

            NativeInjector.Register(services);

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.Use(async (context, next) =>
                {
                    context.Response.Headers.Append("Strict-Transport-Security", "max-age=31536000; includeSubDomains");
                    await next();
                });
            }

            app.Use(async (context, next) =>
            {
                context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
                context.Response.Headers.Append("Content-Security-Policy", "default-src 'self'; script-src 'self'; object-src 'none';");
                context.Response.Headers.Append("Referrer-Policy", "no-referrer");
                context.Response.Headers.Append("X-Frame-Options", "DENY");

                await next();
            });

            app.UseRouting();

            app.UseMiddleware<ErrorHandlerMiddleware>();

            app.UseCors("AllowAnyOrigin");

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }

        private void ConfigureSecuritySettings(IServiceCollection services)
        {
            var securitySettings = new SecuritySettings();
            Configuration.Bind("SecuritySettings", securitySettings);

            if (string.IsNullOrWhiteSpace(securitySettings.SecretKey) ||
                securitySettings.SecretKey == "defaultKey")
            {
                throw new InvalidOperationException("Security key is not configured properly in appsettings.json.");
            }

            services.AddSingleton(securitySettings);
        }

        private void ConfigureCors(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAnyOrigin", builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyHeader()
                           .AllowAnyMethod();
                });
            });
        }

        private void ConfigureSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CareGuideAPI", Version = "v1" });
            });
        }

        private void ConfigureJsonSerializer(IServiceCollection services)
        {
            services.AddControllers()
            .AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
        }

        private void ConfigureMiddlewares(IServiceCollection services)
        {
            services.AddTransient<ErrorHandlerMiddleware>();
        }
    }
}
