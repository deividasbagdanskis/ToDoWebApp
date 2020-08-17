using AutoMapper;
using ToDoApp.Business.Models;
using ToDoApp.Data.Models;

namespace ToDoApp.Business.Profiles
{
    class CategoryProfile : Profile
    {
        public CategoryProfile()
        {
            CreateMap<CategoryDao, CategoryVo>().ReverseMap();
        }
    }
}
