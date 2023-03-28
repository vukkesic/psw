using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> rooms { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Patient> patients { get; set; }
        public DbSet<PatientHealthData> healthdata { get; set; }
        public DbSet<Appointment> appointments { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PatientHealthData>().HasOne(phd => phd.Patient);

            modelBuilder.Entity<Room>().HasData(
                new Room() { Id = 1, Number = "101A", Floor = 1 },
                new Room() { Id = 2, Number = "204", Floor = 2 },
                new Room() { Id = 3, Number = "305B", Floor = 3 }
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
