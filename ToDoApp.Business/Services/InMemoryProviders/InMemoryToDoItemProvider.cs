using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InMemoryProviders
{
    public class InMemoryToDoItemProvider : InMemoryDataProvider<ToDoItem>
    {
        public InMemoryToDoItemProvider() : base()
        {
            Add(new ToDoItem(1, "Read a book", "", 3));
            Add(new ToDoItem(2, "Alna task", "Make progress on Alna software coding camp task", 4));
            Add(new ToDoItem(3, "Go to the gym", "", 3));
        }
    }
}
