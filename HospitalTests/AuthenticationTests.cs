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
            Tokens t = service.Authenticate("Maria", Role.PATIENT);
            t.ShouldNotBeNull();
        }

        private static IJWTManagerRepository CreateStubRepository()
        {
            var stubRepository = new Mock<IJWTManagerRepository>();

            var t = new Tokens()
            {
                Token = "aaaaaaaaaaaaaaaaaaaaaaaaaaaaaa",
                RefreshToken = "bbbbbbbbbbbbbbbbbbbbbbb"
            };

            stubRepository.Setup(m => m.Authenticate("Maria", Role.PATIENT)).Returns(t);
            return stubRepository.Object;
        }
    }
}
