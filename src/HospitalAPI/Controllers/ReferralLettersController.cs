using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ReferralLettersController : ControllerBase
    {
        private readonly IReferralLetterService _referralLetterService;
        private readonly ReferralLetterMapper _referralLetterMapper;
        private readonly ISpecializationService _specializationService;

        public ReferralLettersController(IReferralLetterService referralLetterService, ISpecializationService specializationService)
        {
            _referralLetterService = referralLetterService;
            _specializationService = specializationService;
            _referralLetterMapper = new ReferralLetterMapper(_specializationService);
        }

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_referralLetterService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var referralLetter = _referralLetterService.GetById(id);
            if (referralLetter == null)
            {
                return NotFound();
            }
            return Ok(referralLetter);
        }

        [HttpGet("getMyReferralLetters")]
        public ActionResult GetMyRefferalLetters(int patientId)
        {
            return Ok(_referralLetterService.GetMyReferralLetters(patientId));
        }

        [HttpPost]
        public ActionResult Create(ReferralLetterDTO referralLetterDTO)
        {
            ReferralLetter referralLetter = _referralLetterMapper.MapDTOToReferralLetter(referralLetterDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _referralLetterService.Create(referralLetter);
            return CreatedAtAction("GetById", new { id = referralLetter.Id }, referralLetter);
        }
        [HttpPut("{id}")]
        public ActionResult Update(int id, ReferralLetterDTO referralLetterDTO)
        {
            ReferralLetter referralLetter = _referralLetterMapper.MapDTOToReferralLetter(referralLetterDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (id != referralLetter.Id)
            {
                return BadRequest();
            }
            try
            {
                _referralLetterService.Update(referralLetter);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(referralLetter);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var referralLetter = _referralLetterService.GetById(id);
            if (referralLetter == null)
            {
                return NotFound();
            }
            _referralLetterService.Delete(referralLetter);
            return NoContent();
        }
    }
}
