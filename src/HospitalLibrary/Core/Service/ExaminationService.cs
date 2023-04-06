using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class ExaminationService : IExaminationService
    {
        private readonly IExaminationRepository _examinationRepository;
        public ExaminationService(IExaminationRepository examinationRepository)
        {
            _examinationRepository = examinationRepository;
        }

        public void Create(ExaminationReport examinationReport)
        {
            _examinationRepository.Create(examinationReport);
        }
        public void Delete(ExaminationReport examinationReport)
        {
            _examinationRepository.Delete(examinationReport);
        }

        public IEnumerable<ExaminationReport> GetAll()
        {
            return _examinationRepository.GetAll();
        }

        public ExaminationReport GetById(int id)
        {
            return _examinationRepository.GetById(id);
        }

        public void Update(ExaminationReport examinationReport)
        {
            _examinationRepository.Update(examinationReport);
        }

        public IEnumerable<ExaminationReport> GetByPatientId(int patientId)
        {
            return _examinationRepository.GetByPatientId(patientId);
        }
    }
}
