using EventsListCommon.Models;
using EventsListData.Repositories;
using System.Collections.Generic;
using EventsListBL.Providers.Interfaces;

namespace EventsListBL.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IDataRepository _provider;

        public UserProvider(IDataRepository provider)
        {
            _provider = provider;
        }

        public bool IsUserNameFreeForUserId(int userId, string name)
        {
            return _provider.IsUserNameFreeForUserId(userId, name);
        }

        public bool IsUserNameFree(string name)
        {
            return _provider.IsUserNameFree(name);
        }

        public bool IsRoleNameFree(int? roleId, string name)
        {
            return _provider.IsRoleNameFree(roleId, name);
        }

        public User GetUserByName(string userName)
        {
            return _provider.GetUserByName(userName);
        }

        public IReadOnlyList<User> GetUsers()
        {
            return _provider.GetUsers();
        }

        public IReadOnlyList<Role> GetRolesNotInUser(string userName)
        {
            return _provider.GetRolesNotInUser(userName);
        }

        public IReadOnlyList<Role> GetRoles()
        {
            return _provider.GetRoles();
        }

        public Role GetRolesById(int roleId)
        {
            return _provider.GetRolesById(roleId);
        }

        public bool IsValidUser(string userName, string password)
        {
            return _provider.IsValidUser(userName, password);
        }
    }
}
