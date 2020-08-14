using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Data;
using ToDoApp.Web.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Web.Services.InDbProviders
{
    public class InDbToDoItemTagProvider : IInDbToDoItemTagProvider
    {
        public SampleWebAppContext Context { get; private set; }

        public InDbToDoItemTagProvider(SampleWebAppContext context)
        {
            Context = context;
        }

        public async Task Add(ToDoItemTagDao toDoItemTag)
        {
            Context.Add(toDoItemTag);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int? toDoItemId, int? tagId)
        {
            ToDoItemTagDao toDoItemTag = await Context.ToDoItemTag.FindAsync(toDoItemId, tagId);
            Context.ToDoItemTag.Remove(toDoItemTag);
            await Context.SaveChangesAsync();
        }

        public async Task<ToDoItemTagDao> Get(int? toDoItemId, int? tagId)
        {
            ToDoItemTagDao foundToDoItemTag = await Context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(m => m.ToDoItemId == toDoItemId && m.TagId == tagId);

            return foundToDoItemTag;
        }

        public async Task<List<ToDoItemTagDao>> GetAll()
        {
            var sampleWebAppContext = Context.ToDoItemTag.Include(t => t.Tag).Include(t => t.ToDoItem);
            return await sampleWebAppContext.ToListAsync();
        }

        public async Task Update(ToDoItemTagDao toDoItemTag)
        {
            Context.Update(toDoItemTag);
            await Context.SaveChangesAsync();
        }

        public bool ItemExits(int toDoItemId, int tagId)
        {
            return Context.ToDoItemTag.Any(e => e.ToDoItemId == toDoItemId && e.TagId == tagId);
        }
    }
}
