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
        public DbSet<ReferralLetter> referralletters { get; set; }
        public DbSet<MenstrualData> menstrualdata { get; set; }
        public DbSet<ExaminationReport> examinations { get; set; }
        public DbSet<BlogPost> blogs { get; set; }
        public DbSet<Notification> notifications { get; set; }

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
                LicenseNumber = "123dr2009",
                SpecializationId = 1
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
                LicenseNumber = "198r2009",
                SpecializationId = 2
            },

               new Doctor()
               {
                   Id = 6,
                   Name = "Dunja",
                   Surname = "Jovanovic",
                   DateOfBirth = new DateTime(1995, 5, 8),
                   Email = "dunja@gmail.com",
                   Username = "dunja@gmail.com",
                   Phone = "0656757304",
                   Password = "123",
                   ProfileImage = "",
                   Gender = Gender.FEMALE,
                   Role = Role.DOCTOR,
                   LicenseNumber = "138r2014",
                   SpecializationId = 6
               },

                 new Doctor()
                 {
                     Id = 7,
                     Name = "Anja",
                     Surname = "Ilic",
                     DateOfBirth = new DateTime(1990, 12, 12),
                     Email = "anja@gmail.com",
                     Username = "anja@gmail.com",
                     Phone = "0604489354",
                     Password = "123",
                     ProfileImage = "",
                     Gender = Gender.FEMALE,
                     Role = Role.DOCTOR,
                     LicenseNumber = "14567sd8",
                     SpecializationId = 1
                 }
            );

            modelBuilder.Entity<Admin>().HasData(
               new Patient()
               {
                   Id = 4,
                   Name = "Angel",
                   Surname = "Di Maria",
                   DateOfBirth = new DateTime(1987, 11, 9),
                   Email = "angel@mail.com",
                   Username = "angel@mail.com",
                   Phone = "066119128",
                   Password = "123",
                   ProfileImage = "",
                   Gender = Gender.MALE,
                   Role = Role.ADMIN
               }
               );

            modelBuilder.Entity<PatientHealthData>().HasData(
                  new PatientHealthData() { Id = 1, BloodPresure = "120/80", BodyFatPercentage = "17", BloodSugar = "12", Weight = "102", PatientId = 1 }
              );

            modelBuilder.Entity<Appointment>().HasData(
               new Appointment() { Id = 1, StartTime = new DateTime(2023, 05, 20, 9, 30, 0), EndTime = new DateTime(2023, 05, 20, 10, 00, 0), DoctorId = 2, PatientId = 1, Canceled = false, CancelationTime = new DateTime(), Used = false }
           );

            modelBuilder.Entity<ReferralLetter>().HasData(
                new ReferralLetter() { Id = 1, IsActive = true, PatientId = 1, SpecializationId= 1 }
                );

            modelBuilder.Entity<MenstrualData>().HasData(
               new MenstrualData() { Id = 1, LastPeriod = new DateTime(2023, 03, 20, 9, 30, 0), NextPeriod = new DateTime(2023, 04, 23, 10, 00, 0), ApproxOvulationDay = new DateTime(2023, 04, 03, 10, 00, 0), PatientId = 5}
           );

            modelBuilder.Entity<ExaminationReport>().HasData(
               new ExaminationReport() { Id = 1, DiagnosisCode = "1AFA", DiagnosisDescription = "Dijabetes tipa 2", DoctorId = 2, PatientId = 1, Date = new DateTime(2023, 01, 24, 10, 00, 0), HealthDataId = 1, Prescription = "Ishrana za dijabeticare." }
               );

            modelBuilder.Entity<BlogPost>().HasData(
               new BlogPost() { Id = 1, Title = "Does your child need to gain weight?", Text = "If you are worried about whether your child needs to gain weight, it’s very important to check with your doctor before getting to work on fattening them up. It’s entirely possible that your child’s weight is absolutely fine. Given that one in five children in the US is obese and another one in six is overweight, it’s easy to see how a parent might think their child is too thin in comparison. One way to find out if your child’s weight is healthy is to check their body mass index, a calculation using height and weight that is used for children ages 2 and up." }
           );

            base.OnModelCreating(modelBuilder);
        }
    }
}
