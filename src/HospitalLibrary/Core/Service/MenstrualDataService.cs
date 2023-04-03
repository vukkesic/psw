using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class MenstrualDataService : IMenstrualDataService
    {
        private readonly IMenstrualDataRepository _menstrualDataRepository;

        public MenstrualDataService(IMenstrualDataRepository menstrualDataRepository)
        {
            _menstrualDataRepository = menstrualDataRepository;
        }

        public void Create(MenstrualData data)
        {
            _menstrualDataRepository.Create(data);
        }

        public void Delete(MenstrualData data)
        {
            _menstrualDataRepository.Delete(data);
        }

        public IEnumerable<MenstrualData> GetAll()
        {
            return _menstrualDataRepository.GetAll();
        }

        public MenstrualData GetById(int id)
        {
            return _menstrualDataRepository.GetById(id);
        }

        public MenstrualData GetByPatientId(int patientId)
        {
            return _menstrualDataRepository.GetByPatientId(patientId);
        }

        public void Update(MenstrualData data)
        {
            _menstrualDataRepository.Update(data);
        }
    }
}
