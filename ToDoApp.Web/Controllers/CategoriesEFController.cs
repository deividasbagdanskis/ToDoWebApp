using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Business.Controllers
{
    public class CategoriesEFController : Controller
    {
        private readonly IAsyncDbDataProvider<CategoryDao> _provider;
        private readonly IMapper _mapper;

        public CategoriesEFController(IAsyncDbDataProvider<CategoryDao> provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        // GET: CategoriesEF
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryDao> categories = await _provider.GetAll();

            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        // GET: CategoriesEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryDao category = await _provider.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CategoryViewModel>(category));
        }

        // GET: CategoriesEF/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CategoriesEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name")] CategoryViewModel categoryViewModel)
        {
            if (ModelState.IsValid)
            {
                await _provider.Add(_mapper.Map<CategoryDao>(categoryViewModel));
                return RedirectToAction(nameof(Index));
            }
            return View(categoryViewModel);
        }

        // GET: CategoriesEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryDao category = await _provider.Get(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<CategoryViewModel>(category));
        }

        // POST: CategoriesEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name")] CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provider.Update(_mapper.Map<CategoryDao>(categoryViewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_provider.ItemExits(id))
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
            return View(categoryViewModel);
        }

        // GET: CategoriesEF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await _provider.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<CategoryViewModel>(category));
        }

        // POST: CategoriesEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _provider.Delete(id);

            return RedirectToAction(nameof(Index));
        }
    }
}
