using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public interface IPatientHealthDataRepository
    {
        IEnumerable<PatientHealthData> GetAll();
        PatientHealthData GetById(int id);
        void Create(PatientHealthData data);
        void Update(PatientHealthData data);
        void Delete(PatientHealthData data);
    }
}
