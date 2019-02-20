using System;
using CashelFirmware.NunitTests;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using CashelFirmware.Reporting;
using RelevantCodes.ExtentReports;
using System.IO;
using Microsoft.Office.Interop.Excel;
using CashelFirmware.Utility;
namespace PQStandalone
{
    class PQStandaloneDataConfiguration
    {
        FirmwareCablingTest Config_Cabling;
        
        public ExtentTest InfovarStartTest;
        IWebDriver webdriver;

        public PQStandaloneDataConfiguration(string Cabling)
        {
            Config_Cabling = new FirmwareCablingTest();
    
            SeleniumDriverInitialise(Cabling);        
           // Read_WriteExcel.xlapp = new Application();
        }

        public PQStandaloneDataConfiguration()
        {
            Config_Cabling = new FirmwareCablingTest();
            SeleniumDriverInitialise();
           // Read_WriteExcel.xlapp = new Application();
        }


        public bool SeleniumDriverInitialise(string Cabling)
        {
            string UTC_Time = DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss");
            if (Directory.Exists("C:\\TestDonwloadedFiles\\"+Cabling+ "_" + UTC_Time))
            {

            }
            else
            {
                Directory.CreateDirectory("C:\\TestDonwloadedFiles\\" + Cabling + "_" + UTC_Time);

            }
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content",
                "test-type","ignore-certificate-errors","disable-extensions"});
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);
            options.AddUserProfilePreference("download.default_directory", "C:\\TestDonwloadedFiles\\" + Cabling +  "_" + UTC_Time);

            webdriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25)); //Implicit wait is added so that selenium doesn't fail if any element is not loaded within specified time interval.            
            return true;
        }

        public bool SeleniumDriverInitialise()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content",
                "test-type","ignore-certificate-errors","disable-extensions"});
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            webdriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(100)); //Implicit wait is added so that selenium doesn't fail if any element is not loaded within specified time interval.            
            return true;
        }

        public void KillExcelProcess()
        {            
            System.Diagnostics.Process[] AllProcesses = System.Diagnostics.Process.GetProcessesByName("excel");

            // check to kill the right process
            foreach (System.Diagnostics.Process ExcelProcess in AllProcesses)
            {
                if (ExcelProcess.ProcessName.Equals("EXCEL"))
                    ExcelProcess.Kill();
            }
            AllProcesses = null;
        }

        public void ConfigureCabling(string deviceIP,string CablingType,string PQDuration,string PQDurationUnit)
        {
            try
            {
                string DataSetFolderPath = System.AppDomain.CurrentDomain.BaseDirectory+ @"\TestData\PQStandalone_Ckt_ParamOnly\";
                InfovarStartTest = ReportGeneration.extent.StartTest("Configure PQ param for "+CablingType+" Cabling");
                Config_Cabling.TestCabling(webdriver, deviceIP, InfovarStartTest, CablingType, DataSetFolderPath, false);
                Config_Cabling.ConfigurePQData(deviceIP, webdriver, InfovarStartTest, CablingType, PQDuration, PQDurationUnit);
            }
            catch (Exception ex)
            { }
            finally
            {
                ReportGeneration.EndTestCaseReport(InfovarStartTest,webdriver);
                KillExcelProcess();
                webdriver.Quit();
            }
        }

        public void DonwloadPQData(string deviceIP,string Cabling,string RecordStartTime)
        {
            try
            {
                InfovarStartTest = ReportGeneration.extent.StartTest("Download PQ Record");
                Config_Cabling.DownloadPQData(deviceIP, webdriver, InfovarStartTest, Cabling, RecordStartTime);
            }
            catch (Exception ex)
            {

            }
            finally
            {
                ReportGeneration.EndTestCaseReport(InfovarStartTest,webdriver);
                KillExcelProcess();
                webdriver.Quit();
            }
        }
    }
}
