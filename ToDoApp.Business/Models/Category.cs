using System.Collections.Generic;
using ToDoApp.Commons.Interfaces;

namespace ToDoApp.Business.Models
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
