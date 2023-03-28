﻿using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly HospitalDbContext _context;
        public AppointmentRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public void Create(Appointment appointment)
        {
            _context.appointments.Add(appointment);
            _context.SaveChanges();
        }

        public void Delete(Appointment appointment)
        {
            _context.appointments.Remove(appointment);
            _context.SaveChanges();
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _context.appointments.ToList();
        }
        public Appointment GetById(int id)
        {
            return _context.appointments.Find(id);
        }

        public void Update(Appointment appointment)
        {
            _context.Entry(appointment).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
