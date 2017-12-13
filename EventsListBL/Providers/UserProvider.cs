using System.Collections.Generic;
using EventsListCommon.Models;
using EventsListData.Repositories;

namespace EventsListBL.Providers
{
    public class UserProvider : IUserProvider
    {
        private readonly IDataRepository _provider;

        public UserProvider(IDataRepository provider)
        {
            _provider = provider;
        }

        public bool IsNameFree(int userId, string name)
        {
            return _provider.IsNameFree(userId, name);
        }

        public User GetUserByName(string userName)
        {
            return _provider.GetUserByName(userName);
        }

        public bool IsValidUser(string userName, string password)
        {
            return _provider.IsValidUser(userName, password);
        }
    }
}
