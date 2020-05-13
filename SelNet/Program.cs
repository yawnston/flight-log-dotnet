using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Diagnostics;
using Xunit;

namespace SelNet
{
    public class Program
    {
        private static void Main(string[] args)
        {
        }

        [Fact]
        public void TestCreation()
        {
            using SeleniumTest sel = new SeleniumTest();
            ChromeDriver driver = sel.driver;

            Debug.WriteLine($"Title of page is: {driver.Title}");
            int rowCount = driver.FindElements(By.XPath("/html/body/router-view/div/div/div/table/tbody/tr")).Count;
            IWebElement novyLet = driver.FindElement(By.XPath("/html/body/div[1]/div/ul/li[2]/a"));
            novyLet.Click();

            driver.FindElement(By.XPath("//*[@id=\"takeoffTime\"]")).SendKeys("12120020181956");
            driver.FindElement(By.XPath("/html/body/router-view/div/form/div[2]/div/button")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(1));
            wait.Until(d =>
            {
                try
                {
                    return d.SwitchTo().Alert();
                }
                catch (NoAlertPresentException)
                {
                    return null;
                }
            }).Accept();

            driver.FindElement(By.XPath("/html/body/div[1]/div/ul/li[1]/a")).Click();
            int newCount = driver.FindElements(By.XPath("/html/body/router-view/div/div/div/table/tbody/tr")).Count;
            Assert.Equal(rowCount + 2, newCount);
        }
    }
}
