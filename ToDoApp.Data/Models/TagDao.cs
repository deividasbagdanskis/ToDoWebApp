using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ToDoApp.Commons.Interfaces;

namespace ToDoApp.Data.Models
{
    [Table("Tag")]
    public class TagDao : IHasId
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<ToDoItemTagDao> ToDoItemTags { get; set; }

        [NotMapped]
        public int ToDoItemNumber { get; set; }
    }
}
