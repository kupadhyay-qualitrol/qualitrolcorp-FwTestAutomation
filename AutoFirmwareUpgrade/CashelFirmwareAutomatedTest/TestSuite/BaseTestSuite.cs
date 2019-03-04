using System;
using RelevantCodes.ExtentReports;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Office.Interop.Excel;
using CashelFirmware.Utility;
using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.TestSuite
{
    public class BaseTestSuite
    {
        public ExtentTest InfovarStartTest;        
        public IWebDriver webdriver;
        public string deviceIP = string.Empty;
        public TestProgressStatus testProgress;

        public BaseTestSuite(string DeviceIP)
        {
            testProgress = new TestProgressStatus();
            Read_WriteExcel.xlapp = new Application();
            deviceIP = DeviceIP;
        }


        [SetUp]
        public IWebDriver TestSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content",
                "test-type","ignore-certificate-errors","disable-extensions"});
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false); 
            webdriver = new ChromeDriver(DeviceInformation.BaseDirectoryPath, options);
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(100)); //Implicit wait is added so that selenium doesn't fail if any element is not loaded within specified time interval.
            return webdriver;
        }

        [OneTimeSetUp]
        public void StartReport()
        {
            ReportGeneration.StartReport("Firmware_TestReport_"+ DateTime.Now.ToString("dd_MM_yyy_hh_mm_ss"));
        }

        [OneTimeTearDown]
        public void EndReprot()
        {
            ReportGeneration.EndReprot();

            System.Diagnostics.Process[] AllProcesses = System.Diagnostics.Process.GetProcessesByName("excel");

            // check to kill the right process
            foreach (System.Diagnostics.Process ExcelProcess in AllProcesses)
            {
                if (ExcelProcess.ProcessName.Equals("EXCEL"))
                    ExcelProcess.Kill();
            }
            AllProcesses = null;

        }

        [TearDown]
        public void TearDown()
        {
            ReportGeneration.EndTestCaseReport(InfovarStartTest, webdriver);
            testProgress.TestCase_Status(DateTime.Now.ToString() + "--" + TestContext.CurrentContext.Test.Name, TestContext.CurrentContext.Result.Outcome.ToString());
            webdriver.Quit();
        }
    }
}