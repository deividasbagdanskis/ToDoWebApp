using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemsController : Controller
    {
        private readonly IDataProvider<ToDoItemVo> _todoItemProvider;
        private readonly IMapper _mapper;

        public ToDoItemsController(IDataProvider<ToDoItemVo> todoItemProvider, IMapper mapper)
        {
            _todoItemProvider = todoItemProvider;
            _mapper = mapper;
        }


        // GET: TodoItemsController
        public ActionResult Index()
        {
            IEnumerable<ToDoItemVo> toDoItems = _todoItemProvider.GetAll();
            return View(_mapper.Map<IEnumerable<ToDoItemViewModel>>(toDoItems));
        }

        // GET: TodoItemsController/Details/5
        public ActionResult Details(int id)
        {
            ToDoItemVo toDoItem = _todoItemProvider.Get(id);
            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // GET: TodoItemsController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TodoItemsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ToDoItemViewModel toDoItemViewModel)
        {
            try
            {
                _todoItemProvider.Add(_mapper.Map<ToDoItemVo>(toDoItemViewModel));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItemViewModel);
            }
        }

        // GET: TodoItemsController/Edit/5
        public ActionResult Edit(int id)
        {
            ToDoItemVo toDoItem = _todoItemProvider.Get(id);
            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: TodoItemsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ToDoItemViewModel toDoItemViewModel)
        {
            try
            {
                ToDoItemVo toDoItem = _mapper.Map<ToDoItemVo>(toDoItemViewModel);

                _todoItemProvider.Update(toDoItem);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItemViewModel);
            }
        }

        // GET: TodoItemsController/Delete/5
        public ActionResult Delete(int id)
        {
            ToDoItemVo toDoItem = _todoItemProvider.Get(id);

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: TodoItemsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ToDoItemViewModel toDoItemViewModel)
        {
            try
            {
                _todoItemProvider.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(toDoItemViewModel);
            }
        }
    }
}
