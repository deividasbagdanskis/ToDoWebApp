using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemTagsController : Controller
    {
        private readonly IInDbToDoItemTagProvider _toDoItemTagProvider;
        private readonly IAsyncDbDataProvider<TagVo> _tagProvider;
        private readonly IAsyncDbDataProvider<ToDoItemVo> _toDoItemProvider;
        private readonly IMapper _mapper;

        public ToDoItemTagsController(IInDbToDoItemTagProvider provider, IMapper mapper,
            IAsyncDbDataProvider<TagVo> tagProvider, IAsyncDbDataProvider<ToDoItemVo> toDoItemProvider)
        {
            _toDoItemTagProvider = provider;
            _mapper = mapper;
            _tagProvider = tagProvider;
            _toDoItemProvider = toDoItemProvider;
        }

        // GET: ToDoItemTags
        public async Task<IActionResult> Index()
        {
            IEnumerable<ToDoItemTagVo> toDoItemTags = await _toDoItemTagProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier));

            return View(_mapper.Map<IEnumerable<ToDoItemTagViewModel>>(toDoItemTags));
        }

        // GET: ToDoItemTags/Details/5
        public async Task<IActionResult> Details(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _toDoItemTagProvider.Get(toDoItemId, tagId);

            if (toDoItemTag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ToDoItemTagViewModel>(toDoItemTag));
        }

        // GET: ToDoItemTags/Create
        public async Task<IActionResult> Create()
        {
            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name");
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name");

            return View();
        }

        // POST: ToDoItemTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoItemId,TagId")] ToDoItemTagViewModel toDoItemTagViewModel)
        {
            bool isUnique = await _toDoItemTagProvider.Get(toDoItemTagViewModel.ToDoItemId, toDoItemTagViewModel.TagId) == null;

            if (ModelState.IsValid)
            {
                if (isUnique)
                {
                    ToDoItemTagVo toDoItemTag = _mapper.Map<ToDoItemTagVo>(toDoItemTagViewModel);

                    await _toDoItemTagProvider.Add(toDoItemTag);
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name", toDoItemTagViewModel.TagId);
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name",
                toDoItemTagViewModel.ToDoItemId);

            return View(toDoItemTagViewModel);
        }

        // GET: ToDoItemTags/Edit/5
        public async Task<IActionResult> Edit(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _toDoItemTagProvider.Get(toDoItemId, tagId);

            if (toDoItemTag == null)
            {
                return NotFound();
            }

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name",
                toDoItemTag.ToDoItemId);
            ViewData["OldToDoItemId"] = toDoItemId;
            ViewData["OldTagId"] = tagId;

            return View(_mapper.Map<ToDoItemTagViewModel>(toDoItemTag));
        }

        // POST: ToDoItemTags/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int? oldToDoItemId, int? oldTagId,
            [Bind("ToDoItemId,TagId")] ToDoItemTagViewModel toDoItemTagViewModel)
        {
            if (toDoItemTagViewModel == null)
            {
                return NotFound();
            }

            ToDoItemTagVo oldToDoItemTag = await _toDoItemTagProvider.Get(oldToDoItemId, oldTagId);

            if (oldToDoItemTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _toDoItemTagProvider.Delete(oldToDoItemId, oldTagId);

                    ToDoItemTagVo toDoItemTag = _mapper.Map<ToDoItemTagVo>(toDoItemTagViewModel);

                    await _toDoItemTagProvider.Add(toDoItemTag);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_toDoItemTagProvider.ItemExits(toDoItemTagViewModel.ToDoItemId, toDoItemTagViewModel.TagId))
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

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name", toDoItemTagViewModel.TagId);
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(User
                .FindFirstValue(ClaimTypes.NameIdentifier)), "Id", "Name",
                toDoItemTagViewModel.ToDoItemId);

            return View(toDoItemTagViewModel);
        }

        // GET: ToDoItemTags/Delete/5
        public async Task<IActionResult> Delete(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _toDoItemTagProvider.Get(toDoItemId, tagId);

            if (toDoItemTag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ToDoItemTagViewModel>(toDoItemTag));
        }

        // POST: ToDoItemTags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int toDoItemId, int tagId)
        {
            await _toDoItemTagProvider.Delete(toDoItemId, tagId);

            return RedirectToAction(nameof(Index));
        }
    }
}
