using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class Doctor : User
    {
        public string LicenseNumber { get; set; }

        public Doctor() { }

        public Doctor(int id, string name, string surname, DateTime dateOfBirth,
                string email, string username, string password, string phone,
                Gender gender, string profileImage, Role role, string licenseNumber)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Email = email;
            Username = username;
            Phone = phone;
            Password = password;
            ProfileImage = profileImage;
            Gender = gender;
            Role = role;
            LicenseNumber = licenseNumber;
        }
    }
}
