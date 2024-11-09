using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<UserTable>
    {
        public void Configure(EntityTypeBuilder<UserTable> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(p => p.PersonId).HasColumnName("person_id");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(255).HasColumnName("password");
            builder.Property(x => x.Register).IsRequired().HasColumnName("register");

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(p => p.Person).WithMany().HasForeignKey(p => p.PersonId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
