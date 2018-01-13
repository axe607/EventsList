using EventsListBL.Providers.Interfaces;
using EventsListBL.Services;
using EventsListBL.Services.Interfaces;
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
        private readonly IEncryptService _encryptService;

        public AccountController(IUserOperation userOperation, IUserProvider userProvider, IEncryptService encryptService)
        {
            _userOperation = userOperation;
            _userProvider = userProvider;
            _encryptService = encryptService;
        }

        [Authorize]
        public ActionResult Index()
        {
            return View(_userProvider.GetUserByName(HttpContext.User.Identity.Name));
        }

        [HttpGet]
        public ActionResult EditAccount()
        {
            return View("EditUserInfo", _userProvider.GetUserByName(HttpContext.User.Identity.Name));
        }

        [HttpPost]
        public ActionResult EditAccount(User user)
        {
            if (ModelState.IsValid && IsUserNameFreeForUserId(null, user.UserName))
            {
                _userOperation.EditUserInfo(
                    ((UserPrincipal)HttpContext.User).UserId,
                    user.UserName,
                    string.IsNullOrEmpty(user.Password) ? null : _encryptService.GetEncryptedPassword(user.Password),
                    user.Email
                );
                if (user.UserName != ((UserPrincipal)HttpContext.User).UserName)
                {
                    HttpContext.Response.Cookies.Clear();
                    LoginService loginService = new LoginService(_userProvider, _encryptService);
                    loginService.Logout();
                    return RedirectToAction("Login", "Login");
                }
                return RedirectToAction("Index");
            }
            return View("EditUserInfo", user);
        }

        [Authorize]
        [HttpGet]
        public ActionResult EditOrganizerInfo()
        {
            return View(_userProvider.GetUserByName(HttpContext.User.Identity.Name));
        }

        [Authorize]
        [HttpPost]
        public ActionResult EditOrganizerInfo(User user)
        {
            if (!string.IsNullOrEmpty(user.OrganizerName))
            {
                _userOperation.EditOrganizerInfo(
                    ((UserPrincipal)HttpContext.User).UserId,
                    user.OrganizerName
                    );
                return RedirectToAction("Index");
            }
            return View("EditOrganizerInfo", user);
        }

        [Ajax]
        [Authorize]
        public PartialViewResult GetOrganizerEmails()
        {
            return PartialView(_userProvider.GetUserByName(HttpContext.User.Identity.Name).OrganizerEmails);
        }

        [Ajax]
        [Authorize]
        public PartialViewResult GetOrganizerPhones()
        {
            return PartialView(_userProvider.GetUserByName(HttpContext.User.Identity.Name).OrganizerPhones);
        }

        [Ajax]
        [Authorize]
        public void AddOrganizerEmail(string email)
        {
            if (!string.IsNullOrEmpty(email))
            {
                _userOperation.AddEmail(
                    ((UserPrincipal) HttpContext.User).UserId,
                    email
                );
            }
        }

        [Ajax]
        [Authorize]
        public void AddOrganizerPhone(string phone)
        {
            if (!string.IsNullOrEmpty(phone))
            {
                _userOperation.AddPhone(
                    ((UserPrincipal) HttpContext.User).UserId,
                    phone
                );
            }
        }

        [Ajax]
        [Authorize]
        public void DeleteOrganizerEmail(int emailId)
        {
            _userOperation.DeleteEmailByUserIdAndEmailId(
                ((UserPrincipal)HttpContext.User).UserId,
                emailId
             );
        }

        [Ajax]
        [Authorize]
        public void DeleteOrganizerPhone(int phoneId)
        {
            _userOperation.DeletePhoneByUserIdAndPhoneId(
                ((UserPrincipal)HttpContext.User).UserId,
                phoneId
            );
        }

        [Ajax]
        public bool IsUserNameFreeForUserId(int? userId, string nameToCheck)
        {
            if (string.IsNullOrEmpty(nameToCheck) || string.IsNullOrWhiteSpace(nameToCheck))
            {
                return false;
            }
            if (userId == null)
            {
                userId = ((UserPrincipal)HttpContext.User).UserId;
            }
            return _userProvider.IsUserNameFreeForUserId((int)userId, nameToCheck);
        }

        [Ajax]
        public bool IsUserNameFree(string nameToCheck)
        {
            if (string.IsNullOrEmpty(nameToCheck) || string.IsNullOrWhiteSpace(nameToCheck))
            {
                return false;
            }
            return _userProvider.IsUserNameFree(nameToCheck);
        }

        [Ajax]
        [Admin]
        public bool IsRoleNameFree(int? roleId, string nameToCheck)
        {
            return _userProvider.IsRoleNameFree(roleId, nameToCheck);
        }

        [Admin]
        public ActionResult UsersList()
        {
            return View(_userProvider.GetUsers());
        }

        [Ajax]
        [Admin]
        public PartialViewResult GetUserRoles(string userName)
        {
            return PartialView(_userProvider.GetUserByName(userName).Roles);
        }

        [Ajax]
        [Admin]
        public void DeleteUserRole(string userName, int roleId)
        {
            _userOperation.DeleteUserRole(userName, roleId);
        }

        [Ajax]
        [Admin]
        public void AddUserRole(string userName, int roleId)
        {
            _userOperation.AddRoleToUser(userName, roleId);
        }

        [Admin]
        [HttpGet]
        public ActionResult AddUser()
        {
            return View(new User());
        }

        [Admin]
        [HttpPost]
        public ActionResult AddUser(User user)
        {
            if (ModelState.IsValid && IsUserNameFree(user.UserName))
            {
                if (!string.IsNullOrEmpty(user.Password))
                {
                    _userOperation.AddUser(
                        user.UserName,
                        _encryptService.GetEncryptedPassword(user.Password),
                        user.Email);
                    return RedirectToAction("UsersList");
                }
                ViewBag.PasswordErrorText = "Password is empty!";
            }
            return View(user);
        }

        [HttpGet]
        [Admin]
        public ActionResult EditUser(string userName)
        {
            return View("EditUserInfo", _userProvider.GetUserByName(userName));
        }

        [HttpPost]
        [Admin]
        public ActionResult EditUser(User user)
        {
            if (ModelState.IsValid && IsUserNameFreeForUserId(user.Id, user.UserName))
            {
                _userOperation.EditUserInfo(
                    user.Id,
                    user.UserName,
                    string.IsNullOrEmpty(user.Password) ? null : _encryptService.GetEncryptedPassword(user.Password),
                    user.Email
                );
                return RedirectToAction("UsersList");
            }
            return View("EditUserInfo", user);
        }

        [Ajax]
        [Admin]
        public void DeleteUser(int userId)
        {
            _userOperation.DeleteUser(userId);
        }

        [Admin]
        public ActionResult RolesList()
        {
            return View(_userProvider.GetRoles());
        }

        [Ajax]
        [Admin]
        public void DeleteRole(int roleId)
        {
            _userOperation.DeleteRole(roleId);
        }

        [Admin]
        [HttpGet]
        public ActionResult AddRole()
        {
            return View(new Role());
        }

        [Admin]
        [HttpPost]
        public ActionResult AddRole(Role role)
        {
            if (ModelState.IsValid && IsRoleNameFree(null, role.RoleName))
            {
                _userOperation.AddRole(
                    role.RoleName);
                return RedirectToAction("RolesList");
            }
            return View(role);
        }

        [Admin]
        [HttpGet]
        public ActionResult EditRole(int roleId)
        {
            return View(_userProvider.GetRolesById(roleId));
        }

        [Admin]
        [HttpPost]
        public ActionResult EditRole(Role role)
        {
            if (ModelState.IsValid && IsRoleNameFree(role.Id, role.RoleName))
            {
                _userOperation.EditRole(
                    role.Id,
                    role.RoleName
                    );
                return RedirectToAction("RolesList");
            }
            return View(role);
        }
    }
}