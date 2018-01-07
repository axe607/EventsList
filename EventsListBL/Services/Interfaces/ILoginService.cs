using EventsListCommon.Enums;

namespace EventsListBL.Services.Interfaces
{
    public interface ILoginService
    {
        LoginResult Login(string userName, string password);
        void Logout();
    }
}
