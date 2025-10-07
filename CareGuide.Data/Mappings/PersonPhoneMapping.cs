using CareGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class PersonPhoneMapping : IEntityTypeConfiguration<PersonPhone>
    {
        public void Configure(EntityTypeBuilder<PersonPhone> builder)
        {
            builder.ToTable("person_phones");

            builder.HasKey(x => new { x.PersonId, x.PhoneId });

            builder.Property(x => x.Id).IsRequired().HasColumnName("id");
            builder.Property(x => x.PersonId).HasColumnName("person_id");
            builder.Property(x => x.PhoneId).HasColumnName("phone_id");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnName("updated_at");
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("is_active");

            builder.HasOne(x => x.Person).WithMany(x => x.PersonPhones).HasForeignKey(x => x.PersonId).OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Phone).WithMany(x => x.PersonPhones).HasForeignKey(x => x.PhoneId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
