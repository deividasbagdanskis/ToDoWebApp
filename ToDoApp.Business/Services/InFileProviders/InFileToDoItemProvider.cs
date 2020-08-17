using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InFileProviders
{
    public class InFileToDoItemProvider : InFileDataProvider<ToDoItemVo>
    {
        public InFileToDoItemProvider()
        {
            FilePath = @"Data\todoItems.json";
        }
    }
}
