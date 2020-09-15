using System;

namespace ToDoApp.Commons.Exceptions
{
    public class ToDoItemException : Exception
    {
        public ToDoItemException(string message) : base(message)
        {

        }
    }
}
