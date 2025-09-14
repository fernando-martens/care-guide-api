using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class PersonAnnotationMapping : IEntityTypeConfiguration<PersonAnnotation>
    {
        public void Configure(EntityTypeBuilder<PersonAnnotation> builder)
        {
            builder.ToTable("person_annotation");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(p => p.PersonId).IsRequired().HasColumnName("person_id");
            builder.Property(p => p.Details).IsRequired().HasColumnType("text").HasColumnName("details");
            builder.Property(p => p.FileUrl).HasMaxLength(255).HasColumnName("file_url");
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnName("created_at");
            builder.Property(x => x.UpdatedAt).IsRequired().HasColumnName("updated_at");
            builder.Property(x => x.IsActive).IsRequired().HasDefaultValue(true).HasColumnName("is_active");

            builder.HasOne(p => p.Person).WithMany(p => p.PersonAnnotations).HasForeignKey(p => p.PersonId).HasConstraintName("fk_person_annotation_person").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
