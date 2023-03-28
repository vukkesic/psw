using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class PatientHealthDataDTO
    {
        public int Id { get; set; }
        public string BloodPresure { get; set; }
        public string BloodSugar { get; set; }
        public string BodyFatPercentage { get; set; }
        public string Weight { get; set; }
        public int UserId { get; set; }
        public DateTime MeasurementTime { get; set; }

        public PatientHealthDataDTO() { }

        public PatientHealthDataDTO(int id, string bloodPresure, string bloodSugar, string bodyFatPercentage, string weight, int userId, DateTime measurementTime)
        {
            Id = id;
            BloodPresure = bloodPresure;
            BloodSugar = bloodSugar;
            BodyFatPercentage = bodyFatPercentage;
            Weight = weight;
            UserId = userId;
            MeasurementTime = measurementTime;
        }
    }
}
