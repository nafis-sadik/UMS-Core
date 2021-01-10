using Repositories;
using Services.Abstraction;
using System;
using Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UMSApi.Helpers;
using Dotnet_Core_Scaffolding_Oracle.Models;
using Newtonsoft.Json;

namespace Services
{
    public class RoleFeatureService : IRoleFeatureService
    {
        private IRoleDetailRepo _roleDetailRepo;
        private IRoleRepo _roleRepo;
        private IAppConfigRepo _appConfigRepo;
        private IModuleConfigRepo _moduleConfigRepo;
        private IFeatureConfigRepo _featureConfigRepo;
        private IRoleService _roleService;
        public RoleFeatureService(IRoleDetailRepo roleDetailRepo,
                                  IRoleRepo roleRepo,
                                  IAppConfigRepo appConfigRepo,
                                  IModuleConfigRepo moduleConfigRepo,
                                  IFeatureConfigRepo featureConfigRepo,
                                  IRoleService roleService)
        {
            _roleDetailRepo = roleDetailRepo;
            _roleRepo = roleRepo;
            _appConfigRepo = appConfigRepo;
            _moduleConfigRepo = moduleConfigRepo;
            _featureConfigRepo = featureConfigRepo;
            _roleService = roleService;
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

        public dynamic AddRoleDetailInfo(RoleDetailsInfo roleDetailsInfo)
        {
            try
            {
                UmsRoledtl umsRoledtl = new UmsRoledtl();
                umsRoledtl.Roledtlid = GetDbNextSequence();
                umsRoledtl.Roleid = roleDetailsInfo.RoleId;
                umsRoledtl.Appid = roleDetailsInfo.AppId.ToString();
                umsRoledtl.Moduleid = roleDetailsInfo.ModuleId.ToString();
                umsRoledtl.Featureid = roleDetailsInfo.FeatureId.ToString();
                umsRoledtl.Createyn = roleDetailsInfo.CancelYN.ToString();
                umsRoledtl.Edityn = roleDetailsInfo.EditYN.ToString();
                umsRoledtl.Viewdetailyn = roleDetailsInfo.ViewDetailYN.ToString();
                umsRoledtl.Deleteyn = roleDetailsInfo.DeleteYN;
                umsRoledtl.Authyn = roleDetailsInfo.AuthYN.ToString();
                umsRoledtl.Cancelyn = roleDetailsInfo.CancelYN.ToString();
                umsRoledtl.Recstatus = HelperActionConst.Pending;
                _roleDetailRepo.Add(umsRoledtl);
                _roleDetailRepo.Save();
                _roleDetailRepo.Commit();

                List<Menu> menuList = _roleService.GetUserInfo(roleDetailsInfo.UserId);
                var menu = (menuList.Where(a => a.MenuLocation != null).ToList()).FirstOrDefault(o => o.MenuLocation.ToLower() == HelperActionConst.RoleFeatureControllerName);

                string tableName = umsRoledtl.GetType().Name;
                AuthQueDataModel authQueData = new AuthQueDataModel()
                {
                    ActionType = HelperActionConst.Update,
                    TableName = tableName,
                    PKId = umsRoledtl.Roledtlid.ToString(),
                    FeatureId = menu.FeatureId,
                    UserId = roleDetailsInfo.UserId,
                    UrlLink = "Role/Details",
                    NewRecord = JsonConvert.SerializeObject(umsRoledtl),
                    OldRecord = null,
                };
                bool IsPerformAuthQueSetup = _roleService.SetAuthQueData(authQueData);

                if (!IsPerformAuthQueSetup)
                {
                    _roleDetailRepo.Rollback();
                }
                return true;

            }
            catch (Exception ex)
            {
                _roleDetailRepo.Rollback();
                return false;
            }
        }
        public long GetDbNextSequence()
        {
            long maxPK = _roleDetailRepo.AsQueryable().Max(x => x.Roledtlid);
            return ++maxPK;
        }
    }

}
