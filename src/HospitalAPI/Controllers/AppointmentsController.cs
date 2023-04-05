using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Linq;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private IAppointmentService _appointmentService;
        private IDoctorService _doctorService;
        private AppointmentMapper _appointmentMapper;

        public AppointmentsController(IAppointmentService appointmentService, IDoctorService doctorService)
        {
            _appointmentService = appointmentService;
            _doctorService = doctorService;
            _appointmentMapper = new AppointmentMapper();
        }
        DateTime RoundUp(DateTime dt, TimeSpan d)
        {
            return new DateTime((dt.Ticks + d.Ticks - 1) / d.Ticks * d.Ticks, dt.Kind);
        }

        SuggestionDTO createSuggestion(Appointment[] appointments, DateTime tempStart, DateTime tempEnd, Period period, Doctor d, string message)
        {
            for (DateTime start = tempStart; start < tempEnd; start = start.AddMinutes(30))
            {
                Boolean flag = false;
                foreach (Appointment a in appointments)
                {
                    if (a.CompareStartTime(start))
                        flag = true;
                }
                if (flag == false)
                {
                    return (new SuggestionDTO(start, start.AddMinutes(30), d.Id, period.PatientId, d.Name, message));
                }
            }
            return null;
        }

        [Authorize(Roles = "PATIENT")]
        [HttpPost("checkPeriod")]
        public SuggestionDTO CheckPeriod(Period period)
        {
            DateTime tempStart = RoundUp(period.StartTime, TimeSpan.FromMinutes(30)).ToLocalTime();
            DateTime tempEnd = RoundUp(period.EndTime.AddSeconds(1), TimeSpan.FromMinutes(30)).AddMinutes(-30).ToLocalTime();
            Appointment[] appointments = (_appointmentService.GetByDoctor(period.DoctorId)).ToArray();
            Doctor d = _doctorService.GetById(period.DoctorId);
            SuggestionDTO suggestion = null;

            suggestion = createSuggestion(appointments, tempStart, tempEnd, period, d, "Great! We found appointment for your parameters.");
            if (suggestion != null)
                return suggestion;

            if (period.Priority == 1)
            {
                tempStart = RoundUp(period.EndTime, TimeSpan.FromMinutes(30)).AddMinutes(-30).ToLocalTime();
                tempEnd = RoundUp(period.EndTime.AddSeconds(1).AddDays(7), TimeSpan.FromMinutes(30)).AddMinutes(-30).ToLocalTime();

                suggestion = createSuggestion(appointments, tempStart, tempEnd, period, d, "Sorry, all appointments for your preferred time are busy. We offer you adjusted appointment.");
                if (suggestion != null)
                    return suggestion;

                if (DateTime.Compare(period.StartTime, DateTime.Now.AddDays(9)) > 0)
                {
                    tempStart = RoundUp(period.StartTime.AddDays(-7), TimeSpan.FromMinutes(30)).ToLocalTime();
                }
                else
                {
                    tempStart = RoundUp(DateTime.Now.AddDays(2), TimeSpan.FromMinutes(30)).AddMinutes(-30).ToLocalTime();
                }
                tempEnd = RoundUp(period.StartTime, TimeSpan.FromMinutes(30)).ToLocalTime();

                suggestion = createSuggestion(appointments, tempStart, tempEnd, period, d, "Sorry, all appointments for your preferred time are busy. We offer you adjusted appointment.");
                if (suggestion != null)
                    return suggestion;

            }
            else if (period.Priority == 0)
            {
                d = _doctorService.GetById(period.DoctorId);
                Doctor[] doctors = _doctorService.GetAll().ToArray();
                foreach (Doctor doc in doctors)
                {
                    appointments = (_appointmentService.GetByDoctor(doc.Id)).ToArray();
                    suggestion = createSuggestion(appointments, tempStart, tempEnd, period, doc, "Sorry, all appointments for your preferred doctor are busy. We offer you adjusted appointment.");
                    if (suggestion != null)
                        return suggestion;
                }

            }

            return null;
        }

        //[Authorize(Roles = "PATIENT")]
        [HttpPost("addAppointment")]
        public ActionResult AddAppointment(AppointmentDTO dto)
        {
            Appointment appointment = _appointmentMapper.MapAppointmentDTOToAppointment(dto);
            _appointmentService.Create(appointment);
            return Ok("Added successfull");

        }

        [Authorize(Roles = "PATIENT")]
        [HttpPut("cancel/{id}")]
        public ActionResult Cancel(int id)
        {
            Appointment appointment = _appointmentService.GetById(id);
            if (DateTime.Now <= appointment.StartTime.AddDays(-2))
            {
                appointment.Canceled = true;
                appointment.CancelationTime = DateTime.Now;
                try
                {
                    _appointmentService.Update(appointment);
                }
                catch
                {
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }

            return Ok(appointment);
        }

        [Authorize]
        [HttpGet("getAppointmentsByPatient")]
        public ActionResult GetByPatient(int patientId)
        {
            return Ok(_appointmentService.GetByPatient(patientId));
        }

        [Authorize(Roles = "DOCTOR")]
        [HttpGet("getDoctorTodayAppointments")]
        public ActionResult GetTodayAppointments(DateTime today, int doctorId)
        {
            return Ok(_appointmentService.GetDoctorTodayAppointments(today, doctorId));
        }

        [Authorize(Roles = "DOCTOR")]
        [HttpPut("use/{id}")]
        public ActionResult Use(int id)
        {
            Appointment appointment = _appointmentService.GetById(id);
            appointment.Used = true;
            try
            {
                _appointmentService.Update(appointment);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(appointment);
        }
    }
}
