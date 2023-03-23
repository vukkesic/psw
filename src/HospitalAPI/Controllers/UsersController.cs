using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IUserService _userService;

        public UsersController(IPatientService patientService, IUserService userService)
        {
            _patientService = patientService;
            _userService = userService;
        }

        [HttpPost("userRegistration")]
        public IActionResult Register(RegistrationDTO dto)
        {
            Patient patient = PatientMapper.MapRegistrationDTOToPatient(dto);
            if (_userService.ExistsByUsername(patient.Username) == false)
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                _patientService.Create(patient);
                //return CreatedAtAction("GetById", new { id = patient.Id }, patient);
                return Ok(patient);
            }
            else
            {
                return BadRequest("Username and email you used in form are already taken. Try to login with that email or change it.");
            }
        }
    }
}
