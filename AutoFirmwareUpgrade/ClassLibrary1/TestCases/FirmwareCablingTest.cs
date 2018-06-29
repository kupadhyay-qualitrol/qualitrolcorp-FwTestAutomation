using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.IO;
using Tabindex.Pages;


namespace CashelFirmware.NunitTests
{
    [TestFixture]
    public class FirmwareCablingTest
    {
          IWebDriver webdriver;

        [SetUp]
        public void TestSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content",
                "test-type","ignore-certificate-errors","disable-extensions"});
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            webdriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
        }

        [TearDown]
        public void TearDown()
        {
            webdriver.Close();
        }

        [Test]       
        public void Cabling3U()
        {
            Tabindex_Configuration_dfr test = new Tabindex_Configuration_dfr(webdriver);
            
            Assert.AreEqual("Configuration",test.OpenTabIndexPage("10.75.58.51"));
        }
    }
}
