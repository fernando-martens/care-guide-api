using CareGuide.Models.Constants;
using CareGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.PersonId).IsRequired().HasColumnName("person_id");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(DatabaseConstants.MaxLengthStandardText).HasColumnName("email");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(DatabaseConstants.MaxLengthStandardText).HasColumnName("password");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnName("updated_at");
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("is_active");

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(x => x.Person).WithMany().HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
