using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IPatientHealthDataService
    {
        IEnumerable<PatientHealthData> GetAll();
        PatientHealthData GetById(int id);
        List<PatientHealthData> GetByUserId(int userId);
        void Create(PatientHealthData data);
        void Update(PatientHealthData data);
        void Delete(PatientHealthData data);
    }
}
