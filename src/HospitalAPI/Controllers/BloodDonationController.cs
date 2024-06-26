﻿using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using IntegrationApi.Protos;
using IntegrationAPI;
using IntegrationAPI.Protos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;
using System.Threading.Tasks;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BloodDonationController : ControllerBase
    {
        private readonly IBloodDonationNotificationService _bloodDonationNotificationService;
        public BloodDonationController(IBloodDonationNotificationService bloodDonationNotificationService)
        {
            _bloodDonationNotificationService = bloodDonationNotificationService;
        }
        // GET: api/bloodDontaion

        [HttpGet]
        public ActionResult GetAll()
        {
            return Ok(_bloodDonationNotificationService.GetAll());
        }

        // GET api/bloodDontaion/2
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

        [HttpGet("getPending")]
        public ActionResult GetPending()
        {
            return Ok(_bloodDonationNotificationService.GetPending());
        }

        [HttpGet("getApproved")]
        public ActionResult GetApproved()
        {
            return Ok(_bloodDonationNotificationService.GetApproved());
        }

        // POST api/bloodDontaion
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

        [AllowAnonymous]
        [HttpPost ("makeBloodAppointment")]
        public async Task<BloodDonationAppointment> MakeBloodDonationAppointment(BloodDonationRequestDTO bdn)
        {
            try
            {
                BloodDonationRequest b = new BloodDonationRequest() { StartTime = Timestamp.FromDateTime(bdn.StartTime.ToUniversalTime()), EndTime = Timestamp.FromDateTime(bdn.EndTime.ToUniversalTime()), PatientName = bdn.PatientName, Location = bdn.Location };
                var channel = new Channel("localhost", 8787, ChannelCredentials.Insecure);
                var client = new SpringGrpcService.SpringGrpcServiceClient(channel);
                BloodDonationAppointment appointment = await client.makeBloodDonationAppointmentAsync(b);
                Console.WriteLine(appointment);
                return appointment;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        // PUT api/bloodDontaion/2
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

        // DELETE api/bloodDontaion/2
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

        [HttpPut("approve/{id}")]
        public ActionResult Use(int id)
        {
            BloodDonationNotification notification = _bloodDonationNotificationService.GetById(id);
            notification.Status = "APPROVED";
            try
            {
                _bloodDonationNotificationService.Update(notification);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(notification);
        }

        [HttpPut("deny/{id}")]
        public ActionResult Deny(int id)
        {
            BloodDonationNotification notification = _bloodDonationNotificationService.GetById(id);
            notification.Status = "DENIED";
            try
            {
                _bloodDonationNotificationService.Update(notification);
            }
            catch
            {
                return BadRequest();
            }
            return Ok(notification);
        }
    }
}
