using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IUserService _userService;
        private readonly IDoctorService _doctorService;
        private readonly IMenstrualDataService _menstrualDataService;
        private readonly IAppointmentService _appointmentService;

        public UsersController(IPatientService patientService, IUserService userService, IDoctorService doctorService, IMenstrualDataService menstrualDataService, IAppointmentService appointentService)
        {
            _patientService = patientService;
            _userService = userService;
            _doctorService = doctorService;
            _menstrualDataService = menstrualDataService;
            _appointmentService = appointentService;
        }

        [AllowAnonymous]
        [HttpPost("uploadImage")]
        public IActionResult UploadImage([FromForm] FileDTO file)
        {
            try
            {
                string path = Path.Combine(Directory.GetCurrentDirectory(), "images", file.FileName);
                using (Stream stream = new FileStream(path, FileMode.Create))
                {
                    file.FormFile.CopyTo(stream);
                }
                return Ok(path);
            }
            catch (Exception)
            {
                return BadRequest("Error");
            }
        }

        [AllowAnonymous]
        [HttpPost("userRegistration")]
        public IActionResult Register(RegistrationDTO dto)
        {
            Patient patient = PatientMapper.MapRegistrationDTOToPatient(dto);
            MenstrualData data= new MenstrualData();
            if (_userService.ExistsByUsername(patient.Username) == false)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Patient p = _patientService.Create(patient);
                if(patient.Gender == Gender.FEMALE)
                {
                    data.PatientId = p.Id;
                    _menstrualDataService.Create(data);
                }
                //return CreatedAtAction("GetById", new { id = patient.Id }, patient);   Treba dodati get metodu 
                return Ok(patient);
            }
            else
            {
                return BadRequest("Username and email you used in form are already taken. Try to login with that email or change it.");
            }
        }

        [Authorize]
        [HttpGet("getCurrentUser")]
        public ActionResult getCurrentUser(int id)
        {
            var user = _userService.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [Authorize(Roles = "DOCTOR")]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var room = _doctorService.GetById(id);
            if (room == null)
            {
                return NotFound();
            }

            return Ok(room);
        }

        [Authorize]
        [HttpGet("getAllDoctors")]
        public ActionResult GetAllDoctors()
        {
            return Ok(_doctorService.GetAll());
        }

        [Authorize]
        [HttpGet("getAllSpecialist")]
        public ActionResult GetAllSpecialist(int specializationId)
        {
            return Ok(_doctorService.GetAllSpecialist(specializationId));
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("getBlockablePatients")]
        public ActionResult GetBlockablePatients()
        {
            IEnumerable<Appointment> appointments = _appointmentService.GetLastMonthCanceledAppointments();
            List<Patient> blockablePatients = _patientService.GetAllBlockablePatients(appointments);
            return Ok(blockablePatients);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpGet("getBlockedPatients")]
        public ActionResult GetBlockedPatients()
        {
            return Ok(_patientService.GetBlockedPatients());
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("block/{id}")]
        public ActionResult Block(int id)
        {
            try
            {
                _patientService.BlockPatient(id);
            }
            catch
            {
                return BadRequest();
            }
            Patient patient = _patientService.GetById(id);
            return Ok(patient);
        }

        [Authorize(Roles = "ADMIN")]
        [HttpPut("unblock/{id}")]
        public ActionResult Unblock(int id)
        {
            try
            {
                _patientService.UnblockPatient(id);
            }
            catch
            {
                return BadRequest();
            }
            Patient patient = _patientService.GetById(id);
            return Ok(patient);
        }

        //[Authorize(Roles = "DOCTOR")]
        [AllowAnonymous]
        [HttpGet("getAllPatients")]
        public ActionResult GetAllPatients()
        {
            return Ok(_patientService.GetAll());
        }
    }
}
