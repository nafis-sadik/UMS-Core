using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstraction;
using Repositories;
using System.Linq;

namespace Services
{
    public class LogInService:ILogInService
    {
        private UserInfoRepo userInfoRepo;
        private PassRepo passRepo;
        public LogInService(UserInfoRepo _userInfoRepo, PassRepo _passRepo)
        {
            userInfoRepo = _userInfoRepo;
            passRepo = _passRepo;
        }
        public bool AuthenticateUser(string UserId, string Password)
        {
            var User = userInfoRepo.AsQueryable().Where(x => x.USERID == UserId).FirstOrDefault() ?? new Models.DB.UMS_USERINFO { USERID = "" };
            var Pass = passRepo.AsQueryable().Where(x => x.USERID == UserId).FirstOrDefault();
            if(Pass.USERPASS == Password)
            {
                return true;
            }
            return false;
        }
    }
}
