using Microsoft.EntityFrameworkCore;
using ToDoApp.Projects.Api.Models;

namespace ToDoApp.Projects.Api.Data
{
    public class ToDoAppProjectsApiContext : DbContext
    {
        public ToDoAppProjectsApiContext (DbContextOptions<ToDoAppProjectsApiContext> options)
            : base(options)
        {
        }

        public DbSet<Client> Client { get; set; }

        public DbSet<Project> Project { get; set; }
    }
}
