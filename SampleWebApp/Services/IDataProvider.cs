using System.Collections.Generic;

namespace SampleWebApp.Services
{
    public interface IDataProvider<T>
    {
        List<T> GetAll();

        T Get(int id);

        void Add(T item);

        void Update(T item);

        void Delete(int id);
    }
}
