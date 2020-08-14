using Microsoft.EntityFrameworkCore;
using ToDoApp.Business.Data;
using ToDoApp.Business.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
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

            try
            {
                foundToDoItem.Category = Context.Category.Single(c => c.Id == foundToDoItem.CategoryId);
            }
            catch (InvalidOperationException)
            {
                
            }
            return foundToDoItem;
        }

        public async Task<List<ToDoItem>> GetAll()
        {
            return await Context.ToDoItem.Include(t => t.Category).ToListAsync();
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
