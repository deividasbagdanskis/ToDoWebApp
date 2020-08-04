using System.Threading.Tasks;

namespace SampleWebApp.ViewModels
{
    public interface IToDoItemViewModel
    {
        Task SetCategoriesSelectList();

        Task RetrieveTags();
    }
}