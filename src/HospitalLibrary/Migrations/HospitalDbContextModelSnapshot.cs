﻿// <auto-generated />
using System;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace HospitalLibrary.Migrations
{
    [DbContext(typeof(HospitalDbContext))]
    partial class HospitalDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.10")
                .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            modelBuilder.Entity("HospitalLibrary.Core.Model.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("CancelationTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Canceled")
                        .HasColumnType("boolean");

                    b.Property<int>("DoctorId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<bool>("Used")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.ToTable("appointments");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            CancelationTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Canceled = false,
                            DoctorId = 2,
                            EndTime = new DateTime(2023, 5, 20, 10, 0, 0, 0, DateTimeKind.Unspecified),
                            PatientId = 1,
                            StartTime = new DateTime(2023, 5, 20, 9, 30, 0, 0, DateTimeKind.Unspecified),
                            Used = false
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.PatientHealthData", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<string>("BloodPresure")
                        .HasColumnType("text");

                    b.Property<string>("BloodSugar")
                        .HasColumnType("text");

                    b.Property<string>("BodyFatPercentage")
                        .HasColumnType("text");

                    b.Property<DateTime>("MeasurementTime")
                        .HasColumnType("timestamp without time zone");

                    b.Property<int>("PatientId")
                        .HasColumnType("integer");

                    b.Property<string>("Weight")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("PatientId");

                    b.ToTable("healthdata");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            BloodPresure = "120/80",
                            BloodSugar = "12",
                            BodyFatPercentage = "17",
                            MeasurementTime = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            PatientId = 1,
                            Weight = "102"
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.Room", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<int>("Floor")
                        .HasColumnType("integer");

                    b.Property<string>("Number")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("rooms");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Floor = 1,
                            Number = "101A"
                        },
                        new
                        {
                            Id = 2,
                            Floor = 2,
                            Number = "204"
                        },
                        new
                        {
                            Id = 3,
                            Floor = 3,
                            Number = "305B"
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

                    b.Property<DateTime>("DateOfBirth")
                        .HasColumnType("timestamp without time zone");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Email")
                        .HasColumnType("text");

                    b.Property<int>("Gender")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .HasColumnType("text");

                    b.Property<string>("Phone")
                        .HasColumnType("text");

                    b.Property<string>("ProfileImage")
                        .HasColumnType("text");

                    b.Property<int>("Role")
                        .HasColumnType("integer");

                    b.Property<string>("Surname")
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("users");

                    b.HasDiscriminator<string>("Discriminator").HasValue("User");
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.Doctor", b =>
                {
                    b.HasBaseType("HospitalLibrary.Core.Model.User");

                    b.Property<string>("LicenseNumber")
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue("Doctor");

                    b.HasData(
                        new
                        {
                            Id = 2,
                            DateOfBirth = new DateTime(1967, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "miki@gmail.com",
                            Gender = 0,
                            Name = "Miki",
                            Password = "123",
                            Phone = "0691202148",
                            ProfileImage = "",
                            Role = 1,
                            Surname = "Mikic",
                            Username = "miki@gmail.com",
                            LicenseNumber = "123dr2009"
                        },
                        new
                        {
                            Id = 3,
                            DateOfBirth = new DateTime(1991, 2, 4, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "roki@gmail.com",
                            Gender = 0,
                            Name = "Roki",
                            Password = "123",
                            Phone = "06312909304",
                            ProfileImage = "",
                            Role = 1,
                            Surname = "Rokic",
                            Username = "roki@gmail.com",
                            LicenseNumber = "198r2009"
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.Patient", b =>
                {
                    b.HasBaseType("HospitalLibrary.Core.Model.User");

                    b.Property<bool>("Blocked")
                        .HasColumnType("boolean");

                    b.HasDiscriminator().HasValue("Patient");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DateOfBirth = new DateTime(2018, 7, 24, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "vuk@mail.com",
                            Gender = 0,
                            Name = "Vuk",
                            Password = "123",
                            Phone = "06312212",
                            ProfileImage = "",
                            Role = 0,
                            Surname = "Kesic",
                            Username = "vuk@mail.com",
                            Blocked = false
                        },
                        new
                        {
                            Id = 5,
                            DateOfBirth = new DateTime(1988, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "maria@mail.com",
                            Gender = 1,
                            Name = "Maria",
                            Password = "123",
                            Phone = "06893232",
                            ProfileImage = "",
                            Role = 0,
                            Surname = "Rossi",
                            Username = "maria@mail.com",
                            Blocked = false
                        });
                });

            modelBuilder.Entity("HospitalLibrary.Core.Model.PatientHealthData", b =>
                {
                    b.HasOne("HospitalLibrary.Core.Model.Patient", "Patient")
                        .WithMany()
                        .HasForeignKey("PatientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Patient");
                });
#pragma warning restore 612, 618
        }
    }
}
