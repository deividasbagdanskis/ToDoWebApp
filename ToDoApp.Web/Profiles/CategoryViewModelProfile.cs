using AutoMapper;
using ToDoApp.Web.Models;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class CategoryViewModelProfile : Profile
    {
        public CategoryViewModelProfile()
        {
            CreateMap<CategoryDao, CategoryViewModel>().ReverseMap();
        }
    }
}
