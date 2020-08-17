using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Data.Models;

namespace ToDoApp.Business.Profiles
{
    class ToDoItemTagProfile : Profile
    {
        public ToDoItemTagProfile()
        {
            CreateMap<ToDoItemDao, ToDoItemTagVo>().ReverseMap();
        }
    }
}
