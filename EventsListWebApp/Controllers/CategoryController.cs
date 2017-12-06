using System;
using EventsListBL.Providers;
using log4net;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBusinessProvider _provider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryController));

        public CategoryController(IBusinessProvider providerInput)
        {
            _provider = providerInput;
        }
        
        public PartialViewResult CategoriesBar()
        {
            try
            {
                return PartialView(_provider.GetCategories());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView();
            }
        }
    }
}