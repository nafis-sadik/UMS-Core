using System;
using System.Collections.Generic;
using System.Text;
using Models.DB;
using Dotnet_Core_Scaffolding_Oracle.Models;

namespace Repositories
{
    public interface IUserInfoRepo : IRepositoryBase<UmsUserinfo> { }
    public class UserInfoRepo : RepositoryBase<UmsUserinfo>, IUserInfoRepo
    {
        public UserInfoRepo():base() { }
    }

    public interface IPassRepo : IRepositoryBase<UmsPass> { }
    public class PassRepo : RepositoryBase<UmsPass>, IPassRepo
    {
        public PassRepo() : base() { }
    }
    public interface IRoleRepo : IRepositoryBase<UmsRole> { }
    public class RoleRepo : RepositoryBase<UmsRole>, IRoleRepo
    {
        public RoleRepo() : base() { }
    }

    public interface IRoleDetailRepo : IRepositoryBase<UmsRoledtl> { }
    public class RoleDetailRepo : RepositoryBase<UmsRoledtl>, IRoleDetailRepo
    {
        public RoleDetailRepo() : base() { }
    }

    public interface IAuthqueRepo : IRepositoryBase<UmsAuthque> { }
    public class AuthqueRepo : RepositoryBase<UmsAuthque>, IAuthqueRepo
    {
        public AuthqueRepo() : base() { }
    }

    public interface IFeatureConfigRepo : IRepositoryBase<UmsFeatureconfig> { }
    public class FeatureConfigRepo : RepositoryBase<UmsFeatureconfig>, IFeatureConfigRepo
    {
        public FeatureConfigRepo() : base() { }
    }
}
