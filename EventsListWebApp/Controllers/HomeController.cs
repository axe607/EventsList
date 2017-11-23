using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("HomeController");

        public HomeController(IProvider providerInput)
        {
            _provider = providerInput;
        }
        
        public ActionResult Index()
        {
            return View();
        }

    }
}