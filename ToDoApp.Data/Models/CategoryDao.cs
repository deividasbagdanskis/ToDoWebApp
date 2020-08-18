using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Commons.Interfaces;

namespace ToDoApp.Data.Models
{
    [Table("Category")]
    public class CategoryDao : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ToDoItemDao> ToDoItems { get; set; }
    }
}
