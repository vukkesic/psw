using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Mapper
{
    public class AuthenticationMapper
    {
        private readonly IPatientService _patientService;
        public AuthenticationMapper(IPatientService patientService)
        {
            _patientService = patientService;
        }

        public AuthenticatedUserDTO UserToAuthenticatedUserDTO(User user, string token)
        {
            int r = 0;
            bool b = false;
            if (user.Role == Role.PATIENT)
            {
                r = 0;
                Patient p = _patientService.GetById(user.Id);
                b = p.Blocked;
            }
            else if (user.Role == Role.DOCTOR)
            {
                r = 1;
            }
            else
            {
                r = 2;
            }

            return new AuthenticatedUserDTO
            {
                Username = user.Username,
                Id = user.Id,
                Token = token,
                Role = r,
                Blocked = b
            };
        }
    }
}
