using Moq;
using System;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Commons.Enums;
using ToDoApp.Commons.Exceptions;
using Xunit;

namespace ToDoApp.Business.Tests
{
    public class ToDoItemProviderTests
    {
        private Mock<IAsyncDbDataProvider<ToDoItemVo>> _toDoItemProvider;

        [Fact]
        public async Task TestThereCanOnlyBe1ToDoItemWithWipStatusAndPriority1WhenCreated()
        {
            _toDoItemProvider = new Mock<IAsyncDbDataProvider<ToDoItemVo>>();

            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 1,
                Status = StatusEnum.Wip
            };

            string message = "There can only be a single ToDo item with Wip status and priority of 1";

            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public async Task TestThereCanOnlyBe1ToDoItemWithWipStatusAndPriority1WhenModified()
        {
            _toDoItemProvider = new Mock<IAsyncDbDataProvider<ToDoItemVo>>();

            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 1,
                Status = StatusEnum.Wip
            };

            string message = "There can only be a single ToDo item with Wip status and priority of 1";

            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }
    }
}
