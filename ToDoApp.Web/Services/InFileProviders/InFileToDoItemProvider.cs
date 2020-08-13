using ToDoApp.Web.Models;

namespace ToDoApp.Web.Services.InFileProviders
{
    public class InFileToDoItemProvider : InFileDataProvider<ToDoItem>
    {
        public InFileToDoItemProvider()
        {
            FilePath = @"Data\todoItems.json";
        }
    }
}
