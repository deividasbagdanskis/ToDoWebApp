using System;

namespace ToDoApp.Commons.Exceptions
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int id) : base($"Item with id: {id} was not found.")
        {
        }
    }
}