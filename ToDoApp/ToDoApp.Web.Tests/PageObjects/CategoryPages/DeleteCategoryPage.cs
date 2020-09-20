using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.CategoryPages
{
    class DeleteCategoryPage
    {
        private readonly IWebDriver webDriver;

        private readonly string categoryIndexPageUrl = "https://localhost:44392/CategoriesEF";
        private readonly By deleteCategoryButton = By.XPath("/html/body/div/main/div/form/input[2]");

        public DeleteCategoryPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void DeleteCategory()
        {
            webDriver.Navigate().GoToUrl(categoryIndexPageUrl);

            int indexTableRowSize = webDriver.FindElements(By.XPath(".//*/table/tbody/tr/td[1]")).Count;

            By deleteLastCreatedCategoryPageLink = By.XPath($"/html/body/div/main/table/tbody/" +
                $"tr[{indexTableRowSize}]/td[2]/a[3]");

            webDriver.FindElement(deleteLastCreatedCategoryPageLink).Click();
            webDriver.FindElement(deleteCategoryButton).Click();
        }

        public bool CheckIfCategoryWasDeleted(string categoryName)
        {
            bool wasDeleted = false;

            By deletedCategoryRecord = By.XPath($"//td[normalize-space() = '{categoryName}']");
            
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
