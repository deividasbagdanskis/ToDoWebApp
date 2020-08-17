using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Business.Services.InDbProviders
{
    public interface IAsyncDbDataProvider<T>
    {
        Task<List<T>> GetAll();

        Task<T> Get(int? id);

        Task Add(T item);

        Task Update(T item);

        Task Delete(int id);

        bool ItemExits(int id);
    }
}
