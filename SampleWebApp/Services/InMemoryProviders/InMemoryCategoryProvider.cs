using SampleWebApp.Models;
using SampleWebApp.Services.InMemoryProviders;
using System.Collections.Generic;
using System.Linq;

namespace SampleWebApp.Services
{
    public class InMemoryCategoryProvider : InMemoryDataProvider<Category>
    {
        public InMemoryCategoryProvider() : base()
        {
            Add(new Category(1, "Programming"));
            Add(new Category(2, "Free time"));
            Add(new Category(3, "Other"));
        }
    }
}
