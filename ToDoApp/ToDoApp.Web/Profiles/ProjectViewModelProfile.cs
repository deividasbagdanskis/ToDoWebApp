using AutoMapper;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class ProjectViewModelProfile : Profile
    {
        public ProjectViewModelProfile()
        {
            CreateMap<Project, ProjectViewModel>().ReverseMap();
        }
    }
}
