using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbTagProvider : IAsyncDbDataProvider<Tag>
    {
        private SampleWebAppContext _context;

        public SampleWebAppContext Context { get; }

        public InDbTagProvider(SampleWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(Tag tag)
        {
            _context.Add(tag);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tag = await _context.Tag.FindAsync(id);
            _context.Tag.Remove(tag);
            await _context.SaveChangesAsync();
        }

        public async Task<Tag> Get(int? id)
        {
            var foundTag = await _context.Tag.FirstOrDefaultAsync(t => t.Id == id);

            foundTag.ToDoItemNumber = _context.ToDoItemTag.Where(t => t.TagId == foundTag.Id).Count();

            return foundTag;
        }

        public async Task<List<Tag>> GetAll()
        {
            List<Tag> tags = await _context.Tag.ToListAsync();

            foreach (Tag tag in tags)
            {
                tag.ToDoItemNumber = _context.ToDoItemTag.Where(t => t.TagId == tag.Id).Count();
            }

            return tags;
        }

        public async Task Update(Tag tag)
        {
            _context.Update(tag);
            await _context.SaveChangesAsync();
        }

        public bool ItemExits(int id)
        {
            return _context.Tag.Any(e => e.Id == id);
        }
    }
}
