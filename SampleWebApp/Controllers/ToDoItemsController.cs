using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SampleWebApp.Models;
using SampleWebApp.Services;

namespace SampleWebApp.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly IDataProvider _todoItemProvider;

        public ToDoItemsController(IDataProvider todoItemProvider)
        {
            this._todoItemProvider = todoItemProvider;
        }


        // GET: TodoItemsController
        public ActionResult Index()
        {
            return View(_todoItemProvider.GetAll());
        }

        // GET: TodoItemsController/Details/5
        public ActionResult Details(int id)
        {
            return View(_todoItemProvider.Get(id));
        }

        // GET: TodoItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoItem toDoItem)
        {
            try
            {
                _todoItemProvider.Add(toDoItem);
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
            return View(_todoItemProvider.Get(id));
        }

        // POST: TodoItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDoItem toDoItem)
        {
            try
            {
                _todoItemProvider.Update(toDoItem);
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
            return View(_todoItemProvider.Get(id));
        }

        // POST: TodoItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _todoItemProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
