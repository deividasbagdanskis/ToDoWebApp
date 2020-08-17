using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Business.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly IDataProvider<CategoryDao> _categoryProvider;
        private readonly IMapper _mapper;

        public CategoriesController(IDataProvider<CategoryDao> categoryProvider, IMapper mapper)
        {
            _categoryProvider = categoryProvider;
            _mapper = mapper;
        }


        // GET: CategoriesController
        public ActionResult Index()
        {
            IEnumerable<CategoryDao> toDoItems = _categoryProvider.GetAll();

            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(toDoItems));
        }

        // GET: CategoriesController/Details/5
        public ActionResult Details(int id)
        {
            CategoryDao toDoItem = _categoryProvider.Get(id);

            return View(_mapper.Map<CategoryViewModel>(toDoItem));
        }

        // GET: CategoriesController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoriesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CategoryViewModel categoryViewModel)
        {
            try
            {
                _categoryProvider.Add(_mapper.Map<CategoryDao>(categoryViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(categoryViewModel);
            }
        }

        // GET: CategoriesController/Edit/5
        public ActionResult Edit(int id)
        {
            CategoryDao category = _categoryProvider.Get(id);

            return View(_mapper.Map<CategoryViewModel>(category));
        }

        // POST: CategoriesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CategoryViewModel categoryViewModel)
        {
            try
            {
                CategoryDao category = _mapper.Map<CategoryDao>(categoryViewModel);

                _categoryProvider.Update(category);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(categoryViewModel);
            }
        }

        // GET: CategoriesController/Delete/5
        public ActionResult Delete(int id)
        {
            CategoryDao toDoItem = _categoryProvider.Get(id);

            return View(_mapper.Map<CategoryViewModel>(toDoItem));
        }

        // POST: CategoriesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CategoryViewModel categoryViewModel)
        {
            try
            {
                _categoryProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(categoryViewModel);
            }
        }
    }
}
