using OpenQA.Selenium.Chrome;
using System;

namespace SelNet
{
    internal class SeleniumTest : IDisposable
    {
        public readonly ChromeDriver driver;

        public SeleniumTest()
        {
            driver = new ChromeDriver();
            driver.Url = "https://localhost:44313/";
        }

        public void Dispose()
        {
            driver.Quit();
        }
    }
}
