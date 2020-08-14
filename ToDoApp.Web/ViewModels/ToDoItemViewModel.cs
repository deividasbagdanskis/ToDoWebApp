using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApp.Web.Data;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ToDoApp.Web.ViewModels
{
    public class ToDoItemViewModel : IToDoItemViewModel
    {
        private IAsyncDbDataProvider<CategoryDao> _categoryProvider;

        private IAsyncDbDataProvider<TagDao> _tagProvider;

        private List<CategoryDao> _categories = new List<CategoryDao>();

        public ToDoItemDao ToDoItem { get; set; }

        public List<SelectListItem> CategoriesSelectList { get; private set; }

        public List<TagDao> Tags { get; set; }

        public ToDoItemViewModel()
        {
        }

        public ToDoItemViewModel(SampleWebAppContext context)
        {
            _categoryProvider = new InDbCategoryProvider(context);
            _tagProvider = new InDbTagProvider(context);
        }

        public async Task SetCategoriesSelectList()
        {
            await RetrieveCategories();

            CategoriesSelectList = new List<SelectListItem>();

            CategoriesSelectList.Add(new SelectListItem() { Text = "Uncategorized", Value = "0" });

            foreach (CategoryDao category in _categories)
            {
                CategoriesSelectList.Add(new SelectListItem() { Text = category.Name, Value = category.Id.ToString() });
            }
        }

        private async Task RetrieveCategories()
        {
            _categories = await _categoryProvider.GetAll();
        }

        public async Task RetrieveTags()
        {
            Tags = await _tagProvider.GetAll();
        }
    }
}
