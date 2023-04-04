using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Service
{
    public class AuthenticationService : IAuthenticationService
    {
        private readonly IJWTManagerRepository _jWTManager;
        public AuthenticationService(IJWTManagerRepository jWTManager)
        {
            _jWTManager = jWTManager;
        }
        public Tokens Authenticate(string name, Role r, Gender g)
        {
            return _jWTManager.Authenticate(name, r, g);
        }
    }
}
