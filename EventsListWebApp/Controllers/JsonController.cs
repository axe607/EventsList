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
        private const string ORGANIZERS_KEY = "organizers_key";

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
        public JsonResult GetOrganizers()
        {
            try
            {
                if (HttpRuntime.Cache.Get(ORGANIZERS_KEY) == null)
                {
                    HttpRuntime.Cache.Insert(ORGANIZERS_KEY, _provider.GetOrganizers());
                }
                return Json(HttpRuntime.Cache.Get(ORGANIZERS_KEY), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}