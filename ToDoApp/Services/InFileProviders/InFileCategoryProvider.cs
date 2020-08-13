using ToDoApp.Models;

namespace ToDoApp.Services.InFileProviders
{
    public class InFileCategoryProvider : InFileDataProvider<Category>
    {
        public InFileCategoryProvider()
        {
            FilePath = @"Data\categories.json";
        }
    }
}
