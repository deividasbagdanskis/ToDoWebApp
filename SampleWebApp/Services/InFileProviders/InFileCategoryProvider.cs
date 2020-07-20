using SampleWebApp.Models;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SampleWebApp.Services.InFileProviders
{
    public class InFileCategoryProvider : IDataProvider<Category>
    {
        private List<Category> _categories = new List<Category>();
        private string _filePath = @"Data\categories.json";

        public void Add(Category category)
        {
            category.Id = _categories.Count + 1;
            _categories.Add(category);

            WriteToJson();
        }

        public void Delete(int id)
        {
            _categories.RemoveAll(category => category.Id == id);

            WriteToJson();
        }

        public Category Get(int id)
        {
            return _categories.FirstOrDefault(category => category.Id == id);
        }

        public List<Category> GetAll()
        {
            string jsonString = File.ReadAllText(_filePath);
            _categories = JsonSerializer.Deserialize<List<Category>>(jsonString);

            return _categories;
        }

        public void Update(Category category)
        {
            foreach (Category elem in _categories)
            {
                if (elem.Id == category.Id)
                {
                    elem.Name = category.Name;
                }
            }

            WriteToJson();
        }

        private void WriteToJson()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(_categories, options);
            File.WriteAllText(_filePath, jsonString);
        }
    }
}
