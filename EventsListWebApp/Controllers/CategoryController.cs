using EventsListBL.Providers.Interfaces;
using EventsListBL.Services.Interfaces;
using EventsListCommon.Models;
using EventsListWebApp.Models;
using log4net;
using System;
using System.Web.Mvc;

namespace EventsListWebApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryProvider _categoryProvider;
        private readonly ICategoryOperation _categoryOperation;
        private static readonly ILog Log = LogManager.GetLogger(typeof(CategoryController));

        public CategoryController(ICategoryProvider categoryProvider, ICategoryOperation categoryOperation)
        {
            _categoryProvider = categoryProvider;
            _categoryOperation = categoryOperation;
        }

        public ActionResult Index()
        {
            return View(_categoryProvider.GetCategories());
        }

        public PartialViewResult CategoriesBar()
        {
            try
            {
                return PartialView(_categoryProvider.GetCategories());
            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                ViewBag.Error = ex.Message;
                return PartialView();
            }
        }

        public PartialViewResult SearchBar()
        {
            return PartialView();
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
                try
                {
                    _categoryOperation.AddCategory(
                        createdCategory.Name,
                        createdCategory.Pid);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }
            return View(createdCategory);
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpGet]
        public ActionResult EditCategory(int categoryId)
        {
            return View(_categoryProvider.GetCategoryById(categoryId));
        }

        [AllowTo(Roles = "Admin,Editor")]
        [HttpPost]
        public ActionResult EditCategory(Category editedCategory)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _categoryOperation.EditCategory(
                        editedCategory.Id,
                        editedCategory.Pid,
                        editedCategory.Name);
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
            }
            return View(editedCategory);
        }

        [AllowTo(Roles = "Admin,Editor")]
        [Ajax]
        public void DeleteCategory(int categoryId)
        {
            _categoryOperation.DeleteCategory(categoryId);
        }
    }
}