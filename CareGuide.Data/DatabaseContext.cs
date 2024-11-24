﻿using CareGuide.Data.Mappings;
using CareGuide.Models.Tables;
using Microsoft.EntityFrameworkCore;

namespace CareGuide.Data
{
    public class DatabaseContext : DbContext
    {

        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {

        }

        public DbSet<UserTable> Users { get; set; }
        public DbSet<PersonTable> Persons { get; set; }
        public DbSet<PersonAnnotationTable> PersonAnnotations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new UserMapping());
            modelBuilder.ApplyConfiguration(new PersonMapping());
            modelBuilder.ApplyConfiguration(new PersonAnnotationMapping());

            base.OnModelCreating(modelBuilder);
        }


    }
}
