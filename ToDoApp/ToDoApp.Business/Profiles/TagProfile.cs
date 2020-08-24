using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Data.Models;

namespace ToDoApp.Business.Profiles
{
    class TagProfile : Profile
    {
        public TagProfile()
        {
            CreateMap<TagDao, TagVo>().ReverseMap();
        }
    }
}
