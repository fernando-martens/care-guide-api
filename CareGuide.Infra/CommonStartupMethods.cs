using CareGuide.Data;
using CareGuide.Data.TransactionManagement;
using CareGuide.Infra.CrossCutting;
using CareGuide.Models.DTOs.Account;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.DTOs.PersonHealth;
using CareGuide.Models.DTOs.Phone;
using CareGuide.Models.Mappers.Person;
using CareGuide.Models.Mappers.PersonAnnotation;
using CareGuide.Models.Mappers.PersonHealth;
using CareGuide.Models.Mappers.PersonPhone;
using CareGuide.Models.Mappers.Phone;
using CareGuide.Models.Mappers.User;
using CareGuide.Models.Validators.Account;
using CareGuide.Models.Validators.PersonAnnotation;
using CareGuide.Models.Validators.PersonHealth;
using CareGuide.Models.Validators.Phone;
using CareGuide.Security;
using CareGuide.Security.Interfaces;
using FluentValidation;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.RateLimiting;

namespace CareGuide.Infra
{
    public static class CommonStartupMethods
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            ConfigureSecuritySettings(configuration, services);
            ConfigureAuthentication(configuration, services);
            ConfigureAuthorization(services);
            ConfigureRateLimiting(services);
            ConfigureDatabase(configuration, services);
            ConfigureAutoMapper(services);
            ConfigureValidators(services);
            NativeInjector.Register(services);

            services.AddHttpContextAccessor();
        }

        private static void ConfigureAuthorization(IServiceCollection services)
        {
            services.AddAuthorizationBuilder();
        }

        private static void ConfigureRateLimiting(IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        partitionKey: httpContext.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 10,
                            Window = TimeSpan.FromMinutes(1),
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                            QueueLimit = 0
                        }));

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });
        }

        private static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IEfTransactionUnitOfWork, EfTransactionUnitOfWork>();

            services.AddDbContext<DatabaseContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                var environment = configuration["ASPNETCORE_ENVIRONMENT"];

                opt.UseNpgsql(connectionString);

                if (string.Equals(environment, "Development", StringComparison.OrdinalIgnoreCase))
                {
                    opt.EnableSensitiveDataLogging();
                }
            });
        }

        private static void ConfigureSecuritySettings(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();

            services.AddOptions<SecuritySettings>()
                .Bind(configuration.GetSection("SecuritySettings"))
                .Validate(settings => !string.IsNullOrWhiteSpace(settings.SecretKey), "SecuritySettings:SecretKey is required.")
                .Validate(settings => !string.IsNullOrWhiteSpace(settings.Issuer), "SecuritySettings:Issuer is required.")
                .ValidateOnStart();
        }

        private static void ConfigureAuthentication(IConfiguration configuration, IServiceCollection services)
        {
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
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AccountToPersonProfileMapper>();
                cfg.AddProfile<AccountToUserProfileMapper>();
                cfg.AddProfile<PersonAnnotationProfileMapper>();
                cfg.AddProfile<PersonHealthProfileMapper>();
                cfg.AddProfile<PersonProfileMapper>();
                cfg.AddProfile<UserProfileMapper>();
                cfg.AddProfile<PhoneProfileMapper>();
                cfg.AddProfile<PersonPhoneProfileMapper>();
            });
        }

        private static void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateAccountDto>, CreateAccountDtoValidator>();
            services.AddTransient<IValidator<UpdatePasswordAccountDto>, UpdatePasswordAccountDtoValidator>();
            services.AddTransient<IValidator<CreatePersonAnnotationDto>, CreatePersonAnnotationDtoValidator>();
            services.AddTransient<IValidator<UpdatePersonAnnotationDto>, UpdatePersonAnnotationDtoValidator>();
            services.AddTransient<IValidator<CreatePersonHealthDto>, CreatePersonHealthDtoValidator>();
            services.AddTransient<IValidator<UpdatePersonHealthDto>, UpdatePersonHealthDtoValidator>();
            services.AddTransient<IValidator<CreatePhoneDto>, CreatePhoneDtoValidator>();
            services.AddTransient<IValidator<UpdatePhoneDto>, UpdatePhoneDtoValidator>();
        }
    }
}