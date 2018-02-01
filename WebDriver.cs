using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LetsFindLunchTest
{
    public static class WebDrivers
    {
        private static IWebDriver chromeDriver = null;

        public static IWebDriver ChromeDriver
        {
            get
            {
                if (chromeDriver == null)
                {
                    chromeDriver = new ChromeDriver(Environment.CurrentDirectory);//xunit runs in a different directory than the build directory, so this argument is needed to find chromedriver.exe
                }

                return chromeDriver;
            }
        }
    }
}