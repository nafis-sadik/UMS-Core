using Dotnet_Core_Scaffolding_Oracle.Models;
using Models;
using Models.DB;
using Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Services
{
    public class UserManagerService : IUserManagerService
    {
        private IUserInfoRepo userInfoRepo;

        public UserManagerService()
        {
            userInfoRepo = new UserInfoRepo();
        }

        public bool AddNewUser(UserInfo userInfo)
        {
            try {
                var entity = CastToEntity(userInfo);
                userInfoRepo.Add(entity);
                userInfoRepo.Save();
                userInfoRepo.Commit();
                return true;
            } catch (Exception ex) {
                return false;
            }
        }

        public UserInfo GetUser(string UserId)
        {
            UmsUserinfo entity = userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == UserId);
            if (entity != null)
                return CastToModel(entity);
            else
                return null;
        }

        private static UmsUserinfo CastToEntity(UserInfo userInfo)
        {
            return new UmsUserinfo
            {
                Name = userInfo.Name,
                Userid = userInfo.UserId,
                Categoryid = userInfo.CategoryId,
                Catidval = userInfo.Catidval,
                Cellno = userInfo.Cellno,
                Dob = string.IsNullOrEmpty(userInfo.Dob) ? "" : userInfo.Dob,
                Email = userInfo.Email,
                Ipaddress = userInfo.Ipaddress,
                Macaddress = userInfo.Macaddress,
                Mfa = userInfo.Mfa,
                Picture = userInfo.Picture,
                Recstatus = userInfo.Recstatus,
                Signature = userInfo.Signature,
                Thumb = userInfo.Thumb
            };
        }

        private static UserInfo CastToModel(UmsUserinfo userInfo)
        {
            return new UserInfo
            {
                Name = userInfo.Name,
                UserId = userInfo.Userid,
                CategoryId = userInfo.Categoryid,
                Catidval = userInfo.Catidval,
                Cellno = userInfo.Cellno,
                Dob = userInfo.Dob,
                Email = userInfo.Email,
                Ipaddress = userInfo.Ipaddress,
                Macaddress = userInfo.Macaddress,
                Mfa = userInfo.Mfa,
                Picture = userInfo.Picture,
                Recstatus = userInfo.Recstatus,
                Signature = userInfo.Signature,
                Thumb = userInfo.Thumb
            };
        }
    }
}
