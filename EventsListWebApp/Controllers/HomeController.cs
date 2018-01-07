using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;
using EventsListBL.Providers.Interfaces;

namespace EventsListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IEventProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("HomeController");

        public HomeController(IEventProvider providerInput)
        {
            _provider = providerInput;
        }
        
        public ActionResult Index()
        {
            return View();
        }

    }
}