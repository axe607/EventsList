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
        private const string CATEGORIES_KEY = "categories_key";
        private const string ADDRESSES_KEY = "addresses_key";

        private readonly IBusinessProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(JsonController));

        public JsonController(IBusinessProvider providerInput)
        {
            _provider = providerInput;
        }

        [Ajax]
        public JsonResult GetCategories()
        {
            try
            {
                if (HttpRuntime.Cache.Get(CATEGORIES_KEY) == null)
                {

                    HttpRuntime.Cache.Insert(CATEGORIES_KEY, _provider.GetCategories());
                }
                return Json(HttpRuntime.Cache.Get(CATEGORIES_KEY), JsonRequestBehavior.AllowGet);
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
                if (HttpRuntime.Cache.Get(ADDRESSES_KEY) == null)
                {
                    HttpRuntime.Cache.Insert(ADDRESSES_KEY, _provider.GetAddresses());
                }
                return Json(HttpRuntime.Cache.Get(ADDRESSES_KEY), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}