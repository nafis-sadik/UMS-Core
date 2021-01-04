using Dotnet_Core_Scaffolding_Oracle.Models;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
using Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using UMSApi.Helpers;

namespace Services
{
    public class RoleService : IRoleService
    {
        private IUserInfoRepo _userInfoRepo;
        private IRoleRepo _roleRepo;
        private IAuthqueRepo _authqueRepo;
        private IFeatureConfigRepo _featureConfigRepo;
        private IRoleDetailRepo _roleDetailRepo;
        private IPassRepo _passRepo;

        public RoleService(IRoleRepo roleRepo,
                            IAuthqueRepo authqueRepo,
                            IFeatureConfigRepo featureConfigRepo,
                            IRoleDetailRepo roleDetailRepo,
                            IUserInfoRepo userInfoRepo,
                            IPassRepo passRepo)
        {
            _roleRepo = roleRepo;
            _authqueRepo = authqueRepo;
            _featureConfigRepo = featureConfigRepo;
            _roleDetailRepo = roleDetailRepo;
            _userInfoRepo = userInfoRepo;
            _passRepo = passRepo;
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
            if (roleInfo.Recstatus == "A")
            {
                roleInfo.Recstatus = "Authorized";
            }
            if(roleInfo.Recstatus=="P")
            {
                roleInfo.Recstatus = "Pending";
            }
            return new RoleInfo
            {
                RoleId=roleInfo.Roleid,
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
        public bool UpdateRoleInformation(RoleInfo roleInfo)
        {
            try
            {
                UmsRole oldData = _roleRepo.AsQueryable().FirstOrDefault(x => x.Roleid == roleInfo.RoleId);
                string tableName = oldData.GetType().Name;

                UmsRole newRecord = new UmsRole
                {
                    Roleid = oldData.Roleid,
                    Rolename = roleInfo.Rolename,
                    Purpose = roleInfo.Purpose,
                    Recstatus = HelperActionConst.Pending
                };

                var FeatureId = GetUserInfo(roleInfo.UserId);

                AuthQueDataModel authQueData = new AuthQueDataModel()
                {
                    ActionType = HelperActionConst.Update,
                    TableName = tableName,
                    PKId = oldData.Roleid.ToString(),
                    FeatureId = FeatureId,
                    UserId = roleInfo.UserId,
                    UrlLink = "Role/Details",
                    NewRecord = JsonConvert.SerializeObject(newRecord),
                    OldRecord = JsonConvert.SerializeObject(oldData),
                };

                bool IsPerformAuthQueSetup = SetAuthQueData(authQueData);

                if (!IsPerformAuthQueSetup)
                {
                    _roleRepo.Rollback();
                }

                oldData.Rolename = roleInfo.Rolename;
                oldData.Purpose = roleInfo.Purpose;
                _roleRepo.Update(oldData);
                return true;
            }
            catch (Exception ex)
            {
                _roleRepo.Rollback();
                return false;
            }
        }
        public bool SetAuthQueData(AuthQueDataModel authData)
        {
            try
            {
                UmsFeatureconfig feature = _featureConfigRepo.AsQueryable().Where(f => f.Featureid == authData.FeatureId).FirstOrDefault();

                if (feature == null) 
                    return false;

                UmsAuthque authQue = new UmsAuthque()
                {
                    Authqueid = GetDbNextSequence(),
                    Entrydate = DateTime.Now.ToString(),
                    Featureid = authData.FeatureId,
                    Appid = feature.Appid,
                    Moduleid = feature.Moduleid,
                    Tablename = authData.TableName,
                    Pkvalue = authData.PKId,
                    Maxauthlevel = feature.Authlevel ?? 1,
                    Appauthlevel = 0,
                    Userid = authData.UserId,
                    Action = authData.ActionType,
                    Isauthcancel = HelperActionConst.Authorized,
                    Newrecord = authData.NewRecord,
                    Oldrecord = authData.OldRecord,
                    Oldstatus = null,
                    Urllink = authData.UrlLink + "/" + authData.PKId,
                    Recstatus = HelperActionConst.Pending,
                };

                _authqueRepo.Add(authQue);
                _authqueRepo.Save();
                _authqueRepo.Commit();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }
        public dynamic GetUserInfo(string UserId)
        {
            var authUserData = _userInfoRepo.AsQueryable().Where(e => e.Userid == UserId &&
                                            e.Recstatus == HelperActionConst.Authorized)
                             .Select(s => new
                             {
                                 s.Userid,
                                 s.Name,
                                 s.Dob,
                                 s.Email,
                                 s.Ipaddress,
                                 s.Macaddress,
                                 s.Mfa,
                                 s.Recstatus,
                                 s.Picture,
                                 UmsCategory = new
                                 {
                                     s.Category.Categoryid,
                                     s.Category.Categoryname,
                                     s.Category.Recstatus
                                 },
                                 UmsPass=s.UmsPass.Where(p=>p.Recstatus==HelperActionConst.Authorized)
                                          .Select(sp => new
                                          {
                                              sp.Userpass,
                                              sp.Expiredate,
                                              sp.Createdate,
                                              sp.Recstatus
                                          }),
                                 UmsRoleassign = s.UmsRoleassign.Select(ra => new
                                 {
                                     ra.Roleid,
                                     ra.Role.Rolename,
                                     ra.Recstatus,
                                     ra.Role.Purpose,
                                     module=ra.Role.UmsRoledtl.Where(u=>
                                          u.Appid==HelperActionConst.AppId)
                                          .Select(m=> new
                                          {
                                              m.Moduleid,
                                              m.Module.Name,
                                              Appname=m.Module.Name,
                                              m.Appid,
                                              m.Module.Imagepath,
                                              FeatureId=m.Featureid,
                                              Details=m.Viewdetailyn,
                                              Create=m.Edityn,
                                              Edit = m.Edityn,
                                              Delete =m.Deleteyn,
                                              Auth = m.Authyn,
                                              RoleDetailStatus =m.Recstatus,
                                              Menu=m.Feature.UmsMenu.Where(re=>re.Recstatus==HelperActionConst.Authorized)
                                                  .Select(z => new
                                                  {
                                                      FeatureId=z.Menufeatureid,
                                                      FeatureName=z.Menufeature.Name,
                                                      MenuId=z.Menuid,
                                                      MenuLocation=z.Menulocation,
                                                      MenuSequence=z.Menusequence,
                                                      MenuName=z.Menuname,
                                                      ParentId=z.Parentid,
                                                      MenuStatus=z.Recstatus,
                                                      FeatureType=z.Menufeature.Featuretype
                                                  }).ToList()
                                          }).ToList(),
                                 }).ToList(),
                                 UmsUserfavorite=s.UmsUserfavorite.Where(q=>q.Appid==HelperActionConst.AppId && q.Recstatus==HelperActionConst.Authorized)
                                   .Select(v => new
                                   {
                                       UserId=v.Userid,
                                       UrlLink=v.Urllink,
                                       RecStatus=v.Recstatus,
                                       PageTitle=v.Pagetitle,
                                       Id=v.Id,
                                       Controller=v.Controller,
                                       Action=v.Action,
                                       AppId=v.Appid
                                   }),
                             }).FirstOrDefault();
            List<Menu> menulist = new List<Menu>();
            List<RoleDetailAuth> RoleWisePermission = new List<RoleDetailAuth>();

            foreach (var p in authUserData.UmsRoleassign)
            {
                foreach (var q in p.module)
                {
                    foreach (var r in q.Menu)
                    {
                        long t = r.MenuId;
                        if (!(menulist.Any(y => y.MenuId == t)))
                        {
                            try
                            {
                                Menu ob = new Menu
                                {
                                    MenuId=r.MenuId,
                                    MenuName=r.MenuName,
                                    MenuLocation=r.MenuLocation,
                                    MenuStatus=r.MenuStatus,
                                    FeatureId=r.FeatureId,
                                    FeatureName=r.FeatureName,
                                    MenuSequence= (int)r.MenuSequence,
                                    FeatureType= (int)r.FeatureType,
                                    ParentId=r.ParentId
                                };
                                menulist.Add(ob);
                            }
                            catch (Exception ex)
                            {
                               
                            }
                        }
                    }
                    RoleDetailAuth mnp = new RoleDetailAuth()
                    {
                        FeatureId=q.FeatureId,
                        Create=q.Create,
                        Edit=q.Edit,
                        Delete=q.Delete,
                        Details=q.Details,
                        Auth=q.Auth,
                        ModuleId=q.Moduleid,
                        ModuleName=q.Name,
                        ImagePath=q.Imagepath,
                    };
                    RoleWisePermission.Add(mnp);
                }
            }
            var aaa = menulist;
            var ccc = (aaa.Where(a => a.MenuLocation != null).ToList()).FirstOrDefault(o => o.MenuLocation.ToLower() == HelperActionConst.RoleControllerName);
            var featureId = ccc.FeatureId;
            
            var bbb = RoleWisePermission;
            return featureId;
        }
        public long GetDbNextSequence()
        {
            long maxPK = _roleRepo.AsQueryable().Max(x => x.Roleid);
            return ++maxPK;
        }

        public bool AddRoleInformation(RoleInfo roleInfo)
        {
            try
            {
                UmsRole umsRole = new UmsRole();
                umsRole.Rolename = roleInfo.Rolename;
                umsRole.Purpose = roleInfo.Purpose;
                umsRole.Recstatus = HelperActionConst.Pending;
                umsRole.Roleid = GetDbNextSequence();
                _roleRepo.Add(umsRole);
                _roleRepo.Save();
                _userInfoRepo.Commit();

                var FeatureId = GetUserInfo(roleInfo.UserId);
                string tableName = umsRole.GetType().Name;
                AuthQueDataModel authQueData = new AuthQueDataModel()
                {
                    ActionType = HelperActionConst.Update,
                    TableName = tableName,
                    PKId = umsRole.Roleid.ToString(),
                    FeatureId = FeatureId,
                    UserId = roleInfo.UserId,
                    UrlLink = "Role/Details",
                    NewRecord = JsonConvert.SerializeObject(umsRole),
                    OldRecord = null,
                };
                bool IsPerformAuthQueSetup = SetAuthQueData(authQueData);

                if (!IsPerformAuthQueSetup)
                {
                    _roleRepo.Rollback();
                }
                return true;
            }
            catch (Exception ex)
            {
                _roleRepo.Rollback();
                return false;
            }
        }
    }
}
