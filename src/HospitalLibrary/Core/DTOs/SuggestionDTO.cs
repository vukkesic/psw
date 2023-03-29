using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class SuggestionDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public string DoctorName { get; set; }
        public string Message { get; set; }

        public SuggestionDTO() { }

        public SuggestionDTO(DateTime startTime, DateTime endTime, int doctorId, int patientId, string doctorName, string message)
        {
            StartTime = startTime;
            EndTime = endTime;
            DoctorId = doctorId;
            PatientId = patientId;
            DoctorName = doctorName;
            Message = message;
        }
    }
}
