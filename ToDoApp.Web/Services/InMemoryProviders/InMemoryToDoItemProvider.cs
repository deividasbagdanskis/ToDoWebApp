using ToDoApp.Web.Models;

namespace ToDoApp.Web.Services.InMemoryProviders
{
    public class InMemoryToDoItemProvider : InMemoryDataProvider<ToDoItemDao>
    {
        public InMemoryToDoItemProvider() : base()
        {
            Add(new ToDoItemDao(1, "Read a book", "", 3));
            Add(new ToDoItemDao(2, "Alna task", "Make progress on Alna software coding camp task", 4));
            Add(new ToDoItemDao(3, "Go to the gym", "", 3));
        }
    }
}
