using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InMemoryProviders
{
    public class InMemoryCategoryProvider : InMemoryDataProvider<CategoryVo>
    {
        public InMemoryCategoryProvider() : base()
        {
            Add(new CategoryVo(1, "Programming"));
            Add(new CategoryVo(2, "Free time"));
            Add(new CategoryVo(3, "Other"));
        }
    }
}
