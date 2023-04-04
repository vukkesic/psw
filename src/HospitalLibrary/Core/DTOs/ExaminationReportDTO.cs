using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class ExaminationReportDTO
    {
        public int Id { get; set; }
        public string DiagnosisCode { get; set; }
        public string DiagnosisDescription { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public DateTime Date { get; set; }
        public int HealthDataId { get; set; }
        public string Prescription { get; set; }

        public ExaminationReportDTO(int id, string diagnosisCode, string diagnosisDescription, int doctorId, int patientId, DateTime date, int healthDataId, string prescription)
        {
            Id = id;
            DiagnosisCode = diagnosisCode;
            DiagnosisDescription = diagnosisDescription;
            DoctorId = doctorId;
            PatientId = patientId;
            Date = date;
            HealthDataId = healthDataId;
            Prescription = prescription;
        }
        public ExaminationReportDTO() { }
    }
}
