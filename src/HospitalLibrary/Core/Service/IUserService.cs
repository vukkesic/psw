using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public interface IUserService
    {
        IEnumerable<User> GetAll();
        User GetById(int id);
        bool ExistsByUsername(string username);
        User GetByCredentials(string username, string password);
    }
}
