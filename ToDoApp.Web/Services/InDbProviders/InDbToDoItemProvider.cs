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
        public SampleWebAppContext Context { get; }

        public InDbToDoItemProvider(SampleWebAppContext context)
        {
            Context = context;
        }

        public async Task Add(ToDoItemDao toDoItem)
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

        public async Task<ToDoItemDao> Get(int? id)
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

        public async Task<List<ToDoItemDao>> GetAll()
        {
            return await Context.ToDoItem.Include(t => t.Category).ToListAsync();
        }

        public async Task Update(ToDoItemDao toDoItem)
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
