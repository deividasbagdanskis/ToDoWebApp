using SampleWebApp.Services;
using System.Collections.Generic;

namespace SampleWebApp.Models
{
    public class Category : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ToDoItem> ToDoItems { get; set; }

        public Category()
        {

        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
