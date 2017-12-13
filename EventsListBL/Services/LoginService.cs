using EventsListBL.Providers;
using EventsListCommon.Enums;
using System;
using System.Web;
using System.Web.Security;
using Newtonsoft.Json;

namespace EventsListBL.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserProvider _userProvider;

        public LoginService(IUserProvider userProvider)
        {
            _userProvider = userProvider;
        }
        public LoginResult Login(string userName, string password)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                return LoginResult.EmptyCredentials;
            }

            if (_userProvider.IsValidUser(userName, password))
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
