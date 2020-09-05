using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using Xunit;

namespace ToDoApp.Data.Tests
{
    public class CategoryProviderTests
    {
        private readonly Mock<IAsyncDbDataProvider<CategoryVo>> _categoryProvider;
        private readonly CategoryVo _category;

        public CategoryProviderTests()
        {
            _categoryProvider = new Mock<IAsyncDbDataProvider<CategoryVo>>();
            _category = new CategoryVo(4, "test");
        }

        [Fact]
        public async Task TestGetASpecificCategory()
        {
            _categoryProvider.Setup(o => o.Get(It.IsAny<int>())).ReturnsAsync(new CategoryVo() { Name = "new" });

            CategoryVo returnedToDoItem = await _categoryProvider.Object.Get(8);

            Assert.Equal("new", returnedToDoItem.Name);
        }

        [Fact]
        public async Task TestGetListOfCatagories()
        {
            List<CategoryVo> categories = new List<CategoryVo>
            {
                new CategoryVo(1, "Programming"),
                new CategoryVo(2, "Free time"),
                new CategoryVo(3, "Other")
            };

            _categoryProvider.Setup(o => o.GetAll()).ReturnsAsync(categories);

            List<CategoryVo> returnedToDoItems = (List<CategoryVo>)await _categoryProvider.Object.GetAll();
            
            Assert.Equal(3, returnedToDoItems.Count);
        }

        [Fact]
        public async Task TestAddCategory()
        { 
            await _categoryProvider.Object.Add(_category);

            _categoryProvider.Verify(o => o.Add(_category), Times.Once);
        }

        [Fact]
        public async Task TestModifyCategory()
        {
            _category.Name = "Updated";

            await _categoryProvider.Object.Update(_category);

            _categoryProvider.Verify(o => o.Update(_category), Times.Once);
        }

        [Fact]
        public async Task TestDeleteCategory()
        {
            await _categoryProvider.Object.Delete(3);

            _categoryProvider.Verify(o => o.Delete(3), Times.Once);
        }
    }
}
