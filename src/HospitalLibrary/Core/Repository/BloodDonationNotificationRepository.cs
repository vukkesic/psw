using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class BloodDonationNotificationRepository : IBloodDonationNotificationRepository
    {
        private readonly HospitalDbContext _context;

        public BloodDonationNotificationRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(BloodDonationNotification notification)
        {
            _context.blooddonationnotifications.Add(notification);
            _context.SaveChanges();
        }

        public void Delete(BloodDonationNotification notification)
        {
            _context.blooddonationnotifications.Remove(notification);
            _context.SaveChanges();
        }

        public IEnumerable<BloodDonationNotification> GetAll()
        {
            return _context.blooddonationnotifications.ToList();
        }

        public BloodDonationNotification GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(BloodDonationNotification notification)
        {
            _context.Entry(notification).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IEnumerable<BloodDonationNotification> GetPending()
        {
            return GetAll().Where(x => x.Status == "PENDING");
        }

        public IEnumerable<BloodDonationNotification> GetApproved()
        {
            return GetAll().Where(x => x.Status == "APPROVED");
        }
    }
}
