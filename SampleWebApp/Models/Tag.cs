using System.ComponentModel.DataAnnotations;

namespace SampleWebApp.Models
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public Tag()
        {

        }
    }
}
