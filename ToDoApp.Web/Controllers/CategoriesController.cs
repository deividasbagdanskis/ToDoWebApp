using Microsoft.AspNetCore.Mvc;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services;

namespace ToDoApp.Web.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IDataProvider<CategoryDao> _categoryProvider;

        public CategoriesController(IDataProvider<CategoryDao> categoryProvider)
        {
            _categoryProvider = categoryProvider;
        }


        // GET: CategoriesController
        public ActionResult Index()
        {
            return View(_categoryProvider.GetAll());
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            return View(_categoryProvider.Get(id));
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryDao category)
        {
            try
            {
                _categoryProvider.Add(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(_categoryProvider.Get(id));
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryDao category)
        {
            try
            {
                _categoryProvider.Update(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(_categoryProvider.Get(id));
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryDao category)
        {
            try
            {
                _categoryProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }
    }
}
