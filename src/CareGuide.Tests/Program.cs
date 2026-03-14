using CareGuide.Core.Interfaces;
using CareGuide.Data;
using CareGuide.Models.DTOs.Account;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CareGuide.Tests
{
    public class Program : IDisposable
    {
        public ServiceProvider serviceProvider;

        public Program()
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables()
                .Build();

            IServiceCollection services = new ServiceCollection();
            CareGuide.Infra.CommonStartupMethods.ConfigureServices(configuration, services);
            services.AddScoped<CareGuide.Security.Interfaces.IUserSessionContext, CareGuide.Tests.Context.UserSessionContextTests>();
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

            CreateSeeds();
        }

        private void CreateSeeds()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    IAccountService accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();

                    CreateAccountDto createAccount = new CreateAccountDto(
                        "test123@domaintest.com.br",
                        "Test123",
                        "Test name",
                        Models.Enums.Gender.F,
                        DateOnly.FromDateTime(DateTime.Now)
                    );

                    var _ = accountService.CreateAccountAsync(createAccount, CancellationToken.None);
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while create seeds: {ex.Message}");
                }
            }
        }

        public void Dispose()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    var context = scope.ServiceProvider.GetRequiredService<DatabaseContext>();
                    context.Database.EnsureDeleted();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error occurred while deleting the database: {ex.Message}");
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
