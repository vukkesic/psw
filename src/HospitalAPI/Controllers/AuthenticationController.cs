using HospitalLibrary.Core.DTOs;
using HospitalLibrary.Core.Mapper;
using HospitalLibrary.Core.Model;
using HospitalLibrary.Core.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HospitalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private IAuthenticationService _authenticationService;
        private IUserService _userService;
        private AuthenticationMapper _authenticationMapper;
        public AuthenticationController(IAuthenticationService authenticationService, IUserService userService, IPatientService patientService)
        {
            _authenticationService = authenticationService;
            _userService = userService;
            _authenticationMapper = new AuthenticationMapper(patientService);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Authenticate(AuthenticationDTO authenticationDTO)
        {
            User u = _userService.GetByCredentials(authenticationDTO.Username, authenticationDTO.Password);
            if (u != null)
            {
                Tokens t = _authenticationService.Authenticate(u.Name, u.Role, u.Gender);
                AuthenticatedUserDTO dto = _authenticationMapper.UserToAuthenticatedUserDTO(u, t.Token);
                if (dto.Blocked == true)
                    return BadRequest("You are blocked and can't access system.");
                return Ok(dto);
            }
            else
            {
                return BadRequest("Username or password is incorrect");
            }
        }
    }
}
