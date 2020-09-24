using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using System.Threading.Tasks;
using AutoMapper;
using System.Collections.Generic;
using ToDoApp.Web.ViewModels;
using ToDoApp.Commons.Exceptions;
using System.Security.Claims;

namespace ToDoApp.Web.Controllers
{
    public class CategoriesEFController : Controller
    {
        private readonly IAsyncDbDataProvider<CategoryVo> _provider;
        private readonly IMapper _mapper;

        public CategoriesEFController(IAsyncDbDataProvider<CategoryVo> provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
        }

        // GET: CategoriesEF
        public async Task<IActionResult> Index()
        {
            IEnumerable<CategoryVo> categories = await _provider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier));

            return View(_mapper.Map<IEnumerable<CategoryViewModel>>(categories));
        }

        // GET: CategoriesEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            CategoryVo category = await _provider.Get(id);

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
                try
                {
                    await _provider.Add(_mapper.Map<CategoryVo>(categoryViewModel));
                }
                catch (CategoryNameException ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;

                    return View(categoryViewModel);
                }
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

            CategoryVo category = await _provider.Get(id);

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryViewModel categoryViewModel)
        {
            if (id != categoryViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provider.Update(_mapper.Map<CategoryVo>(categoryViewModel));
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
                catch(CategoryNameException ex)
                {
                    ViewData["ErrorMessage"] = ex.Message;

                    return View(categoryViewModel);
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
