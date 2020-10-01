using System;
using System.Collections.Generic;
using System.Linq;
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
using ToDoApp.Commons.Exceptions;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    [Authorize]
    public class ToDoItemsEFController : Controller
    {
        private readonly IAsyncDbDataProvider<ToDoItemVo> _toDoItemProvider;
        private readonly IAsyncDbDataProvider<CategoryVo> _categoryProvider;
        private readonly IMapper _mapper;
        private readonly IApiClient _apiClient;
        private readonly string _userId;

        public ToDoItemsEFController(IAsyncDbDataProvider<ToDoItemVo> provider, IMapper mapper,
            IAsyncDbDataProvider<CategoryVo> categoryProvider, IApiClient apiClient,
            IHttpContextAccessor httpContextAccessor)
        {
            _toDoItemProvider = provider;
            _mapper = mapper;
            _categoryProvider = categoryProvider;
            _apiClient = apiClient;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: ToDoItemsEF
        public async Task<IActionResult> Index()
        {
            IEnumerable<ToDoItemVo> toDoItems = await _toDoItemProvider.GetAll(_userId);

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

            ToDoItemVo toDoItem = await _toDoItemProvider.Get(id, _userId);

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
            if (ModelState.IsValid)
            {
                ToDoItemVo toDoItem = _mapper.Map<ToDoItemVo>(toDoItemViewModel);

                string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                toDoItem.CreationDate = DateTime.Today;
                toDoItem.UserId = userId;

                if (toDoItem.CategoryId == 0)
                {
                    toDoItem.CategoryId = null;
                }

                try
                {
                    await _toDoItemProvider.Add(toDoItem);
                }
                catch(Exception ex) when (ex is ToDoItemException || ex is ToDoItemPriorityException || 
                                          ex is ToDoItemDeadlineDateException || ex is ToDoItemUniqueNameException)
                {
                    ViewData["ErrorMessage"] = ex.Message;
                    ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");
                    ViewData["ProjectId"] = new SelectList(await GetProjects(), "Id", "Name");
                    
                    return View(toDoItemViewModel);
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");
            ViewData["ProjectId"] = new SelectList(await GetProjects(), "Id", "Name");
            
            return View(toDoItemViewModel);
        }

        // GET: ToDoItemsEF/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ToDoItemVo toDoItem = await _toDoItemProvider.Get(id, _userId);

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

                    try
                    {
                        await _toDoItemProvider.Update(toDoItem);
                    }
                    catch (Exception ex) when (ex is ToDoItemException || ex is ToDoItemPriorityException ||
                                               ex is ToDoItemDeadlineDateException)
                    {
                        ViewData["ErrorMessage"] = ex.Message;
                        ViewData["CategoryId"] = new SelectList(await GetCategoriesForView(), "Id", "Name");
                        ViewData["ProjectId"] = new SelectList(await GetProjects(), "Id", "Name");

                        return View(toDoItemViewModel);
                    }
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

            ToDoItemVo toDoItem = await _toDoItemProvider.Get(id, _userId);

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
            await _toDoItemProvider.Delete(id, _userId);

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<CategoryVo>> GetCategoriesForView()
        {
            string userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            List<CategoryVo> categories = (List<CategoryVo>) await _categoryProvider.GetAll(userId);
            categories.Insert(0, new CategoryVo(0, "Uncategorized"));

            return categories;
        }

        private async Task<string> GetProjectName(int projectId)
        {
            string projectName = null;

            try
            {
                Project project = await _apiClient.ApiProjectsGetAsync(projectId, _userId);
                projectName = project.Name;
            }
            catch (ApiException)
            {

            }

            return projectName;
        }

        private async Task<IEnumerable<Project>> GetProjects()
        {
            List<Project> projects = (List<Project>) await _apiClient.ApiProjectsGetAsync(_userId);
            projects.Insert(0, new Project() { Id = 0, Name = "None" });

            return projects;
        }
    }
}
