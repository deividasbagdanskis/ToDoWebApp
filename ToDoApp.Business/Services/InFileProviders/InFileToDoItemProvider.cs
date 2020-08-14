using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InFileProviders
{
    public class InFileToDoItemProvider : InFileDataProvider<ToDoItem>
    {
        public InFileToDoItemProvider()
        {
            FilePath = @"Data\todoItems.json";
        }
    }
}
