using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Mapper
{
    public class AppointmentMapper
    {
        public Appointment MapAppointmentDTOToAppointment(AppointmentDTO dto)
        {
            return new Appointment(dto.Id, dto.StartTime, dto.EndTime, dto.DoctorId, dto.PatientId, dto.Canceled, dto.CancelationTime, dto.Used);
        }

        public AppointmentDTO MapAppointmentToAppointmentDTO(Appointment appointment)
        {
            return new AppointmentDTO(appointment.Id, appointment.StartTime, appointment.EndTime, appointment.DoctorId, appointment.PatientId, appointment.Canceled, appointment.CancelationTime, appointment.Used);
        }
    }
}
