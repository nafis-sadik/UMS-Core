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

        public bool UpdateUser(UserInfo userInfo)
        {
            try
            {
                UmsUserinfo entity = userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == userInfo.UserId);
                entity.Name = userInfo.Name;
                entity.Userid = userInfo.UserId;
                entity.Categoryid = userInfo.CategoryId;
                entity.Catidval = userInfo.Catidval;
                entity.Cellno = userInfo.Cellno;
                entity.Dob = string.IsNullOrEmpty(userInfo.Dob) ? "" : userInfo.Dob;
                entity.Email = userInfo.Email;
                entity.Ipaddress = userInfo.Ipaddress;
                entity.Macaddress = userInfo.Macaddress;
                entity.Mfa = userInfo.Mfa;
                entity.Picture = userInfo.Picture;
                entity.Recstatus = userInfo.Recstatus;
                entity.Signature = userInfo.Signature;
                entity.Thumb = userInfo.Thumb;
                userInfoRepo.Update(entity);
                userInfoRepo.Commit();
                userInfoRepo.Save();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
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
