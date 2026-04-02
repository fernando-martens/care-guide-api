using CareGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Infra.Mappings
{
    public class PersonPhoneMapping : IEntityTypeConfiguration<PersonPhone>
    {
        public void Configure(EntityTypeBuilder<PersonPhone> builder)
        {
            builder.ToTable("person_phones");

            builder.HasKey(x => new { x.PersonId, x.PhoneId });

            builder.Property(x => x.PersonId).IsRequired().HasColumnName("person_id");
            builder.Property(x => x.PhoneId).IsRequired().HasColumnName("phone_id");

            builder.HasOne(x => x.Person).WithMany(x => x.PersonPhones).HasForeignKey(x => x.PersonId).HasConstraintName("fk_person_phone_person").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Phone).WithMany(x => x.PersonPhones).HasForeignKey(x => x.PhoneId).HasConstraintName("fk_person_phone_phone").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
