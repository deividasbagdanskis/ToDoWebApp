using AutoMapper;
using ToDoApp.Business.Models;
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
