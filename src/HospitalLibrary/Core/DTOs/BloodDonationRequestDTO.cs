using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class BloodDonationRequestDTO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public String PatientName { get; set; }
        public String Location { get; set; }

        public BloodDonationRequestDTO() { }

        BloodDonationRequestDTO(DateTime startTime, DateTime endTime, string patientName, string location)
        {
            StartTime = startTime;
            EndTime = endTime;
            PatientName = patientName;
            Location = location;
        }
    }
}
