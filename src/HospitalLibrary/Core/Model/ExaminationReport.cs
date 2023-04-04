using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class ExaminationReport
    {
        public int Id { get; set; }
        public string DiagnosisCode { get; set; }
        public string DiagnosisDescription { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public int? HealthDataId { get; set; }
        public virtual PatientHealthData HealthData { get; set; }
        public string Prescription { get; set; }


        public ExaminationReport() { }

        public ExaminationReport(int id, string diagnosisCode, string diagnosisDescription, int doctorId, int patientId, DateTime date, int? healthDataId, PatientHealthData healthData, string prescription)
        {
            Id = id;
            DiagnosisCode = diagnosisCode;
            DiagnosisDescription = diagnosisDescription;
            DoctorId = doctorId;
            PatientId = patientId;
            Date = date;
            HealthDataId = healthDataId;
            HealthData = healthData;
            Prescription = prescription;
        }
    }
}
