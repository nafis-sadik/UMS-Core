using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/Home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private ILogInService _logInService;
        private IUserManagerService _userManagerService;
        public HomeController(ILogInService logInService, IUserManagerService userManagerService)
        {
            _logInService = logInService;
            _userManagerService = userManagerService;
        }

        [HttpPost]
        [Route("TestingCon")]
        public ActionResult TestingCon(string UserId, string Pass)
        {
            if (_logInService.AuthenticateUser(UserId, Pass))
                return Ok();
            else
                return Conflict();
        }
    }
}
