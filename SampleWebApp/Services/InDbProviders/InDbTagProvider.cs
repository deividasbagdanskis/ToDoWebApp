using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using SampleWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Services.InDbProviders
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

            return foundTag;
        }

        public async Task<List<Tag>> GetAll()
        {
            return await _context.Tag.ToListAsync();
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
