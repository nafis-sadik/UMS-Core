using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/Users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private IUserManagerService userManagerService;
        public UsersController()
        {
            userManagerService = new UserManagerService();
        }
        
        [HttpPost]
        [Route("Add")]
        public IActionResult AddNewUser(UserInfo userInfo)
        {
            if (userManagerService.AddNewUser(userInfo))
                return Ok();
            else
                return Conflict();
        }

        [HttpGet]
        [Route("Get/{UserId}")]
        public IActionResult GetUser(string UserId)
        {
            UserInfo userInfo = userManagerService.GetUser(UserId);
            if (userInfo == null)
                return StatusCode((int)HttpStatusCode.NotFound);
            else
                return StatusCode((int)HttpStatusCode.OK);
        }
    }
}
