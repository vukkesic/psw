﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Appointment
    {
        [Key]
        public int Id { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int DoctorId { get; set; }
        public int PatientId { get; set; }
        public bool Canceled { get; set; }
        public DateTime CancelationTime { get; set; }
        public bool Used { get; set; }

        public Appointment() { }

        public Appointment(int id, DateTime startTime, DateTime endTime, int doctorId, int patientId, bool canceled, DateTime cancelationTime, bool used)
        {
            Id = id;
            StartTime = startTime;
            EndTime = endTime;
            DoctorId = doctorId;
            PatientId = patientId;
            Canceled = canceled;
            CancelationTime = cancelationTime;
            Used = used;
        }

        public bool CompareStartTime(DateTime startTime)
        {
            if (DateTime.Compare(StartTime, startTime) == 0)
            {
                return true;
            }
            return false;
        }
        
        public bool IsCancelable()
        {
            if (DateTime.Now.AddDays(2) <= StartTime)
            {
                return true;
            }
            return false;
        }

        public void Cancel()
        {
            Canceled = true;
            CancelationTime = DateTime.Now;
        }
    }
}
