using Microsoft.AspNetCore.Mvc.Rendering;
using ToDoApp.Web.Data;
using ToDoApp.Web.Models;
using ToDoApp.Web.Services.InDbProviders;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System;
using ToDoApp.Commons.Enums;

namespace ToDoApp.Web.ViewModels
{
    public class ToDoItemViewModel : IToDoItemViewModel
    {
        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public DateTime? DeadlineDate { get; set; }

        private int _priority = 3;

        [Required]
        [Range(1, 5)]
        public int Priority
        {
            get { return _priority; }
            set
            {
                if (value > 5)
                    _priority = 5;
                else if (value > 0 && value < 6)
                    _priority = value;
            }
        }

        public StatusEnum Status { get; set; } = StatusEnum.Backlog;

        public int? CategoryId { get; set; }

        public CategoryDao Category { get; set; }

        private IAsyncDbDataProvider<CategoryDao> _categoryProvider;

        private List<CategoryDao> _categories = new List<CategoryDao>();

        public ToDoItemDao ToDoItem { get; set; }

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

            foreach (CategoryDao category in _categories)
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
