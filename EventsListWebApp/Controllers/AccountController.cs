using EventsListBL.Providers;
using EventsListBL.Services;
using EventsListCommon.Models;
using EventsListWebApp.Models;
using System;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly IUserOperation _userOperation;
        private readonly IUserProvider _userProvider;

        public AccountController(IUserOperation userOperation, IUserProvider userProvider)
        {
            _userOperation = userOperation;
            _userProvider = userProvider;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_userProvider.GetUserByName(HttpContext.User.Identity.Name));
        }

        public ActionResult EditOrganizerInfo()
        {
            return View(_userProvider.GetUserByName(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        public ActionResult EditUserInfo()
        {
            return View(_userProvider.GetUserByName(HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult EditUserInfo(User user)
        {
            if (ModelState.IsValid)
            {
                if (IsNameFree(user.UserName))
                {
                    _userOperation.EditUserInfo(
                        ((UserPrincipal)HttpContext.User).UserId,
                        user.UserName,
                        user.Email
                    );
                    HttpContext.Response.Cookies.Clear();
                    LoginService loginService = new LoginService(_userProvider);
                    loginService.Logout();
                    return RedirectToAction("Login","Login");
                }
            }
            return View(user);
        }

        [Ajax]
        public bool IsNameFree(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return false;
            }
            return _userProvider.IsNameFree(((UserPrincipal)HttpContext.User).UserId, name);
        }

        [Ajax]
        public void DeleteOrganizerEmail(int emailId)
        {
            throw new NotImplementedException();
        }

        [Ajax]
        public void DeleteOrganizerPhone(int phoneId)
        {
            throw new NotImplementedException();
        }
    }
}