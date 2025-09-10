using CareGuide.Models.Enums;
using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person", p =>
            {
                p.HasCheckConstraint("CK_Person_Gender", "gender IN ('M', 'F', 'O')");
            });

            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id).HasColumnName("id");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255).HasColumnName("name");
            builder.Property(p => p.Gender).IsRequired().HasColumnType("char(1)").HasConversion(v => v.ToString(), v => (Gender)Enum.Parse(typeof(Gender), v)).HasColumnName("gender");
            builder.Property(p => p.Birthday).IsRequired().HasColumnType("date").HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v)).HasColumnName("birthday");
            builder.Property(p => p.Picture).IsRequired().HasColumnName("picture");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnName("updated_at");
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("is_active");
        }
    }
}
