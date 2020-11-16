using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private LogInService logInService;
        public HomeController(LogInService _logInService)
        {
            logInService = _logInService;
        }

        [HttpPost]
        public ActionResult TestingCon(string UserId, string Pass)
        {
            if (logInService.AuthenticateUser(UserId, Pass))
                return Ok();
            else
                return Conflict();
        }
    }
}
