using System;

namespace ToDoApp.Commons.Exceptions
{
    public class CategoryNameException : Exception
    {
        public CategoryNameException(string categoryName) 
            : base($"Category name {categoryName} is too short.\nMust be longer than 2 characters.")
        {

        }
    }
}
