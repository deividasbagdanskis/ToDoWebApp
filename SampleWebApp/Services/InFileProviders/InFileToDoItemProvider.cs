using SampleWebApp.Models;

namespace SampleWebApp.Services.InFileProviders
{
    public class InFileToDoItemProvider : InFileDataProvider<ToDoItem>
    {
        public InFileToDoItemProvider()
        {
            FilePath = @"Data\todoItems.json";
        }
    }
}
