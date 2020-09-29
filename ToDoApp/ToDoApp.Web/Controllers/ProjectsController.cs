using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    [Authorize]
    public class ProjectsController : Controller
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;
        private readonly IInDbProjectToDoItemProvider _toDoItemProvider;
        private readonly string _userId;

        public ProjectsController(IApiClient apiClient, IMapper mapper, IInDbProjectToDoItemProvider toDoItemProvider,
            IHttpContextAccessor httpContextAccessor)
        {
            _apiClient = apiClient;
            _mapper = mapper;
            _toDoItemProvider = toDoItemProvider;
            _userId = httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        // GET: ProjectsController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Project> projects = await _apiClient.ApiProjectsGetAsync(_userId);

            foreach (Project project in projects)
            {
                project.Client = await _apiClient.ApiClientsGetAsync(project.ClientId, _userId);
            }

            return View(_mapper.Map<IEnumerable<ProjectViewModel>>(projects));
        }

        // GET: ProjectsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Project project = await _apiClient.ApiProjectsGetAsync(id,_userId);

            if (project == null)
            {
                return NotFound();
            }

            project.Client = await _apiClient.ApiClientsGetAsync(project.ClientId, _userId);

            IEnumerable<ToDoItemVo> toDoItems = _toDoItemProvider.GetToDoItemsByProjectId(project.Id);
            ViewData["ToDoItems"] = _mapper.Map<IEnumerable<ToDoItemViewModel>>(toDoItems);
            return View(_mapper.Map<ProjectViewModel>(project));
        }

        // GET: ProjectsController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(_userId), "Id", "Name");

            return View();
        }

        // POST: ProjectsController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ProjectViewModel projectViewModel)
        {
            Project project = _mapper.Map<Project>(projectViewModel);

            if (ModelState.IsValid)
            {
                project.UserId = _userId;

                await _apiClient.ApiProjectsPostAsync(project);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(_userId), "Id", "Name");

            return View(projectViewModel);
        }

        // GET: ProjectsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Project project = await _apiClient.ApiProjectsGetAsync(id,_userId);

            if (project == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(_userId), "Id", "Name");

            return View(_mapper.Map<ProjectViewModel>(project));
        }

        // POST: ProjectsController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ProjectViewModel projectViewModel)
        {
            if (id != projectViewModel.Id)
            {
                return BadRequest();
            }

            Project project = _mapper.Map<Project>(projectViewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    await _apiClient.ApiProjectsPutAsync(id, project);
                }
                catch
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(_userId), "Id", "Name");

            return View(projectViewModel);
        }

        // GET: ProjectsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Project project = await _apiClient.ApiProjectsGetAsync(id, _userId);

            if (project == null)
            {
                return NotFound();
            }

            project.Client = await _apiClient.ApiClientsGetAsync(project.ClientId, _userId);

            return View(_mapper.Map<ProjectViewModel>(project));
        }

        // POST: ProjectsController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ProjectViewModel projectViewModel)
        {
            if (id != projectViewModel.Id)
            {
                return BadRequest();
            }

            try
            {
                await _apiClient.ApiProjectsDeleteAsync(id, _userId);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(projectViewModel);
            }
        }
    }
}
