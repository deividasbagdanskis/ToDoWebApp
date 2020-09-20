using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.CategoryPages
{
    class CategoryDetailsPage
    {
        private readonly IWebDriver webDriver;

        private readonly string categoryIndexPageUrl = "https://localhost:44392/CategoriesEF";
        private readonly By categoryName = By.XPath("/html/body/div/main/div[1]/dl/dd");
        
        public CategoryDetailsPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void GoToDetailsPage()
        {
            webDriver.Navigate().GoToUrl(categoryIndexPageUrl);

            int indexTableRowSize = webDriver.FindElements(By.XPath(".//*/table/tbody/tr/td[1]")).Count;

            By lastCategoryDetailsPageLink = By.XPath($"/html/body/div/main/table/tbody/tr[{indexTableRowSize}]/" +
                $"td[2]/a[2]");

            webDriver.FindElement(lastCategoryDetailsPageLink).Click();
        }

        public string GetCategoryName()
        {
            return webDriver.FindElement(categoryName).Text;
        }
    }
}
