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

        [Fact]
        public void Blockable_patient_not_found()
        {
            var appointments = new List<Appointment>();
            var app1 = new Appointment(1, new DateTime(2023, 29, 10, 9, 30, 0), new DateTime(2023, 03, 29, 10, 00, 0), 2, 1, true, new DateTime().AddDays(-17), false);
            var app2 = new Appointment(2, new DateTime(2023, 04, 01, 9, 30, 0), new DateTime(2023, 04, 01, 10, 00, 0), 4, 1, true, new DateTime().AddDays(-10), false);
            appointments.Add(app1);
            appointments.Add(app2);
            IEnumerable<Appointment> a = appointments;
            PatientService service = new PatientService(CreateStubRepository());
            List<Patient> bp = service.GetAllBlockablePatients(a);
            bp.ShouldBeEmpty();
        }

        [Fact]
        public void Blockable_patient_found()
        {
            var appointments = new List<Appointment>();
            var app1 = new Appointment(1, new DateTime(2023, 03, 29, 9, 30, 0), new DateTime(2023, 03, 29, 10, 00, 0), 2, 1, true, new DateTime(2023, 03, 27, 10, 00, 0), false);
            var app2 = new Appointment(2, new DateTime(2023, 04, 01, 9, 30, 0), new DateTime(2023, 04, 01, 10, 00, 0), 4, 1, true, new DateTime(2023, 03, 29, 10, 00, 0), false);
            var app3 = new Appointment(3, new DateTime(2023, 04, 04, 9, 30, 0), new DateTime(2023, 04, 04, 10, 00, 0), 4, 1, true, new DateTime(2023, 03, 29, 10, 00, 0), false);
            appointments.Add(app1);
            appointments.Add(app2);
            appointments.Add(app3);
            IEnumerable<Appointment> a = appointments;
            PatientService service = new PatientService(CreateStubRepository());
            List<Patient> bp = service.GetAllBlockablePatients(a);
            bp.ShouldNotBeEmpty();
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
            stubRepository.Setup(m => m.GetActivePatientById(1)).Returns(p1);
            stubRepository.Setup(m => m.GetAll()).Returns(patients);
            return stubRepository.Object;
        }
    }
}
