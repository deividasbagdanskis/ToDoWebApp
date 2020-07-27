using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using SampleWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Services.InDbProviders
{
    public class InDbToDoItemProvider : IAsyncDbDataProvider<ToDoItem>
    {
        public SampleWebAppContext Context { get; }

        public InDbToDoItemProvider(SampleWebAppContext context)
        {
            Context = context;
        }

        public async Task Add(ToDoItem toDoItem)
        {
            Context.Add(toDoItem);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var toDoItem = await Context.ToDoItem.FindAsync(id);
            Context.ToDoItem.Remove(toDoItem);
            await Context.SaveChangesAsync();
        }

        public async Task<ToDoItem> Get(int? id)
        {
            var foundToDoItem = await Context.ToDoItem.FindAsync(id);
            return foundToDoItem;
        }

        public async Task<List<ToDoItem>> GetAll()
        {
            return await Context.ToDoItem.ToListAsync();
        }

        public async Task Update(ToDoItem toDoItem)
        {
            Context.Update(toDoItem);
            Context.Entry(toDoItem).Property("CreationDate").IsModified = false;
            await Context.SaveChangesAsync();
        }
        public bool ItemExits(int id)
        {
            return Context.ToDoItem.Any(e => e.Id == id);
        }
    }
}
