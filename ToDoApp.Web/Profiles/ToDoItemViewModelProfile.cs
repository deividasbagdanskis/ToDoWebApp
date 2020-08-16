using AutoMapper;
using ToDoApp.Web.Models;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class ToDoItemViewModelProfile : Profile
    {
        public ToDoItemViewModelProfile()
        {
            CreateMap<ToDoItemDao, ToDoItemViewModel>().ReverseMap();
        }
    }
}
