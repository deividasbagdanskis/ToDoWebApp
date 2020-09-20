using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.CategoryPages
{
    class CategoryIndexPage
    {
        private readonly IWebDriver webDriver;

        private readonly string categoryIndexPageUrl = "https://localhost:44392/CategoriesEF";

        private readonly By categoriesTableHeader = By.XPath("/html/body/div/main/table/thead/tr/th[1]");

        public CategoryIndexPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public string GetPageTitle()
        {
            webDriver.Navigate().GoToUrl(categoryIndexPageUrl);

            return webDriver.Title;
        }

        public string GetCategoriesTableHeader()
        {
            webDriver.Navigate().GoToUrl(categoryIndexPageUrl);

            return webDriver.FindElement(categoriesTableHeader).Text;
        }
    }
}
