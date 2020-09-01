using System.Collections.Generic;
using ToDoApp.Business.Models;

namespace ToDoApp.Business.Services.InDbProviders
{
    public interface IInDbProjectToDoItemProvider
    {
        IEnumerable<ToDoItemVo> GetToDoItemsByProjectId(int projectId);
    }
}
