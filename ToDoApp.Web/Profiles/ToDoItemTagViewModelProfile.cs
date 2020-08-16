using AutoMapper;
using ToDoApp.Web.Models;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class ToDoItemTagViewModelProfile : Profile
    {
        public ToDoItemTagViewModelProfile()
        {
            CreateMap<ToDoItemTagDao, ToDoItemTagViewModel>().ReverseMap();
        }
    }
}
