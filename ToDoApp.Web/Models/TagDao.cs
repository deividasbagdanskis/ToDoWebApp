using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Web.Models
{
    [Table("Tag")]
    public class TagDao
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ToDoItemTagDao> ToDoItemTags { get; set; }

        [NotMapped]
        [DisplayName("ToDo items")]
        public int ToDoItemNumber { get; set; }
    }
}
