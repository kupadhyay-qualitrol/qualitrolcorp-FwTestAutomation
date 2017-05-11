using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support;


namespace SeleniumProject
{
    public class Selenium
    {
        private IWebDriver driver;

        public void Test()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.google.co.in/?gfe_rd=cr&ei=b4SZV6CoL8SDoAPWoJmwDQ&gws_rd=ssl");
        }
    }
}
