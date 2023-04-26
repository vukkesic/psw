using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IBloodDonationNotificationRepository
    {
        IEnumerable<BloodDonationNotification> GetAll();
        BloodDonationNotification GetById(int id);
        void Create(BloodDonationNotification notification);
        void Update(BloodDonationNotification notification);
        void Delete(BloodDonationNotification notification);
    }
}
