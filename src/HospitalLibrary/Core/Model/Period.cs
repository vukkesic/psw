using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Period
    {
            public DateTime StartTime { get; set; }
            public DateTime EndTime { get; set; }
            public int DoctorId { get; set; }
            public int PatientId { get; set; }
            public int Priority { get; set; }
            public Period()
            {
            }

            public Period(DateTime startTime, DateTime endTime, int doctorId, int patientId, int priority)
            {
                StartTime = startTime;
                EndTime = endTime;
                DoctorId = doctorId;
                PatientId = patientId;
                Priority = priority;
            }
        }
}
