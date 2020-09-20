using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using ToDoApp.Commons.Enums;
using ToDoApp.Web.Tests.PageObjects.ToDoItemPages;
using Xunit;

namespace ToDoApp.Web.Tests
{
    public class ToDoItemViewsTests
    {
        [Fact]
        public void SmokeTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            ToDoItemIndexPage toDoItemIndexPage = new ToDoItemIndexPage(webDriver);

            string pageTitle = "Index - SampleWebApp";

            Assert.Equal(pageTitle, toDoItemIndexPage.GetPageTitle());
        }

        [Fact]
        public void TestIndexPage()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            ToDoItemIndexPage toDoItemIndexPage = new ToDoItemIndexPage(webDriver);

            Assert.Equal("Name", toDoItemIndexPage.GetToDoItemTableHeader());
        }

        [Fact]
        public void TestDetailsPage()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            string toDoItemName = "test";
            string description = "some description";
            DateTime deadlineDate = new DateTime(2020, 10, 5);
            int priority = 4;
            StatusEnum status = StatusEnum.Backlog;

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);
            createToDoItemPage.CreateToDoItem(toDoItemName, description, deadlineDate, priority, status);

            ToDoItemDetailsPage toDoItemDetailsPage = new ToDoItemDetailsPage(webDriver);
            toDoItemDetailsPage.GoToDetailsPage();

            Assert.Equal(toDoItemName, toDoItemDetailsPage.GetToDoItemName());

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();
        }

        [Fact]
        public void TestCreateToDoItem()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(20);
            int priority = 4;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            Assert.Equal(name, createToDoItemPage.GetCreatedToDoItemName(name));

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();
        }

        [Fact]
        public void TestUpdateToDoItem()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(20);
            int priority = 4;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            UpdateToDoItemPage updateToDoItemPage = new UpdateToDoItemPage(webDriver);
            updateToDoItemPage.GoToUpdateToDoItemPage();

            string updatedName = "updated test";
            string updatedDescription = "updated description";
            DateTime updatedDeadlineDate = DateTime.Today.AddDays(30);
            int updatedPriority = 5;
            StatusEnum updatedStatus = StatusEnum.Backlog;

            updateToDoItemPage.UpdateToDoItem(updatedName, updatedDescription, updatedDeadlineDate, updatedPriority,
                updatedStatus);

            Assert.Equal(updatedName, updateToDoItemPage.GetUpdatedToDoItemName(updatedName));

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();
        }

        [Fact]
        public void TestDeleteToDoItems()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(20);
            int priority = 4;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();

            Assert.True(deleteToDoItemPage.CheckIfToDoItemWasDeleted(name));
        }

        [Fact]
        public void TestDuplicateToDoItemNameErrorWhenCreating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(20);
            int priority = 4;
            StatusEnum status = StatusEnum.Backlog;
            
            for (int i = 0; i < 2; i++)
            {
                createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);
            }

            string errorMessage = $"ToDo item with name {name} already exists.";

            Assert.Equal(errorMessage, createToDoItemPage.GetErrorMessage());

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();
        }

        [Fact]
        public void TestPriorityErrorWhenCreating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(20);
            int priority = 10;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            string errorMessage = "The field Priority must be between 1 and 5.";

            Assert.Equal(errorMessage, createToDoItemPage.GetPriorityErrorMessage());
        }

        [Fact]
        public void TestPriorityErrorWhenUpdating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(20);
            int priority = 3;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            UpdateToDoItemPage updateToDoItemPage = new UpdateToDoItemPage(webDriver);
            updateToDoItemPage.GoToUpdateToDoItemPage();

            int updatedPriority = 15;

            updateToDoItemPage.UpdateToDoItem(name, description, deadlineDate, updatedPriority, status);

            string errorMessage = "The field Priority must be between 1 and 5.";

            Assert.Equal(errorMessage, updateToDoItemPage.GetPriorityErrorMessage());

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();
        }

        [Fact]
        public void TestDeadlineDateIsEarlierThanTodayWhenCreating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(-10);
            int priority = 5;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            string errorMessage = $"Deadline date must be later than {DateTime.Today}";

            Assert.Equal(errorMessage, createToDoItemPage.GetErrorMessage());
        }

        [Fact]
        public void TestDeadlineDateIsEarlierThanTodayWhenUpdating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateToDoItemPage createToDoItemPage = new CreateToDoItemPage(webDriver);

            string name = "test";
            string description = "some description";
            DateTime deadlineDate = DateTime.Today.AddDays(10);
            int priority = 5;
            StatusEnum status = StatusEnum.Backlog;

            createToDoItemPage.CreateToDoItem(name, description, deadlineDate, priority, status);

            UpdateToDoItemPage updateToDoItemPage = new UpdateToDoItemPage(webDriver);
            updateToDoItemPage.GoToUpdateToDoItemPage();

            DateTime updatedDeadlineDate = DateTime.Today.AddDays(-5);

            updateToDoItemPage.UpdateToDoItem(name, description, updatedDeadlineDate, priority, status);

            string errorMessage = $"Deadline date must be later than {DateTime.Today}";

            Assert.Equal(errorMessage, createToDoItemPage.GetErrorMessage());

            DeleteToDoItemPage deleteToDoItemPage = new DeleteToDoItemPage(webDriver);
            deleteToDoItemPage.DeleteToDoItem();
        }
    }
}
