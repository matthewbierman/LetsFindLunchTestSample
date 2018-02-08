using System;
using System.Collections.ObjectModel;
using System.IO;
using OpenQA.Selenium;
using Xunit;

namespace LetsFindLunchTest
{
    public static class WebDriverFunctions
    {
        public static void TakeScreenShot(this IWebDriver driver, string title)
        {
            Screenshot screenshot = ((ITakesScreenshot)driver).GetScreenshot();

            string fileName = $"{DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")} {title}.png";

            string directoryPath = Path.Combine(Environment.CurrentDirectory, "Screenshots");

            if (!Directory.Exists(directoryPath))
            {
                Directory.CreateDirectory(directoryPath);
            }

            string filePath = Path.Combine(directoryPath, fileName);

            screenshot.SaveAsFile(filePath, ScreenshotImageFormat.Png);
        }

        public static IWebElement GetElement(this ISearchContext context, By by)
        {
            ReadOnlyCollection<IWebElement> elements = null;

            elements = context.FindElements(by);

            Assert.True(elements.Count == 1);

            return elements[0];
        }
    }
}