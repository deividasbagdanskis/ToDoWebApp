using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Data.Models
{
    [Table("ToDoItemTag")]
    public class ToDoItemTagDao
    {
        public int ToDoItemId { get; set; }
        public ToDoItemDao ToDoItem { get; set; }

        public int TagId { get; set; }
        public TagDao Tag { get; set; }

        public string UserId { get; set; }

        public User User { get; set; }
    }
}
