using Microsoft.Extensions.DependencyInjection;
using CareGuide.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CareGuide.Tests
{
    public class Program
    {
        public ServiceProvider serviceProvider;

        public Program()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .AddEnvironmentVariables()
                       .Build();

            Startup startup = new Startup(configuration);

            IServiceCollection services = new ServiceCollection();
            startup.ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();

            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
                }
            }

        }
    }

    [CollectionDefinition("Startup")]
    public class StartupCollection : ICollectionFixture<Program>
    {
        private readonly Program Program;

        public StartupCollection(Program program)
        {
            Program = program;
        }

        public Program GetProgramInstance()
        {
            return Program;
        }
    }


}
