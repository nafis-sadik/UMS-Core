using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using Services.Abstraction;
using System.Linq;
using System.Threading.Tasks;
using Application.Helper;
using Models;
using System.Net;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private IRoleService _roleService;
        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }
        [HttpGet]
        [Route("GetRoleInfos/{PageNumber}")]
        //[CustomAuthentication]
        public IActionResult GetRoleInformations(int PageNumber)
        {
            PagingParam pagingParam = new PagingParam();
            pagingParam.Page = PageNumber;
            pagingParam.PageSize = 10;
            return Ok(_roleService.GetRoleInformations(pagingParam));
        }
        [HttpGet]
        [Route("GetRoleInfo/{Roleid}")]
        //[CustomAuthentication]
        public IActionResult GetRoleInformation(long Roleid)
        {
            RoleInfo roleInfo = _roleService.GetRoleInformation(Roleid);
            if (roleInfo == null)
                return StatusCode((int)HttpStatusCode.NotFound);
            else
                return Ok(roleInfo);
        }
    }
}
