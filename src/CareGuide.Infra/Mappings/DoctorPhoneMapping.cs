using CareGuide.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Infra.Mappings
{
    public class DoctorPhoneMapping : IEntityTypeConfiguration<DoctorPhone>
    {
        public void Configure(EntityTypeBuilder<DoctorPhone> builder)
        {
            builder.ToTable("doctor_phones");

            builder.HasKey(x => new { x.DoctorId, x.PhoneId });

            builder.Property(x => x.DoctorId).IsRequired().HasColumnName("doctor_id");
            builder.Property(x => x.PhoneId).IsRequired().HasColumnName("phone_id");

            builder.HasOne(x => x.Doctor).WithMany(x => x.DoctorPhones).HasForeignKey(x => x.DoctorId).HasConstraintName("fk_doctor_phone_doctor").OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(x => x.Phone).WithMany(x => x.DoctorPhones).HasForeignKey(x => x.PhoneId).HasConstraintName("fk_doctor_phone_phone").OnDelete(DeleteBehavior.Cascade);
        }
    }
}