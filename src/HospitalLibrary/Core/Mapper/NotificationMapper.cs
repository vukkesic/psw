using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Mapper
{
    public class NotificationMapper
    {
        public NotificationMapper() { }

        public NotificationDTO MapNotificationToNotificationDTO(Notification notification)
        {
            return new NotificationDTO(notification.Id, notification.Title, notification.Text);
        }

        public Notification MapNotificationDTOToNotification(NotificationDTO notificationDTO)
        {
            return new Notification(notificationDTO.Id, notificationDTO.Title, notificationDTO.Text);
        }
    }
}
