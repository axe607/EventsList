using EventsListBL.Providers.Interfaces;
using EventsListBL.Services.Interfaces;
using EventsListCommon.Enums;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Security;

namespace EventsListBL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserProvider _userProvider;
        private readonly IEncryptService _encryptService;

        public LoginService(IUserProvider userProvider, IEncryptService encryptService)
        {
            _userProvider = userProvider;
            _encryptService = encryptService;
        }
        public LoginResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return LoginResult.EmptyCredentials;
            }

            if (_userProvider.IsValidUser(userName, _encryptService.GetEncryptedPassword(password)))
            {
                var user = _userProvider.GetUserByName(userName);
                var userData = JsonConvert.SerializeObject(user);
                var ticket = new FormsAuthenticationTicket(2, user.UserName, DateTime.Now, DateTime.Now.AddHours(1), false, userData);
                var encTicket = FormsAuthentication.Encrypt(ticket);
                var authCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                HttpContext.Current.Response.Cookies.Add(authCookie);
                return LoginResult.NoError;
            }

            return LoginResult.InvalidCredentials;
        }

        public void Logout()
        {
            FormsAuthentication.SignOut();
        }
    }
}
