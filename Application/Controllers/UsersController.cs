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
        private IUserManagerService _userManagerService;
        public UsersController(IUserManagerService userManagerService)
        {
            _userManagerService = userManagerService;
        }
        
        [HttpPost]
        [Route("Add")]
        public IActionResult AddNewUser(UserInfo userInfo)
        {
            if (_userManagerService.AddNewUser(userInfo))
                return StatusCode((int)HttpStatusCode.OK);
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpGet]
        [Route("Get/{UserId}")]
        public IActionResult GetUser(string UserId)
        {
            UserInfo userInfo = _userManagerService.GetUser(UserId);
            if (userInfo == null)
                return StatusCode((int)HttpStatusCode.NotFound);
            else
                return Ok(userInfo);
        }

        [HttpPost]
        [Route("Update")]
        public IActionResult UpdateUser(UserInfo userInfo)
        {
            if (_userManagerService.UpdateUser(userInfo))
                return Ok();
            else
                return StatusCode((int)HttpStatusCode.NotFound);
        }
    }
}
