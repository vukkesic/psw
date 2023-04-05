using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecializationsController : ControllerBase
    {
        private readonly ISpecializationService _specializationService;
        public SpecializationsController(ISpecializationService specializationService)
        {
            _specializationService = specializationService;
        }

        [Authorize(Roles = "DOCTOR")]
        [HttpGet("getAllSpecializations")]
        public ActionResult GetAllSpecializations()
        {
            return Ok(_specializationService.GetAll());
        }
    }
}
