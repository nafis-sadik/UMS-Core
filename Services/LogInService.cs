using System;
using Services.Abstraction;
using Repositories;
using System.Linq;
using DevOne.Security.Cryptography.BCrypt;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Text;
using Microsoft.AspNetCore.Http;

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
        public bool AuthenticateUser(string UserId, string Password, out byte[]? Token, out string? Salt)
        {
            Token = null;
            Salt = null;
            try
            {
                var User = _userInfoRepo.AsQueryable().FirstOrDefault(x => x.Userid == UserId);
                if (User == null)
                    return false;
                var Pass = _passRepo.AsQueryable().FirstOrDefault(x => x.Userid == UserId);
                if (BCryptHelper.CheckPassword(Password, Pass.Userpass))
                {
                    Salt = BCryptHelper.GenerateSalt();
                    byte[] salt = Encoding.Default.GetBytes(Salt);
                    Token = KeyDerivation.Pbkdf2(Password, salt, KeyDerivationPrf.HMACSHA1, 1000, 256);
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
