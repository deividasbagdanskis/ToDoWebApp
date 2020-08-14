using Microsoft.AspNetCore.Mvc;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly IDataProvider<ToDoItemDao> _todoItemProvider;

        public ToDoItemsController(IDataProvider<ToDoItemDao> todoItemProvider)
        {
            _todoItemProvider = todoItemProvider;
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
        public ActionResult Create(ToDoItemDao toDoItem)
        {
            try
            {
                _todoItemProvider.Add(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItem);
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
        public ActionResult Edit(int id, ToDoItemDao toDoItem)
        {
            try
            {
                _todoItemProvider.Update(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItem);
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
        public ActionResult Delete(int id, ToDoItemDao toDoItem)
        {
            try
            {
                _todoItemProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItem);
            }
        }
    }
}
