﻿// <auto-generated />
using System;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace EFCoreRelationshipsPractice.Migrations
{
    [DbContext(typeof(CompanyDbContext))]
    [Migration("20201127152211_AddCompanyId_in_employee22")]
    partial class AddCompanyId_in_employee22
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
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

                    b.Property<int?>("ProfileEntityId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ProfileEntityId");

                    b.ToTable("Companies");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.EmployeeEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("Age")
                        .HasColumnType("int");

                    b.Property<int>("CompanyId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("CompanyId");

                    b.ToTable("Employees");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Repository.ProfileEntity", b =>
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
                    b.HasOne("EFCoreRelationshipsPractice.Repository.ProfileEntity", "ProfileEntity")
                        .WithMany()
                        .HasForeignKey("ProfileEntityId");
                });

            modelBuilder.Entity("EFCoreRelationshipsPractice.Entities.EmployeeEntity", b =>
                {
                    b.HasOne("EFCoreRelationshipsPractice.Entities.CompanyEntity", null)
                        .WithMany("Employees")
                        .HasForeignKey("CompanyId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
