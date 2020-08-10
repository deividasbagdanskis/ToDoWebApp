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

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ToDoItemTag>().HasKey(tt => new { tt.ToDoItemId, tt.TagId });

            modelBuilder.Entity<ToDoItemTag>()
                .HasOne<ToDoItem>(tt => tt.ToDoItem)
                .WithMany(t => t.ToDoItemTags)
                .HasForeignKey(tt => tt.ToDoItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoItemTag>()
                .HasOne<Tag>(tt => tt.Tag)
                .WithMany(t => t.ToDoItemTags)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<Category> Category { get; set; }

        public DbSet<ToDoItem> ToDoItem { get; set; }

        public DbSet<ToDoItemTag> ToDoItemTag { get; set; }

        public DbSet<Tag> Tag { get; set; }
    }
}
