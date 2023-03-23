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
    public class PatientTests
    {
        [Fact]
        public void Finds_patient()
        {

            PatientService service = new PatientService(CreateStubRepository());
            Patient p = service.GetById(1);
            p.ShouldNotBeNull();
        }

        [Fact]
        public void Patient_not_found()
        {
            PatientService service = new PatientService(CreateStubRepository());
            Patient p = service.GetById(2);
            p.ShouldBeNull();
        }

        [Fact]
        public void Patient_exists_by_id()
        {
            PatientService service = new PatientService(CreateStubRepository());
            bool b = service.ExistsById(5);
            b.ShouldBeTrue();
        }

        [Fact]
        public void Patient_not_exists_by_id()
        {
            PatientService service = new PatientService(CreateStubRepository());
            bool b = service.ExistsById(2);
            b.ShouldBeFalse();
        }

        private static IPatientRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IPatientRepository>();
            var patients = new List<Patient>();
            var p1 = new Patient()
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
            };
            var p2 = new Patient()
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
            };

            patients.Add(p1);
            patients.Add(p2);

            stubRepository.Setup(m => m.GetById(1)).Returns(p1);
            stubRepository.Setup(m => m.GetById(5)).Returns(p2);
            stubRepository.Setup(m => m.ExistsById(1)).Returns(true);
            stubRepository.Setup(m => m.ExistsById(5)).Returns(true);
            stubRepository.Setup(m => m.GetAll()).Returns(patients);
            return stubRepository.Object;
        }
    }
}
