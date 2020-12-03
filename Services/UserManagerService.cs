using DevOne.Security.Cryptography.BCrypt;
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
        private IUserInfoRepo _userInfoRepo;
        private IPassRepo _passRepo;
        public UserManagerService(IUserInfoRepo userInfoRepo, IPassRepo passRepo)
        {
            _userInfoRepo = userInfoRepo;
            _passRepo = passRepo;
        }

        public bool? AddNewUser(UserInfo userInfo)
        {
            try {
                UmsUserinfo entity = _userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == userInfo.UserId);
                if (entity == null)
                {
                    entity = CastToEntity(userInfo);
                    _userInfoRepo.Add(entity);
                    _userInfoRepo.Save();
                    // throw new ArgumentException("Test Exception", "Successful Invocation");
                    _userInfoRepo.Commit();
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception ex) {
                _userInfoRepo.Rollback();
                return false;
            }
        }

        public UserInfo GetUser(string UserId)
        {
            UmsUserinfo entity = _userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == UserId);
            if (entity != null)
                return CastToModel(entity);
            else
                return null;
        }
        public bool UpdateUser(UserInfo userInfo)
        {
            try
            {
                UmsUserinfo entity = _userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == userInfo.UserId);
                entity.Name = userInfo.Name;
                //entity.Userid = userInfo.UserId;
                //entity.Categoryid = userInfo.CategoryId;
                //entity.Catidval = userInfo.Catidval;
                entity.Cellno = userInfo.Cellno;
                entity.Dob = string.IsNullOrEmpty(userInfo.Dob) ? "" : userInfo.Dob;
                entity.Email = userInfo.Email;
                //entity.Ipaddress = userInfo.Ipaddress;
                //entity.Macaddress = userInfo.Macaddress;
                //entity.Mfa = userInfo.Mfa;
                //entity.Picture = userInfo.Picture;
                //entity.Recstatus = userInfo.Recstatus;
                //entity.Signature = userInfo.Signature;
                //entity.Thumb = userInfo.Thumb;
                _userInfoRepo.Update(entity);
                _userInfoRepo.Save();
                _userInfoRepo.Commit();             
                return true;
            }
            catch (Exception ex)
            {
                _userInfoRepo.Rollback();
                return false;
            }
        }
        private static UmsUserinfo CastToEntity(UserInfo userInfo)
        {
            return new UmsUserinfo
            {
                Name = userInfo.Name,
                Userid = userInfo.UserId,
                Categoryid = 2,
                Catidval = "s",
                Cellno = userInfo.Cellno,
                Dob = string.IsNullOrEmpty(userInfo.Dob) ? "" : userInfo.Dob,
                Email = userInfo.Email,
                Ipaddress = userInfo.Ipaddress,
                Macaddress = userInfo.Macaddress,
                Mfa = "b",
                Picture = Encoding.ASCII.GetBytes("bb"),
                Recstatus = "b",
                Signature = Encoding.ASCII.GetBytes("cc"),
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

        public List<UserInfo> GetAllUsers(PagingParam pagingParam)
        {
            List<UmsUserinfo> users = _userInfoRepo.AsQueryable().OrderByDescending(x => x.Userid).Skip(pagingParam.Skip).Take(pagingParam.PageSize).ToList();
            List<UserInfo> response = new List<UserInfo>();
            foreach(UmsUserinfo user in users)
            {
                response.Add(CastToModel(user));
            }
            return response;
        }

        public bool ChangePassword(string UserId, string OldPass, string NewPass)
        {
            try
            {
                UmsPass userPass = _passRepo.AsQueryable().FirstOrDefault(x => x.Userid == UserId);
                if (BCryptHelper.CheckPassword(OldPass, userPass.Userpass))
                {
                    var Salt = BCryptHelper.GenerateSalt(12);
                    var Passss = BCryptHelper.HashPassword(NewPass, Salt);
                    userPass.Userpass = Passss;
                    _passRepo.Update(userPass);
                    _passRepo.Save();
                    _passRepo.Commit();
                    return true;
                }
                else
                {
                    _userInfoRepo.Rollback();
                    return false;
                }
            }
            catch (Exception ex)
            {
                //_userInfoRepo.Rollback();
                var msg = ex.Message;
                return false;
            }
        }
    }
}
