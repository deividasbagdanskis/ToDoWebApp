using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Models;
using SampleWebApp.Services.InDbProviders;
using SampleWebApp.ViewModels;

namespace SampleWebApp.Controllers
{
    public class ToDoItemsEFController : Controller
    {
        private IAsyncDbDataProvider<ToDoItem> _provider;

        public ToDoItemsEFController(IAsyncDbDataProvider<ToDoItem> provider)
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

            ToDoItem toDoItem = await _provider.Get(id);

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

            return View(toDoItemViewModel);
        }

        // POST: ToDoItemsEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItem toDoItem = toDoItemViewModel.ToDoItem;

            toDoItem.CreationDate = DateTime.Today;
            if (ModelState.IsValid)
            {
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

            ToDoItem toDoItem = await _provider.Get(id);

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
            ToDoItem toDoItem = toDoItemViewModel.ToDoItem;

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

            ToDoItem toDoItem = await _provider.Get(id);

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
