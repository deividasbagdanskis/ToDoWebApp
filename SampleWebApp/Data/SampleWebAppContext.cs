using Microsoft.EntityFrameworkCore;
using SampleWebApp.Models;

namespace SampleWebApp.Data
{
    public class SampleWebAppContext : DbContext
    {
        public SampleWebAppContext (DbContextOptions<SampleWebAppContext> options)
            : base(options)
        {
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<ToDoItem> ToDoItem { get; set; }
    }
}
