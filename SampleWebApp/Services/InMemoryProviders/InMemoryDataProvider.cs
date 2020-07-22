using System.Collections.Generic;
using System.Linq;

namespace SampleWebApp.Services.InMemoryProviders
{
    public class InMemoryDataProvider<T> : IDataProvider<T> where T : IHasId
    {
        private static int _maxId = 0;

        private List<T> _data = new List<T>();

        public void Add(T item)
        {
            item.Id = ++_maxId;
            _data.Add(item);
        }

        public void Delete(int id)
        {
            bool itemExits = Get(id) != null;

            if (itemExits)
            {
                _data.RemoveAll(item => item.Id == id);
            }
            else
            {
                throw new ItemNotFoundException(id);
            }
        }

        public T Get(int id)
        {
            T foundItem = _data.FirstOrDefault(item => item.Id == id);

            if (foundItem == null)
            {
                throw new ItemNotFoundException(id);
            }

            return foundItem;
            
        }

        public List<T> GetAll()
        {
            return _data;
        }

        public void Update(T item)
        {
            T oldItem = Get(item.Id);
            
            if (oldItem != null)
            {
                _data.Insert(_data.IndexOf(oldItem), item);
                _data.Remove(oldItem);
            }
            else
            {
                throw new ItemNotFoundException(item.Id);
            }
        }
    }
}
