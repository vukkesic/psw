using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _notificationRepository;
        public NotificationService(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        public void Create(Notification notification)
        {
            _notificationRepository.Create(notification);
        }

        public void Delete(Notification notification)
        {
            _notificationRepository.Delete(notification);
        }

        public IEnumerable<Notification> GetAll()
        {
            return _notificationRepository.GetAll();
        }

        public Notification GetById(int id)
        {
            return _notificationRepository.GetById(id);
        }

        public void Update(Notification notification)
        {
            _notificationRepository.Update(notification);
        }
    }
}
