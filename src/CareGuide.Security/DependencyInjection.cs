using CareGuide.Security.Contexts;
using CareGuide.Security.DTOs;
using CareGuide.Security.Interfaces;
using CareGuide.Security.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace CareGuide.Security
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddSecurity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IJwtService, JwtService>();
            services.AddScoped<IUserSessionContext, UserSessionContext>();

            services.AddOptions<SecuritySettingsDto>()
                .Bind(configuration.GetSection("SecuritySettings"))
                .Validate(settings => !string.IsNullOrWhiteSpace(settings.SecretKey), "SecuritySettings:SecretKey is required.")
                .Validate(settings => !string.IsNullOrWhiteSpace(settings.Issuer), "SecuritySettings:Issuer is required.")
                .ValidateOnStart();

            var secretKey = configuration["SecuritySettings:SecretKey"];
            var issuer = configuration["SecuritySettings:Issuer"];
            var audience = configuration["SecuritySettings:Audience"];

            if (string.IsNullOrWhiteSpace(secretKey))
                throw new InvalidOperationException("SecuritySettings:SecretKey is not configured.");

            if (string.IsNullOrWhiteSpace(issuer))
                throw new InvalidOperationException("SecuritySettings:Issuer is not configured.");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidateAudience = !string.IsNullOrWhiteSpace(audience),
                    ValidAudience = audience,
                    NameClaimType = "sub"
                };
            });

            services.AddAuthorizationBuilder();
            services.AddHttpContextAccessor();

            return services;
        }
    }
}
