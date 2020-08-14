using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InMemoryProviders;

namespace ToDoApp.Web.Services
{
    public class InMemoryCategoryProvider : InMemoryDataProvider<CategoryDao>
    {
        public InMemoryCategoryProvider() : base()
        {
            Add(new CategoryDao(1, "Programming"));
            Add(new CategoryDao(2, "Free time"));
            Add(new CategoryDao(3, "Other"));
        }
    }
}
