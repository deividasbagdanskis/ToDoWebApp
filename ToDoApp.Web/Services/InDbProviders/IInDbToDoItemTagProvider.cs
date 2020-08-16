using ToDoApp.Web.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Web.Services.InDbProviders
{
    public interface IInDbToDoItemTagProvider
    {
        Task Add(ToDoItemTagDao toDoItemTag);
        Task Delete(int? toDoItemId, int? tagId);
        Task<ToDoItemTagDao> Get(int? toDoItemId, int? tagId);
        Task<List<ToDoItemTagDao>> GetAll();
        Task Update(ToDoItemTagDao toDoItemTag);
        bool ItemExits(int toDoItemId, int tagId);
    }
}