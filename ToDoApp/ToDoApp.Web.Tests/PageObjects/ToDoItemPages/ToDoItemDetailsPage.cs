using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.ToDoItemPages
{
    class ToDoItemDetailsPage
    {
        private readonly IWebDriver webDriver;
        
        private readonly string toDoItemIndexPageUrl = "https://localhost:44392/ToDoItemsEF";
        private readonly By toDoItemName = By.XPath("/html/body/div/main/div[1]/dl/dd[1]");

        public ToDoItemDetailsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void GoToDetailsPage()
        {
            webDriver.Navigate().GoToUrl(toDoItemIndexPageUrl);

            int indexTableRowSize = webDriver.FindElements(By.XPath(".//*/table/tbody/tr/td[1]")).Count;

            By lastToDoItemDetailsPageLink = By.XPath($"/html/body/div/main/table/tbody/tr[{indexTableRowSize}]/" +
                $"td[8]/a[2]");

            webDriver.FindElement(lastToDoItemDetailsPageLink).Click();
        }

        public string GetToDoItemName()
        {
            return webDriver.FindElement(toDoItemName).Text;
        }
    }
}
