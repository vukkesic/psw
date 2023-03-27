using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Repository;
using HospitalLibrary.Core.Service;
using Moq;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace HospitalTests
{
    public class AuthenticationTests
    {
        [Fact]
        public void User_authenticated()
        {
            AuthenticationService service = new AuthenticationService(CreateStubRepository());
            Tokens t = service.Authenticate(new Patient()
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
            });
            t.ShouldNotBeNull();
        }

        private static IJWTManagerRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IJWTManagerRepository>();

            var maria = new Patient()
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

            var t = new Tokens()
            {
                Token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                RefreshToken = "bbbbbbbbbbbbbbbbbbbbbbb"
            };

            stubRepository.Setup(m => m.Authenticate(maria)).Returns(t);
            return stubRepository.Object;

        }
    }
}
