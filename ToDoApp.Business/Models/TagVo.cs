using System.Collections.Generic;
using ToDoApp.Commons.Interfaces;

namespace ToDoApp.Business.Models
{
    public class TagVo : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public List<ToDoItemTagVo> ToDoItemTags { get; set; }

        public int ToDoItemNumber { get; set; }
    }
}
