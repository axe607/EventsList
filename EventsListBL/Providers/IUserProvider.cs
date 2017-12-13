using EventsListCommon.Models;

namespace EventsListBL.Providers
{
    public interface IUserProvider
    {
        bool IsValidUser(string userName, string password);
        bool IsNameFree(int userId, string name);
        User GetUserByName(string userName);
    }
}
