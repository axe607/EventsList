using EventsListBL.Providers.Interfaces;
using EventsListWebApp.Models;
using log4net;
using System;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class JsonController : Controller
    {
        private readonly ICategoryProvider _categoryProvider;
        private readonly IAddressProvider _addressProvider;
        private readonly IUserProvider _userProvider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonController));

        public JsonController(ICategoryProvider categoryProvider, IAddressProvider addressProvider, IUserProvider userProvider)
        {
            _categoryProvider = categoryProvider;
            _addressProvider = addressProvider;
            _userProvider = userProvider;
        }

        [Ajax]
        public JsonResult GetCategories()
        {
            try
            {
                return Json(_categoryProvider.GetCategories(), JsonRequestBehavior.AllowGet);
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
                return Json(_addressProvider.GetAddresses(), JsonRequestBehavior.AllowGet);
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