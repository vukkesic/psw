using HospitalLibrary.Core.Model;
using Microsoft.EntityFrameworkCore;
using System;

namespace HospitalLibrary.Settings
{
    public class HospitalDbContext : DbContext
    {
        public DbSet<Room> rooms { get; set; }
        public DbSet<User> users { get; set; }

        public HospitalDbContext(DbContextOptions<HospitalDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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

            base.OnModelCreating(modelBuilder);
        }
    }
}
