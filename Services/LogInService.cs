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
            try
            {
                // Asqueryable data found
                var x = userInfoRepo.AsQueryable().FirstOrDefault(x => x.USERID == UserId);
                // failed tolist



                var User = userInfoRepo.AsQueryable().Where(x => x.USERID == UserId).FirstOrDefault();
                var Pass = passRepo.AsQueryable().Where(x => x.USERID == UserId).FirstOrDefault();
                if (Pass.USERPASS == Password)
                {
                    return true;
                }
                return false;
            }catch (Exception ex)
            {
                return false;
            }
        }
    }
}
