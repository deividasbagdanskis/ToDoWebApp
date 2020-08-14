using ToDoApp.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
{
    public interface IInDbToDoItemTagProvider
    {
        //SampleWebAppContext Context { get; }

        Task Add(ToDoItemTag toDoItemTag);
        Task Delete(int? toDoItemId, int? tagId);
        Task<ToDoItemTag> Get(int? toDoItemId, int? tagId);
        Task<List<ToDoItemTag>> GetAll();
        Task Update(ToDoItemTag toDoItemTag);
        bool ItemExits(int toDoItemId, int tagId);
    }
}