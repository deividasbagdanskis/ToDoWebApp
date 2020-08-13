using ToDoApp.Models;
using ToDoApp.Services.InMemoryProviders;

namespace ToDoApp.Services
{
    public class InMemoryCategoryProvider : InMemoryDataProvider<Category>
    {
        public InMemoryCategoryProvider() : base()
        {
            Add(new Category(1, "Programming"));
            Add(new Category(2, "Free time"));
            Add(new Category(3, "Other"));
        }
    }
}
