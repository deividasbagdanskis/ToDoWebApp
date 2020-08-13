using ToDoApp.Web.Services;
using System.Collections.Generic;

namespace ToDoApp.Web.Models
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
