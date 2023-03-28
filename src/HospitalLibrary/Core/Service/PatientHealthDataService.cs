using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class PatientHealthDataService : IPatientHealthDataService
    {
        private readonly IPatientHealthDataRepository _patientHealthDataRepositoty;

        public PatientHealthDataService(IPatientHealthDataRepository patientHealthDataRepositoty)
        {
            _patientHealthDataRepositoty = patientHealthDataRepositoty;
        }


        public void Create(PatientHealthData data)
        {
            _patientHealthDataRepositoty.Create(data);
        }

        public void Delete(PatientHealthData data)
        {
            _patientHealthDataRepositoty.Delete(data);
        }

        public IEnumerable<PatientHealthData> GetAll()
        {
            return _patientHealthDataRepositoty.GetAll();
        }

        public PatientHealthData GetById(int id)
        {
            return _patientHealthDataRepositoty.GetById(id);
        }

        public List<PatientHealthData> GetByUserId(int userId)
        {
            List<PatientHealthData> data = _patientHealthDataRepositoty.GetAll().ToList();
            var filteredData = new List<PatientHealthData>();
            foreach (PatientHealthData d in data)
            {
                if (d.Patient.Id == userId)
                {
                    filteredData.Add(d);
                }
            }
            return filteredData;
        }

        public void Update(PatientHealthData data)
        {
            _patientHealthDataRepositoty.Update(data);
        }
    }
}
