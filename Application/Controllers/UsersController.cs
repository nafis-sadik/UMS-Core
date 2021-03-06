﻿using Application.Helper;
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

/* API Documentation
 * Action : AddNewUser
 * Response : Created - Sign up Successful
 *            InternalServerError - Un handeled exception found, start debuging
 *            Conflict - User Already exists
 * Action : GetUser
 * Response : NotFound - User was not found
 *            Ok - User found successfully, object also returned
 * Action : UpdateUser
 * Response : Ok - Successful
 *            NotModified - Failed to Update, unhandeled exception occured, start debuging
 */

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
        [CustomAuthentication]
        public IActionResult AddNewUser(UserInfo userInfo)
        {
            bool? response = _userManagerService.AddNewUser(userInfo);
            if (response == true)
                return StatusCode((int)HttpStatusCode.Created);
            else if (response == false)
                return StatusCode((int)HttpStatusCode.InternalServerError);
            else
                return StatusCode((int)HttpStatusCode.Conflict);
        }

        [HttpGet]
        [Route("Get/{UserId}")]
        [CustomAuthentication]
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
        [CustomAuthentication]
        public IActionResult UpdateUser(UserInfo userInfo)
        {
            if (_userManagerService.UpdateUser(userInfo))
                return Ok(userInfo);
            else
                return StatusCode((int)HttpStatusCode.NotModified);
        }

        [HttpGet]
        [Route("GetAll/{PageNumber}")]
        [CustomAuthentication]
        public IActionResult GetAllUsers(int PageNumber)
        {
            PagingParam pagingParam = new PagingParam();
            pagingParam.Page = PageNumber;
            pagingParam.PageSize = 10;
            return Ok(_userManagerService.GetAllUsers(pagingParam));
        }

        [HttpPost]
        [Route("ChangePassword/{UserId}/{OldPass}/{NewPass}")]
        public IActionResult ChangePassword(string UserId, string OldPass, string NewPass)
        {
            if (_userManagerService.ChangePassword(UserId, OldPass, NewPass))
                return Ok();
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }

        [HttpPost]
        [Route("ResetPassword/{UserId}")]
        public IActionResult ResetPassword(string UserId)
        {
            if (_userManagerService.ResetPassword(UserId))
                return Ok();
            else
                return StatusCode((int)HttpStatusCode.InternalServerError);
        }
    }
}

