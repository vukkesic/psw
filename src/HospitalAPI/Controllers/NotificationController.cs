using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _notificationService;
        private readonly NotificationMapper _notificationMapper;
        public NotificationController(INotificationService notificationService)
        {
            _notificationService = notificationService;
            _notificationMapper = new NotificationMapper();
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_notificationService.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            var notification = _notificationService.GetById(id);
            if (notification == null)
            {
                return NotFound();
            }
            return Ok(notification);
        }

        [HttpPost]
        public IActionResult Create(NotificationDTO notificationDTO)
        {
            Notification notification = _notificationMapper.MapNotificationDTOToNotification(notificationDTO);
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            _notificationService.Create(notification);
            return CreatedAtAction("GetById", new { id = notification.Id }, notification);
        }

    }
}
