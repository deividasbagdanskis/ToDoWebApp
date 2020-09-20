using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.ToDoItemPages
{
    class ToDoItemIndexPage
    {
        private readonly IWebDriver webDriver;

        private readonly string toDoItemIndexPageUrl = "https://localhost:44392/ToDoItemsEF";
        private readonly By toDoItemTableHeader = By.XPath("/html/body/div/main/table/thead/tr/th[1]");

        public ToDoItemIndexPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public string GetPageTitle()
        {
            webDriver.Navigate().GoToUrl(toDoItemIndexPageUrl);

            return webDriver.Title;
        }

        public string GetToDoItemTableHeader()
        {
            webDriver.Navigate().GoToUrl(toDoItemIndexPageUrl);

            return webDriver.FindElement(toDoItemTableHeader).Text;
        }
    }
}
