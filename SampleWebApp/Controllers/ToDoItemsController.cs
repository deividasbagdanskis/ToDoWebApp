using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Services;

namespace SampleWebApp.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly ITodoItemProvider todoItemProvider;

        public ToDoItemsController(ITodoItemProvider todoItemProvider)
        {
            this.todoItemProvider = todoItemProvider;
        }


        // GET: TodoItemsController
        public ActionResult Index()
        {
            return View(todoItemProvider.GetAll());
        }

        // GET: TodoItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: TodoItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: TodoItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: TodoItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: TodoItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
