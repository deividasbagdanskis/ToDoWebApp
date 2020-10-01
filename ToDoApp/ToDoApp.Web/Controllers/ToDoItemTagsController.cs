using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    [Authorize]
    public class ToDoItemTagsController : Controller
    {
        private readonly IInDbToDoItemTagProvider _toDoItemTagProvider;
        private readonly IAsyncDbDataProvider<TagVo> _tagProvider;
        private readonly IAsyncDbDataProvider<ToDoItemVo> _toDoItemProvider;
        private readonly IMapper _mapper;
        private readonly string _userId;

        public ToDoItemTagsController(IInDbToDoItemTagProvider provider, IMapper mapper,
            IAsyncDbDataProvider<TagVo> tagProvider, IAsyncDbDataProvider<ToDoItemVo> toDoItemProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _toDoItemTagProvider = provider;
            _mapper = mapper;
            _tagProvider = tagProvider;
            _toDoItemProvider = toDoItemProvider;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: ToDoItemTags
        public async Task<IActionResult> Index()
        {
            IEnumerable<ToDoItemTagVo> toDoItemTags = await _toDoItemTagProvider.GetAll(_userId);

            return View(_mapper.Map<IEnumerable<ToDoItemTagViewModel>>(toDoItemTags));
        }

        // GET: ToDoItemTags/Details/5
        public async Task<IActionResult> Details(int? toDoItemId, int? tagId)
        {
            if (toDoItemId == null || tagId == null)
            {
                return NotFound();
            }

            var toDoItemTag = await _toDoItemTagProvider.Get(toDoItemId, tagId, _userId);

            if (toDoItemTag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ToDoItemTagViewModel>(toDoItemTag));
        }

        // GET: ToDoItemTags/Create
        public async Task<IActionResult> Create()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(userId), "Id", "Name");
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(userId), "Id", "Name");

            return View();
        }

        // POST: ToDoItemTags/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ToDoItemId,TagId")] ToDoItemTagViewModel toDoItemTagViewModel)
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            bool isUnique = await _toDoItemTagProvider.Get(toDoItemTagViewModel.ToDoItemId, 
                toDoItemTagViewModel.TagId, _userId) == null;

            if (ModelState.IsValid)
            {
                if (isUnique)
                {
                    ToDoItemTagVo toDoItemTag = _mapper.Map<ToDoItemTagVo>(toDoItemTagViewModel);

                    toDoItemTag.UserId = userId;

                    await _toDoItemTagProvider.Add(toDoItemTag);
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(userId), "Id", "Name", 
                toDoItemTagViewModel.TagId);
            
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(userId), "Id", "Name",
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

            var toDoItemTag = await _toDoItemTagProvider.Get(toDoItemId, tagId, _userId);

            if (toDoItemTag == null)
            {
                return NotFound();
            }

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(userId), "Id", "Name", toDoItemTag.TagId);
            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(userId), "Id", "Name",
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

            ToDoItemTagVo oldToDoItemTag = await _toDoItemTagProvider.Get(oldToDoItemId, oldTagId, _userId);

            if (oldToDoItemTag == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _toDoItemTagProvider.Delete(oldToDoItemId, oldTagId, _userId);

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

            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            ViewData["TagId"] = new SelectList(await _tagProvider.GetAll(userId), "Id", "Name", 
                toDoItemTagViewModel.TagId);

            ViewData["ToDoItemId"] = new SelectList(await _toDoItemProvider.GetAll(userId), "Id", "Name",
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

            var toDoItemTag = await _toDoItemTagProvider.Get(toDoItemId, tagId, _userId);

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
            await _toDoItemTagProvider.Delete(toDoItemId, tagId, _userId);

            return RedirectToAction(nameof(Index));
        }
    }
}
