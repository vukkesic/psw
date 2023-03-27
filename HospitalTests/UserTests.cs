using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using Xunit;

namespace HospitalTests
{
    public class UserTests
    {
        [Fact]
        public void Username_is_already_taken()
        {
            UserService service = new UserService(CreateStubRepository());
            bool b = service.ExistsByUsername("vuk@mail.com");
            b.ShouldBeTrue();
        }

        [Fact]
        public void Username_is_not_taken()
        {
            UserService service = new UserService(CreateStubRepository());
            bool b = service.ExistsByUsername("vuk@gmail.com");
            b.ShouldBeFalse();
        }

        [Fact]
        public void User_found_by_credentials()
        {
            UserService service = new UserService(CreateStubRepository());
            User u = service.GetByCredentials("vuk@mail.com", "123");
            u.ShouldNotBeNull();
        }

        [Fact]
        public void User_not_found_by_credentials()
        {
            UserService service = new UserService(CreateStubRepository());
            User u = service.GetByCredentials("vuk@mail.com", "123");
            u.ShouldNotBeNull();
        }
        private static IUserRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IUserRepository>();
            var patients = new List<Patient>();
            var p1 = new Patient()
            {
                Id = 1,
                Name = "Vuk",
                Surname = "Kesic",
                DateOfBirth = new DateTime(2018, 7, 24),
                Email = "vuk@mail.com",
                Username = "vuk@mail.com",
                Phone = "06312212",
                Password = "123",
                ProfileImage = "",
                Gender = Gender.MALE,
                Role = Role.PATIENT,
                Blocked = false
            };
            var p2 = new Patient()
            {
                Id = 5,
                Name = "Maria",
                Surname = "Rossi",
                DateOfBirth = new DateTime(1988, 2, 12),
                Email = "maria@mail.com",
                Username = "maria@mail.com",
                Phone = "06893232",
                Password = "123",
                ProfileImage = "",
                Gender = Gender.FEMALE,
                Role = Role.PATIENT,
                Blocked = false
            };

            patients.Add(p1);
            patients.Add(p2);

            stubRepository.Setup(m => m.GetById(1)).Returns(p1);
            stubRepository.Setup(m => m.GetById(5)).Returns(p2);
            stubRepository.Setup(m => m.GetByCredentials("vuk@mail.com", "123")).Returns(p1);
            stubRepository.Setup(m => m.GetByCredentials("maria@mail.com", "123")).Returns(p2);
            stubRepository.Setup(m => m.GetAll()).Returns(patients);
            return stubRepository.Object;
        }
    }
}
