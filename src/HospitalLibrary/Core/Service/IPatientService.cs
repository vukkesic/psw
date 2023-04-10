using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IPatientService
    {
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        Patient Create(Patient user);
        void Update(Patient user);
        void Delete(Patient user);
        bool ExistsById(int id);
        List<Patient> GetAllBlockablePatients(IEnumerable<Appointment> appointments);
        void BlockPatient(int id);
    }
}
