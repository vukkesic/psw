using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool ExistsByUsername(string username)
        {

            List<User> users = _userRepository.GetAll().ToList();
            return users.Any(user => user.Username != null && user.Username == username);
        }

        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetByCredentials(string username, string password)
        {
            return _userRepository.GetByCredentials(username, password);
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
    }
}
