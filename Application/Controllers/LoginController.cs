using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;

namespace Application.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private ILogInService _logInService;
        public LoginController(ILogInService logInService)
        {
            _logInService = logInService;
        }

        [HttpPost]
        [Route("Login/{UserId}/{Pass}")]
        public ActionResult Login(string UserId, string Pass)
        {
            if (_logInService.AuthenticateUser(UserId, Pass, out byte[] Token, out string Salt))
            {
                if (Token != null)
                {
                    HttpContext.Session.SetString(UserId, Salt);
<<<<<<< HEAD
                return Ok(Token);            
=======
                    return Ok(Token);
                }
                else
                    return StatusCode((int)HttpStatusCode.InternalServerError);
>>>>>>> 35b95c84ac04d454f466ed1a483b2830fe6cb577
            }
            else
                return StatusCode((int)HttpStatusCode.NotFound);
        }
    }
}


