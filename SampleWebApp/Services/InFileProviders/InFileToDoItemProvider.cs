using SampleWebApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace SampleWebApp.Services.InFileProviders
{
    public class InFileToDoItemProvider : IDataProvider<ToDoItem>
    {
        private List<ToDoItem> _toDoItems = new List<ToDoItem>();
        private string _filePath = @"Data\todoItems.json";

        public void Add(ToDoItem toDoItem)
        {
            toDoItem.Id = _toDoItems.Count + 1;
            _toDoItems.Add(toDoItem);

            WriteToJson();
        }

        public void Delete(int id)
        {
            _toDoItems.RemoveAll(todo => todo.Id == id);

            WriteToJson();
        }

        public ToDoItem Get(int id)
        {
            return _toDoItems.FirstOrDefault(todo => todo.Id == id);
        }

        public List<ToDoItem> GetAll()
        {
            string jsonString = File.ReadAllText(_filePath);
            _toDoItems = JsonSerializer.Deserialize<List<ToDoItem>>(jsonString);

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

            WriteToJson();
        }

        private void WriteToJson()
        {
            JsonSerializerOptions options = new JsonSerializerOptions() { WriteIndented = true };

            string jsonString = JsonSerializer.Serialize(_toDoItems, options);
            File.WriteAllText(_filePath, jsonString);
        }
    }
}
