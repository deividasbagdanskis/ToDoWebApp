using Microsoft.EntityFrameworkCore;
using ToDoApp.Data;
using ToDoApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Services.InDbProviders
{
    public class InDbCategoryProvider : IAsyncDbDataProvider<Category>
    {
        private SampleWebAppContext _context;

        public SampleWebAppContext Context { get; }

        public InDbCategoryProvider(SampleWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(Category category)
        {
            _context.Add(category);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var category = await _context.Category.FindAsync(id);
            _context.Category.Remove(category);
            await _context.SaveChangesAsync();
        }

        public async Task<Category> Get(int? id)
        {
            var foundCategory = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            return foundCategory;
        }

        public async Task<List<Category>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task Update(Category category)
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
        public bool ItemExits(int id)
        {
            return _context.Category.Any(e => e.Id == id);
        }
    }
}
