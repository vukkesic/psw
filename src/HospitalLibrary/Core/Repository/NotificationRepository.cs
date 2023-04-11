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
    public class NotificationRepository : INotificationRepository
    {
        private readonly HospitalDbContext _context;
        public NotificationRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(Notification notification)
        {
            _context.notifications.Add(notification);
            _context.SaveChanges();
        }

        public void Delete(Notification notification)
        {
            _context.notifications.Remove(notification);
            _context.SaveChanges();
        }

        public IEnumerable<Notification> GetAll()
        {
            return _context.notifications.ToList();
        }

        public Notification GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(Notification notification)
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
    }
}
