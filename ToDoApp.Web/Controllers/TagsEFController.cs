using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;

namespace ToDoApp.Web.Controllers
{
    public class TagsEFController : Controller
    {
        private readonly IAsyncDbDataProvider<TagDao> _provider;
        private readonly IMapper _mapper;

        public TagsEFController(IAsyncDbDataProvider<TagDao> provider, IMapper mapper)
        {
            _provider = provider;
            _mapper = mapper;
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

            TagDao tag = await _provider.Get(id);

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
        public async Task<IActionResult> Create([Bind("Id,Name")] TagDao tag)
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

            TagDao tag = await _provider.Get(id);

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
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] TagDao tag)
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

            TagDao tag = await _provider.Get(id);

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
