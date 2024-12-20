﻿using Microsoft.Extensions.DependencyInjection;
using CareGuide.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using CareGuide.Models.DTOs.Auth;
using CareGuide.Core.Interfaces;

namespace CareGuide.Tests
{
    public class Program: IDisposable
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

            CreateSeeds();

        }

        private void CreateSeeds()
        {
            using (var scope = serviceProvider.CreateScope())
            {
                try
                {
                    IAccountService accountService = scope.ServiceProvider.GetRequiredService<IAccountService>();

                    CreateAccountDto createAccount = new CreateAccountDto()
                    {
                        Email = "test123@domaintest.com.br",
                        Password = "Test123",
                        Name = "Test name",
                        Gender = Models.Enums.Gender.F,
                        Birthday = DateOnly.FromDateTime(DateTime.Now)
                    };

                    accountService.CreateAccount(createAccount);
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
