using System;
using Xunit;
using OpenQA.Selenium;
using System.Collections.ObjectModel;
using OpenQA.Selenium.Support.UI;

namespace LetsFindLunchTest
{
    public class IndexTest : IDisposable
    {

        private const string indexURL = "https://lets-find-lunch-sample.glitch.me/";

        [Fact]
        public void PageLoads()
        {
            var driver = WebDrivers.GetChromeDriver();

            driver.Navigate().GoToUrl(indexURL);

            var headerElement = driver.GetElement(By.TagName("header"));

            var h1Element = headerElement.GetElement(By.TagName("h1"));

            driver.TakeScreenShot("PageLoads");

            Assert.True(h1Element.Text == "Let's find lunch");
        }


        [Theory]
        [InlineData("52807")]
        [InlineData("61201")]
        public void ResultsFound(string zipCode)
        {
            var driver = WebDrivers.GetChromeDriver();

            driver.Navigate().GoToUrl(indexURL);

            var olLocations = driver.GetElement(By.Id("olLocations"));

            Assert.True(olLocations.FindElements(By.TagName("li")).Count == 0);

            var txtLocation = driver.GetElement(By.Id("txtLocation"));

            txtLocation.SendKeys(zipCode);

            Assert.True(txtLocation.GetAttribute("value") == zipCode);

            var btnFindLunch = driver.GetElement(By.Id("btnFindLunch"));

            btnFindLunch.Click();

            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(2));

            var listItems = wait.Until(drv => olLocations.FindElements(By.TagName("li")));

            Assert.True(listItems.Count == 1);

            btnFindLunch.Click();

            listItems = wait.Until(drv => olLocations.FindElements(By.TagName("li")));

            driver.TakeScreenShot($"ResultsFound{zipCode}");

            Assert.True(listItems.Count == 2);
        }


        public void Dispose()
        {
            WebDrivers.DestroyChromeDriver();
        }




    }
}
