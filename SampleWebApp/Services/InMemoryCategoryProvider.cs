using SampleWebApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApp.Services
{
    public class InMemoryCategoryProvider : IDataProvider<Category>
    {
        static private List<Category> _categories = new List<Category>()
        {
            new Category(1, "Programming"),
            new Category(2, "Free time"),
            new Category(3, "Other")
        };

        public void Add(Category category)
        {
            category.Id = _categories.Count + 1;
            _categories.Add(category);
        }

        public void Delete(int id)
        {
            _categories.RemoveAll(category => category.Id == id);
        }

        public Category Get(int id)
        {
            return _categories.FirstOrDefault(category => category.Id == id);
        }

        public List<Category> GetAll()
        {
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
        }
    }
}
