﻿// <auto-generated />
using System;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreRelationshipsPractice.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    partial class CompanyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.CompanyEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("ProfileId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int?>("CompanyEntityId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CompanyEntityId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.ProfileEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CertId")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("RegisteredCapital")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Profiles");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.CompanyEntity", b =>
                {
                    b.HasOne("EFCoreRelationshipsPractice.Entities.ProfileEntity", "Profile")
                        .WithMany()
                        .HasForeignKey("ProfileId");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("EFCoreRelationshipsPractice.Entities.CompanyEntity", null)
                        .WithMany("Employees")
                        .HasForeignKey("CompanyEntityId");
                });
#pragma warning restore 612, 618
        }
    }
}
