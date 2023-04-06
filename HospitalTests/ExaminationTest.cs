using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using Moq;
using Shouldly;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests
{
    public class ExaminationTest
    {
        [Fact]
        public void Get_examinaion_by_id()
        {
            ExaminationService service = new ExaminationService(CreateStubRepository());
            ExaminationReport e = service.GetById(1);
            e.ShouldNotBeNull();
        }

        [Fact]
        public void Get_examinaion_by_id_not_found()
        {
            ExaminationService service = new ExaminationService(CreateStubRepository());
            ExaminationReport e = service.GetById(10);
            e.ShouldBeNull();
        }

        [Fact]
        public void Get_examinaion_by_patient_id()
        {
            ExaminationService service = new ExaminationService(CreateStubRepository());
            IEnumerable<ExaminationReport> e = service.GetByPatientId(1);
            e.ShouldNotBeEmpty();
        }

        [Fact]
        public void Get_examinaion_by_patient_id_not_found()
        {
            ExaminationService service = new ExaminationService(CreateStubRepository());
            IEnumerable<ExaminationReport> e = service.GetByPatientId(10);
            e.ShouldBeEmpty();
        }

        private static IExaminationRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IExaminationRepository>();
            var ex1 = new ExaminationReport() { Id = 1, DiagnosisCode = "1AFA", DiagnosisDescription = "Dijabetes tipa 2", DoctorId = 2, PatientId = 1, Date = new DateTime(2023, 01, 24, 10, 00, 0), HealthDataId = 1, Prescription = "Ishrana za dijabeticare." };
            var list1 = new List<ExaminationReport>();
            list1.Add(ex1);
            IEnumerable<ExaminationReport> res1 = list1;
            stubRepository.Setup(m => m.GetById(1)).Returns(ex1);
            stubRepository.Setup(m => m.GetByPatientId(1)).Returns(res1);
            return stubRepository.Object;
        }
    }
}
