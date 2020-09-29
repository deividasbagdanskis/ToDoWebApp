using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    [Authorize]
    public class TagsEFController : Controller
    {
        private readonly IAsyncDbDataProvider<TagVo> _provider;
        private readonly IMapper _mapper;
        private readonly string _userId;

        public TagsEFController(IAsyncDbDataProvider<TagVo> provider, IMapper mapper,
            IHttpContextAccessor httpContextAccessor)
        {
            _provider = provider;
            _mapper = mapper;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: TagsEF
        public async Task<IActionResult> Index()
        {
            IEnumerable<TagVo> tags = await _provider.GetAll(_userId);

            return View(_mapper.Map<IEnumerable<TagViewModel>>(tags));
        }

        // GET: TagsEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagVo tag = await _provider.Get(id, _userId);

            if (tag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TagViewModel>(tag));
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
        public async Task<IActionResult> Create([Bind("Name")] TagViewModel tagViewModel)
        {
            if (ModelState.IsValid)
            {
                TagVo tag = _mapper.Map<TagVo>(tagViewModel);

                tag.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                await _provider.Add(tag);
                return RedirectToAction(nameof(Index));
            }
            return View(tagViewModel);
        }

        // GET: TagsEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagVo tag = await _provider.Get(id, _userId);

            if (tag == null)
            {
                return NotFound();
            }
            return View(_mapper.Map<TagViewModel>(tag));
        }

        // POST: TagsEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, Name")] TagViewModel tagViewModel)
        {
            if (id != tagViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _provider.Update(_mapper.Map<TagVo>(tagViewModel));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_provider.ItemExits(tagViewModel.Id))
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
            return View(tagViewModel);
        }

        // GET: TagsEF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            TagVo tag = await _provider.Get(id, _userId);

            if (tag == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<TagViewModel>(tag));
        }

        // POST: TagsEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _provider.Delete(id, _userId);
            return RedirectToAction(nameof(Index));
        }
    }
}
