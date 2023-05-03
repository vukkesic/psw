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
    [Route("api/[controller]")]
    [ApiController]
    public class MenstrualDataController : ControllerBase
    {
        private readonly IMenstrualDataService _menstrualDataService;
        private readonly MenstrualDataMapper _menstrualDataMapper;

        public MenstrualDataController(IMenstrualDataService menstrualDataService, IPatientService patientService)
        {
            _menstrualDataService = menstrualDataService;
            _menstrualDataMapper = new MenstrualDataMapper(patientService);
        }
        [Authorize]
        [HttpGet("getMyMenstrualData")]
        public ActionResult getMenstrualDataForUser(int patientId)
        {
            MenstrualData data = _menstrualDataService.GetByPatientId(patientId);
            return Ok(data);
        }

        [Authorize(Roles = "PATIENT")]
        [HttpPut("{id}")]
        public ActionResult Update(int id, MenstrualDataDTO dto)
        {
            MenstrualData data = _menstrualDataMapper.MapDTOToMenstrualData(dto);

            if (id != data.Id)
            {
                return BadRequest();
            }
            try
            {
                _menstrualDataService.Update(data);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(data);
        }
    }
}
