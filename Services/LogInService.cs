using System;
using Services.Abstraction;
using Repositories;
using System.Linq;

namespace Services
{
    public class LogInService:ILogInService
    {
        private IUserInfoRepo _userInfoRepo;
        private IPassRepo _passRepo;
        public LogInService(IUserInfoRepo userInfoRepo, IPassRepo passRepo)
        {
            _userInfoRepo = userInfoRepo;
            _passRepo = passRepo;
        }
        public bool AuthenticateUser(string UserId, string Password)
        {
            try
            {
                var User = _userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == UserId);
                if (User == null)
                    return false;
                var Pass = _passRepo.AsQueryable().Where(x => x.Userid == UserId).FirstOrDefault();
                //CustomHash.ValidatePassword(Password, Pass.Userpass);
                if (Pass.Userpass == Password)
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
