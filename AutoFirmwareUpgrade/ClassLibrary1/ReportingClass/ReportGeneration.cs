using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RelevantCodes.ExtentReports;
using NUnit.Framework.Interfaces;
using NUnit.Framework;

namespace CashelFirmware.Reporting
{
    public class ReportGeneration
    {
        public ExtentReports  extent;
       // public ExtentTest test;
 
        public void StartReport()
        {
            string path = System.Reflection.Assembly.GetCallingAssembly().CodeBase;
            string actualPath = path.Substring(0, path.LastIndexOf("bin"));
            string projectPath = new Uri(actualPath).LocalPath;
            string reportPath = projectPath + "Reports\\CashelFirmwareReport.html";

            extent = new ExtentReports(reportPath, true);
            extent
            .AddSystemInfo("Host Name", "QTRL-FLTHQ72")
            .AddSystemInfo("Environment", "Windows")
            .AddSystemInfo("User Name", "Rahuldev Gupta");
            extent.LoadConfig(projectPath + "extent-config.xml");
        }

        public void EndTestCaseReport(ExtentTest test)
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

        public void EndReprot()
        {
            extent.Flush();
            extent.Close();
        }
    }
}
