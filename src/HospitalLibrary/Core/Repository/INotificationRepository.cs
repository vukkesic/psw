using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface INotificationRepository
    {
        IEnumerable<Notification> GetAll();
        Notification GetById(int id);
        void Create(Notification notification);
        void Update(Notification notification);
        void Delete(Notification notification);
    }
}
