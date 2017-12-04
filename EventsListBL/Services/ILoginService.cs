using EventsListCommon.Enums;

namespace EventsListBL.Services
{
    public interface ILoginService
    {
        LoginResult Login(string userName, string password);
        void Logout();
    }
}
