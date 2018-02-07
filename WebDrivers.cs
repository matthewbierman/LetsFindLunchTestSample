using System;
using System.Collections.ObjectModel;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Xunit;

namespace LetsFindLunchTest
{
    public static class WebDrivers
    {
        private static IWebDriver chromeDriver = null;

        public static IWebDriver GetChromeDriver()
        {
            if (chromeDriver == null)
            {
                chromeDriver = new ChromeDriver(Environment.CurrentDirectory);//xunit runs in a different directory than the build directory, so this argument is needed to find chromedriver.exe
            }

            return chromeDriver;
        }

        public static IWebElement GetElement(this ISearchContext context, By by)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            elements = context.FindElements(by);

            Assert.True(elements.Count == 1);

            return elements[0];
        }

        public static void DestroyChromeDriver()
        {
            if (chromeDriver != null)
            {
                chromeDriver.Quit();
                chromeDriver = null;
            }
        }
    }
}