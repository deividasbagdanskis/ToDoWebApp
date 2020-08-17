using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InMemoryProviders
{
    public class InMemoryToDoItemProvider : InMemoryDataProvider<ToDoItemVo>
    {
        public InMemoryToDoItemProvider() : base()
        {
            Add(new ToDoItemVo(1, "Read a book", "", 3));
            Add(new ToDoItemVo(2, "Alna task", "Make progress on Alna software coding camp task", 4));
            Add(new ToDoItemVo(3, "Go to the gym", "", 3));
        }
    }
}
