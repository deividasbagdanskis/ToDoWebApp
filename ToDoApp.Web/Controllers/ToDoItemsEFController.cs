using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemsEFController : Controller
    {
        private IAsyncDbDataProvider<ToDoItemDao> _provider;

        public ToDoItemsEFController(IAsyncDbDataProvider<ToDoItemDao> provider)
        {
            _provider = provider;
        }

        // GET: ToDoItemsEF
        public async Task<IActionResult> Index()
        {
            return View(await _provider.GetAll());
        }

        // GET: ToDoItemsEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemDao toDoItem = await _provider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // GET: ToDoItemsEF/Create
        public async Task<IActionResult> Create()
        {
            IToDoItemViewModel toDoItemViewModel = new ToDoItemViewModel(_provider.Context);
            await toDoItemViewModel.SetCategoriesSelectList();
            await toDoItemViewModel.RetrieveTags();

            return View(toDoItemViewModel);
        }

        // POST: ToDoItemsEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItemDao toDoItem = toDoItemViewModel.ToDoItem;

            toDoItem.CreationDate = DateTime.Today;

            if (ModelState.IsValid)
            {
                if (toDoItem.CategoryId == 0)
                {
                    toDoItem.CategoryId = null;
                }

                await _provider.Add(toDoItem);
                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }

        // GET: ToDoItemsEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemDao toDoItem = await _provider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            IToDoItemViewModel toDoItemViewModel = new ToDoItemViewModel(_provider.Context)
            {
                ToDoItem = toDoItem
            };

            await toDoItemViewModel.SetCategoriesSelectList();

            return View(toDoItemViewModel);
        }

        // POST: ToDoItemsEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItemDao toDoItem = toDoItemViewModel.ToDoItem;

            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (toDoItem.CategoryId == 0)
                    {
                        toDoItem.CategoryId = null;
                    }

                    await _provider.Update(toDoItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_provider.ItemExits(toDoItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            toDoItemViewModel = new ToDoItemViewModel(_provider.Context);
            await toDoItemViewModel.SetCategoriesSelectList();
            return View(toDoItemViewModel);
        }

        // GET: ToDoItemsEF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemDao toDoItem = await _provider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(toDoItem);
        }

        // POST: ToDoItemsEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _provider.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
