using System.Collections.Generic;
using EventsListCommon.Models;

namespace EventsListBL.Providers.Interfaces
{
    public interface IUserProvider
    {
        bool IsValidUser(string userName, string password);
        bool IsUserNameFree(int userId, string name);
        bool IsRoleNameFree(int? roleId, string name);
        User GetUserByName(string userName);
        IReadOnlyList<User> GetUsers();
        IReadOnlyList<Role> GetRolesNotInUser(string userName);
        IReadOnlyList<Role> GetRoles();
        Role GetRolesById(int roleId);
    }
}
