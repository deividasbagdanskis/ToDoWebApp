using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InFileProviders
{
    public class InFileCategoryProvider : InFileDataProvider<Category>
    {
        public InFileCategoryProvider()
        {
            FilePath = @"Data\categories.json";
        }
    }
}
