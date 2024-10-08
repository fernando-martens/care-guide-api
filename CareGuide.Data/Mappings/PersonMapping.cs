﻿using CareGuide.Models.Enums;
using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CareGuide.Data.Mappings
{
    public class PersonMapping : IEntityTypeConfiguration<Person>
    {
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.ToTable("person", t =>
            {
                t.HasCheckConstraint("CK_Person_Gender", "gender IN ('M', 'F', 'O')");
            });

            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id).HasColumnName("id");
            builder.Property(p => p.UserId).HasColumnName("user_id");
            builder.Property(p => p.Name).IsRequired().HasMaxLength(255).HasColumnName("name");
            builder.Property(p => p.Gender).IsRequired().HasColumnType("char(1)").HasConversion(v => v.ToString(), v => (Gender)Enum.Parse(typeof(Gender), v)).HasColumnName("gender");
            builder.Property(p => p.Birthday).IsRequired().HasColumnType("date").HasConversion(v => v.ToDateTime(TimeOnly.MinValue), v => DateOnly.FromDateTime(v)).HasColumnName("birthday");
            builder.Property(x => x.Register).IsRequired().HasColumnName("register");

            builder.HasOne(p => p.User).WithOne(u => u.Person).HasForeignKey<Person>(p => p.UserId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
