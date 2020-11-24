using System;
using System.Collections.Generic;
using System.Text;
using Models.DB;
using Dotnet_Core_Scaffolding_Oracle.Models;

namespace Repositories
{
    public interface IUserInfoRepo : IRepositoryBase<UmsUserinfo> { }
    public class UserInfoRepo : RepositoryBase<UmsUserinfo>, IUserInfoRepo { }
    public interface IPassRepo : IRepositoryBase<UmsPass> { }
    public class PassRepo : RepositoryBase<UmsPass>, IPassRepo { }
}
