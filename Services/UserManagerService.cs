using Models;
using Models.DB;
using Repositories;
using Services.Abstraction;
using System;
using System.Collections.Generic;
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

        private UMS_USERINFO CastToEntity(UserInfo userInfo)
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
    }
}
