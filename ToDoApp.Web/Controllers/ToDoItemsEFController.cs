﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemsEFController : Controller
    {
        private readonly IAsyncDbDataProvider<ToDoItemDao> _toDoItemProvider;
        private readonly IAsyncDbDataProvider<CategoryDao> _categoryProvider;
        private readonly IMapper _mapper;

        public ToDoItemsEFController(IAsyncDbDataProvider<ToDoItemDao> provider, IMapper mapper, 
            IAsyncDbDataProvider<CategoryDao> categoryProvider)
        {
            _toDoItemProvider = provider;
            _mapper = mapper;
            _categoryProvider = categoryProvider;
        }

        // GET: ToDoItemsEF
        public async Task<IActionResult> Index()
        {
            IEnumerable<ToDoItemDao> toDoItems = await _toDoItemProvider.GetAll();

            return View(_mapper.Map<IEnumerable<ToDoItemViewModel>>(toDoItems));
        }

        // GET: ToDoItemsEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemDao toDoItem = await _toDoItemProvider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // GET: ToDoItemsEF/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");

            return View();
        }

        // POST: ToDoItemsEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItemDao toDoItem = _mapper.Map<ToDoItemDao>(toDoItemViewModel);

            toDoItem.CreationDate = DateTime.Today;

            if (ModelState.IsValid)
            {
                if (toDoItem.CategoryId == 0)
                {
                    toDoItem.CategoryId = null;
                }

                await _toDoItemProvider.Add(toDoItem);

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

            ToDoItemDao toDoItem = await _toDoItemProvider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: ToDoItemsEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItemDao toDoItem = _mapper.Map<ToDoItemDao>(toDoItemViewModel);

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

                    await _toDoItemProvider.Update(toDoItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_toDoItemProvider.ItemExits(toDoItem.Id))
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

            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");

            return View(toDoItemViewModel);
        }

        // GET: ToDoItemsEF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemDao toDoItem = await _toDoItemProvider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: ToDoItemsEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _toDoItemProvider.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<List<CategoryDao>> GetCategoriesForView()
        {
            List<CategoryDao> categories = await _categoryProvider.GetAll();
            categories.Insert(0, new CategoryDao(0, "Uncategorized"));

            return categories;
        }
    }
}
