using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;

namespace SeleniumProject
{
    
    public class Framework
    {
        public IWebDriver Chromedriver;
        
        public  void LaunchBrowser()
        {
            
            //To Launch Chrome browser
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content", 
                "test-type","ignore-certificate-errors","disable-extensions"});
            
            Chromedriver = new ChromeDriver("C:\\Users\\rdev\\Downloads\\selenium-dotnet-3.0.0\\net40", options);
            ApplicationSpecific App = new ApplicationSpecific();
            App.fn_ValidateDFR();
        }
        
    }
}
