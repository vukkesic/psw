using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> rooms { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<PatientHealthData> healthdata { get; set; }
        public DbSet<Appointment> appointments { get; set; }
        public DbSet<Doctor> doctors { get; set; }
        public DbSet<Specialization> specializations { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.Entity<PatientHealthData>().HasOne(phd => phd.Patient);
            modelBuilder.Entity<Doctor>().HasOne(doc => doc.Specialization);

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
            );

            modelBuilder.Entity<Specialization>().HasData(
               new Specialization() { Id = 1, SpecName = "Allergy and immunology" },
               new Specialization() { Id = 2, SpecName = "Anesthesiology" },
               new Specialization() { Id = 3, SpecName = "Dermatology" },
               new Specialization() { Id = 4, SpecName = "Diagnostic radiology" },
               new Specialization() { Id = 5, SpecName = "Emergency medicine" },
               new Specialization() { Id = 6, SpecName = "Family medicine" },
               new Specialization() { Id = 7, SpecName = "Internal medicine" },
               new Specialization() { Id = 8, SpecName = "Medical genetics" },
               new Specialization() { Id = 9, SpecName = "Neurology" },
               new Specialization() { Id = 10, SpecName = "Nuclear medicine" },
               new Specialization() { Id = 11, SpecName = "Obstetrics and gynecology" },
               new Specialization() { Id = 12, SpecName = "Ophthalmology" },
               new Specialization() { Id = 13, SpecName = "Pathology" },
               new Specialization() { Id = 14, SpecName = "Pediatrics" },
               new Specialization() { Id = 15, SpecName = "Physical medicine and rehabilitation" },
               new Specialization() { Id = 16, SpecName = "Preventive medicine" },
               new Specialization() { Id = 17, SpecName = "Psychiatry" },
               new Specialization() { Id = 18, SpecName = "Radiation oncology" },
               new Specialization() { Id = 19, SpecName = "Surgery" },
               new Specialization() { Id = 20, SpecName = "Urology" }
               );

            modelBuilder.Entity<Patient>().HasData(
                new Patient()
                {
                    Id = 1,
                    Name = "Vuk",
                    Surname = "Kesic",
                    DateOfBirth = new DateTime(2018, 7, 24),
                    Email = "vuk@mail.com",
                    Username = "vuk@mail.com",
                    Phone = "06312212",
                    Password = "123",
                    ProfileImage = "",
                    Gender = Gender.MALE,
                    Role = Role.PATIENT,
                    Blocked = false
                }
                );

            modelBuilder.Entity<Patient>().HasData(
               new Patient()
               {
                   Id = 5,
                   Name = "Maria",
                   Surname = "Rossi",
                   DateOfBirth = new DateTime(1988, 2, 12),
                   Email = "maria@mail.com",
                   Username = "maria@mail.com",
                   Phone = "06893232",
                   Password = "123",
                   ProfileImage = "",
                   Gender = Gender.FEMALE,
                   Role = Role.PATIENT,
                   Blocked = false
               }
               );


            modelBuilder.Entity<Doctor>().HasData(
            new Doctor()
            {
                Id = 2,
                Name = "Miki",
                Surname = "Mikic",
                DateOfBirth = new DateTime(1967, 7, 24),
                Email = "miki@gmail.com",
                Username = "miki@gmail.com",
                Phone = "0691202148",
                Password = "123",
                ProfileImage = "",
                Gender = Gender.MALE,
                Role = Role.DOCTOR,
                LicenseNumber = "123dr2009"
            },
            new Doctor()
            {
                Id = 3,
                Name = "Roki",
                Surname = "Rokic",
                DateOfBirth = new DateTime(1991, 2, 4),
                Email = "roki@gmail.com",
                Username = "roki@gmail.com",
                Phone = "06312909304",
                Password = "123",
                ProfileImage = "",
                Gender = Gender.MALE,
                Role = Role.DOCTOR,
                LicenseNumber = "198r2009"
            }
            );


        modelBuilder.Entity<PatientHealthData>().HasData(
              new PatientHealthData() { Id = 1, BloodPresure = "120/80", BodyFatPercentage = "17", BloodSugar = "12", Weight = "102", PatientId = 1 }
          );

            modelBuilder.Entity<Appointment>().HasData(
               new Appointment() { Id = 1, StartTime = new DateTime(2023, 05, 20, 9, 30, 0), EndTime = new DateTime(2023, 05, 20, 10, 00, 0), DoctorId = 2, PatientId = 1, Canceled = false, CancelationTime = new DateTime(), Used = false }
           );

            base.OnModelCreating(modelBuilder);
        }
    }
}
