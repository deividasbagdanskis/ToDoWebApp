using ToDoApp.Commons.Interfaces;

namespace ToDoApp.Business.Models
{
    public class CategoryVo : IHasId
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public CategoryVo()
        {

        }

        public CategoryVo(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
