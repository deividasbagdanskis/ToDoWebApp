using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Projects.Api.Data;
using ToDoApp.Projects.Api.Models;

namespace ToDoApp.Projects.Api.Controllers
{
    [Route("api/clients/{clientId}/projects")]
    [ApiController]
    public class ClientProjectsController : ControllerBase
    {
        private readonly ToDoAppProjectsApiContext _context;

        public ClientProjectsController(ToDoAppProjectsApiContext context)
        {
            _context = context;
        }

        // GET: api/clients/1/projects
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Project>>> GetClientProject(int clientId)
        {
            bool clientDoesNotExists = await _context.Client.FindAsync(clientId) == null;

            if (clientDoesNotExists)
            {
                return BadRequest();
            }

            var projects = await _context.Project.Where(p => p.ClientId == clientId).ToListAsync();

            if (projects == null)
            {
                return NotFound();
            }

            return projects;
        }

        // POST: api/clients/projects
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Project>> PostClientProject(int clientId, Project project)
        {
            bool clientDoesNotExists = await _context.Client.FindAsync(clientId) == null;

            if (clientDoesNotExists)
            {
                return BadRequest();
            }

            project.ClientId = clientId;

            _context.Project.Add(project);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetProject", new { controller = "Projects", id = project.Id}, project);
        }

        // PUT: api/clients/1/projects/2
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{projectId}")]
        public async Task<IActionResult> PutClientProject(int clientId, int projectId, Project project)
        {
            bool projectIdsDiffer = projectId != project.Id;
            bool clientIdsDiffer = clientId != project.ClientId;
            bool clientDoesNotExists = await _context.Client.FindAsync(clientId) == null;

            if (projectIdsDiffer || clientIdsDiffer || clientDoesNotExists)
            {
                return BadRequest();
            }

            _context.Entry(project).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProjectExists(projectId))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // DELETE: api/clients/1/projects/5
        [HttpDelete("{projectId}")]
        public async Task<ActionResult<Project>> DeleteProject(int clientId, int projectId)
        {
            bool clientDoesNotExists = await _context.Client.FindAsync(clientId) == null;

            if (clientDoesNotExists)
            {
                return BadRequest();
            }

            var project = await _context.Project.FindAsync(projectId);

            if (project == null)
            {
                return NotFound();
            }

            _context.Project.Remove(project);
            await _context.SaveChangesAsync();

            return project;
        }

        private bool ProjectExists(int id)
        {
            return _context.Project.Any(e => e.Id == id);
        }
    }
}
