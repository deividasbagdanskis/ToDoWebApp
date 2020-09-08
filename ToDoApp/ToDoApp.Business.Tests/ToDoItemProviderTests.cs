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
        private readonly Mock<IAsyncDbDataProvider<ToDoItemVo>> _toDoItemProvider;

        public ToDoItemProviderTests()
        {
            _toDoItemProvider = new Mock<IAsyncDbDataProvider<ToDoItemVo>>();
        }

        [Fact]
        public async Task TestThereCanOnlyBe1ToDoItemWithWipStatusAndPriority1WhenCreated()
        {
            

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

        [Fact]
        public async Task TestThereCanOnlyBe3ToDoItemWithWipStatusAndPriority2WhenCreated()
        {
            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 2,
                Status = StatusEnum.Wip
            };

            string message = "There can only be three ToDo item with Wip status and priority of 2";

            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public async Task TestThereCanOnlyBe3ToDoItemWithWipStatusAndPriority2WhenModified()
        {
            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 2,
                Status = StatusEnum.Wip
            };

            string message = "There can only be three ToDo item with Wip status and priority of 2";

            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public async Task TestToDoItemHasDeadlineDateAndItsNotLessThanAWeekInFutureWithPriority1WhenCreated()
        {
            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 1,
                Status = StatusEnum.Wip,
                CreationDate = new DateTime(2020, 09, 08),
                DeadlineDate = new DateTime(2020, 09, 20)
            };

            string message = "Deadline date must not be less than a week in the future";

            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public async Task TestToDoItemHasDeadlineDateAndItsNotLessThanAWeekInFutureWithPriority1WhenModified()
        {
            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 1,
                Status = StatusEnum.Wip,
                CreationDate = new DateTime(2020, 09, 08),
                DeadlineDate = new DateTime(2020, 09, 20)
            };

            string message = "Deadline date must not be less than a week in the future";

            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public async Task TestToDoItemHasDeadlineDateAndItsNotLessThan2DaysInFutureWithPriority2WhenCreated()
        {
            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 2,
                Status = StatusEnum.Wip,
                CreationDate = new DateTime(2020, 09, 08),
                DeadlineDate = new DateTime(2020, 09, 11)
            };

            string message = "Deadline date must not be less than 2 days in the future";

            _toDoItemProvider.Setup(o => o.Add(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Add(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }

        [Fact]
        public async Task TestToDoItemHasDeadlineDateAndItsNotLessThan2DaysInFutureWithPriority2WhenModified()
        {
            ToDoItemVo _toDoItem = new ToDoItemVo()
            {
                Id = 4,
                Name = "Lorem ipsum",
                Description = "Lorem ipsum",
                Priority = 2,
                Status = StatusEnum.Wip,
                CreationDate = new DateTime(2020, 09, 08),
                DeadlineDate = new DateTime(2020, 09, 11)
            };

            string message = "Deadline date must not be less than 2 days in the future";

            _toDoItemProvider.Setup(o => o.Update(_toDoItem)).ThrowsAsync(new ToDoItemException(message));

            Func<Task> action = async () => await _toDoItemProvider.Object.Update(_toDoItem);

            ToDoItemException ex = await Assert.ThrowsAsync<ToDoItemException>(action);

            Assert.Equal(message, ex.Message);
        }
    }
}
