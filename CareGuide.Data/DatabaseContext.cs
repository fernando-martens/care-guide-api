using CareGuide.Data.Mappings;
using CareGuide.Models.Entities.Shared;
using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<PersonAnnotation> PersonAnnotations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new PersonAnnotationMapping());

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            UpdateEntityTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            UpdateEntityTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void UpdateEntityTimestamps()
        {
            var currentTime = DateTime.UtcNow;

            var entries = ChangeTracker.Entries()
                .Where(e => e.Entity is IEntity &&
                           (e.State == EntityState.Added || e.State == EntityState.Modified));

            foreach (var entityEntry in entries)
            {
                var entity = (IEntity)entityEntry.Entity;
                entity.UpdatedAt = currentTime;

                if (entityEntry.State == EntityState.Added)
                {
                    entity.CreatedAt = currentTime;
                }
            }
        }
    }
}
