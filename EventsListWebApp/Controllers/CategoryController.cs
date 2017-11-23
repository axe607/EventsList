using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger("CategoryController");

        public CategoryController(IProvider providerInput)
        {
            _provider = providerInput;
        }

        public PartialViewResult CategoriesBar()
        {
            return PartialView(_provider.GetCategories());
        }
    }
}