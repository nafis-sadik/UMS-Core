using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Abstraction
{
   public interface IRoleService
    {
        List<RoleInfo> GetRoleInformations(PagingParam pagingParam);
        RoleInfo GetRoleInformation(long Roleid);
        bool UpdateRoleInformation(RoleInfo roleInfo);
        public dynamic GetUserInfo(string UserId);
        bool AddRoleInformation(RoleInfo roleInfo);
    }
}
