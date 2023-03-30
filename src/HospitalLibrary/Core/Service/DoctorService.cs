using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class DoctorService :IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;
        public DoctorService(IDoctorRepository doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

        public IEnumerable<Doctor> GetAll()
        {
            return _doctorRepository.GetAll();
        }

        public Doctor GetById(int id)
        {
            return _doctorRepository.GetById(id);
        }
        public Doctor GetBySpecialization(int specId)
        {
            return _doctorRepository.GetBySpecialization(specId);
        }

        public IEnumerable<Doctor> GetAllSpecialist(int specializationId)
        {
            return _doctorRepository.GetAllSpecialist(specializationId);
        }
    }
}
