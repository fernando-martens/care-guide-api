using CareGuide.Data.Mappings;
using CareGuide.Models.Entities;
using CareGuide.Models.Entities.Shared;
using CareGuide.Security.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CareGuide.Data
{
    public class DatabaseContext : DbContext
    {
        private readonly IUserSessionContext? _userSessionContext;

        public DatabaseContext(DbContextOptions<DatabaseContext> options, IUserSessionContext? userSessionContext = null) : base(options)
        {
            _userSessionContext = userSessionContext;
        }

        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Person> Persons { get; set; } = null!;
        public DbSet<PersonAnnotation> PersonAnnotations { get; set; } = null!;
        public DbSet<PersonHealth> PersonHealths { get; set; } = null!;
        public DbSet<Phone> Phones { get; set; } = null!;
        public DbSet<RefreshToken> RefreshTokens { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new PersonAnnotationMapping());
            modelBuilder.ApplyConfiguration(new PersonHealthMapping());
            modelBuilder.ApplyConfiguration(new PhoneMapping());
            modelBuilder.ApplyConfiguration(new RefreshTokenMapping());

            if (_userSessionContext != null && _userSessionContext.PersonId != Guid.Empty)
            {
                var personId = _userSessionContext.PersonId;

                foreach (var entityType in modelBuilder.Model.GetEntityTypes())
                {
                    if (typeof(IPersonOwnedEntity).IsAssignableFrom(entityType.ClrType))
                    {
                        var parameter = Expression.Parameter(entityType.ClrType, "e");
                        var property = Expression.Property(parameter, nameof(IPersonOwnedEntity.PersonId));

                        var body = Expression.OrElse(
                            Expression.Equal(property, Expression.Constant(null, typeof(Guid?))),
                            Expression.Equal(property, Expression.Constant((Guid?)personId, typeof(Guid?)))
                        );

                        var lambda = Expression.Lambda(body, parameter);
                        modelBuilder.Entity(entityType.ClrType).HasQueryFilter(lambda);
                    }
                }
            }

            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            ApplyPersonIdRules();
            UpdateEntityTimestamps();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
        {
            ApplyPersonIdRules();
            UpdateEntityTimestamps();
            return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        private void ApplyPersonIdRules()
        {
            if (_userSessionContext == null || _userSessionContext.PersonId == Guid.Empty)
                return;

            var personId = _userSessionContext.PersonId;

            foreach (var entry in ChangeTracker.Entries<IPersonOwnedEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    if (!entry.Entity.PersonId.HasValue)
                        entry.Entity.PersonId = personId;
                }
                else if (entry.State == EntityState.Modified || entry.State == EntityState.Deleted)
                {
                    if (entry.Entity.PersonId.HasValue && entry.Entity.PersonId != personId)
                        throw new UnauthorizedAccessException("You are not allowed to modify entities from another person.");
                }
            }
        }

        private void UpdateEntityTimestamps()
        {
            var currentTime = DateTime.UtcNow;
            var entries = ChangeTracker.Entries().Where(e => e.Entity is IEntity && (e.State == EntityState.Added || e.State == EntityState.Modified));

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
