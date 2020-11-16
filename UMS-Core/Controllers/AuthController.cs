using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using Services.Abstraction;

namespace UMS_Core.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private ILogInService logInService;
        public AuthController(ILogInService _logInService)
        {
            logInService = _logInService;
        }

        public IActionResult LogIn(string UserId, string Password)
        {
            try
            {
                return Ok(this.logInService.AuthenticateUser(UserId, Password));
            }
            catch (Exception ex)
            {
                return Conflict(ex.Message);
            }
        }
    }
}
