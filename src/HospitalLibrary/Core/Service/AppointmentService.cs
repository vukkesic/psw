using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class AppointmentService :IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;
        public AppointmentService(IAppointmentRepository appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public void Create(Appointment appointment)
        {
            _appointmentRepository.Create(appointment);
        }

        public void Delete(Appointment appointment)
        {
            _appointmentRepository.Delete(appointment);
        }

        public IEnumerable<Appointment> GetAll()
        {
            return _appointmentRepository.GetAll();
        }

        public Appointment GetById(int id)
        {
            return _appointmentRepository.GetById(id);
        }

        public void Update(Appointment appointment)
        {
            _appointmentRepository.Update(appointment);
        }

        public IEnumerable<Appointment> GetByDoctor(int doctorId)
        {
            return _appointmentRepository.GetByDoctor(doctorId);
        }

        public IEnumerable<Appointment> GetByPatient(int patientId)
        {
            return _appointmentRepository.GetByPatient(patientId);
        }

        public IEnumerable<Appointment> GetDoctorTodayAppointments(DateTime today, int doctorId)
        {
            return _appointmentRepository.GetDoctorTodayAppointments(today, doctorId);
        }

        public IEnumerable<Appointment> GetLastMonthCanceledAppointments()
        {
            DateTime today = DateTime.Now;
            return _appointmentRepository.GetLastMonthCanceledAppointments(today);
        }


        //This is function that takes arguments and returns best appointment for your parameters
        //Appointments are on each half a hour and only in working hours
        private SuggestionDTO CreateSuggestion(Appointment[] appointments, Period period, Doctor d, string message)
        {
            for (DateTime start = period.StartTime; start < period.EndTime; start = start.AddMinutes(30))
            {
                if (isWorkingTime(start) && isWorkday(start))
                {
                    Boolean flag = false;
                    foreach (Appointment a in appointments)
                    {
                        if (a.CompareStartTime(start))
                            flag = true;
                    }
                    if (flag == false)
                        return (new SuggestionDTO(start, start.AddMinutes(30), d.Id, period.PatientId, d.Name, message));
                }
            }
            return null;
        }

        //This function check are working hours
        private bool isWorkingTime(DateTime startTime)
        {
            return (startTime.TimeOfDay.Subtract(new TimeSpan(08, 00, 00)).Ticks >= 0 && startTime.TimeOfDay.Subtract(new TimeSpan(15, 30, 00)).Ticks <= 0);
        }

        //This function check is workday
        private bool isWorkday(DateTime startTime)
        {
            return (startTime.DayOfWeek != DayOfWeek.Saturday && startTime.DayOfWeek != DayOfWeek.Sunday);
        }

        public SuggestionDTO CheckPeriod(Period period, Doctor doctor, Doctor[] doctors)
        {
            period.RoundUpPeriod();
            Appointment[] appointments = (_appointmentRepository.GetByDoctor(period.DoctorId)).ToArray();
            SuggestionDTO suggestion = null;

            suggestion = CreateSuggestion(appointments, period, doctor, "Great! We found appointment for your parameters.");
            if (suggestion != null)
                return suggestion;

            //Selected priority is doctor
            if (period.Priority == 1)
            {
                Period tempPeriod = new Period(period.StartTime, period.EndTime, period.DoctorId, period.PatientId, period.Priority);
                tempPeriod.EndTime = tempPeriod.EndTime.AddDays(7);

                // check 7 days after prefered period
                suggestion = CreateSuggestion(appointments, tempPeriod, doctor, "Sorry, all appointments for your preferred time are busy. We offer you adjusted appointment.");
                if (suggestion != null)
                    return suggestion;

                // check 7 days before prefered period if possible
                if (DateTime.Compare(period.StartTime, DateTime.Now.AddDays(9)) > 0)
                {
                    tempPeriod.StartTime = tempPeriod.StartTime.AddDays(7); ;
                }
                else
                {
                    tempPeriod.StartTime = DateTime.Now.AddDays(2);
                    tempPeriod.RoundUpPeriod();
                }
                tempPeriod.EndTime = period.StartTime;

                suggestion = CreateSuggestion(appointments, tempPeriod, doctor, "Sorry, all appointments for your preferred time are busy. We offer you adjusted appointment.");
                if (suggestion != null)
                    return suggestion;
            }
            // Selected priority is period 
            else if (period.Priority == 0)
            {
                foreach (Doctor doc in doctors)
                {
                    appointments = (_appointmentRepository.GetByDoctor(doc.Id)).ToArray();
                    suggestion = CreateSuggestion(appointments, period, doc, "Sorry, all appointments for your preferred doctor are busy. We offer you adjusted appointment.");
                    if (suggestion != null)
                        return suggestion;
                }
            }
            return null;
        }
    }
}
