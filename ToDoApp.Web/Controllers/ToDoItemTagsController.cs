using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemTagsController : Controller
    {
        private readonly IInDbToDoItemTagProvider _provider;

        public ToDoItemTagsController(IInDbToDoItemTagProvider provider)
        {
            _provider = provider;
        }

        // GET: ToDoItemTags
        public async Task<IActionResult> Index()
        {
            
            return View(await _provider.GetAll());
        }

        // GET: ToDoItemTags/Details/5
        public async Task<IActionResult> Details(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _provider.Get(toDoItemId, tagId);

            if (toDoItemTag == null)
            {
                return NotFound();
            }

            return View(toDoItemTag);
        }

        // GET: ToDoItemTags/Create
        public IActionResult Create()
        {
            ViewData["TagId"] = new SelectList(_provider.Context.Tag, "Id", "Name");
            ViewData["ToDoItemId"] = new SelectList(_provider.Context.ToDoItem, "Id", "Name");
            return View();
        }

        // POST: ToDoItemTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoItemId,TagId")] ToDoItemTagDao toDoItemTag)
        {
            bool isUnique = await _provider.Get(toDoItemTag.ToDoItemId, toDoItemTag.TagId) == null;

            if (ModelState.IsValid)
            {
                if (isUnique)
                {
                    await _provider.Add(toDoItemTag);
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["TagId"] = new SelectList(_provider.Context.Tag, "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(_provider.Context.ToDoItem, "Id", "Name", toDoItemTag.ToDoItemId);
            return View(toDoItemTag);
        }

        // GET: ToDoItemTags/Edit/5
        public async Task<IActionResult> Edit(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _provider.Get(toDoItemId, tagId);
            if (toDoItemTag == null)
            {
                return NotFound();
            }
            ViewData["TagId"] = new SelectList(_provider.Context.Tag, "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(_provider.Context.ToDoItem, "Id", "Name", toDoItemTag.ToDoItemId);
            ViewData["OldToDoItemId"] = toDoItemId;
            ViewData["OldTagId"] = tagId;
            return View(toDoItemTag);
        }

        // POST: ToDoItemTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? oldToDoItemId, int? oldTagId,
            [Bind("ToDoItemId,TagId")] ToDoItemTagDao toDoItemTag)
        {
            if (toDoItemTag == null)
            {
                return NotFound();
            }

            ToDoItemTagDao oldToDoItemTag = await _provider.Get(oldToDoItemId, oldTagId);
            
            if (oldToDoItemTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provider.Delete(oldToDoItemId, oldTagId);
                    await _provider.Add(toDoItemTag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_provider.ItemExits(toDoItemTag.ToDoItemId, toDoItemTag.TagId))
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
            ViewData["TagId"] = new SelectList(_provider.Context.Tag, "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(_provider.Context.ToDoItem, "Id", "Name", toDoItemTag.ToDoItemId);
            return View(toDoItemTag);
        }

        // GET: ToDoItemTags/Delete/5
        public async Task<IActionResult> Delete(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _provider.Get(toDoItemId, tagId);

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
            await _provider.Delete(toDoItemId, tagId);
            return RedirectToAction(nameof(Index));
        }
    }
}
