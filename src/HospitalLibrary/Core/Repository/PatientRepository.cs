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
    public class PatientRepository : IPatientRepository
    {
        private readonly HospitalDbContext _context;
        public PatientRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public PatientRepository()
        {
        }

        public IEnumerable<Patient> GetAll()
        {
            return _context.patients.ToList();
        }

        public Patient GetById(int id)
        {
            return _context.patients.Find(id);
        }

        public Patient Create(Patient user)
        {
            if (!ExistsById(user.Id))
            {
                _context.patients.Add(user);
                _context.SaveChanges();
                return user;
            }
            return null;
        }

        public void Update(Patient user)
        {
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Patient user)
        {
            _context.patients.Remove(user);
            _context.SaveChanges();
        }

        public bool ExistsById(int id)
        {
            if (GetById(id) == null)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public Patient GetByCredentials(string username, string password)
        {
            return GetAll().FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
        }

        public IEnumerable<Patient> GetBlockedPatients()
        {
            return GetAll().Where(u => u.Blocked == true);
        }
    }
}
