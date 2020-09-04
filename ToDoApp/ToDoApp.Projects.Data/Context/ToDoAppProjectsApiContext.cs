using Microsoft.EntityFrameworkCore;
using ToDoApp.Projects.Data.Models;

namespace ToDoApp.Projects.Data.Context
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
