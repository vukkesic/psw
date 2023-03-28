using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthDataController : ControllerBase
    {
        private readonly IPatientHealthDataService _patientHealthDataService;
        private readonly PatientHealthDataMapper _patientHealthDataMapper;

        public HealthDataController(IPatientHealthDataService patientHealthDataService, IPatientService patientService)
        {
            _patientHealthDataService = patientHealthDataService;
            _patientHealthDataMapper = new PatientHealthDataMapper(patientService);
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_patientHealthDataService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var healthData = _patientHealthDataService.GetById(id);
            if (healthData == null)
            {
                return NotFound();
            }

            return Ok(healthData);
        }

        [HttpGet("getHealthData")]
        public ActionResult getHealthDataForUser(int userId)
        {
            List<PatientHealthData> data = _patientHealthDataService.GetByUserId(userId);
            return Ok(data);
        }

        [HttpPost]
        public ActionResult Create(PatientHealthDataDTO dto)
        {
            PatientHealthData data = _patientHealthDataMapper.MapDtoToData(dto);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _patientHealthDataService.Create(data);
            return CreatedAtAction("GetById", new { id = data.Id }, data);
        }
    }
}
