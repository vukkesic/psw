using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private IAppointmentService _appointmentService;
        private IDoctorService _doctorService;

        public AppointmentsController(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
        }
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        [HttpPost("checkPeriod")]
        public SuggestionDTO CheckPeriod(Period period)
        {
            DateTime tempStart = RoundUp(period.StartTime, TimeSpan.FromMinutes(30)).ToLocalTime();
            DateTime tempEnd = RoundUp(period.EndTime.AddSeconds(1), TimeSpan.FromMinutes(30)).AddMinutes(-30).ToLocalTime();
            Appointment[] appointments = (_appointmentService.GetByDoctor(period.DoctorId)).ToArray();
            Doctor d = _doctorService.GetById(period.DoctorId);
            for (DateTime start = tempStart; start < tempEnd; start = start.AddMinutes(30))
            {
                Boolean flag = false;
                foreach (Appointment a in appointments)
                {
                    flag = a.CompareStartTime(start);
                }
                if (flag == false)
                {
                    return (new SuggestionDTO(start, start.AddMinutes(30), period.DoctorId, period.PatientId, d.Name, "Great! We found appointment for your parameters."));
                }
            }
            return null;
        }
    }
}
