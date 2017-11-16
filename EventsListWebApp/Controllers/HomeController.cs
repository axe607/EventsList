using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using EventsListBL;
using EventsListWebApp.Models;
using log4net;

namespace EventsListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ITestServiceProvider _provider;
        private static readonly ILog log = LogManager.GetLogger("HomeController");

        public HomeController(ITestServiceProvider providerInput)
        {
            _provider = providerInput;
        }

        // GET: Home
        public ActionResult Index()
        {
            HomeViewModel model = new HomeViewModel{Message = _provider.GetMessage()};
            log.Debug("Test Log");
            return View(model);
        }
    }
}