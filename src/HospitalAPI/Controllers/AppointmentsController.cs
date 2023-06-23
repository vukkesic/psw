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

        [Authorize(Roles = "PATIENT")]
        [HttpPost("checkPeriod")]
        public SuggestionDTO CheckPeriod(Period period)
        {
            Doctor doctor = _doctorService.GetById(period.DoctorId);
            Doctor[] doctors = _doctorService.GetAllSpecialist(doctor.SpecializationId).ToArray();
            return _appointmentService.CheckPeriod(period, doctor, doctors);
        }

        [Authorize(Roles = "PATIENT")]
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
            if (appointment.IsCancelable())
            {
                appointment.Cancel();
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
                return BadRequest("You can't cancel appointment scheduled in less than two days.");
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
