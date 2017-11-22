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

        // GET: Home
        public ActionResult Index()
        {
            Log.Debug("Test Log");
            return View();
        }

        public PartialViewResult CategoriesBar()
        {
            return PartialView(_provider.GetCategories());
        }

        public PartialViewResult Events(int categoryId, bool isCategory)
        {
            if (isCategory)
            {
                return PartialView(_provider.GetEventsByCategoryId(categoryId));
            }
            return PartialView(_provider.GetEventsBySubcategoryId(categoryId));
        }

        public PartialViewResult DetailEvent(int id)
        {
            var detailEvent = _provider.GetEventById(id);
            ViewBag.Organizer = _provider.GetOrganizerById(detailEvent.OrganizerId);
            ViewBag.Category = _provider.GetCategoryBySubcategoryId(detailEvent.SubcategoryId);
            ViewBag.Subcategory = _provider.GetSubcategoryBySubcategoryId(detailEvent.SubcategoryId);
            return PartialView(detailEvent);
        }
    }
}