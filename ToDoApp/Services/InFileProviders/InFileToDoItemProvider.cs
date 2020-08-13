using ToDoApp.Models;

namespace ToDoApp.Services.InFileProviders
{
    public class InFileToDoItemProvider : InFileDataProvider<ToDoItem>
    {
        public InFileToDoItemProvider()
        {
            FilePath = @"Data\todoItems.json";
        }
    }
}
