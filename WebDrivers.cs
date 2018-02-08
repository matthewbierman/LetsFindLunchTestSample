using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace LetsFindLunchTest
{
    public class WebDrivers : IDisposable
    {
        private IWebDriver chromeDriver = null;

        public IWebDriver GetChromeDriver()
        {
            if (chromeDriver == null)
            {
                chromeDriver = new ChromeDriver(Environment.CurrentDirectory);//xunit runs in a different directory than the build directory, so this argument is needed to find chromedriver.exe
            }

            return chromeDriver;
        }

        public void Dispose()
        {
            if (chromeDriver != null)
            {
                chromeDriver.Quit();
                chromeDriver = null;
            }
        }
    }
}