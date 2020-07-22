using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Models;
using SampleWebApp.Services;

namespace SampleWebApp.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IDataProvider<Category> _categoryProvider;

        public CategoriesController(IDataProvider<Category> categoryProvider)
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
        public ActionResult Create(Category category)
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
        public ActionResult Edit(int id, Category category)
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
        public ActionResult Delete(int id, Category category)
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
