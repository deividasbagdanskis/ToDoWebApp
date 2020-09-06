using System;

namespace ToDoApp.Commons.Exceptions
{
    public class ToDoItemUniqueNameException : Exception
    {
        public ToDoItemUniqueNameException(string toDoItemName)
            : base($"ToDo item with name {toDoItemName} already exists.")
        {

        }
    }
}
