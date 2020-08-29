using AutoMapper;
using ToDoApp.Projects.ApiClient;
using ToDoApp.Web.ViewModels;

namespace ToDoApp.Web.Profiles
{
    public class ClientViewModelProfile : Profile
    {
        public ClientViewModelProfile()
        {
            CreateMap<Client, ClientViewModel>().ReverseMap();
        }
    }
}
