using Moq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ToDoApp.Business.Models;
using ToDoApp.Business.Services.InDbProviders;
using ToDoApp.Commons.Exceptions;
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
            _toDoItem = new ToDoItemVo() 
            {   
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 4,
                CreationDate = new DateTime(2020, 9, 6), 
                DeadlineDate = new DateTime(2020, 9, 4)
            };
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

        [Fact]
        public async Task TestCreatedToDoItemNameMustBeUnique()
        {
            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).Throws(new ToDoItemUniqueNameException(_toDoItem.Name));

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemUniqueNameException ex = await Assert.ThrowsAsync<ToDoItemUniqueNameException>(action);

            string expected = "ToDo item with name Lorem ipsum already exists.";

            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task TestModifiedToDoItemNameMustBeUnique()
        {
            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).Throws(new ToDoItemUniqueNameException(_toDoItem.Name));

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemUniqueNameException ex = await Assert.ThrowsAsync<ToDoItemUniqueNameException>(action);

            string expected = "ToDo item with name Lorem ipsum already exists.";

            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task TestCreatedToDoItemDeadlineDateMustBeLaterThanCreationDate()
        {
            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).Throws(new ToDoItemDeadlineDateException());

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemDeadlineDateException ex = await Assert.ThrowsAsync<ToDoItemDeadlineDateException>(action);

            string expected = $"Deadline date must be later than {DateTime.Today}";

            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task TestModifiedToDoItemDeadlineDateMustBeLaterThanCreationDate()
        {
            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).Throws(new ToDoItemDeadlineDateException());

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemDeadlineDateException ex = await Assert.ThrowsAsync<ToDoItemDeadlineDateException>(action);

            string expected = $"Deadline date must be later than {DateTime.Today}";

            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task TestCreatedToDoItemPriorityMustBeFrom1To5()
        {
            _toDoItem.Priority = 6;

            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).Throws(new ToDoItemPriorityException(_toDoItem.Priority));

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemPriorityException ex = await Assert.ThrowsAsync<ToDoItemPriorityException>(action);

            string expected = $"Priority value of {_toDoItem.Priority} is invalid. Must be from 1 to 5.";

            Assert.Equal(expected, ex.Message);
        }

        [Fact]
        public async Task TestModifiedToDoItemPriorityMustBeFrom1To5()
        {
            _toDoItem.Priority = 6;

            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).Throws(new ToDoItemPriorityException(_toDoItem.Priority));

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemPriorityException ex = await Assert.ThrowsAsync<ToDoItemPriorityException>(action);

            string expected = $"Priority value of {_toDoItem.Priority} is invalid. Must be from 1 to 5.";

            Assert.Equal(expected, ex.Message);
        }
    }
}
