using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Models;
using SampleWebApp.Services.InDbProviders;
using System.Threading.Tasks;

namespace SampleWebApp.Controllers
{
    public class CategoriesEFController : Controller
    {
        private IAsyncDataProvider<Category> _provider;

        public CategoriesEFController(IAsyncDataProvider<Category> provider)
        {
            _provider = provider;
        }

        // GET: CategoriesEF
        public async Task<IActionResult> Index()
        {
            return View(await _provider.GetAll());
        }

        // GET: CategoriesEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _provider.Get(id);

            if (category == null)
            {
                return NotFound();
            }

            return View(category);
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
        public async Task<IActionResult> Create([Bind("Id,Name")] Category category)
        {
            if (ModelState.IsValid)
            {
                await _provider.Add(category);
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: CategoriesEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Category category = await _provider.Get(id);

            if (category == null)
            {
                return NotFound();
            }
            return View(category);
        }

        // POST: CategoriesEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Category category)
        {
            if (id != category.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provider.Update(category);
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
            return View(category);
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

            return View(category);
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
