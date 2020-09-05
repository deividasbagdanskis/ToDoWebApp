using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using Xunit;

namespace ToDoApp.Data.Tests
{
    public class ToDoItemProviderTests
    {
        private readonly Mock<IAsyncDbDataProvider<ToDoItemVo>> _toDoItemProvider;
        private readonly ToDoItemVo _toDoItem;

        public ToDoItemProviderTests()
        {
            _toDoItemProvider = new Mock<IAsyncDbDataProvider<ToDoItemVo>>();
            _toDoItem = new ToDoItemVo(4, "Lorem ipsum", "Lorem ipsum", 4);
        }

        [Fact]
        public async Task TestGetASpecificToDoItem()
        {
            _toDoItemProvider.Setup(o => o.Get(It.IsAny<int>())).ReturnsAsync(_toDoItem);

            ToDoItemVo returnedToDoItem = await _toDoItemProvider.Object.Get(7);

            Assert.Equal(_toDoItem.Name, returnedToDoItem.Name);
        }

        [Fact]
        public async Task TestGetListOfToDoItems()
        {
            List<ToDoItemVo> todoItems = new List<ToDoItemVo>
            {
                new ToDoItemVo() { Id = 1, Name = "Read a book", Description = "", Priority = 3 },
                new ToDoItemVo() { Id = 2, Name = "Alna task",
                    Description = "Make progress on Alna software coding camp task", Priority = 4 },
                new ToDoItemVo() { Id = 3, Name = "Go to the gym", Description = "", Priority = 3 }
            };

            _toDoItemProvider.Setup(o => o.GetAll()).ReturnsAsync(todoItems);

            List<ToDoItemVo> returnedToDoItems = (List<ToDoItemVo>)await _toDoItemProvider.Object.GetAll();

            Assert.Equal(3, returnedToDoItems.Count);
        }

        [Fact]
        public async Task TestAddToDoItem()
        {
            await _toDoItemProvider.Object.Add(_toDoItem);

            _toDoItemProvider.Verify(o => o.Add(_toDoItem), Times.Once);
        }

        [Fact]
        public async Task TestModifyToDoItem()
        {
            _toDoItem.Name = "Updated";

            await _toDoItemProvider.Object.Update(_toDoItem);

            _toDoItemProvider.Verify(o => o.Update(_toDoItem), Times.Once);
        }

        [Fact]
        public async Task TestDeleteToDoItem()
        {
            await _toDoItemProvider.Object.Delete(3);

            _toDoItemProvider.Verify(o => o.Delete(3), Times.Once);
        }
    }
}
