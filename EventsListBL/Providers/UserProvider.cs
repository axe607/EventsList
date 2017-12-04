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
