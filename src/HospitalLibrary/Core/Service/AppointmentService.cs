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
    }
}
