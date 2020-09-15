using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IApiClient _apiClient;
        private readonly IMapper _mapper;

        public ClientsController(IApiClient apiClient, IMapper mapper)
        {
            _apiClient = apiClient;
            _mapper = mapper;
        }

        // GET: ClientController
        public async Task<ActionResult> Index()
        {
            IEnumerable<Client> clients = await _apiClient.ApiClientsGetAsync();

            return View(_mapper.Map<IEnumerable<ClientViewModel>>(clients));
        }

        // GET: ClientController/Details/5
        public async Task<ActionResult> Details(int id)
        {
            Client client = await _apiClient.ApiClientsGetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ClientViewModel>(client));
        }

        // GET: ClientController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ClientController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ClientViewModel clientViewModel)
        {
            Client client = _mapper.Map<Client>(clientViewModel);

            if (ModelState.IsValid)
            {
                await _apiClient.ApiClientsPostAsync(client);

                return RedirectToAction(nameof(Index));
            }

            return View(clientViewModel);
        }

        // GET: ClientController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            Client client = await _apiClient.ApiClientsGetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ClientViewModel>(client));
        }

        // POST: ClientController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                return BadRequest();
            }

            Client client = _mapper.Map<Client>(clientViewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    await _apiClient.ApiClientsPutAsync(id, client);
                }
                catch
                {
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(clientViewModel);
        }

        // GET: ClientController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            Client client = await _apiClient.ApiClientsGetAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return View(_mapper.Map<ClientViewModel>(client));
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(int id, ClientViewModel clientViewModel)
        {
            if (id != clientViewModel.Id)
            {
                return BadRequest();
            }

            try
            {
                await _apiClient.ApiClientsDeleteAsync(id);

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(clientViewModel);
            }
        }
    }
}
