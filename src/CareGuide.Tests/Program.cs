using CareGuide.Core;
using CareGuide.Core.Interfaces;
using CareGuide.Infra;
using CareGuide.Infra.Contexts;
using CareGuide.Models;
using CareGuide.Models.DTOs.Account;
using CareGuide.Security;
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

            services
                .AddInfrastructure(configuration)
                .AddCoreServices()
                .AddSecurity(configuration)
                .AddModelMappings()
                .AddModelValidation();

            services.AddScoped<Security.Interfaces.IUserSessionContext, Context.UserSessionContextTests>();

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

                    accountService.CreateAccountAsync(createAccount, CancellationToken.None).GetAwaiter().GetResult();
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
    }
}