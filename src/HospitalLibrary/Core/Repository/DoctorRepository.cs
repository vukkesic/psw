using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class DoctorRepository : IDoctorRepository
    {
        private readonly HospitalDbContext _context;
        public DoctorRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public DoctorRepository()
        { }
        public IEnumerable<Doctor> GetAll()
        {
            return _context.doctors.ToList();
        }

        public Doctor GetById(int id)
        {
            return _context.doctors.Find(id);
        }
    }
}
