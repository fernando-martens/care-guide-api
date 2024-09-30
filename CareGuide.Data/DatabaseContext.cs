using CareGuide.Data.Mappings;
using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data
{
    public class DatabaseContext: DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());

            base.OnModelCreating(modelBuilder);
        }


    }
}
