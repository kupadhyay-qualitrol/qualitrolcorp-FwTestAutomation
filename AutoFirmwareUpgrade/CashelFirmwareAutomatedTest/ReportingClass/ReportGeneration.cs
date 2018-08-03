using System;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;
using NUnit.Framework;

namespace CashelFirmware.Reporting
{
    public static class ReportGeneration
    {
        public static ExtentReports  extent;
 
        public static void StartReport(string TestSuite)
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + @"Reports\"+TestSuite+".html";

            extent = new ExtentReports(reportPath, true);
            extent
            .AddSystemInfo("Host Name", "QTRL-FLTHQ72")
            .AddSystemInfo("Environment", "Windows")
            .AddSystemInfo("User Name", "Rahuldev Gupta");
            extent.LoadConfig(projectPath + "extent-config.xml");
        }

        public static void EndTestCaseReport(ExtentTest test)
        {
            var status = TestContext.CurrentContext.Result.Outcome.Status;
            var stackTrace = "<pre>" + TestContext.CurrentContext.Result.StackTrace + "</pre>";
            var errorMessage = TestContext.CurrentContext.Result.Message;

            if (status == TestStatus.Failed)
            {
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
