using AutoMapper;
using ToDoApp.Web.Models;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class TagViewModelProfile : Profile
    {
        public TagViewModelProfile()
        {
            CreateMap<TagDao, TagViewModel>().ReverseMap();
        }
    }
}
