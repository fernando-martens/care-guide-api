using CareGuide.Infra.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CareGuide.Infra.Factories
{
    public class DatabaseContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
    {
        public DatabaseContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<DatabaseContext>();

            var connectionString = configuration.GetConnectionString("DatabaseConnection");
            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("CareGuide.Infra"));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
