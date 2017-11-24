using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class EventController : Controller
    {
        private readonly IProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("HomeController");

        public EventController(IProvider providerInput)
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
            var detailEvent = _provider.GetEventById(id);
            ViewBag.Organizer = _provider.GetOrganizerById(detailEvent.OrganizerId);
            ViewBag.Category = _provider.GetCategoryBySubcategoryId(detailEvent.SubcategoryId);
            ViewBag.Subcategory = _provider.GetSubcategoryBySubcategoryId(detailEvent.SubcategoryId);
            ViewBag.Address = _provider.GetAddressById(detailEvent.AddressId);
            return PartialView(detailEvent);
        }
    }
}