﻿// <auto-generated />
using System;
using CareGuide.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CareGuide.Data.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    [Migration("20241004000641_PersonCreate")]
    partial class PersonCreate
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CareGuide.Models.Tables.Person", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<DateTime>("Birthday")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("birthday");

                    b.Property<string>("Gender")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("gender");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("name");

                    b.Property<DateTime>("Register")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("register");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid")
                        .HasColumnName("user_id");

                    b.HasKey("Id");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("person", (string)null);
                });

            modelBuilder.Entity("CareGuide.Models.Tables.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid")
                        .HasColumnName("id");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("email");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("character varying(255)")
                        .HasColumnName("password");

                    b.Property<DateTime>("Register")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("register");

                    b.Property<string>("SessionToken")
                        .HasColumnType("TEXT")
                        .HasColumnName("session_token");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.ToTable("user", (string)null);
                });

            modelBuilder.Entity("CareGuide.Models.Tables.Person", b =>
                {
                    b.HasOne("CareGuide.Models.Tables.User", "User")
                        .WithOne("Person")
                        .HasForeignKey("CareGuide.Models.Tables.Person", "UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("CareGuide.Models.Tables.User", b =>
                {
                    b.Navigation("Person")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
