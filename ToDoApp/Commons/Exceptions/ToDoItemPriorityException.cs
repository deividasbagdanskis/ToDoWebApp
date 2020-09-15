using System;

namespace ToDoApp.Commons.Exceptions
{
    public class ToDoItemPriorityException : Exception
    {
        public ToDoItemPriorityException(int priority)
            : base($"Priority value of {priority} is invalid. Must be from 1 to 5.")
        {

        }
    }
}
