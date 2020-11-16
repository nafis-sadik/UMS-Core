using System;
using System.Collections.Generic;
using System.Text;
using Models.DB;
namespace Repositories
{
    public interface IUserInfo : IRepositoryBase<UMS_USERINFO> { }
    public class UserInfoRepo : RepositoryBase<UMS_USERINFO>, IUserInfo { }
    public interface IPassRepo : IRepositoryBase<UMS_PASS> { }
    public class PassRepo : RepositoryBase<UMS_PASS>, IPassRepo { }
}
