using SampleWebApp.Models;

namespace SampleWebApp.Services.InFileProviders
{
    public class InFileCategoryProvider : InFileDataProvider<Category>
    {
        public InFileCategoryProvider()
        {
            FilePath = @"Data\categories.json";
        }
    }
}
