using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests
{
    public class BloodDonationTests
    {
        [Fact]
        public void Get_approved()
        {
            BloodDonationNotificationService service = new BloodDonationNotificationService(CreateStubRepository());
            IEnumerable<BloodDonationNotification> notifications = service.GetApproved();
            notifications.ShouldNotBeEmpty();
        }

        [Fact]
        public void Get_pending()
        {
            BloodDonationNotificationService service = new BloodDonationNotificationService(CreateStubRepository());
            IEnumerable<BloodDonationNotification> notifications = service.GetPending();
            notifications.ShouldNotBeEmpty();
        }

        private static IBloodDonationNotificationRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IBloodDonationNotificationRepository>();

            var bdn1 = new BloodDonationNotification(1, "Obavestenje", "Obavestavamo vas da ce se organizovati ddk.", new DateTime(2023, 05, 20, 9, 00, 0), new DateTime(2023, 05, 20, 15, 00, 0), "Novi Sad", "APPROVED");
            var bdn2 = new BloodDonationNotification(2, "Obavestenje", "Obavestavamo vas da ce se organizovati ddk.", new DateTime(2023, 06, 24, 7, 00, 0), new DateTime(2023, 06, 24, 11, 00, 0), "Temerin", "PENDING");

            var notifications = new List<BloodDonationNotification>();
            notifications.Add(bdn1);
            notifications.Add(bdn2);
            var list1 = new List<BloodDonationNotification>();
            list1.Add(bdn1);
            IEnumerable<BloodDonationNotification> res1 = list1;
            var list2 = new List<BloodDonationNotification>();
            list2.Add(bdn2);
            IEnumerable<BloodDonationNotification> res2 = list2;

            stubRepository.Setup(m => m.GetAll()).Returns(notifications);
            stubRepository.Setup(m => m.GetApproved()).Returns(res1);
            stubRepository.Setup(m => m.GetPending()).Returns(res2);
            return stubRepository.Object;
        }
    }
}
