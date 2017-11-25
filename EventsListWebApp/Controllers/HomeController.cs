using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IBusinessProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("HomeController");

        public HomeController(IBusinessProvider providerInput)
        {
            _provider = providerInput;
        }
        
        public ActionResult Index()
        {
            return View();
        }

    }
}