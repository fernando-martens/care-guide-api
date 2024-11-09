using CareGuide.Data;
using CareGuide.Infra.CrossCutting;
using CareGuide.Security;
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
            NativeInjector.Register(services);
        }

        private static void ConfigureDatabase(IConfiguration configuration, IServiceCollection services)
        {
            services.AddDbContext<DatabaseContext>(opt =>
            {
                var connectionString = configuration.GetConnectionString("DatabaseConnection");
                opt.UseNpgsql(connectionString).EnableSensitiveDataLogging();
            });
        }

        private static void ConfigureAutoMapper(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(UserProfile));
            services.AddAutoMapper(typeof(PersonProfile));
        }

        private static void ConfigureSecuritySettings(IConfiguration configuration, IServiceCollection services)
        {
            var securitySettings = new SecuritySettings();
            configuration.Bind("SecuritySettings", securitySettings);

            if (string.IsNullOrWhiteSpace(securitySettings.SecretKey) ||
                securitySettings.SecretKey == "defaultKey")
            {
                throw new InvalidOperationException("Security key is not configured properly in appsettings.json.");
            }

            services.AddSingleton(securitySettings);
        }


    }
}
