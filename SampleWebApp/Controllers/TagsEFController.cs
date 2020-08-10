using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Models;
using SampleWebApp.Services.InDbProviders;

namespace SampleWebApp.Controllers
{
    public class TagsEFController : Controller
    {
        private readonly IAsyncDbDataProvider<Tag> _provider;

        public TagsEFController(IAsyncDbDataProvider<Tag> provider)
        {
            _provider = provider;
        }

        // GET: TagsEF
        public async Task<IActionResult> Index()
        {
            return View(await _provider.GetAll());
        }

        // GET: TagsEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag tag = await _provider.Get(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: TagsEF/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: TagsEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                await _provider.Add(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: TagsEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag tag = await _provider.Get(id);

            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: TagsEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provider.Update(tag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_provider.ItemExits(tag.Id))
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
            return View(tag);
        }

        // GET: TagsEF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Tag tag = await _provider.Get(id);

            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: TagsEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _provider.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
