using Microsoft.AspNetCore.Mvc.Rendering;
using SampleWebApp.Data;
using SampleWebApp.Models;
using SampleWebApp.Services.InDbProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SampleWebApp.ViewModels
{
    public class ToDoItemViewModel : IToDoItemViewModel
    {
        private IAsyncDbDataProvider<Category> _categoryProvider;

        private List<Category> _categories = new List<Category>();

        public ToDoItem ToDoItem { get; set; }

        public List<SelectListItem> CategoriesSelectList { get; private set; }

        public ToDoItemViewModel()
        {
        }

        public ToDoItemViewModel(SampleWebAppContext context)
        {
            _categoryProvider = new InDbCategoryProvider(context);
        }

        public async Task SetCategoriesSelectList()
        {
            await RetrieveCategories();

            CategoriesSelectList = new List<SelectListItem>();

            CategoriesSelectList.Add(new SelectListItem() { Text = "Uncategorized", Value = "0" });

            foreach (Category category in _categories)
            {
                CategoriesSelectList.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
        }

        private async Task RetrieveCategories()
        {
            _categories = await _categoryProvider.GetAll();
        }
    }
}
