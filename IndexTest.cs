using System;
using Xunit;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace LetsFindLunchTest
{
    public class IndexTest
    {

        [Fact]
        public void PageLoads()
        {
            Driver.Navigate().GoToUrl(indexURL);

            var headerElement = GetElement(By.TagName("header"));

            var h1Element = GetElement(By.TagName("h1"), headerElement);

            Assert.True(h1Element.Text == "Let's find lunch");
        }

        private IWebDriver Driver
        {
            get
            {
                return WebDrivers.ChromeDriver;
            }
        }

        [Theory]
        [InlineData("52807")]
        public void ResultsFound(string zipCode)
        {
            Driver.Navigate().GoToUrl(indexURL);

            var olLocations = GetElement(By.Id("olLocations"));

            Assert.True(olLocations.FindElements(By.TagName("li")).Count == 0);

            var txtLocation = GetElement(By.Id("txtLocation"));

            txtLocation.SendKeys(zipCode);

            Assert.True(txtLocation.GetAttribute("value") == zipCode);

            var btnFindLunch = GetElement(By.Id("btnFindLunch"));

            btnFindLunch.Click();

            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(2));

            var listItems = wait.Until(driver=> olLocations.FindElements(By.TagName("li")));

            Assert.True(listItems.Count == 1);

            btnFindLunch.Click();

            listItems = wait.Until(driver=> olLocations.FindElements(By.TagName("li")));

            Assert.True(listItems.Count == 2);
        }

        private const string indexURL = "https://lets-find-lunch-sample.glitch.me/";

        private IWebElement GetElement(By by, IWebElement element = null)
        {
            ISearchContext context = (ISearchContext)element ?? (ISearchContext)Driver;

            ReadOnlyCollection<IWebElement> elements = null;

            elements = context.FindElements(by);

            Assert.True(elements.Count == 1);

            return elements[0];
        }

    }
}
