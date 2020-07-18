using SampleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SampleWebApp.Services
{
    public interface ITodoItemProvider
    {
        List<ToDoItem> GetAll();

        ToDoItem Get(int id);

        void Add(ToDoItem toDoItem);

        void Update(ToDoItem toDoItem);

        void Delete(int id);
    }
}
