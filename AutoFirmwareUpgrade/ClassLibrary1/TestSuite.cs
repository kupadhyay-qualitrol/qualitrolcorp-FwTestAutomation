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

        [Test, Order(1)]
        public void TestCabling3U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver,deviceIP, InfovarStartTest,"3U");
        }
        [Test,Order(2)]
        public void TestCabling3U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I");
        }

        [Test, Order(3)]
        public void TestCabling3U3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I");
        }

        [Test, Order(4)]
        public void TestCabling3U3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I");
        }

        [Test, Order(5)]
        public void TestCabling3U3I3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I");
        }

        [Test, Order(6)]
        public void TestCabling3U3I3I3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U3I3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I");
        }

        [Test,Order(7)]
        public void TestCabling2M3U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U");
        }

        [Test, Order(8)]
        public void TestCabling3U3U3I3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I");
        }

        [Test, Order(9)]
        public void TestCabling2M3U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I");
        }

        [Test, Order(10)]
        public void TestCabling2M3U3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I");
        }

        [Test, Order(11)]
        public void TestCabling2M3U3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I");
        }

        [Test, Order(12)]
        public void TestCabling2M3U3I3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I");
        }

        [Test,Order(13)]
        public void TestCabling4U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U");
        }

        [Test, Order(14)]
        public void TestCabling4U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I");
        }

        [Test, Order(15)]
        public void TestCabling4U3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I");
        }

        [Test, Order(16)]
        public void TestCabling4U3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I");
        }

        [Test, Order(17)]
        public void TestCabling4U3I3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I");
        }

        [Test, Order(18)]
        public void TestCabling4U4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I");
        }

        [Test, Order(19)]
        public void TestCabling4U4I4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I");
        }

        [Test, Order(20)]
        public void TestCabling4U4I4I4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U4I4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I");
        }

        [Test,Order(21)]
        public void TestCabling2M4U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U");
        }

        [Test, Order(22)]
        public void TestCabling2M4U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I");
        }

        [Test, Order(23)]
        public void TestCabling2M4U3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I");
        }

        [Test, Order(24)]
        public void TestCabling2M4U3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I");
        }

        [Test, Order(25)]
        public void TestCabling2M4U4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I");
        }

        [Test, Order(26)]
        public void TestCabling2M4U4I4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 2M4U4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I");
        }

        [Test,Order(27)]
        public void TestCabling4U3U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U");
        }

        [Test, Order(28)]
        public void TestCabling4U3U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I");
        }

        [Test, Order(29)]
        public void TestCabling4U3U3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I");
        }

        [Test, Order(30)]
        public void TestCabling4U3U3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I");
        }

        [Test, Order(31)]
        public void TestCabling3U4U()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U");
        }

        [Test, Order(32)]
        public void TestCabling3U4U3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I");
        }

        [Test, Order(33)]
        public void TestCabling3U4U3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I");
        }

        [Test, Order(34)]
        public void TestCabling3U4U3I3I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I");
        }

        [Test, Order(35)]
        public void TestCabling4U3U4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I");
        }

        [Test, Order(36)]
        public void TestCabling4U3U4I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I");
        }

        [Test, Order(37)]
        public void TestCabling4U3U4I4I3I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 4U3U4I4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I");
        }

        [Test, Order(38)]
        public void TestCabling3U4U3I4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U4U3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I");
        }

        [Test, Order(39)]
        public void TestCabling3U4U3I3I4I()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test 3U4U3I3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I");
        }

        [Test, Order(40)]
        public void TestCablingNOCIRCUIT()
        {
            InfovarStartTest = ExtentReport.extent.StartTest("Test NOCIRCUIT Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
        }
    }
}
