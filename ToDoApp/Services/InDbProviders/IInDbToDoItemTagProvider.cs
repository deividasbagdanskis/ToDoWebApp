using ToDoApp.Data;
using ToDoApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Services.InDbProviders
{
    public interface IInDbToDoItemTagProvider
    {
        SampleWebAppContext Context { get; }

        Task Add(ToDoItemTag toDoItemTag);
        Task Delete(int? toDoItemId, int? tagId);
        Task<ToDoItemTag> Get(int? toDoItemId, int? tagId);
        Task<List<ToDoItemTag>> GetAll();
        Task Update(ToDoItemTag toDoItemTag);
        bool ItemExits(int toDoItemId, int tagId);
    }
}