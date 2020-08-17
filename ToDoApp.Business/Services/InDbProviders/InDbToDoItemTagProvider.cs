using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Data;
using ToDoApp.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbToDoItemTagProvider : IInDbToDoItemTagProvider
    {
        private SampleWebAppContext _context;

        public InDbToDoItemTagProvider(SampleWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(ToDoItemTagDao toDoItemTag)
        {
            _context.Add(toDoItemTag);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int? toDoItemId, int? tagId)
        {
            ToDoItemTagDao toDoItemTag = await _context.ToDoItemTag.FindAsync(toDoItemId, tagId);
            _context.ToDoItemTag.Remove(toDoItemTag);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItemTagDao> Get(int? toDoItemId, int? tagId)
        {
            ToDoItemTagDao foundToDoItemTag = await _context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(m => m.ToDoItemId == toDoItemId && m.TagId == tagId);

            return foundToDoItemTag;
        }

        public async Task<List<ToDoItemTagDao>> GetAll()
        {
            var toDoItemTags = _context.ToDoItemTag.Include(t => t.Tag).Include(t => t.ToDoItem);
            return await toDoItemTags.ToListAsync();
        }

        public async Task Update(ToDoItemTagDao toDoItemTag)
        {
            _context.Update(toDoItemTag);
            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int toDoItemId, int tagId)
        {
            return _context.ToDoItemTag.Any(e => e.ToDoItemId == toDoItemId && e.TagId == tagId);
        }
    }
}
