using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InFileProviders
{
    public class InFileCategoryProvider : InFileDataProvider<CategoryVo>
    {
        public InFileCategoryProvider()
        {
            FilePath = @"..\ToDoApp\ToDoApp.Data\Data\categories.json";
        }
    }
}
