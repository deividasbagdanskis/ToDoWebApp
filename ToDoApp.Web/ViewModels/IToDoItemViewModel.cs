using System.Threading.Tasks;

namespace ToDoApp.Web.ViewModels
{
    public interface IToDoItemViewModel
    {
        Task SetCategoriesSelectList();

        Task RetrieveTags();
    }
}