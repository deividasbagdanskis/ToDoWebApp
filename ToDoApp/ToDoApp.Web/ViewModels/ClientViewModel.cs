using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Web.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
    }
}
