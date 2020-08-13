using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ToDoApp.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ToDoItemTag> ToDoItemTags { get; set; }

        [NotMapped]
        [DisplayName("ToDo items")]
        public int ToDoItemNumber { get; set; }

        public Tag()
        {

        }
    }
}
