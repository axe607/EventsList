using EventsListBL.Services.Interfaces;
using EventsListCommon.Enums;
using EventsListWebApp.Models;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class LoginController : Controller
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LoginController));

        private readonly ILoginService _loginService;

        public LoginController(ILoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string userName, string password)
        {
            Log.Debug($"Try log in username[{userName}]; password[{password}]");

            var model = new LoginViewModel();

            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(password))
            {
                model.Message = "Credentials should not be empty";
                Log.Info(model.Message);
            }
            else
            {
                var result = _loginService.Login(userName, password);
                if (result == LoginResult.NoError)
                {
                    Log.Info(userName + " No error");
                    return RedirectToAction("Index", "Home");
                }

                if (result == LoginResult.EmptyCredentials)
                {
                    Log.Info(userName+ " Empty Credentials");
                    model.Message = "Check user name and password";
                }
                if (result == LoginResult.InvalidCredentials)
                {
                    Log.Info(userName + " Invalid Credentials");
                    model.Message = "The user is not valid";
                }
            }
            return View(model);
        }

        public ActionResult Logout()
        {
            _loginService.Logout();
            return RedirectToAction("Index", "Home");
        }
    }
}