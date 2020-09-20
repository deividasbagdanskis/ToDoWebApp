using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.ToDoItemPages
{
    class DeleteToDoItemPage
    {
        private readonly IWebDriver webDriver;

        private readonly string toDoItemIndexPageUrl = "https://localhost:44392/ToDoItemsEF";
        private readonly By deleteToDoItemButton = By.XPath("/html/body/div/main/div/form/input[2]");

        public DeleteToDoItemPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void DeleteToDoItem()
        {
            webDriver.Navigate().GoToUrl(toDoItemIndexPageUrl);

            int indexTableRowSize = webDriver.FindElements(By.XPath(".//*/table/tbody/tr/td[1]")).Count;

            By deleteLastCreatedToDoItemPageLink = By.XPath($"/html/body/div/main/table/tbody/" +
                $"tr[{indexTableRowSize}]/td[8]/a[3]");

            webDriver.FindElement(deleteLastCreatedToDoItemPageLink).Click();
            webDriver.FindElement(deleteToDoItemButton).Click();
        }

        public bool CheckIfToDoItemWasDeleted(string name)
        {
            bool wasDeleted = false;

            By deletedCategoryRecord = By.XPath($"//td[normalize-space() = '{name}']");

            try
            {
                IWebElement element = webDriver.FindElement(deletedCategoryRecord);
            }
            catch (NoSuchElementException)
            {
                wasDeleted = true;
            }

            return wasDeleted;
        }
    }
}
