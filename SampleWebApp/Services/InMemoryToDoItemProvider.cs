using SampleWebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApp.Services
{
    public class InMemoryToDoItemProvider : ITodoItemProvider
    {
        static private List<ToDoItem> _toDoItems = new List<ToDoItem>()
        {
            new ToDoItem(1, "Read a book", "", 3),
            new ToDoItem(2, "Alna task", "Make some progress on Alna software coding camp task", 4),
            new ToDoItem(3, "Go to the gym", "", 3)
        };

        public void Add(ToDoItem toDoItem)
        {
            toDoItem.Id = _toDoItems.Count + 1;
            _toDoItems.Add(toDoItem);
        }

        public void Delete(int id)
        {
            _toDoItems.RemoveAll(todo => todo.Id == id);
        }

        public ToDoItem Get(int id)
        {
            return _toDoItems.FirstOrDefault(todo => todo.Id == id);
        }

        public List<ToDoItem> GetAll()
        {
            return _toDoItems;
        }

        public void Update(ToDoItem toDoItem)
        {
            foreach (ToDoItem elem in _toDoItems)
            {
                if (elem.Id == toDoItem.Id)
                {
                    elem.Name = toDoItem.Name;
                    elem.Description = toDoItem.Description;
                    elem.Priority = toDoItem.Priority;
                }
            }
        }
    }
}
