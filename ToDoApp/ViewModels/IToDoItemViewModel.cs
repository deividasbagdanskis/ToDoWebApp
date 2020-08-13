using System.Threading.Tasks;

namespace ToDoApp.ViewModels
{
    public interface IToDoItemViewModel
    {
        Task SetCategoriesSelectList();

        Task RetrieveTags();
    }
}