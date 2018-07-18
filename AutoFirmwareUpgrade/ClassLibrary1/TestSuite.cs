using System;
using CashelFirmware.NunitTests;
using NUnit.Framework;
using CashelFirmware.Reporting;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using RelevantCodes.ExtentReports;

namespace CashelFirmware.TestSuite
{
    public class TestSuite
    {
        ReportGeneration ExtentReport;
        public ExtentTest InfovarStartTest;
        FirmwareCablingTest CablingTest;
        IWebDriver webdriver;
        public string deviceIP = string.Empty;
        public TestSuite()
        {
            ExtentReport = new ReportGeneration();
            CablingTest = new FirmwareCablingTest();
        }

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
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25)); //Implicit wait is added so that selenium doesn't fail if any element is not loaded within specified time interval.
            deviceIP = "10.75.58.51";
            //ExtentReport.StartReport();
        }

        [OneTimeSetUp]
        public void StartReport()
        {
            ExtentReport.StartReport();
        }

        [OneTimeTearDown]
        public void EndReprot()
        {
            ExtentReport.EndReprot();
        }

        [TearDown]
        public void TearDown()
        {
            ExtentReport.EndTestCaseReport(InfovarStartTest);
            webdriver.Quit();
        }
        [Test,Order(1)]
        public void DemoTest()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start Demo Testing");
            CablingTest.DemoReportPass(InfovarStartTest);
        }
        [Test, Order(2)]
        public void TestCabling3U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start 3U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver,deviceIP, InfovarStartTest,"3U");
        }
        [Test,Order(3)]
        public void TestCabling3U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start 3U3I Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I");
        }

        [Test,Order(4)]
        public void TestCabling2M3U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start 2M3U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U");
        }

        [Test,Order(5)]
        public void TestCabling4U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start 4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U");
        }

        [Test,Order(6)]
        public void TestCabling2M4U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U");
        }
        [Test,Order(7)]
        public void TestCabling4U3U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Start 4U3U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U");
        }
    }
}
