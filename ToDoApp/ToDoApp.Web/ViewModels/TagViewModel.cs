using System.ComponentModel;

namespace ToDoApp.Web.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [DisplayName("ToDo items")]
        public int ToDoItemNumber { get; set; }
    }
}
