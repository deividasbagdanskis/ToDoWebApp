using SampleWebApp.Data;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebApp.Services.InDbProviders
{
    public interface IAsyncDbDataProvider<T>
    {
        public SampleWebAppContext Context { get; }

        Task<List<T>> GetAll();

        Task<T> Get(int? id);

        Task Add(T item);

        Task Update(T item);

        Task Delete(int id);

        bool ItemExits(int id);
    }
}
