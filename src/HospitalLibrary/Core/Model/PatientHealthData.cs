using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class PatientHealthData
    {
        public int Id { get; set; }
        public string BloodPresure { get; set; }
        public string BloodSugar { get; set; }
        public string BodyFatPercentage { get; set; }
        public string Weight { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }
        public DateTime MeasurementTime { get; set; }

        public PatientHealthData()
        {
            BloodPresure = "";
            BloodSugar = "";
            BodyFatPercentage = "";
            Weight = "";
        }

        public PatientHealthData(int id, string bloodPresure, string bloodSugar, string bodyFatPercentage, string weight, Patient patient, DateTime measurementTime)
        {
            Id = id;
            BloodPresure = bloodPresure;
            BloodSugar = bloodSugar;
            BodyFatPercentage = bodyFatPercentage;
            Weight = weight;
            PatientId = patient.Id;
            Patient = patient;
            MeasurementTime = measurementTime;
        }
    }
}
