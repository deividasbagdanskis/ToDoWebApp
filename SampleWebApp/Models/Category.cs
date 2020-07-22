using SampleWebApp.Services;

namespace SampleWebApp.Models
{
    public class Category : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public Category()
        {

        }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
