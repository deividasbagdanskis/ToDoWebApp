using AutoMapper;
using ToDoApp.Business.Models;
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
