using Dotnet_Core_Scaffolding_Oracle.Models;
using Models;
using Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMSApi.Helpers;

namespace Services
{
    public class RoleService : IRoleService
    {
        private IRoleRepo _roleRepo;

        public RoleService(IRoleRepo roleRepo)
        {
            _roleRepo = roleRepo;
        }
        public List<RoleInfo> GetRoleInformations(PagingParam pagingParam)
        {
            List<UmsRole> roleInfo = _roleRepo.AsQueryable().Where(x=>
                                                  x.Recstatus == HelperActionConst.Authorized||
                                                  x.Recstatus == HelperActionConst.Pending)
                                                  .Skip(pagingParam.Skip).Take(pagingParam.PageSize)
                                                  .ToList();

            List<RoleInfo> response = new List<RoleInfo>();
            foreach (UmsRole item in roleInfo)
            {
                response.Add(CastToModel(item));
            }
            return response;
        }
        private static RoleInfo CastToModel(UmsRole roleInfo)
        {
            return new RoleInfo
            {
                Roleid=roleInfo.Roleid,
                Rolename=roleInfo.Rolename,
                Purpose=roleInfo.Purpose,
                Recstatus=roleInfo.Recstatus
            };
        }

        public RoleInfo GetRoleInformation(long Roleid)
        {
            UmsRole data = _roleRepo.AsQueryable().FirstOrDefault(x => x.Roleid == Roleid);
            if (data != null)
                return CastToModel(data);
            else
                return null;
        }
    }
}
