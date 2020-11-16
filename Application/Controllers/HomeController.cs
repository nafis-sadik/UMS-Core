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
        private ILogInService logInService;
        public HomeController()
        {
            logInService = new LogInService();;
        }

        [HttpPost]
        [Route("TestingCon")]
        public ActionResult TestingCon(string UserId, string Pass)
        {
            if (logInService.AuthenticateUser(UserId, Pass))
                return Ok();
            else
                return Conflict();
        }
    }
}
