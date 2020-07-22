using System;

namespace SampleWebApp.Services.InMemoryProviders
{
    public class ItemNotFoundException : Exception
    {
        public ItemNotFoundException(int id) : base($"Item with id: {id} was not found.")
        {
        }
    }
}