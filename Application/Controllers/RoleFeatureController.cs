using Application.Helper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleFeatureController : ControllerBase
    {
        private IRoleFeatureService _roleFeatureService;
        public RoleFeatureController(IRoleFeatureService roleFeatureService)
        {
            _roleFeatureService = roleFeatureService;
        }

        [HttpGet]
        [Route("GetRoleFeatureInfos/{PageNumber}")]
        [CustomAuthentication]
        public IActionResult GetRoleInformations(int PageNumber)
        {
            PagingParam pagingParam = new PagingParam();
            pagingParam.Page = PageNumber;
            pagingParam.PageSize = 10;
            return Ok(_roleFeatureService.GetRoleDetailInformations(pagingParam));
        }
        [HttpGet]
        [Route("GetRoleFeatureInfo/{id}")]
        //[CustomAuthentication]
        public IActionResult GetRoleInformations(long id)
        {
            var roleDtlInfo = _roleFeatureService.GetRoleDetailInformation(id);
            if (roleDtlInfo == null)
                return StatusCode((int)HttpStatusCode.NotFound);
            else
                return Ok(roleDtlInfo);
        }
        [HttpGet]
        [Route("DropdownList")]
        public IActionResult DropDownRoleList()
        {
            var list = _roleFeatureService.DropDownRoleAppList();
            return Ok(list);
        }
        [HttpGet]
        [Route("AppModuleList/{appId}")]
        public IActionResult AppModuleList(string appId)
        {
            var list = _roleFeatureService.AppModuleList(appId);
            return Ok(list);
        }
        [HttpGet]
        [Route("ModuleFeatureList/{moduleId}")]
        public IActionResult ModuleFeatureList(string moduleId)
        {
            var list = _roleFeatureService.ModuleFeatureList(moduleId);
            return Ok(list);
        }
    }
}
