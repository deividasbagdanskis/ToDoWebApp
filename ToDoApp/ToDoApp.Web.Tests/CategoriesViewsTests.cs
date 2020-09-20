using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using System;
using ToDoApp.Web.Tests.PageObjects.CategoryPages;
using Xunit;

namespace ToDoApp.Web.Tests
{
    public class CategoriesViewsTests
    {
        [Fact]
        public void SmokeTest()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CategoryIndexPage categoryIndexPage = new CategoryIndexPage(webDriver);

            string pageTitle = "Index - SampleWebApp";

            Assert.Equal(pageTitle, categoryIndexPage.GetPageTitle());
        }

        [Fact]
        public void TestIndexPage()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CategoryIndexPage categoryIndexPage = new CategoryIndexPage(webDriver);

            Assert.Equal("Name", categoryIndexPage.GetCategoriesTableHeader());
        }

        [Fact]
        public void TestDetailsPage()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            string newCategoryName = "test";

            CreateCategoryPage createCategoryPage = new CreateCategoryPage(webDriver);
            createCategoryPage.CreateCategory(newCategoryName);

            CategoryDetailsPage categoryDetailsPage = new CategoryDetailsPage(webDriver);
            categoryDetailsPage.GoToDetailsPage();

            Assert.Equal(newCategoryName, categoryDetailsPage.GetCategoryName());

            DeleteCategoryPage deleteCategoryPage = new DeleteCategoryPage(webDriver);
            deleteCategoryPage.DeleteCategory();
        }

        [Fact]
        public void TestCreateCategory()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateCategoryPage createCategoryPage = new CreateCategoryPage(webDriver);

            string newCategoryName = "test";

            createCategoryPage.CreateCategory(newCategoryName);

            Assert.Equal(newCategoryName, createCategoryPage.GetCreatedCategoryName(newCategoryName));

            DeleteCategoryPage deleteCategoryPage = new DeleteCategoryPage(webDriver);
            deleteCategoryPage.DeleteCategory();
        }

        [Fact]
        public void TestUpdateCategory()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateCategoryPage createCategoryPage = new CreateCategoryPage(webDriver);

            string categoryName = "test";

            createCategoryPage.CreateCategory(categoryName);

            UpdateCategoryPage updateCategoryPage = new UpdateCategoryPage(webDriver);

            updateCategoryPage.GoToUpdateCategoryPage();

            string updatedCategoryName = "test_updated";

            updateCategoryPage.UpdateCategoryName(updatedCategoryName);

            Assert.Equal(updatedCategoryName, updateCategoryPage.GetUpdatedCategoryName(updatedCategoryName));

            DeleteCategoryPage deleteCategoryPage = new DeleteCategoryPage(webDriver);
            deleteCategoryPage.DeleteCategory();
        }

        [Fact]
        public void TestDeleteCategory()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateCategoryPage createCategoryPage = new CreateCategoryPage(webDriver);

            string newCategoryName = "test";

            createCategoryPage.CreateCategory(newCategoryName);

            DeleteCategoryPage deleteCategoryPage = new DeleteCategoryPage(webDriver);
            deleteCategoryPage.DeleteCategory();

            Assert.True(deleteCategoryPage.CheckIfCategoryWasDeleted(newCategoryName));
        }

        [Fact]
        public void TestCategoryNameTooShortErrorWhenCreating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateCategoryPage createCategoryPage = new CreateCategoryPage(webDriver);

            string newCategoryName = "ts";

            createCategoryPage.CreateCategory(newCategoryName);

            Assert.True(createCategoryPage.GetCategoryNameErrorMessage() != "");
        }

        [Fact]
        public void TestCategoryNameTooShortErrorWhenUpdating()
        {
            FirefoxOptions options = new FirefoxOptions();
            options.AcceptInsecureCertificates = true;

            using IWebDriver webDriver = new FirefoxDriver(options);

            CreateCategoryPage createCategoryPage = new CreateCategoryPage(webDriver);

            string categoryName = "test";

            createCategoryPage.CreateCategory(categoryName);

            UpdateCategoryPage updateCategoryPage = new UpdateCategoryPage(webDriver);

            updateCategoryPage.GoToUpdateCategoryPage();
            
            string updatedCategoryName = "ts";

            updateCategoryPage.UpdateCategoryName(updatedCategoryName);

            Assert.True(updateCategoryPage.GetCategoryNameErrorMessage() != "");

            DeleteCategoryPage deleteCategoryPage = new DeleteCategoryPage(webDriver);
            deleteCategoryPage.DeleteCategory();
        }
    }
}
