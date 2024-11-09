using Microsoft.Extensions.DependencyInjection;
using CareGuide.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace CareGuide.Tests
{
    public class Program : IDisposable
    {

        public Program()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                       .AddJsonFile("appsettings.json")
                       .AddEnvironmentVariables()
                       .Build();

            IServiceCollection services = new ServiceCollection();

            Startup startup = new Startup(configuration);
            startup.ConfigureServices(services);
            ServiceProvider provider = services.BuildServiceProvider();

            using (var scope = provider.CreateScope())
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

            //try
            //{
            //    PopulateSharedDataBetweenTests();
            //}
            //catch (Exception)
            //{
            //    CleanUpSharedDataBetweenTests();
            //    throw new ApplicationException();
            //}
        }

        public void Dispose()
        {
            // CleanUpSharedDataBetweenTests();
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
