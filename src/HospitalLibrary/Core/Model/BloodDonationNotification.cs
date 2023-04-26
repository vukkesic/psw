using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class BloodDonationNotification
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Location { get; set; }

        public BloodDonationNotification() { }

        public BloodDonationNotification(int id, string title, string text, DateTime startTime, DateTime endTime, string location)
        {
            Id = id;
            Title = title;
            Text = text;
            StartTime = startTime;
            EndTime = endTime;
            Location = location;
        }
    }
}
