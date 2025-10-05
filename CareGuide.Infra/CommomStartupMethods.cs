using CareGuide.Data;
using CareGuide.Data.TransactionManagement;
using CareGuide.Infra.CrossCutting;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.DTOs.PersonAnnotation;
using CareGuide.Models.DTOs.PersonHealth;
using CareGuide.Models.DTOs.Phone;
using CareGuide.Models.Mappers;
using CareGuide.Models.Mappers.PersonHealth;
using CareGuide.Models.Mappers.Phone;
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
    public static class CommomStartupMethods
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            ConfigureAuthentication(configuration, services);
            ConfigureRateLimiting(services);
            ConfigureDatabase(configuration, services);
            ConfigureAutoMapper(services);
            ConfigureSecuritySettings(configuration, services);
            ConfigureValidators(services);
            NativeInjector.Register(services);

            services.AddHttpContextAccessor();
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
                opt.UseNpgsql(connectionString).EnableSensitiveDataLogging();
            });
        }

        private static void ConfigureSecuritySettings(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IJwtService, JwtService>();

            SecuritySettings securitySettings = new SecuritySettings();
            configuration.Bind("SecuritySettings", securitySettings);

            if (string.IsNullOrEmpty(securitySettings.SecretKey))
                throw new InvalidOperationException("Security key is not configured properly in appsettings.json.");

            services.AddSingleton(securitySettings);
        }

        private static void ConfigureAuthentication(IConfiguration configuration, IServiceCollection services)
        {
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
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["SecuritySettings:SecretKey"] ?? "")),
                    ValidateIssuer = true,
                    ValidIssuer = configuration["SecuritySettings:Issuer"] ?? "",
                    ValidateAudience = false,
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
