using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using ToDoApp.Exceptions;

namespace ToDoApp.Services.InFileProviders
{
    public class InFileDataProvider<T> : IDataProvider<T> where T : IHasId
    {
        private List<T> _data = new List<T>();
        protected string FilePath;

        public void Add(T item)
        {
            item.Id = _data.Count + 1;
            _data.Add(item);

            WriteToJson();
        }

        public void Delete(int id)
        {
            bool itemExits = Get(id) != null;

            if (itemExits)
            {
                _data.RemoveAll(item => item.Id == id);

                WriteToJson();
            }
            else
            {
                throw new ItemNotFoundException(id);
            }
        }

        public T Get(int id)
        {
            ReadJson();

            T foundItem = _data.FirstOrDefault(item => item.Id == id);

            if (foundItem == null)
            {
                throw new ItemNotFoundException(id);
            }

            return foundItem;
        }

        public List<T> GetAll()
        {
            ReadJson();

            return _data;
        }

        public void Update(T item)
        {
            T oldItem = Get(item.Id);

            if (oldItem != null)
            {
                _data.Insert(_data.IndexOf(oldItem), item);
                _data.Remove(oldItem);

                WriteToJson();
            }
            else
            {
                throw new ItemNotFoundException(item.Id);
            }
        }

        private void WriteToJson()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(_data, options);
            File.WriteAllText(FilePath, jsonString);
        }

        private void ReadJson()
        {
            string jsonString = File.ReadAllText(FilePath);
            _data = JsonSerializer.Deserialize<List<T>>(jsonString);
        }
    }
}
