using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests
{
    public class DoctorTests
    {
        [Fact]
        public void Finds_doctor()
        {

            DoctorService service = new DoctorService(CreateStubRepository());
            Doctor d = service.GetById(2);
            d.ShouldNotBeNull();
        }

        [Fact]
        public void Doctor_not_found()
        {
            DoctorService service = new DoctorService(CreateStubRepository());
            Doctor d = service.GetById(10);
            d.ShouldBeNull();
        }

        [Fact]
        public void Find_doctor_by_specialization()
        {
            DoctorService service = new DoctorService(CreateStubRepository());
            Doctor d = service.GetBySpecialization(1);
            d.ShouldNotBeNull();
        }

        private static IDoctorRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IDoctorRepository>();
            var doctors = new List<Doctor>();

            Doctor miki = new Doctor(2, "Miki", "Mikic", new DateTime(1967, 7, 24), "miki@gmail.com", "miki@gmail.com", "0691202148", "123", Gender.MALE, " ", Role.DOCTOR, "123dr2009", new Specialization(1, "specijalista"));

            Doctor roki = new Doctor()
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
            };

            Doctor dunja = new Doctor()
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
            };
            doctors.Add(roki);
            doctors.Add(miki);
            doctors.Add(dunja);

            stubRepository.Setup(m => m.GetById(2)).Returns(doctors[0]);
            stubRepository.Setup(m => m.GetById(3)).Returns(doctors[1]);
            stubRepository.Setup(m => m.GetById(6)).Returns(doctors[2]);
            stubRepository.Setup(m => m.GetBySpecialization(1)).Returns(doctors[0]);
            return stubRepository.Object;
        }
    }
}
