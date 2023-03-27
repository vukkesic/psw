using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class AuthenticatedUserDTO
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Role { get; set; }
        public string Token { get; set; }
        public bool Blocked { get; set; }
    }
}
