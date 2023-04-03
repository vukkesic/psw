using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System;

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

        public UsersController(IPatientService patientService, IUserService userService, IDoctorService doctorService, IMenstrualDataService menstrualDataService)
        {
            _patientService = patientService;
            _userService = userService;
            _doctorService = doctorService;
            _menstrualDataService = menstrualDataService;
        }
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
                _patientService.Create(patient);
                if(patient.Gender == Gender.FEMALE)
                {
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

        [HttpGet("getAllDoctors")]
        public ActionResult GetAllDoctors()
        {
            return Ok(_doctorService.GetAll());
        }

        [HttpGet("getAllSpecialist")]
        public ActionResult GetAllSpecialist(int specializationId)
        {
            return Ok(_doctorService.GetAllSpecialist(specializationId));
        }
    }
}
