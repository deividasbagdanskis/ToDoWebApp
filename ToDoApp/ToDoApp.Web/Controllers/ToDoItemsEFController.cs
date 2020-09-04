using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ToDoItemsEFController : Controller
    {
        private readonly IAsyncDbDataProvider<ToDoItemVo> _toDoItemProvider;
        private readonly IAsyncDbDataProvider<CategoryVo> _categoryProvider;
        private readonly IMapper _mapper;
        private readonly ApiClient _apiClient;

        public ToDoItemsEFController(IAsyncDbDataProvider<ToDoItemVo> provider, IMapper mapper,
            IAsyncDbDataProvider<CategoryVo> categoryProvider, ApiClient apiClient)
        {
            _toDoItemProvider = provider;
            _mapper = mapper;
            _categoryProvider = categoryProvider;
            _apiClient = apiClient;
        }

        // GET: ToDoItemsEF
        public async Task<IActionResult> Index()
        {
            IEnumerable<ToDoItemVo> toDoItems = await _toDoItemProvider.GetAll();

            foreach (ToDoItemVo toDoItem in toDoItems)
            {
                toDoItem.ProjectName = await GetProjectName(toDoItem.ProjectId);
            }

            return View(_mapper.Map<IEnumerable<ToDoItemViewModel>>(toDoItems));
        }

        // GET: ToDoItemsEF/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemVo toDoItem = await _toDoItemProvider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }
            
            toDoItem.ProjectName = await GetProjectName(toDoItem.ProjectId);

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // GET: ToDoItemsEF/Create
        public async Task<IActionResult> Create()
        {
            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(await GetProjects(), "Id", "Name");

            return View();
        }

        // POST: ToDoItemsEF/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItemVo toDoItem = _mapper.Map<ToDoItemVo>(toDoItemViewModel);

            toDoItem.CreationDate = DateTime.Today;

            if (ModelState.IsValid)
            {
                if (toDoItem.CategoryId == 0)
                {
                    toDoItem.CategoryId = null;
                }

                await _toDoItemProvider.Add(toDoItem);

                return RedirectToAction(nameof(Index));
            }
            return View(toDoItem);
        }

        // GET: ToDoItemsEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemVo toDoItem = await _toDoItemProvider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(await GetProjects(), "Id", "Name");

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: ToDoItemsEF/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDoItemViewModel toDoItemViewModel)
        {
            ToDoItemVo toDoItem = _mapper.Map<ToDoItemVo>(toDoItemViewModel);

            if (id != toDoItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (toDoItem.CategoryId == 0)
                    {
                        toDoItem.CategoryId = null;
                    }

                    await _toDoItemProvider.Update(toDoItem);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_toDoItemProvider.ItemExits(toDoItem.Id))
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

            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(await GetProjects(), "Id", "Name");

            return View(toDoItemViewModel);
        }

        // GET: ToDoItemsEF/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemVo toDoItem = await _toDoItemProvider.Get(id);

            if (toDoItem == null)
            {
                return NotFound();
            }

            toDoItem.ProjectName = await GetProjectName(toDoItem.ProjectId);

            return View(_mapper.Map<ToDoItemViewModel>(toDoItem));
        }

        // POST: ToDoItemsEF/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _toDoItemProvider.Delete(id);

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<CategoryVo>> GetCategoriesForView()
        {
            List<CategoryVo> categories = (List<CategoryVo>) await _categoryProvider.GetAll();
            categories.Insert(0, new CategoryVo(0, "Uncategorized"));

            return categories;
        }

        private async Task<string> GetProjectName(int projectId)
        {
            string projectName = null;

            try
            {
                Project project = await _apiClient.ApiProjectsGetAsync(projectId);
                projectName = project.Name;
            }
            catch (ApiException)
            {

            }

            return projectName;
        }

        private async Task<IEnumerable<Project>> GetProjects()
        {
            List<Project> projects = (List<Project>) await _apiClient.ApiProjectsGetAsync();
            projects.Insert(0, new Project() { Id = 0, Name = "None" });

            return projects;
        }
    }
}
