using System;
using System.Collections.Generic;
using System.Text;
using Models.DB;
namespace Repositories
{
    public interface IUserInfoRepo : IRepositoryBase<UMS_USERINFO> { }
    public class UserInfoRepo : RepositoryBase<UMS_USERINFO>, IUserInfoRepo { }
    public interface IPassRepo : IRepositoryBase<UMS_PASS> { }
    public class PassRepo : RepositoryBase<UMS_PASS>, IPassRepo { }
}
