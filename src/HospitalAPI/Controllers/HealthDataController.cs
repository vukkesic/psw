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
    public class HealthDataController : ControllerBase
    {
        private readonly IPatientHealthDataService _patientHealthDataService;
        private readonly PatientHealthDataMapper _patientHealthDataMapper;

        public HealthDataController(IPatientHealthDataService patientHealthDataService, IPatientService patientService)
        {
            _patientHealthDataService = patientHealthDataService;
            _patientHealthDataMapper = new PatientHealthDataMapper(patientService);
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
