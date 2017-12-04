using EventsListCommon.Models;

namespace EventsListBL.Providers
{
    public interface IUserProvider
    {
        bool IsValidUser(string userName, string password);
        User GetUserByName(string userName);
    }
}
