using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class MenstrualDataDTO
    {
        public int Id { get; set; }
        public DateTime LastPeriod { get; set; }
        public DateTime NextPeriod { get; set; }
        public DateTime ApproxOvulationDay { get; set; }
        public int PatientId { get; set; }

        public MenstrualDataDTO()
        {
        }

        public MenstrualDataDTO(int id, DateTime lastPeriod, DateTime nextPeriod, DateTime approxOvulationDay, int patientId)
        {
            Id = id;
            LastPeriod = lastPeriod;
            NextPeriod = nextPeriod;
            ApproxOvulationDay = approxOvulationDay;
            PatientId = patientId;
        }
    }
}
