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
    public class ReferralLetterRepository : IReferralLetterRepository
    {
        private readonly HospitalDbContext _context;
        public ReferralLetterRepository(HospitalDbContext context)
        {
            _context = context;
        }

        public void Create(ReferralLetter referralLetter)
        {
            _context.referralletters.Add(referralLetter);
            _context.SaveChanges();
        }

        public void Delete(ReferralLetter referralLetter)
        {
            _context.referralletters.Remove(referralLetter);
            _context.SaveChanges();
        }

        public IEnumerable<ReferralLetter> GetAll()
        {
            return _context.referralletters.ToList();
        }

        public IEnumerable<ReferralLetter> GetMyReferralLetters(int patientId)
        {
            return GetAll().Where(r => r.PatientId == patientId && r.IsActive == true);
        }

        public ReferralLetter GetById(int id)
        {
            return _context.referralletters.Find(id);
        }

        public void Update(ReferralLetter referralLetter)
        {
            _context.Entry(referralLetter).State = EntityState.Modified;

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
