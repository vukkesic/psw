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
    public class ExaminationRepository : IExaminationRepository
    {
        private readonly HospitalDbContext _context;
        public ExaminationRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(ExaminationReport examinationReport)
        {
            _context.examinations.Add(examinationReport);
            _context.SaveChanges();
        }

        public void Delete(ExaminationReport examinationReport)
        {
            _context.examinations.Remove(examinationReport);
            _context.SaveChanges();
        }

        public IEnumerable<ExaminationReport> GetAll()
        {
            return _context.examinations.ToList();
        }

        public ExaminationReport GetById(int id)
        {
            return GetAll().FirstOrDefault(x => x.Id == id);
        }

        public void Update(ExaminationReport examinationReport)
        {
            _context.Entry(examinationReport).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public IEnumerable<ExaminationReport> GetByPatientId(int patientId)
        {
            return GetAll().Where(x => x.PatientId == patientId);
        }

        public IEnumerable<ExaminationReport> GetLastTwoWeekFluReports(int patientId, DateTime today)
        {
            throw new NotImplementedException();
        }
    }
}
