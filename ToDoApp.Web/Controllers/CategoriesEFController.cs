using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;
using System.Threading.Tasks;
using AutoMapper;

namespace ToDoApp.Web.Controllers
{
    public class CategoriesEFController : Controller
    {
        private IAsyncDbDataProvider<CategoryDao> _provider;
        private readonly IMapper _mapper;

        public CategoriesEFController(IAsyncDbDataProvider<CategoryDao> provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
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

            CategoryDao category = await _provider.Get(id);

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
        public async Task<IActionResult> Create([Bind("Id,Name")] CategoryDao category)
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

            CategoryDao category = await _provider.Get(id);

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] CategoryDao category)
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
