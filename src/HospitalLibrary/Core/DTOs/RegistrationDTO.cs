using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.DTOs
{
    public class RegistrationDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string ProfileImageName { get; set; }
        public int Gender { get; set; }

        public int Role { get; set; }
        public RegistrationDTO() { }

        public RegistrationDTO(int id, string name, string surname, DateTime dateOfBirth, string phone, string email, string username, string password, string profileImageName, int gender, int role)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Phone = phone;
            Email = email;
            Username = username;
            Password = password;
            ProfileImageName = profileImageName;
            Gender = gender;
            Role = role;
        }
    }
}
