using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using SampleWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Services.InDbProviders
{
    public class InDbToDoItemProvider : IAsyncDataProvider<ToDoItem>
    {
        private SampleWebAppContext _context;

        public InDbToDoItemProvider(SampleWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(ToDoItem toDoItem)
        {
            _context.Add(toDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toDoItem = await _context.ToDoItem.FindAsync(id);
            _context.ToDoItem.Remove(toDoItem);
            await _context.SaveChangesAsync();
        }

        public async Task<ToDoItem> Get(int? id)
        {
            var foundToDoItem = await _context.ToDoItem.FindAsync(id);
            return foundToDoItem;
        }

        public async Task<List<ToDoItem>> GetAll()
        {
            return await _context.ToDoItem.ToListAsync();
        }

        public async Task Update(ToDoItem toDoItem)
        {
            _context.Update(toDoItem);
            _context.Entry(toDoItem).Property("CreationDate").IsModified = false;
            await _context.SaveChangesAsync();
        }
        public bool ItemExits(int id)
        {
            return _context.ToDoItem.Any(e => e.Id == id);
        }
    }
}
