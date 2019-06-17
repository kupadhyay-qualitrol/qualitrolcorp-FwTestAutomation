/*This file contains Report Generation methods using Extent Report Library.
 */

using System;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;
using NUnit.Framework;
using OpenQA.Selenium;

namespace CashelFirmware.Reporting
{
    public static class ReportGeneration
    {
        public static ExtentReports  extent;
        public static IWebDriver webDriver;
 
        public static void StartReport(string TestSuite)
        {
            string projectPath = new Uri(Environment.CurrentDirectory).LocalPath;
            string reportPath = projectPath + @"\Reports\"+TestSuite+".html";

            extent = new ExtentReports(reportPath, true);
            extent
            .AddSystemInfo("Host Name", "QTRL-FLTHQ72")
            .AddSystemInfo("Environment", "Windows")
            .AddSystemInfo("User Name", "Rahuldev Gupta");
            extent.LoadConfig(projectPath + "extent-config.xml");
        }

        public static void EndTestCaseReport(ExtentTest test,IWebDriver webDriver)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
               // string screenShotPath = ScreenShotCapture.Capture(webDriver, "ScreenShotName");
               // test.Log(LogStatus.Fail, "Snapshot below: " + test.AddScreenCapture(screenShotPath));
                test.Log(LogStatus.Fail, stackTrace + errorMessage);
            }
            extent.EndTest(test);            
        }

        public static void EndReprot()
        {
            if (extent == null)
            {
            }
            else
            {
                extent.Flush();
                extent.Close();
            }
        }
    }
}
