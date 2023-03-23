using HospitalLibrary.Core.Model;
using HospitalLibrary.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly HospitalDbContext _context;
        public UserRepository(HospitalDbContext context)
        {
            _context = context;
        }
        public IEnumerable<User> GetAll()
        {
            return _context.users.ToList();
        }

        public User GetByCredentials(string username, string password)
        {
            return GetAll().FirstOrDefault(u => u.Username.Equals(username) && u.Password.Equals(password));
        }

        public User GetById(int id)
        {
            return _context.users.Find(id);
        }
    }
}
