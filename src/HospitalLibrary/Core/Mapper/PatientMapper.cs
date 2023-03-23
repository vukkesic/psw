using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Mapper
{
    public class PatientMapper
    {
        public static Patient MapRegistrationDTOToPatient(RegistrationDTO userDTO)
        {
            Patient user = new Patient();
            user.Id = userDTO.Id;
            user.Name = userDTO.Name;
            user.Surname = userDTO.Surname;
            user.DateOfBirth = userDTO.DateOfBirth;
            user.Email = userDTO.Email;
            user.Phone = userDTO.Phone;
            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            Gender g = new Gender();
            if (userDTO.Gender == 0)
            {
                g = Gender.MALE;
            }
            else if (userDTO.Gender == 1)
            {
                g = Gender.FEMALE;
            }
            user.Gender = g;
            user.ProfileImage = userDTO.ProfileImageName;
            user.Role = Role.PATIENT;
            user.Blocked = false;
            return user;
        }
    }
}
