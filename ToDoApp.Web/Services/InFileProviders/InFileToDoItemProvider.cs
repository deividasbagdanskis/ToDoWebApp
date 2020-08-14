using ToDoApp.Web.Models;

namespace ToDoApp.Web.Services.InFileProviders
{
    public class InFileToDoItemProvider : InFileDataProvider<ToDoItemDao>
    {
        public InFileToDoItemProvider()
        {
            FilePath = @"Data\todoItems.json";
        }
    }
}
