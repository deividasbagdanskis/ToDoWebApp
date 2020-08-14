using ToDoApp.Web.Services;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Web.Models
{
    [Table("Category")]
    public class CategoryDao : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ToDoItemDao> ToDoItems { get; set; }

        public CategoryDao()
        {

        }

        public CategoryDao(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
