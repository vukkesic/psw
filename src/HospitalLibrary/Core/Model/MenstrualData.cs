using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class MenstrualData
    {
        public int Id { get; set; }
        public DateTime LastPeriod { get; set; }
        public DateTime NextPeriod { get; set; }
        public DateTime ApproxOvulationDay { get; set; }
        public int PatientId { get; set; }
        public virtual Patient Patient { get; set; }

        public MenstrualData()
        {
        }

        public MenstrualData(int id, DateTime lastPeriod, DateTime nextPeriod, DateTime approxOvulationDay, int patientId, Patient patient)
        {
            Id = id;
            LastPeriod = lastPeriod;
            NextPeriod = nextPeriod;
            ApproxOvulationDay = approxOvulationDay;
            PatientId = patientId;
            Patient = patient;
        }
    }

    
}
