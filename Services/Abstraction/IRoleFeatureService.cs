using System;
using System.Collections.Generic;
using System.Text;
using Models;

namespace Services.Abstraction
{
    public interface IRoleFeatureService
    {
        public dynamic GetRoleDetailInformations(PagingParam pagingParam);
        public dynamic GetRoleDetailInformation(long id);
        public dynamic DropDownRoleAppList();
        public dynamic AppModuleList(string appId);
        public dynamic ModuleFeatureList(string moduleId);
        public dynamic AddRoleDetailInfo(RoleDetailsInfo roleDetailsInfo);
    }
}
