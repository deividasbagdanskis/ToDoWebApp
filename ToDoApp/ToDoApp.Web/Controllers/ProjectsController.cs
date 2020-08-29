using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ProjectsController : Controller
    {
        private readonly ApiClient _apiClient;
        private readonly IMapper _mapper;

        public ProjectsController(ApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        // GET: ProjectsController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Project> projects = await _apiClient.ApiProjectsGetAsync();

            foreach (Project project in projects)
            {
                project.Client = await _apiClient.ApiClientsGetAsync(project.ClientId);
            }

            return View(_mapper.Map<IEnumerable<ProjectViewModel>>(projects));
        }

        // GET: ProjectsController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Project project = await _apiClient.ApiProjectsGetAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.Client = await _apiClient.ApiClientsGetAsync(project.ClientId);

            return View(_mapper.Map<ProjectViewModel>(project));
        }

        // GET: ProjectsController/Create
        public async Task<ActionResult> Create()
        {
            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(), "Id", "Name");

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
                await _apiClient.ApiProjectsPostAsync(project);

                return RedirectToAction(nameof(Index));
            }

            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(), "Id", "Name");

            return View(projectViewModel);
        }

        // GET: ProjectsController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Project project = await _apiClient.ApiProjectsGetAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(), "Id", "Name");

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

            ViewData["ClientId"] = new SelectList(await _apiClient.ApiClientsGetAsync(), "Id", "Name");

            return View(projectViewModel);
        }

        // GET: ProjectsController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Project project = await _apiClient.ApiProjectsGetAsync(id);

            if (project == null)
            {
                return NotFound();
            }

            project.Client = await _apiClient.ApiClientsGetAsync(project.ClientId);

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
                await _apiClient.ApiProjectsDeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(projectViewModel);
            }
        }
    }
}
