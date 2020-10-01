using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
{
    public interface IAsyncDbDataProvider<T>
    {
        Task<IEnumerable<T>> GetAll(string userId);

        Task<T> Get(int? id, string userId);

        Task Add(T item);

        Task Update(T item);

        Task Delete(int id, string userId);

        bool ItemExits(int id);
    }
}
