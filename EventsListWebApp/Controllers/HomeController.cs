using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using log4net;

namespace EventsListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private static readonly ILog log = LogManager.GetLogger("HomeController");

        // GET: Home
        public ActionResult Index()
        {
            log.Debug("Test Log");
            return View();
        }
    }
}