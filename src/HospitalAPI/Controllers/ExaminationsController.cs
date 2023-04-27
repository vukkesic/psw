using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HospitalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExaminationsController : ControllerBase
    {
        private readonly IExaminationService _examinationService;
        private readonly IPatientHealthDataService _patientHealthDataService;
        private readonly ExaminationReportMapper _examinationReportMapper;
        public ExaminationsController(IExaminationService examinationService, IPatientHealthDataService patientHealthDataService)
        {
            _examinationService = examinationService;
            _examinationReportMapper = new ExaminationReportMapper(patientHealthDataService);
            _patientHealthDataService = patientHealthDataService;
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_examinationService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var examination = _examinationService.GetById(id);
            if (examination == null)
            {
                return NotFound();
            }
            return Ok(examination);
        }

        [Authorize(Roles = "DOCTOR")]
        [HttpPost]
        public ActionResult Create(ExaminationReportDTO examinationDTO)
        {
            ExaminationReport examination = _examinationReportMapper.MapExaminationReportDTOToExaminationReport(examinationDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _examinationService.Create(examination);
            return CreatedAtAction("GetById", new { id = examination.Id }, examination);
        }

        [Authorize(Roles = "PATIENT")]
        [HttpGet("getExamination")]
        public ActionResult getExaminationForUser(int patientId)
        {
            IEnumerable<ExaminationReport> data = _examinationService.GetByPatientId(patientId);
            return Ok(data);
        }
        [Authorize(Roles = "DOCTOR")]
        [HttpGet("getLastTwoWeeksFluReports")]
        public ActionResult getLastTwoWeeksFluReports(int patientId)
        {
            IEnumerable<ExaminationReport> data = _examinationService.GetLastTwoWeekFluReports(patientId);
            return Ok(data);
        }
    }
}
