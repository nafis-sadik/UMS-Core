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
            var x = userInfoRepo.AsQueryable().FirstOrDefault(x => x.USERID == UserId);
            return CastToModel(x);
        }

        private static UMS_USERINFO CastToEntity(UserInfo userInfo)
        {
            return new UMS_USERINFO
            {
                NAME = userInfo.Name,
                USERID = userInfo.UserId,
                CATEGORYID = userInfo.CategoryId,
                CATIDVAL = userInfo.Catidval,
                CELLNO = userInfo.Cellno,
                DOB = string.IsNullOrEmpty(userInfo.Dob) ? "" : userInfo.Dob,
                EMAIL = userInfo.Email,
                IPADDRESS = userInfo.Ipaddress,
                MACADDRESS = userInfo.Macaddress,
                MFA = userInfo.Mfa,
                PICTURE = userInfo.Picture,
                RECSTATUS = userInfo.Recstatus,
                SIGNATURE = userInfo.Signature,
                THUMB = userInfo.Thumb
            };
        }

        private static UserInfo CastToModel(UMS_USERINFO userInfo)
        {
            return new UserInfo
            {
                Name = userInfo.NAME,
                UserId = userInfo.USERID,
                CategoryId = userInfo.CATEGORYID,
                Catidval = userInfo.CATIDVAL,
                Cellno = userInfo.CELLNO,
                Dob = userInfo.DOB,
                Email = userInfo.EMAIL,
                Ipaddress = userInfo.IPADDRESS,
                Macaddress = userInfo.MACADDRESS,
                Mfa = userInfo.MFA,
                Picture = userInfo.PICTURE,
                Recstatus = userInfo.RECSTATUS,
                Signature = userInfo.SIGNATURE,
                Thumb = userInfo.THUMB
            };
        }
    }
}
