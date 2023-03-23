using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace HospitalLibrary.Core.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public Gender Gender { get; set; }
        public string ProfileImage { get; set; }
        public Role Role { get; set; }

        public User() { }

        public User(int id, string name, string surname, DateTime dateOfBirth, string email, string username,
            string password, string phone, Gender gender, string profileImage, Role role)
        {
            Id = id;
            Name = name;
            Surname = surname;
            DateOfBirth = dateOfBirth;
            Email = email;
            Username = username;
            Password = password;
            Phone = phone;
            Gender = gender;
            ProfileImage = profileImage;
            Role = role;
        }
    }
}
