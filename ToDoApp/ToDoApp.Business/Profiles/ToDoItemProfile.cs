using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Data.Models;

namespace ToDoApp.Business.Profiles
{
    class ToDoItemProfile : Profile
    {
        public ToDoItemProfile()
        {
            CreateMap<ToDoItemDao, ToDoItemVo>().ReverseMap();
        }
    }
}
