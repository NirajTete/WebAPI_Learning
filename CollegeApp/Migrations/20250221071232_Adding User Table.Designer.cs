﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using WebAPI_Learning.Data;

#nullable disable

namespace WebAPI_Learning.Migrations
{
    [DbContext(typeof(CollegeDBContext))]
    [Migration("20250221071232_Adding User Table")]
    partial class AddingUserTable
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.13")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("WebAPI_Learning.Data.Department", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("DepartmentName")
                        .IsRequired()
                        .HasMaxLength(200)
                        .HasColumnType("nvarchar(200)");

                    b.Property<string>("Description")
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.HasKey("Id");

                    b.ToTable("Departments", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DepartmentName = "OT",
                            Description = "Operation Theater Technician"
                        },
                        new
                        {
                            Id = 2,
                            DepartmentName = "School",
                            Description = "NA"
                        },
                        new
                        {
                            Id = 3,
                            DepartmentName = "Haldirams",
                            Description = "Kapsi Plant"
                        },
                        new
                        {
                            Id = 4,
                            DepartmentName = "GNM",
                            Description = "Bhandara College"
                        },
                        new
                        {
                            Id = 5,
                            DepartmentName = "Singer",
                            Description = "Black Pink"
                        });
                });

            modelBuilder.Entity("WebAPI_Learning.Data.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("nvarchar(500)");

                    b.Property<DateTime>("DOB")
                        .HasColumnType("datetime2");

                    b.Property<int?>("DepartmentId")
                        .HasColumnType("int");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("StudentName")
                        .IsRequired()
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Address = "Kapsi",
                            DOB = new DateTime(2004, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "janvi@gmail.com",
                            StudentName = "Janvi"
                        },
                        new
                        {
                            Id = 2,
                            Address = "Nagpur",
                            DOB = new DateTime(2008, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "hasari@gmail.com",
                            StudentName = "Hasari"
                        },
                        new
                        {
                            Id = 3,
                            Address = "Kapsi",
                            DOB = new DateTime(2004, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "jay@gmail.com",
                            StudentName = "Jay"
                        },
                        new
                        {
                            Id = 4,
                            Address = "Bhandara",
                            DOB = new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "lisa@gmail.com",
                            StudentName = "Lisa"
                        },
                        new
                        {
                            Id = 5,
                            Address = "Bhandara",
                            DOB = new DateTime(2005, 6, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "rosy@gmail.com",
                            StudentName = "Rosy"
                        });
                });

            modelBuilder.Entity("WebAPI_Learning.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("IsActive")
                        .HasColumnType("bit");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("ModifiedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserType")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Users", (string)null);
                });

            modelBuilder.Entity("WebAPI_Learning.Data.Student", b =>
                {
                    b.HasOne("WebAPI_Learning.Data.Department", "Department")
                        .WithMany("Students")
                        .HasForeignKey("DepartmentId")
                        .HasConstraintName("FK_Student_Department");

                    b.Navigation("Department");
                });

            modelBuilder.Entity("WebAPI_Learning.Data.Department", b =>
                {
                    b.Navigation("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
