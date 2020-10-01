using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Models;

namespace ToDoApp.Data.Context
{
    public class SampleWebAppContext : IdentityDbContext<User>
    {
        public SampleWebAppContext (DbContextOptions<SampleWebAppContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ToDoItemTagDao>().HasKey(tt => new { tt.ToDoItemId, tt.TagId });

            modelBuilder.Entity<ToDoItemTagDao>()
                .HasOne<ToDoItemDao>(tt => tt.ToDoItem)
                .WithMany(t => t.ToDoItemTags)
                .HasForeignKey(tt => tt.ToDoItemId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ToDoItemTagDao>()
                .HasOne<TagDao>(tt => tt.Tag)
                .WithMany(t => t.ToDoItemTags)
                .HasForeignKey(tt => tt.TagId)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public DbSet<CategoryDao> Category { get; set; }

        public DbSet<ToDoItemDao> ToDoItem { get; set; }

        public DbSet<ToDoItemTagDao> ToDoItemTag { get; set; }

        public DbSet<TagDao> Tag { get; set; }
    }
}
