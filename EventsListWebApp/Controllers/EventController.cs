using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IBusinessProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("HomeController");

        public EventController(IBusinessProvider providerInput)
        {
            _provider = providerInput;
        }


        public PartialViewResult EventsByCategory(int categoryId)
        {
            return PartialView("Events", _provider.GetEventsByCategoryId(categoryId));
        }

        public PartialViewResult EventsBySubcategory(int subcategoryId)
        {
            return PartialView("Events", _provider.GetEventsBySubcategoryId(subcategoryId));
        }

        public PartialViewResult Events()
        {
            return PartialView("Events", _provider.GetEvents());
        }

        public PartialViewResult DetailEvent(int id)
        {
            return PartialView(_provider.GetEventInfoDetailById(id));
        }
    }
}