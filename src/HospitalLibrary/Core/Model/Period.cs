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

        public void RoundUpPeriod()
        {
            StartTime = RoundUp(StartTime, TimeSpan.FromMinutes(30)).ToLocalTime();
            EndTime= RoundUp(EndTime.AddSeconds(1), TimeSpan.FromMinutes(30)).AddMinutes(-30).ToLocalTime();
        }

        //This function rounds up date so you can only get half a hour period 
        private DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }
    }
}
