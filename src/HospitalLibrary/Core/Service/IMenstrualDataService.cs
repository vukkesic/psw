using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IMenstrualDataService
    {
        IEnumerable<MenstrualData> GetAll();
        MenstrualData GetById(int id);
        void Create(MenstrualData data);
        void Update(MenstrualData data);
        void Delete(MenstrualData data);
        MenstrualData GetByPatientId(int patientId);
    }
}
