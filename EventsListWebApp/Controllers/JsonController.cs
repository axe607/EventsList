using System;
using System.Web;
using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;
using EventsListWebApp.Models;

namespace EventsListWebApp.Controllers
{
    public class JsonController : Controller
    {
        private readonly IBusinessProvider _provider;
        private readonly IUserProvider _userProvider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonController));

        public JsonController(IBusinessProvider providerInput, IUserProvider userProvider)
        {
            _provider = providerInput;
            _userProvider = userProvider;
        }

        [Ajax]
        public JsonResult GetCategories()
        {
            try
            {
                return Json(_provider.GetCategories(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [Ajax]
        public JsonResult GetAddresses()
        {
            try
            {
                return Json(_provider.GetAddresses(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

        [Ajax]
        [Admin]
        public JsonResult GetRolesNotInUser(string userName)
        {
            try
            {
                return Json(_userProvider.GetRolesNotInUser(userName), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }

    }
}