using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodDonationController : ControllerBase
    {
        private readonly IBloodDonationNotificationService _bloodDonationNotificationService;
        public BloodDonationController(IBloodDonationNotificationService bloodDonationNotificationService)
        {
            _bloodDonationNotificationService = bloodDonationNotificationService;
        }
        // GET: api/rooms
        [Authorize(Roles ="DOCTOR")]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodDonationNotificationService.GetAll());
        }

        // GET api/rooms/2
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var bdn = _bloodDonationNotificationService.GetById(id);
            if (bdn == null)
            {
                return NotFound();
            }

            return Ok(bdn);
        }

        // POST api/rooms
        [AllowAnonymous]
        [HttpPost]
        public ActionResult Create(BloodDonationNotification bdn)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _bloodDonationNotificationService.Create(bdn);
            return CreatedAtAction("GetById", new { id = bdn.Id }, bdn);
        }

        // PUT api/rooms/2
        [HttpPut("{id}")]
        public ActionResult Update(int id, BloodDonationNotification bloodDonationNotification)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != bloodDonationNotification.Id)
            {
                return BadRequest();
            }

            try
            {
                _bloodDonationNotificationService.Update(bloodDonationNotification);
            }
            catch
            {
                return BadRequest();
            }

            return Ok(bloodDonationNotification);
        }

        // DELETE api/rooms/2
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var bdn = _bloodDonationNotificationService.GetById(id);
            if (bdn == null)
            {
                return NotFound();
            }

            _bloodDonationNotificationService.Delete(bdn);
            return NoContent();
        }
    }
}
