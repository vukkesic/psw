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
    public class MenstrualDataRepository : IMenstrualDataRepository
    {
        private readonly HospitalDbContext _context;

        public MenstrualDataRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(MenstrualData data)
        {
            _context.menstrualdata.Add(data);
            _context.SaveChanges();
        }

        public void Delete(MenstrualData data)
        {
            _context.menstrualdata.Remove(data);
            _context.SaveChanges();
        }

        public IEnumerable<MenstrualData> GetAll()
        {
            return _context.menstrualdata.ToList();
        }

        public MenstrualData GetById(int id)
        {
            return _context.menstrualdata.Find(id);
        }

        public MenstrualData GetByPatientId(int patientId)
        {
            return GetAll().FirstOrDefault(a => a.PatientId.Equals(patientId));
        }

        public void Update(MenstrualData data)
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
