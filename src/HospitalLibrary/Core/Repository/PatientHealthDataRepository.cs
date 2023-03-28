using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class PatientHealthDataRepository :IPatientHealthDataRepository
    {
        private readonly HospitalDbContext _context;
        public PatientHealthDataRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public PatientHealthDataRepository() { }
        public void Create(PatientHealthData data)
        {

            _context.healthdata.Add(data);
            _context.SaveChanges();
        }

        public void Delete(PatientHealthData data)
        {
            _context.healthdata.Remove(data);
            _context.SaveChanges();
        }

        public IEnumerable<PatientHealthData> GetAll()
        {
            return _context.healthdata.Include((x => x.Patient)).ToList();
        }

        public PatientHealthData GetById(int id)
        {
            return _context.healthdata.Find(id);
        }

        public void Update(PatientHealthData data)
        {
            _context.Entry(data).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }
    }
}
