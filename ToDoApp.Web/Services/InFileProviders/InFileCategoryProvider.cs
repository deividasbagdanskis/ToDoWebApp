using ToDoApp.Web.Models;

namespace ToDoApp.Web.Services.InFileProviders
{
    public class InFileCategoryProvider : InFileDataProvider<CategoryDao>
    {
        public InFileCategoryProvider()
        {
            FilePath = @"Data\categories.json";
        }
    }
}
