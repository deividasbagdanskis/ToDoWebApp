using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;
using ToDoApp.Web.ViewModels;

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
            IEnumerable<TagDao> tags = await _provider.GetAll();

            return View(_mapper.Map<IEnumerable<TagViewModel>>(tags));
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
                await _provider.Add(_mapper.Map<TagDao>(tagViewModel));
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

            TagDao tag = await _provider.Get(id);

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
                    await _provider.Update(_mapper.Map<TagDao>(tagViewModel));
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

            TagDao tag = await _provider.Get(id);

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
            await _provider.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
