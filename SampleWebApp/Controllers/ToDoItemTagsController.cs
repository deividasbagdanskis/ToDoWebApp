using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using SampleWebApp.Models;

namespace SampleWebApp.Controllers
{
    public class ToDoItemTagsController : Controller
    {
        private readonly SampleWebAppContext _context;

        public ToDoItemTagsController(SampleWebAppContext context)
        {
            _context = context;
        }

        // GET: ToDoItemTags
        public async Task<IActionResult> Index()
        {
            var sampleWebAppContext = _context.ToDoItemTag.Include(t => t.Tag).Include(t => t.ToDoItem);
            return View(await sampleWebAppContext.ToListAsync());
        }

        // GET: ToDoItemTags/Details/5
        public async Task<IActionResult> Details(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(m => m.ToDoItemId == toDoItemId && m.TagId == tagId);
            if (toDoItemTag == null)
            {
                return NotFound();
            }

            return View(toDoItemTag);
        }

        // GET: ToDoItemTags/Create
        public IActionResult Create()
        {
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name");
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name");
            return View();
        }

        // POST: ToDoItemTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoItemId,TagId")] ToDoItemTag toDoItemTag)
        {
            bool isUnique = await _context.ToDoItemTag.FindAsync(toDoItemTag.ToDoItemId, toDoItemTag.TagId) == null;

            if (ModelState.IsValid)
            {
                if (isUnique)
                {
                    _context.Add(toDoItemTag);
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name", toDoItemTag.ToDoItemId);
            return View(toDoItemTag);
        }

        // GET: ToDoItemTags/Edit/5
        public async Task<IActionResult> Edit(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _context.ToDoItemTag.FindAsync(toDoItemId, tagId);
            if (toDoItemTag == null)
            {
                return NotFound();
            }
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name", toDoItemTag.ToDoItemId);
            return View(toDoItemTag);
        }

        // POST: ToDoItemTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ToDoItemId,TagId")] ToDoItemTag toDoItemTag)
        {
            if (id != toDoItemTag.ToDoItemId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(toDoItemTag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ToDoItemTagExists(toDoItemTag.ToDoItemId))
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
            ViewData["TagId"] = new SelectList(_context.Tag, "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(_context.ToDoItem, "Id", "Name", toDoItemTag.ToDoItemId);
            return View(toDoItemTag);
        }

        // GET: ToDoItemTags/Delete/5
        public async Task<IActionResult> Delete(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(m => m.ToDoItemId == toDoItemId && m.TagId == tagId);
            if (toDoItemTag == null)
            {
                return NotFound();
            }

            return View(toDoItemTag);
        }

        // POST: ToDoItemTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int toDoItemId, int tagId)
        {
            var toDoItemTag = await _context.ToDoItemTag.FindAsync(toDoItemId, tagId);
            _context.ToDoItemTag.Remove(toDoItemTag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ToDoItemTagExists(int id)
        {
            return _context.ToDoItemTag.Any(e => e.ToDoItemId == id);
        }
    }
}
