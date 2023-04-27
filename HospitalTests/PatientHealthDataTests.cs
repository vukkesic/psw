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
    public class PatientHealthDataTests
    {
        [Fact]
        public void Get_health_data_by_user_id()
        {
            PatientHealthDataService service = new PatientHealthDataService(CreateStubRepository());
            List<PatientHealthData> d = service.GetByUserId(1);
            d.ShouldNotBeEmpty();
        }

        [Fact]
        public void Get_health_data_by_user_id_not_found()
        {
            PatientHealthDataService service = new PatientHealthDataService(CreateStubRepository());
            List<PatientHealthData> d = service.GetByUserId(2);
            d.ShouldBeEmpty();
        }

        [Fact]
        public void Get_last_two_days_health_data()
        {
            PatientHealthDataService service = new PatientHealthDataService(CreateStubRepository());
            IEnumerable<PatientHealthData> d = service.GetLastTwoDaysHealthData(1);
            d.ShouldNotBeEmpty();
        }

        [Fact]
        public void Get_last_two_days_health_data_not_found()
        {
            PatientHealthDataService service = new PatientHealthDataService(CreateStubRepository());
            IEnumerable<PatientHealthData> d = service.GetLastTwoDaysHealthData(2);
            d.ShouldBeEmpty();
        }
        private static IPatientHealthDataRepository CreateStubRepository()
        {
            var p = new Patient()
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
            var stubRepository = new Mock<IPatientHealthDataRepository>();
            var phd = new PatientHealthData() { Id = 1, BloodPresure = "120/80", BodyFatPercentage = "17", BloodSugar = "12", Weight = "102", PatientId = 1, Patient = p, MeasurementTime = DateTime.Now };
            var phd2 = new PatientHealthData() { Id = 1, BloodPresure = "120/80", BodyFatPercentage = "7", BloodSugar = "8", Weight = "82", PatientId = 1, Patient = p, MeasurementTime = DateTime.Now };
            var list = new List<PatientHealthData>();
            list.Add(phd);
            list.Add(phd2);
            IEnumerable<PatientHealthData> res = list;
            stubRepository.Setup(m => m.GetAll()).Returns(list);
            stubRepository.Setup(m => m.GetLastTwoDaysHealthData(DateTime.Now.Date, 1)).Returns(res);
            return stubRepository.Object;
        }
    }
}
