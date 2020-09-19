using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.CategoryPages
{
    class UpdateCategoryPage
    {
        private readonly IWebDriver webDriver;

        private readonly string categoryIndexPageUrl = "https://localhost:44392/CategoriesEF";
        private readonly By categotyNameInput = By.Id("Name");
        private readonly By updateCategotyButton = By.XPath("/html/body/div/main/div[1]/div/form/div[2]/input");
        private readonly By categoryNameError = By.XPath("/html/body/div/main/div[1]/div/form/div[1]");

        public UpdateCategoryPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void GoToUpdateCategoryPage()
        {
            webDriver.Navigate().GoToUrl(categoryIndexPageUrl);

            int indexTableRowSize = webDriver.FindElements(By.XPath(".//*/table/tbody/tr/td[1]")).Count;

            By updateLastCategoryPageLink = By.XPath($"/html/body/div/main/table/tbody/tr[{indexTableRowSize}]/" +
                $"td[2]/a[1]");

            webDriver.FindElement(updateLastCategoryPageLink).Click();
        }

        public void UpdateCategoryName(string name)
        {
            webDriver.FindElement(categotyNameInput).Clear();
            webDriver.FindElement(categotyNameInput).SendKeys(name);

            webDriver.FindElement(updateCategotyButton).Click();
        }

        public string GetUpdatedCategoryName(string name)
        {
            By updatedCategoryRecord = By.XPath($"//td[normalize-space() = '{name}']");

            string updatedCategoryName = "";

            try
            {
                IWebElement element = webDriver.FindElement(updatedCategoryRecord);

                updatedCategoryName = element.Text;
            }
            catch (NoSuchElementException)
            {

            }

            return updatedCategoryName;
        }

        public string GetCategoryNameErrorMessage()
        {
            string errorMessage = "";

            try
            {
                errorMessage = webDriver.FindElement(categoryNameError).Text;
            }
            catch (NoSuchElementException)
            {

            }

            return errorMessage;
        }
    }
}
