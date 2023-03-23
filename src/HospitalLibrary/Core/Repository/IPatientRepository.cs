using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IPatientRepository
    {
        IEnumerable<Patient> GetAll();
        Patient GetById(int id);
        Patient GetByCredentials(string username, string password);
        Patient Create(Patient user);
        void Update(Patient user);
        void Delete(Patient user);
        bool ExistsById(int id);
        IEnumerable<Patient> GetBlockedPatients();

    }
}
