using CareGuide.Data;
using CareGuide.Data.TransactionManagement;
using CareGuide.Infra.CrossCutting;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Models.Mappers;
using CareGuide.Models.Validators;
using CareGuide.Security;
using CareGuide.Security.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Infra
{
    public static class CommomStartupMethods
    {
        public static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        {
            ConfigureDatabase(configuration, services);
            ConfigureAutoMapper(services);
            ConfigureSecuritySettings(configuration, services);
            ConfigureValidators(services);
            NativeInjector.Register(services);

            services.AddHttpContextAccessor();
        }

        private static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddDbContext<DatabaseContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                opt.UseNpgsql(connectionString).EnableSensitiveDataLogging();
            });
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<AccountToPersonProfileMapper>();
                cfg.AddProfile<AccountToUserProfileMapper>();
                cfg.AddProfile<PersonAnnotationProfileMapper>();
                cfg.AddProfile<PersonProfileMapper>();
                cfg.AddProfile<UserProfileMapper>();
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

        private static void ConfigureValidators(IServiceCollection services)
        {
            services.AddTransient<IValidator<CreateAccountDto>, CreateAccountDtoValidator>();
            services.AddTransient<IValidator<UpdatePasswordAccountDto>, UpdatePasswordAccountDtoValidator>();
        }
    }
}
