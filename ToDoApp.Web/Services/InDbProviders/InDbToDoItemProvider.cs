using Microsoft.EntityFrameworkCore;
using ToDoApp.Web.Data;
using ToDoApp.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Web.Services.InDbProviders
{
    public class InDbToDoItemProvider : IAsyncDbDataProvider<ToDoItemDao>
    {
        private SampleWebAppContext _context;

        public InDbToDoItemProvider(SampleWebAppContext context)
        {
            _context = context;
        }

        public async Task Add(ToDoItemDao toDoItem)
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

        public async Task<ToDoItemDao> Get(int? id)
        {
            var foundToDoItem = await _context.ToDoItem.FindAsync(id);

            try
            {
                foundToDoItem.Category = _context.Category.Single(c => c.Id == foundToDoItem.CategoryId);
            }
            catch (InvalidOperationException)
            {
                
            }
            return foundToDoItem;
        }

        public async Task<List<ToDoItemDao>> GetAll()
        {
            return await _context.ToDoItem.Include(t => t.Category).ToListAsync();
        }

        public async Task Update(ToDoItemDao toDoItem)
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
