using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using System;
using ToDoApp.Commons.Enums;

namespace ToDoApp.Web.Tests.PageObjects.ToDoItemPages
{
    class UpdateToDoItemPage
    {
        private readonly IWebDriver webDriver;

        private readonly string toDoItemIndexPageUrl = "https://localhost:44392/ToDoItemsEF";
        private readonly By nameInput = By.Id("Name");
        private readonly By descriptionInput = By.Id("Description");
        private readonly By deadlineDateInput = By.Id("DeadlineDate");
        private readonly By priorityInput = By.Id("Priority");
        private readonly By statusDropDown = By.Id("Status");
        private readonly By createButton = By.XPath("/html/body/div/main/div[1]/div/form/div[8]/input");
        private readonly By errorDiv = By.XPath("/html/body/div/main/div[1]/div/form/div[1]");
        private readonly By priorityError = By.Id("Priority-error");

        public UpdateToDoItemPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void GoToUpdateToDoItemPage()
        {
            webDriver.Navigate().GoToUrl(toDoItemIndexPageUrl);

            int indexTableRowSize = webDriver.FindElements(By.XPath(".//*/table/tbody/tr/td[1]")).Count;

            By lastToDoItemDetailsPageLink = By.XPath($"/html/body/div/main/table/tbody/tr[{indexTableRowSize}]/" +
                $"td[8]/a[1]");

            webDriver.FindElement(lastToDoItemDetailsPageLink).Click();
        }

        public void UpdateToDoItem(string name, string description, DateTime deadlineDate, int priority,
            StatusEnum status)
        {
            webDriver.FindElement(nameInput).Clear();
            webDriver.FindElement(nameInput).SendKeys(name);

            webDriver.FindElement(descriptionInput).Clear();
            webDriver.FindElement(descriptionInput).SendKeys(description);

            webDriver.FindElement(deadlineDateInput).Clear();
            webDriver.FindElement(deadlineDateInput).SendKeys(deadlineDate.Date.ToString("yyyy-MM-dd"));

            webDriver.FindElement(priorityInput).Clear();
            webDriver.FindElement(priorityInput).SendKeys(priority.ToString());

            IWebElement statusDropDownElement = webDriver.FindElement(statusDropDown);

            SelectElement statusOptions = new SelectElement(statusDropDownElement);
            statusOptions.SelectByText(status.ToString());

            webDriver.FindElement(createButton).Click();
        }

        public string GetUpdatedToDoItemName(string name)
        {
            By updatedToDoItemRecord = By.XPath($"//td[normalize-space() = '{name}']");

            string updatedToDoItemName = "";

            try
            {
                IWebElement element = webDriver.FindElement(updatedToDoItemRecord);

                updatedToDoItemName = element.Text;
            }
            catch (NoSuchElementException)
            {

            }

            return updatedToDoItemName;
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
