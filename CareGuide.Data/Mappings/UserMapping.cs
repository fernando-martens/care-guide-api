using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class UserMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("user");

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(x => x.Email).IsRequired().HasMaxLength(255).HasColumnName("email");
            builder.Property(x => x.Password).IsRequired().HasMaxLength(255).HasColumnName("password");
            builder.Property(x => x.SessionToken).HasColumnType("TEXT").HasColumnName("session_token");
            builder.Property(x => x.Register).IsRequired().HasColumnName("register");

            builder.HasIndex(x => x.Email).IsUnique();

            builder.HasOne(u => u.Person).WithOne(p => p.User).HasForeignKey<Person>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
