using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class ToDoItemViewModelProfile : Profile
    {
        public ToDoItemViewModelProfile()
        {
            CreateMap<ToDoItemVo, ToDoItemViewModel>().ReverseMap();
        }
    }
}
