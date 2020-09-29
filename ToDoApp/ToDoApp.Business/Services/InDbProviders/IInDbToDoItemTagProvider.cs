using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InDbProviders
{
    public interface IInDbToDoItemTagProvider
    {
        Task Add(ToDoItemTagVo toDoItemTag);
        Task Delete(int? toDoItemId, int? tagId, string userId);
        Task<ToDoItemTagVo> Get(int? toDoItemId, int? tagId, string userId);
        Task<IEnumerable<ToDoItemTagVo>> GetAll(string userId);
        Task Update(ToDoItemTagVo toDoItemTag);
        bool ItemExits(int toDoItemId, int tagId);
    }
}