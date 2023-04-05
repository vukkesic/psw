using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IAppointmentService
    {
        IEnumerable<Appointment> GetAll();
        Appointment GetById(int id);
        void Create(Appointment appointment);
        void Update(Appointment appointment);
        void Delete(Appointment appointment);
        IEnumerable<Appointment> GetByDoctor(int doctorId);
        IEnumerable<Appointment> GetByPatient(int patientId);
        IEnumerable<Appointment> GetDoctorTodayAppointments(DateTime today, int doctorId);
    }
}
