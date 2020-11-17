using System;
using System.Collections.Generic;
using System.Text;
using Services.Abstraction;
using Repositories;
using System.Linq;
using Models.DB;

namespace Services
{
    public class LogInService:ILogInService
    {
        private IUserInfoRepo userInfoRepo;
        private IPassRepo passRepo;
        public LogInService()
        {
            userInfoRepo = new UserInfoRepo();
            passRepo = new PassRepo();
        }
        public bool AuthenticateUser(string UserId, string Password)
        {
            // Asqueryable data found
            userInfoRepo.Add(new UMS_USERINFO { USERID = "Nafis Sadik", NAME = "Nafis Sadik" });
            userInfoRepo.Save();
            return true;
            var x = userInfoRepo.AsQueryable().Where(x => x.USERID == UserId);
            // failed tolist
            var c = x.Count();
            var d = x.FirstOrDefault();
            var q = x.ToList();
            foreach(var z in x)
            {
                var qz = z.USERID;
            }



            var User = userInfoRepo.AsQueryable().Where(x => x.USERID == UserId).FirstOrDefault();
            var Pass = passRepo.AsQueryable().Where(x => x.USERID == UserId).FirstOrDefault();
            if(Pass.USERPASS == Password)
            {
                return true;
            }
            return false;
        }
    }
}
