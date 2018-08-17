﻿using System;
using RelevantCodes.ExtentReports;
using CashelFirmware.NunitTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Microsoft.Office.Interop.Excel;
using CashelFirmware.Utility;
using NUnit.Framework;
using CashelFirmware.Reporting;

namespace CashelFirmware.TestSuite
{
    public class BaseTestSuite
    {
        public ExtentTest InfovarStartTest;        
        public IWebDriver webdriver;
        public string deviceIP = string.Empty;

        public BaseTestSuite()
        {            
            Read_WriteExcel.xlapp = new Application();
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
        }

        [OneTimeSetUp]
        public void StartReport()
        {
            ReportGeneration.StartReport("CablingTest");
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
            webdriver.Quit();
        }
    }
}
