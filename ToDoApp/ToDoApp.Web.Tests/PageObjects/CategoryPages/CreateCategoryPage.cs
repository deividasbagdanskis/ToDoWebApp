using OpenQA.Selenium;

namespace ToDoApp.Web.Tests.PageObjects.CategoryPages
{
    class CreateCategoryPage
    {
        private readonly IWebDriver webDriver;

        private readonly string createCategoryPageUrl = "https://localhost:44392/CategoriesEF/Create";
        private readonly By categotyNameInput = By.Id("Name");
        private readonly By createCategotyButton = By.XPath("/html/body/div/main/div[1]/div/form/div[2]/input");
        private readonly By errorDiv = By.XPath("/html/body/div/main/div[1]/div/form/div[1]");

        public CreateCategoryPage(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        public void CreateCategory(string name)
        {
            webDriver.Navigate().GoToUrl(createCategoryPageUrl);

            webDriver.FindElement(categotyNameInput).SendKeys(name);
            webDriver.FindElement(createCategotyButton).Click();
        }

        public string GetCreatedCategoryName(string name)
        {
            By createdCategoryRecord = By.XPath($"//td[normalize-space() = '{name}']");

            string createdCategoryName = "";

            try
            {
                IWebElement element = webDriver.FindElement(createdCategoryRecord);
                
                createdCategoryName = element.Text;
            }
            catch (NoSuchElementException)
            {

            }

            return createdCategoryName;
        }

        public string GetCategoryNameErrorMessage()
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
    }
}
