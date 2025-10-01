using CareGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class RefreshTokenMapping : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_token");
            builder.HasKey(rt => rt.Id);

            builder.Property(rt => rt.Id).HasColumnName("id");
            builder.Property(rt => rt.UserId).IsRequired().HasColumnName("user_id");
            builder.Property(rt => rt.Token).IsRequired().HasMaxLength(255).HasColumnName("token");
            builder.Property(rt => rt.ExpiresAt).IsRequired().HasColumnName("expires_at");
            builder.Property(rt => rt.Revoked).IsRequired().HasDefaultValue(false).HasColumnName("revoked");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnName("updated_at");
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("is_active");

            builder.HasOne(rt => rt.User).WithMany(u => u.RefreshTokens).HasForeignKey(rt => rt.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
