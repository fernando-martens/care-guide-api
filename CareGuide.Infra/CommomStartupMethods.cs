using CareGuide.Data;
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



    }
}
