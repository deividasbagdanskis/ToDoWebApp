using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ToDoApp.Commons.Enums;

namespace ToDoApp.Web.Tests.PageObjects.ToDoItemPages
{
    class CreateToDoItemPage
    {
        private readonly IWebDriver webDriver;

        private readonly string createToDoItemPageUrl = "https://localhost:44392/ToDoItemsEF/Create";
        private readonly By nameInput = By.Id("Name");
        private readonly By descriptionInput = By.Id("Description");
        private readonly By deadlineDateInput = By.Id("DeadlineDate");
        private readonly By priorityInput = By.Id("Priority");
        private readonly By statusDropDown = By.Id("Status");
        private readonly By categoryDropDown = By.Id("CategoryId");
        private readonly By projectDropDown = By.Id("ProjectId");
        private readonly By createButton = By.XPath("/html/body/div/main/div[1]/div/form/div[8]/input");
        private readonly By errorDiv = By.XPath("/html/body/div/main/div[1]/div/form/div[1]");
        private readonly By priorityError = By.Id("Priority-error");

        public CreateToDoItemPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void CreateToDoItem(string name, string description, DateTime deadlineDate, int priority,
            StatusEnum status)
        {
            webDriver.Navigate().GoToUrl(createToDoItemPageUrl);
            webDriver.FindElement(nameInput).SendKeys(name);
            webDriver.FindElement(descriptionInput).SendKeys(description);
            webDriver.FindElement(deadlineDateInput).SendKeys(deadlineDate.Date.ToString("yyyy-MM-dd"));
            webDriver.FindElement(priorityInput).SendKeys(priority.ToString());

            IWebElement statusDropDownElement = webDriver.FindElement(statusDropDown);

            SelectElement statusOptions = new SelectElement(statusDropDownElement);
            statusOptions.SelectByText(status.ToString());

            IWebElement categoryDropDownElement = webDriver.FindElement(categoryDropDown);

            SelectElement categoryOptions = new SelectElement(categoryDropDownElement);

            int categoryOptionsCount = categoryOptions.Options.Count;

            if (categoryOptionsCount > 0)
            {
                categoryOptions.SelectByIndex(categoryOptionsCount - 1);
            }

            IWebElement projectDropDownElement = webDriver.FindElement(projectDropDown);

            SelectElement projectOptions = new SelectElement(projectDropDownElement);

            int projectOptionsCount = projectOptions.Options.Count;

            if (projectOptionsCount > 0)
            {
                projectOptions.SelectByIndex(projectOptionsCount - 1);
            }

            webDriver.FindElement(createButton).Click();
        }

        public string GetCreatedToDoItemName(string name)
        {
            By createdToDoItemRecord = By.XPath($"//td[normalize-space() = '{name}']");

            string createdToDoItemName = "";

            try
            {
                IWebElement element = webDriver.FindElement(createdToDoItemRecord);

                createdToDoItemName = element.Text;
            }
            catch (NoSuchElementException)
            {

            }

            return createdToDoItemName;
        }

        public string GetErrorMessage()
        {
            string errorMessage = "";

            try
            {
                errorMessage = webDriver.FindElement(errorDiv).Text;
            }
            catch (NoSuchElementException)
            {

            }

            return errorMessage;
        }

        public string GetPriorityErrorMessage()
        {
            string errorMessage = "";

            try
            {
                errorMessage = webDriver.FindElement(priorityError).Text;
            }
            catch (NoSuchElementException)
            {

            }

            return errorMessage;
        }
    }
}
