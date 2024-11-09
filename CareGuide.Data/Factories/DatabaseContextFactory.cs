using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace CareGuide.Data
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
            optionsBuilder.UseNpgsql(connectionString, b => b.MigrationsAssembly("CareGuide.Data"));

            return new DatabaseContext(optionsBuilder.Options);
        }
    }
}
