using Dotnet_Core_Scaffolding_Oracle.Models;
using Microsoft.AspNetCore.Http;
using Models;
using Newtonsoft.Json;
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
        private IAuthqueRepo _authqueRepo;
        private IFeatureConfigRepo _featureConfigRepo;
        private IRoleDetailRepo _roleDetailRepo;
        public RoleService(IRoleRepo roleRepo,
                            IAuthqueRepo authqueRepo,
                            IFeatureConfigRepo featureConfigRepo,
                            IRoleDetailRepo roleDetailRepo)
        {
            _roleRepo = roleRepo;
            _authqueRepo = authqueRepo;
            _featureConfigRepo = featureConfigRepo;
            _roleDetailRepo = roleDetailRepo;
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
                    AppId = oldData.AppId,
                    Roleid = oldData.Roleid,
                    Rolename = roleInfo.Rolename,
                    Purpose = roleInfo.Purpose,
                    Recstatus = HelperActionConst.Pending
                };

                UmsAuthque umsAuthque= _authqueRepo.AsQueryable().FirstOrDefault(x => x.Appid == oldData.AppId);
                //UmsRoledtl umsRoledtl= _roleDetailRepo.AsQueryable().FirstOrDefault(x => x.Roleid == oldData.Roleid);
          
                AuthQueDataModel authQueData = new AuthQueDataModel()
                {
                    ActionType = HelperActionConst.Update,
                    TableName = tableName,
                    PKId = oldData.Roleid.ToString(),
                    FeatureId = umsAuthque.Featureid,
                    UserId = umsAuthque.Userid,
                    UrlLink = umsAuthque.Urllink,
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
            catch
            {
                return false;
            }

        }
        public long GetDbNextSequence()
        {
            long maxPK = _authqueRepo.AsQueryable().Max(x => x.Authqueid);
            return ++maxPK;
        }
    }
}
