using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Repository
{
    public class UserRepository : IUserRepository
    {
        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetByCredentials(string username, string password)
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            throw new NotImplementedException();
        }
    }
}
