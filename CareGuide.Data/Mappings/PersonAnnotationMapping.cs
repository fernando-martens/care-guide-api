using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class PersonAnnotationMapping : IEntityTypeConfiguration<PersonAnnotationTable>
    {
        public void Configure(EntityTypeBuilder<PersonAnnotationTable> builder)
        {
            builder.ToTable("person_annotation");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(p => p.PersonId).IsRequired().HasColumnName("person_id");
            builder.Property(p => p.Details).IsRequired().HasColumnType("text").HasColumnName("details");
            builder.Property(p => p.FileUrl).HasMaxLength(255).HasColumnName("file_url");
            builder.Property(p => p.Register).IsRequired().HasColumnName("register");
            builder.HasOne(p => p.Person).WithMany(p => p.PersonAnnotations).HasForeignKey(p => p.PersonId).HasConstraintName("fk_person_annotation_person").OnDelete(DeleteBehavior.Cascade);
        }
    }
}
