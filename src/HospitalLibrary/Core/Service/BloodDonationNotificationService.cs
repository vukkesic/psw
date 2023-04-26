using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class BloodDonationNotificationService : IBloodDonationNotificationService
    {
        private readonly IBloodDonationNotificationRepository _bloodDonationNotificationRepository;

        public BloodDonationNotificationService(IBloodDonationNotificationRepository bloodDonationNotificationRepository)
        {
            _bloodDonationNotificationRepository = bloodDonationNotificationRepository;
        }
        public void Create(BloodDonationNotification notification)
        {
            _bloodDonationNotificationRepository.Create(notification);
        }

        public void Delete(BloodDonationNotification notification)
        {
            _bloodDonationNotificationRepository.Delete(notification);
        }

        public IEnumerable<BloodDonationNotification> GetAll()
        {
            return _bloodDonationNotificationRepository.GetAll();
        }

        public BloodDonationNotification GetById(int id)
        {
            return _bloodDonationNotificationRepository.GetById(id);
        }

        public void Update(BloodDonationNotification notification)
        {
            _bloodDonationNotificationRepository.Update(notification);
        }
    }
}
