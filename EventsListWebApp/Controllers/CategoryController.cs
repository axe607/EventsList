using EventsListBL.Providers;
using EventsListWebApp.Models;
using log4net;
using System;
using System.Web.Mvc;
using EventsListBL.Services;
using EventsListCommon.Models;

namespace EventsListWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IBusinessProvider _provider;
        private readonly IEventOperation _eventOperation;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryController));

        public CategoryController(IBusinessProvider businessProvider, IEventOperation eventOperation)
        {
            _provider = businessProvider;
            _eventOperation = eventOperation;
        }

        public ActionResult Index()
        {
            return View(_provider.GetCategories());
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

        [AllowTo(Roles = "Admin,Editor")]
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View(new Category());
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult AddCategory(Category createdCategory)
        {
            if (ModelState.IsValid)
            {
                _eventOperation.AddCategory(
                    createdCategory.Name,
                    createdCategory.Pid);
                JsonController.ClearCategroriesCache();
                return RedirectToAction("Index");
            }
            return View(createdCategory);
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpGet]
        public ActionResult EditCategory(int categoryId)
        {
            return View(_provider.GetCategoryById(categoryId));
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult EditCategory(Category editedCategory)
        {
            if (ModelState.IsValid)
            {
                _eventOperation.EditCategory(
                    editedCategory.Id,
                    editedCategory.Pid,
                    editedCategory.Name);
                JsonController.ClearCategroriesCache();
                return RedirectToAction("Index");
            }
            return View(editedCategory);
        }

        [AllowTo(Roles = "Admin,Editor")]
        public RedirectToRouteResult DeleteCategory(int categoryId)
        {
            _eventOperation.DeleteCategory(categoryId);
            return RedirectToAction("Index");
        }
    }
}