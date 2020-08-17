using Microsoft.EntityFrameworkCore;
using ToDoApp.Data.Data;
using ToDoApp.Data.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
{
    public class InDbCategoryProvider : IAsyncDbDataProvider<CategoryDao>
    {
        private SampleWebAppContext _context;

        public InDbCategoryProvider(SampleWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(CategoryDao category)
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

        public async Task<CategoryDao> Get(int? id)
        {
            var foundCategory = await _context.Category.FirstOrDefaultAsync(m => m.Id == id);

            return foundCategory;
        }

        public async Task<List<CategoryDao>> GetAll()
        {
            return await _context.Category.ToListAsync();
        }

        public async Task Update(CategoryDao category)
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
