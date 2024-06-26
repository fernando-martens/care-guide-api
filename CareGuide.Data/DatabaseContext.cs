using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data
{
    public class DatabaseContext: DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        //public DbSet<PetsTable> Pets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.ApplyConfiguration(new PetsTableMapping());

            base.OnModelCreating(modelBuilder);
        }


    }
}
