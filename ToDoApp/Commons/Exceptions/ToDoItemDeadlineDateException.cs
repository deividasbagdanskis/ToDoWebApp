using System;

namespace ToDoApp.Commons.Exceptions
{
    public class ToDoItemDeadlineDateException : Exception
    {
        public ToDoItemDeadlineDateException()
            : base($"Deadline date must be later than {DateTime.Today}")
        {

        }
    }
}
