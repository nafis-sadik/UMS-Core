using Repositories;
using Services.Abstraction;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMSApi.Helpers;

namespace Services
{
    public class RoleFeatureService : IRoleFeatureService
    {
        private IRoleDetailRepo _roleDetailRepo;
        private IRoleRepo _roleRepo;
        private IAppConfigRepo _appConfigRepo;
        private IModuleConfigRepo _moduleConfigRepo;
        private IFeatureConfigRepo _featureConfigRepo;
        public RoleFeatureService(IRoleDetailRepo roleDetailRepo,
                                  IRoleRepo roleRepo,
                                  IAppConfigRepo appConfigRepo,
                                  IModuleConfigRepo moduleConfigRepo,
                                  IFeatureConfigRepo featureConfigRepo)
        {
            _roleDetailRepo = roleDetailRepo;
            _roleRepo = roleRepo;
            _appConfigRepo = appConfigRepo;
            _moduleConfigRepo = moduleConfigRepo;
            _featureConfigRepo = featureConfigRepo;
        }
        public dynamic GetRoleDetailInformations(PagingParam pagingParam)
        {
            var roleDetailInfo = _roleDetailRepo.AsQueryable().Where(rd =>
                        rd.Role.Recstatus == HelperActionConst.Authorized
                        && rd.App.Recstatus == HelperActionConst.Authorized
                        && rd.Module.Recstatus == HelperActionConst.Authorized
                        && rd.Feature. Recstatus== HelperActionConst.Authorized
                        && (rd.Recstatus == HelperActionConst.Pending || rd.Recstatus == HelperActionConst.Authorized))
                         .Skip(pagingParam.Skip).Take(pagingParam.PageSize)
                         .OrderByDescending(o=>o.Roledtlid)
                    .Select(rds => new
                    {
                        RoleDtlId = rds.Roledtlid,
                        RoleId = rds.Roleid,
                        AppId = rds.Appid,
                        ModuleId = rds.Moduleid,
                        FeatureId = rds.Featureid,
                        CreateYN = rds.Createyn,
                        ViewDetailYN = rds.Viewdetailyn,
                        EditYN = rds.Edityn,
                        CancelYN = rds.Cancelyn,
                        DeleteYN = rds.Deleteyn,
                        AuthYN = rds.Authyn,
                        //rds.AuthLevel,
                        RoleDetailStatus = rds.Recstatus,
                        Role = new
                        {
                            RoleId = rds.Role.Roleid,
                            RoleName = rds.Role.Rolename,
                            Purpose = rds.Role.Purpose,
                            RoleStatus = rds.Role.Recstatus
                        },
                        App = new
                        {
                            AppId = rds.App.Appid,
                            Name = rds.App.Name,
                            Purpose = rds.App.Purpose,
                            AppStatus = rds.App.Recstatus,
                        },
                        Module = new
                        {
                            ModuleId = rds.Module.Moduleid,
                            Name = rds.Module.Name,
                            Purpose = rds.Module.Purpose,
                            ModuleStatus = rds.Module.Recstatus,
                        },
                        Feature = new
                        {
                            FeatureId = rds.Feature.Featureid,
                            Name = rds.Feature.Name,
                            Purpose = rds.Feature.Purpose,
                            rds.Feature.Otp,
                            rds.Feature.Mfa,
                            FeatureStatus = rds.Feature.Recstatus
                        }
                    }).ToList();

            return roleDetailInfo;
        }

        public dynamic GetRoleDetailInformation(long id)
        {
            var roleDetailInfo = _roleDetailRepo.AsQueryable().Where(rd =>
                         rd.Roledtlid == id &&
                         rd.Role.Recstatus == HelperActionConst.Authorized
                         && rd.App.Recstatus == HelperActionConst.Authorized
                         && rd.Module.Recstatus == HelperActionConst.Authorized
                         && rd.Feature.Recstatus == HelperActionConst.Authorized
                         && (rd.Recstatus == HelperActionConst.Pending || rd.Recstatus == HelperActionConst.Authorized))
      
                     .Select(rds => new
                     {
                         RoleDtlId = rds.Roledtlid,
                         RoleId = rds.Roleid,
                         AppId = rds.Appid,
                         ModuleId = rds.Moduleid,
                         FeatureId = rds.Featureid,
                         CreateYN = rds.Createyn,
                         ViewDetailYN = rds.Viewdetailyn,
                         EditYN = rds.Edityn,
                         CancelYN = rds.Cancelyn,
                         DeleteYN = rds.Deleteyn,
                         AuthYN = rds.Authyn,
                        //rds.AuthLevel,
                        RoleDetailStatus = rds.Recstatus,
                         Role = new
                         {
                             RoleId = rds.Role.Roleid,
                             RoleName = rds.Role.Rolename,
                             Purpose = rds.Role.Purpose,
                             RoleStatus = rds.Role.Recstatus
                         },
                         App = new
                         {
                             AppId = rds.App.Appid,
                             Name = rds.App.Name,
                             Purpose = rds.App.Purpose,
                             AppStatus = rds.App.Recstatus,
                         },
                         Module = new
                         {
                             ModuleId = rds.Module.Moduleid,
                             Name = rds.Module.Name,
                             Purpose = rds.Module.Purpose,
                             ModuleStatus = rds.Module.Recstatus,
                         },
                         Feature = new
                         {
                             FeatureId = rds.Feature.Featureid,
                             Name = rds.Feature.Name,
                             Purpose = rds.Feature.Purpose,
                             rds.Feature.Otp,
                             rds.Feature.Mfa,
                             FeatureStatus = rds.Feature.Recstatus
                         }
                     }).FirstOrDefault();

            return roleDetailInfo;
        }

        public dynamic DropDownRoleAppList()
        {
            var roles = _roleRepo.AsQueryable().Where(r => r.Recstatus == HelperActionConst.Authorized)
                                         .Select(s => new
                                         {
                                             RoleId=s.Roleid,
                                             s.Rolename,
                                         });
            var apps = _appConfigRepo.AsQueryable().Where(a => a.Recstatus == HelperActionConst.Authorized)
                                         .Select(s => new
                                         {
                                             AppId=s.Appid,
                                             s.Name
                                         });
            var obj= new
            {
                Roles = roles,
                Apps=apps
            };
            return obj;
        }

        public dynamic AppModuleList(string appId)
        {
            var moduleList = _moduleConfigRepo.AsQueryable().Where(m => m.Appid == appId)
                            .Select(s => new
                            {
                                ModuleId = s.Moduleid,
                                s.Name
                            });
            if (moduleList.Count() == 0)
            {
                return null;
            }
            return moduleList;
        }

        public dynamic ModuleFeatureList(string moduleId)
        {
            var moduleFeatureList = _featureConfigRepo.AsQueryable().Where(m => m.Moduleid == moduleId
                                      && m.Recstatus == HelperActionConst.Authorized)
                                    .Select(s => new
                                    {
                                        FeatureId=s.Featureid,
                                        s.Name
                                    });
            if (moduleFeatureList.Count() == 0)
            {
                return null;
            }
            return moduleFeatureList;
        }
    }

}
