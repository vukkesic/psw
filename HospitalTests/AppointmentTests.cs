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
    public class AppointmentTests
    {
        [Fact]
        public void Get_by_patient()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetByPatient(1);
            a.ShouldNotBeEmpty();
        }


        [Fact]
        public void Get_by_patient_not_found()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetByPatient(10);
            a.ShouldBeEmpty();
        }

        [Fact]
        public void Get_by_doctor()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetByDoctor(4);
            a.ShouldNotBeEmpty();
        }


        [Fact]
        public void Get_by_doctor_not_found()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetByDoctor(3);
            a.ShouldBeEmpty();
        }

        [Fact]
        public void Get_doctor_today_appointments()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetDoctorTodayAppointments(new DateTime(), 4);
            a.ShouldNotBeEmpty();
        }


        [Fact]
        public void Get_doctor_today_appointments_not_found()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetDoctorTodayAppointments(new DateTime(), 3);
            a.ShouldBeEmpty();
        }
        [Fact]
        public void Start_time_equals()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            Appointment a = service.GetById(1);
            bool b = a.CompareStartTime(new DateTime(2023, 05, 20, 9, 30, 0));
            b.ShouldBeTrue();
        }

        [Fact]
        public void Start_time_not_equals()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            Appointment a = service.GetById(1);
            bool b = a.CompareStartTime(new DateTime(2023, 05, 20, 9, 31, 0));
            b.ShouldBeFalse();
        }

        [Fact]
        public void Get_last_month_canceled_appointments()
        {
            AppointmentService service = new AppointmentService(CreateStubRepository());
            IEnumerable<Appointment> a = service.GetLastMonthCanceledAppointments();
            a.ShouldBeEmpty();
        }

        private static IAppointmentRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IAppointmentRepository>();
            var appointments = new List<Appointment>();

            var app1 = new Appointment(1, new DateTime(2023, 05, 20, 9, 30, 0), new DateTime(2023, 05, 20, 10, 00, 0), 2, 1, false, new DateTime(), false);
            var app2 = new Appointment(2, new DateTime(2023, 04, 20, 9, 30, 0), new DateTime(2023, 04, 20, 10, 00, 0), 4, 3, false, new DateTime(), false);
            var app3 = new Appointment(3, new DateTime(), new DateTime(), 4, 3, false, new DateTime(), false);
            appointments.Add(app1);
            appointments.Add(app2);
            appointments.Add(app3);
            var list3 = new List<Appointment>();
            list3.Add(app3);
            IEnumerable<Appointment> res3 = list3;
            var list2 = new List<Appointment>();
            list2.Add(app2);
            list2.Add(app3);
            IEnumerable<Appointment> res2 = list2;
            var list1 = new List<Appointment>();
            list1.Add(app1);
            IEnumerable<Appointment> res1 = list1;
            stubRepository.Setup(m => m.GetAll()).Returns(appointments);
            stubRepository.Setup(m => m.GetByPatient(3)).Returns(res2);
            stubRepository.Setup(m => m.GetByPatient(1)).Returns(res1);
            stubRepository.Setup(m => m.GetByDoctor(2)).Returns(res1);
            stubRepository.Setup(m => m.GetByDoctor(4)).Returns(res2);
            stubRepository.Setup(m => m.GetById(1)).Returns(app1);
            stubRepository.Setup(m => m.GetById(2)).Returns(app2);
            stubRepository.Setup(m => m.GetDoctorTodayAppointments(new DateTime(), 4)).Returns(res3);
            return stubRepository.Object;
        }
    }
}
