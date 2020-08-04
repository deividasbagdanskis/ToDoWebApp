﻿using Microsoft.EntityFrameworkCore;
using SampleWebApp.Data;
using SampleWebApp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Services.InDbProviders
{
    public class InDbToDoItemTagProvider : IInDbToDoItemTagProvider
    {
        public SampleWebAppContext Context { get; private set; }

        public InDbToDoItemTagProvider(SampleWebAppContext context)
        {
            Context = context;
        }

        public async Task Add(ToDoItemTag toDoItemTag)
        {
            Context.Add(toDoItemTag);
            await Context.SaveChangesAsync();
        }

        public async Task Delete(int? toDoItemId, int? tagId)
        {
            ToDoItemTag toDoItemTag = await Context.ToDoItemTag.FindAsync(toDoItemId, tagId);
            Context.ToDoItemTag.Remove(toDoItemTag);
            await Context.SaveChangesAsync();
        }

        public async Task<ToDoItemTag> Get(int? toDoItemId, int? tagId)
        {
            ToDoItemTag foundToDoItemTag = await Context.ToDoItemTag
                .Include(t => t.Tag)
                .Include(t => t.ToDoItem)
                .FirstOrDefaultAsync(m => m.ToDoItemId == toDoItemId && m.TagId == tagId);

            return foundToDoItemTag;
        }

        public async Task<List<ToDoItemTag>> GetAll()
        {
            var sampleWebAppContext = Context.ToDoItemTag.Include(t => t.Tag).Include(t => t.ToDoItem);
            return await sampleWebAppContext.ToListAsync();
        }

        public async Task Update(ToDoItemTag toDoItemTag)
        {
            Context.Update(toDoItemTag);
            await Context.SaveChangesAsync();
        }

        public bool ItemExits(int toDoItemId, int tagId)
        {
            return Context.ToDoItemTag.Any(e => e.ToDoItemId == toDoItemId && e.TagId == tagId);
        }
    }
}
