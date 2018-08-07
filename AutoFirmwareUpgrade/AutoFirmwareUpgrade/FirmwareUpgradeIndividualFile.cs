using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;
using System.Diagnostics;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Windows.Forms;
using OpenQA.Selenium.Support.UI;


namespace AutoFirmwareUpgrade
{
    public class FirmwareUpgrade

    {
        Ping pinger = new Ping();
        PingReply reply;
        Read_WriteExcel rdexcel = new Read_WriteExcel();
        string[] dspchannelmap = new string[25];
        string[] pq_10min_calcnum = new string[1190];
        public string UTC_Time = string.Empty;

        public IWebDriver driver;
        TestReportLog Log;

        public FirmwareUpgrade()
        {
             Log = new TestReportLog();
        }
        
        
        public class WebClientWithTimeout : System.Net.WebClient
        {
            public int Timeout { get; set; }

            protected override System.Net.WebRequest GetWebRequest(Uri address)
            {
                System.Net.WebRequest _webreq = base.GetWebRequest(address);
                _webreq.Timeout = Timeout;
                //((HttpWebRequest)_webreq).ReadWriteTimeout = Timeout;
                return _webreq;
            }
        }


        /// <summary>
        /// This method is used to upgrade the firmware package.
        /// </summary>
        public bool UpgradePackage(string deviceIPAddress)
        {
            try
            {
                Log.addLine("Firmware Upgrade for device " + deviceIPAddress + " has Started", "", "");

                if (DisableProtocol(deviceIPAddress))
                {
                    if (UploadMultipart(File.ReadAllBytes(AutoFWUpgrade.FWBinPath + "\\PackageUpgrade.tar"),
                        "PackageUpgrade.tar", "application/octet-stream",
                        "http://" + deviceIPAddress + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_ALL"))
                    {


                        if (UploadMultipart("application/octet-stream", "http://" + deviceIPAddress + "/cgi-bin/upgrade.cgi?cmd=TOGGLE_PACKAGE"))
                        { 
                        
                        }
                    }
                    
                }
                else
                {
                    return false;
                }
                return true;
  
            }
            catch (Exception ex)
            {
                Log.addLine("Some exception occured in Firmware Upgrade", "Fail", ex.Message);
                return false;
            }
        }

        public void UpgradeCPUApplication(string deviceIPAdd)
        {
            try
            {
                //DisableProtocol(deviceIPAdd);
                //UpgradePackage();
                AutoFWUpgrade.CPUFileName = Path.GetFileName(AutoFWUpgrade.firmwareBinpathCPU);
                UploadMultipart(File.ReadAllBytes(AutoFWUpgrade.firmwareBinpathCPU ),
                AutoFWUpgrade.CPUFileName, "application/octet-stream",
                "http://" + deviceIPAdd + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_CPU_APP");

                AutoFWUpgrade.DSPFileName = Path.GetFileName(AutoFWUpgrade.firmwareBinpathDSP);
                UploadMultipart(File.ReadAllBytes(AutoFWUpgrade.firmwareBinpathDSP),
                AutoFWUpgrade.DSPFileName, "application/octet-stream",
                "http://" + deviceIPAdd + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_DSP_FW");

                
                AutoFWUpgrade.FPGAFileName = Path.GetFileName(AutoFWUpgrade.firmwareBinpathFPGA);
                UploadMultipart(File.ReadAllBytes(AutoFWUpgrade.firmwareBinpathFPGA),
                AutoFWUpgrade.FPGAFileName, "application/octet-stream",
                "http://" + deviceIPAdd + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_FPGA_FW");

                AutoFWUpgrade.PICFilename = Path.GetFileName(AutoFWUpgrade.firmwareBinpathPIC);
                UploadMultipart(File.ReadAllBytes(AutoFWUpgrade.firmwareBinpathPIC),
                AutoFWUpgrade.PICFilename, "application/octet-stream",
                "http://" + deviceIPAdd + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_PIC_FW");

                AutoFWUpgrade.MMIFileName = Path.GetFileName(AutoFWUpgrade.firmwareBinpathMMI);
                UploadMultipart(File.ReadAllBytes(AutoFWUpgrade.firmwareBinpathMMI),
                AutoFWUpgrade.MMIFileName, "application/octet-stream",
                "http://" + deviceIPAdd + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_MMI_FW");

                UploadMultipart("application/octet-stream", "http://" + deviceIPAdd + "/cgi-bin/upgrade.cgi?cmd=TOGGLE_ALL_FW&TOGGLE_CPU_APP&TOGGLE_DSP_FW&TOGGLE_FPGA_FW&TOGGLE_PIC_FW&TOGGLE_MMI_FW&");
                
            }
            catch (Exception ex)
            {
                string Errormessage = ex.Message;
            }
        }

        /// <summary>
        /// This method is used to upload data to device using Webclient method
        /// </summary>
        /// <param name="file">Contains the firmware upgrade file path</param>
        /// <param name="filename">Name of the binary file</param>
        /// <param name="contentType"></param>
        /// <param name="url">Address of the device whose firmware to be upgraded</param>

        public Boolean UploadMultipart(byte[] file, string filename, string contentType, string url)
        {
            try
            {
                var webClient = new WebClientWithTimeout();
                webClient.Timeout = 600000;
                string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
                webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);
                var fileData = webClient.Encoding.GetString(file);
                var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"file\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n{3}\r\n--{0}--\r\n", boundary, filename, contentType, fileData);

                var nfile = webClient.Encoding.GetBytes(package);
                byte[] resp = webClient.UploadData(url, "POST", nfile);

                string result = ASCIIEncoding.ASCII.GetString(resp);

                if (result.Contains("Firmware Upgradation For CPU_APPLICATION Completed"))
                {
                    Log.addLine("Firmware sent to device successfully", "Pass", "");
                    return true;
                }
                else
                {
                    Log.addLine("Firmware sent to device unsuccessfully", "Fail", "");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.addLine("Exception occured in Firmware package sent to device", "Fail", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This method is used to toggle the image of the firmware
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="url">Address of the device whose firmware to be upgraded</param>

        public bool UploadMultipart(string contentType, string url)
        {
            try
            {
                var webClient = new WebClientWithTimeout();
                webClient.Timeout = 600000;
                string boundary = "------------------------" + DateTime.Now.Ticks.ToString("x");
                webClient.Headers.Add("Content-Type", "multipart/form-data; boundary=" + boundary);

                var package = string.Format("--{0}\r\nContent-Disposition: form-data; name=\"ckbox\"\r\n\r\nTOGGLE_CPU_APP\r\n--{0}\r\nContent-Disposition: form-data; name=\"ckbox\"\r\n\r\nTOGGLE_DSP_FW\r\n--{0}\r\nContent-Disposition: form-data; name=\"ckbox\"\r\n\r\nTOGGLE_FPGA_FW\r\n--{0}\r\nContent-Disposition: form-data; name=\"ckbox\"\r\n\r\nTOGGLE_PIC_FW\r\n--{0}\r\nContent-Disposition: form-data; name=\"ckbox\"\r\n\r\nTOGGLE_MMI_FW\r\n--{0}", boundary);

                var nfile = webClient.Encoding.GetBytes(package);
                byte[] resp = webClient.UploadData(url, "POST", nfile);

                string result = ASCIIEncoding.ASCII.GetString(resp);

                if (result.Contains("SUCCESS"))
                {
                    Log.addLine("Firmware toggled successfully", "Pass", "");
                    return true;
                }
                else
                {
                    Log.addLine("Firmware toggled unsuccessfully", "Fail", "");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Log.addLine("Exception occured in toggle", "Fail", ex.Message);
                return false;
            }
        }


        public Boolean SeleniumDriverInitialise()
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content", 
                "test-type","ignore-certificate-errors","disable-extensions"});
                options.AddUserProfilePreference("credentials_enable_service", false);
                options.AddUserProfilePreference("profile.password_manager_enabled", false);

                driver = new ChromeDriver(Directory.GetCurrentDirectory() + "\\Driver\\net40", options);
                driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                Log.addLine("Chromedriver instance initialised successfully", "Pass", "");
                return true;
            }
            catch(Exception ex)
            {
                Log.addLine("Error in Chromedriver initialisation", "Fail", ex.Message);
                return false;
            }
        }

        public bool DisableProtocol(string deviceIPAddress)
        {

            try
            {
                //Ping pinger = new Ping();
                //PingReply reply;

                Log.addLine("Check that protocol is enabled/disabled", "", "");
                if (SeleniumDriverInitialise())
                {
                    reply = pinger.Send(deviceIPAddress);
                    if (reply.Status.ToString().Equals("Success"))
                    {

                        Log.addLine("Login to Tabindex Page", "", "");
                        driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIPAddress + "/tabindex");
                        Thread.Sleep(3000);
                        driver.Navigate().GoToUrl(@"http://" + deviceIPAddress + "/tabindex");
                        //driver.SwitchTo().Alert().SendKeys("qualitrol");
                        Thread.Sleep(2000);
                        // Password();
                        // Thread.Sleep(1000);

                        driver.FindElement(By.XPath("//*[contains(text(),'Configuration')]")).Click();
                        Thread.Sleep(1000);
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe")));
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[3]")).Click();
                        Thread.Sleep(1000);
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[3]/iframe")));
                        Thread.Sleep(1000);
                        driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[4]/tbody/tr/td/a")).Click();
                        Thread.Sleep(1000);
                        driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[4]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a/img")).Click();
                        Thread.Sleep(1000);
                        var Protocol_Status = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[4]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[2]/tbody/tr/td[3]/span/a/select"));

                        OpenQA.Selenium.Support.UI.SelectElement test = new OpenQA.Selenium.Support.UI.SelectElement(Protocol_Status);

                        Thread.Sleep(1000);
                        if (test.SelectedOption.Text == "ENABLE")
                        {
                            Log.addLine("Protocol is Enabled, need to remove it", "", "");
                            test.SelectByText("DISABLE");
                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(1000);
                            var btn_Commit = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]"));
                            Thread.Sleep(1000);
                            btn_Commit.Click();

                            Log.addLine("Protocol is disabled successfully", "", "");
                            Thread.Sleep(1000);
                            driver.SwitchTo().DefaultContent();
                            Thread.Sleep(1000);
                            IWebElement Data = driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]"));
                            Thread.Sleep(1000);
                            Data.Click();
                            Thread.Sleep(1000);
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe")));
                            Thread.Sleep(1000);
                            IWebElement soh = driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[3]"));
                            soh.Click();
                            Thread.Sleep(1000);
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[3]/iframe")));
                            Thread.Sleep(1000);
                            driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                            Thread.Sleep(1000);
                            var soh_control = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/a"));
                            Thread.Sleep(1000);
                            soh_control.Click();
                            Thread.Sleep(1000);
                            driver.SwitchTo().ParentFrame();
                            Thread.Sleep(1000);
                            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
                            js.ExecuteScript("arguments[0].value='soh:soh/control/reset_cashel=1';", driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[2]")));
                            Thread.Sleep(1000);
                            driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]")).Click();
                            Thread.Sleep(1000);

                            Log.addLine("Rebooted the device", "", "");
                            Thread.Sleep(20000);

                            reply = pinger.Send(deviceIPAddress);

                            do
                            {
                                reply = pinger.Send(deviceIPAddress);
                            }
                            while (!reply.Status.ToString().Equals("Success"));

                            Thread.Sleep(50000);
                            driver.Dispose();
                            return true;

                        }
                        else
                        {
                            Log.addLine("Protocol is already disabled", "", "");
                            driver.Dispose();
                            return true;
                        }
                    }
                    else
                    {
                        Log.addLine("Device is not responding", "Fail", "");
                        driver.Dispose();
                        return false;
                    }
                }
                driver.Dispose();
                return false;
            }
            catch (Exception ex)
            {
                Log.addLine("Some exception occured in disable protocol", "Fail", ex.Message);
                return false;
            }
        }

        public void Password()
        {
            Process scriptProc = new Process();
            scriptProc.StartInfo.FileName = Directory.GetCurrentDirectory() + "\\Password.vbs";
            scriptProc.Start();
            scriptProc.WaitForExit();
            scriptProc.Close();
        }

        public void ConfigureCabling_3U(string deviceIP,string Filename)
        {      
            try
            {
                // Log.addLine("Login to Tabindex Page", "", "");
                
                SeleniumDriverInitialise();
                driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");

                Thread.Sleep(2000);

                driver.FindElement(By.XPath("//*[contains(text(),'Configuration')]")).Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe"))); //switch to dfr frame
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[6]")).Click();
                Thread.Sleep(1000);

                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[6]/iframe"))); //switch to analog channel frame
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                Thread.Sleep(1000);

                driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a")).Click();
                
                for (int i = 0; i < 18; i++)
                {
                    IWebElement Ch = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table["+(i+1)+"]/tbody/tr/td[2]/a"));
                    Ch.Click();
                    var Ch_Busbar_select = new SelectElement(driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table["+(i+1)+"]/tbody/tr/td[2]/div/table[5]/tbody/tr/td[3]/span/a/select")));
                    //Ch0_Busbar_select.SelectByText("BUSBAR1");
                    Ch_Busbar_select.SelectByText(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "Cabling",i,"busbar"));//Filename
                    var Ch_Feeder_Select = new SelectElement(driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table["+(i + 1)+"]/tbody/tr/td[2]/div/table[4]/tbody/tr/td[3]/span/a/select")));
                    Ch_Feeder_Select.SelectByText(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "Cabling", i, "feeder_number"));//Filename
                    var Ch_Phase_Select = new SelectElement(driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[" + (i + 1) + "]/tbody/tr/td[2]/div/table[6]/tbody/tr/td[3]/span/a/select")));
                    Ch_Phase_Select.SelectByText(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "Cabling", i, "phase"));//Filename
                    var Ch_usage_Select = new SelectElement(driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[" + (i + 1) + "]/tbody/tr/td[2]/div/table[9]/tbody/tr/td[3]/span/a/select")));
                    Ch_usage_Select.SelectByText(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "Cabling", i, "usage"));//Filename
                }

                //Commit Changes
                driver.SwitchTo().ParentFrame();
                //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe")));
                var btn_Commit = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]"));
                btn_Commit.Click();

                //Reboot device
                driver.SwitchTo().DefaultContent();

                IWebElement Data = driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]"));  //Data Tab navigation to apply cabling
                Data.Click();
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe")));

                IWebElement soh = driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[3]"));
                soh.Click();

                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[3]/iframe")));
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                var soh_control = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/a"));
                soh_control.Click();
                driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[4]/tbody/tr/td[2]/span/a/input")).Clear();
                driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[4]/tbody/tr/td[2]/span/a/input")).SendKeys("1");
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a")).Click();
                driver.SwitchTo().ParentFrame();
                Thread.Sleep(1000);
                IJavaScriptExecutor exescript = (IJavaScriptExecutor)driver;
                Thread.Sleep(1000);
                exescript.ExecuteScript("arguments[0].value='soh:soh/control/reset_cashel=1';", driver.FindElement(By.XPath(("/html/body/div/table/tbody/tr/td[1]/form/input[2]"))));
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]")).Click();

                Thread.Sleep(40000);

                do
                {
                    reply = pinger.Send(deviceIP);
                }
                while (!reply.Status.ToString().Equals("Success"));

                Thread.Sleep(60000);

                driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");

                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]")).Click();  //Data Tab navigation to apply cabling
                //Data.Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe")));

                Thread.Sleep(1000);
                IWebElement pmp = driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[4]"));
                pmp.Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[4]/iframe")));  //Data Page
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));

                var pmp_Data = driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
                pmp_Data.Click();

                //Check pmp cabling
                var PMP_PQCabling = new SelectElement(driver.FindElement(By.Name("pmp:pmp/data/pq_cabling_config")));
               // var PMP_PQCabling = new SelectElement(driver.FindElement(By.XPath("//html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[26]/tbody/tr/td[2]/span/a/select"))); //html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[26]/tbody/tr/td[2]/span/a/select
                string PMP_PQCabling_Text = PMP_PQCabling.SelectedOption.Text.ToString();

                var PMP_FRCabling = new SelectElement(driver.FindElement(By.Name("pmp:pmp/data/fr_cabling_config")));
                string PMP_FRCabling_Text = PMP_FRCabling.SelectedOption.Text.ToString();


                if (rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "Cabling", 0, "PMP_PQ_Cabling") == PMP_PQCabling_Text) //Filename
                {
                    //Pass
                }
                else
                {
                    //fail
                }

                if (rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "Cabling", 0, "PMP_FR_Cabling") == PMP_FRCabling_Text) //Filename
                {
                    //Pass
                }
                else
                {
                    //fail
                }

                driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//*[contains(text(),'Configuration')]")).Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe"))); //switch to dfr frame
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//a[contains(text(),'pqp')]")).Click();
                Thread.Sleep(10000);

                var pqp_get_attribute = (driver.FindElement(By.XPath("//a[contains(text(),'pqp')]")).GetAttribute("href"));
                var pqp_locate_number = pqp_get_attribute.Substring(pqp_get_attribute.Length - 4, 2);
                //var test1 = (test.GetAttribute("href");
               // driver.FindElement(By.XPath("//a[contains(text(),'pqp')]")).Click();

                //Thread.Sleep(10000);
                
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[" + (Convert.ToUInt32(pqp_locate_number) + 1) + "]/iframe"))); //switch to pqpframe
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));


                //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[21]/iframe"))); //switch to pqpframe
                //driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a")).Click(); //click on b1_config
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a")).Click(); //click on record config
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/a")).Click(); //click on paramid_list
                string pq_config_param = string.Empty;
                for (int inputcalcnum = 0; inputcalcnum < 1189; inputcalcnum++)
                {

                    if (rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "StandaloneParameters", inputcalcnum, "CalcNo") != "16777215") //Filename
                    {
                        //driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).Clear();

                        // driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).SendKeys(""+inputcalcnum);
                        //driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).Click();
                        if (inputcalcnum == 0)
                        {
                            pq_config_param ="pqp:pqp/config/b1_config/record_config/paramid_list/calc_no[" + inputcalcnum + "]=" + Convert.ToInt64(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "StandaloneParameters", inputcalcnum, "CalcNo"));
                        }
                        else
                        {
                            pq_config_param = pq_config_param +"&"+ "pqp:pqp/config/b1_config/record_config/paramid_list/calc_no[" + inputcalcnum + "]=" + Convert.ToInt64(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "StandaloneParameters", inputcalcnum, "CalcNo"));
                        }

                    }
                    else if (rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "StandaloneParameters", inputcalcnum, "CalcNo") == "16777215")
                    {
                        //    driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).Clear();
                        //    driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).Click();
                        //    driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).SendKeys(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "StandaloneParameters", inputcalcnum, "CalcNo"));
                        //    driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (inputcalcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).Click();
                        pq_config_param = pq_config_param + "&" + "pqp:pqp/config/b1_config/record_config/paramid_list/calc_no[" + inputcalcnum + "]=" + Convert.ToInt64(rdexcel.ReadExcel(@"C:\Users\rdev\Documents\My Received Files\3U_Standalone.xlsx", "StandaloneParameters", inputcalcnum, "CalcNo"));
                    break;
                    }
                    else
                    {
                        break;
                    }

                }

                //IJavaScriptExecutor test2 = (IJavaScriptExecutor)driver;
                //test2.ExecuteScript(@"arguments[0].value=""13"";", driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[2]/tbody/tr/td[3]/span/a/input")));
               // driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/a")).Click();
                //driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/a")).Click();

                //Commit Changes
                driver.SwitchTo().ParentFrame();
              //  string pqp_Hidden = driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[2]")).GetAttribute("value");
                //driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe")));
                IJavaScriptExecutor test = (IJavaScriptExecutor)driver;
                test.ExecuteScript("arguments[0].value='"+ pq_config_param + "';", driver.FindElement(By.XPath(("/html/body/div/table/tbody/tr/td[1]/form/input[2]"))));
                driver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]")).Click();//commit changes

                driver.Quit();
               // do
               // {
               //     Thread.Sleep(1000);
               // }
               // while ((DateTime.Now.Minute % 10) == 0);                
                
           
               //// string Start_Time = DateTime.Now.ToString(@"dd\/MM\/yyyy hh:mm");
               // string Start_Time = DateTime.Now.AddMinutes(10).ToString(@"dd\/MM\/yyyy hh:mm");
               // Thread.Sleep(1200000);   //wait 20 minute to accumulate PQ 10 min and Free Interval Data

               // //Goto Records to download PQ 10 min record and PQ Free Interval Record

               // driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[4]")).Click();  //Click on Record tab

               // driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[4]/iframe"))); //switch to Record page frame
               // driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
               // Thread.Sleep(1000);
               // driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/table[1]/tbody/tr/td/a")).Click();
               // driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a[3]")).Click(); //////*[@id="PQForm"]
                                                                                               
               // Thread.Sleep(1000);

               // var StartTime_Editbox = driver.FindElements(By.XPath("//*[@id='StartTime']"));
               // var EndTime_Editbox = driver.FindElements(By.XPath("//*[@id='EndTime']"));
               // StartTime_Editbox[1].Clear();
               // Thread.Sleep(2000);
               // StartTime_Editbox[1].SendKeys(Convert.ToString(Start_Time));
               // EndTime_Editbox[1].Clear();
               // EndTime_Editbox[1].SendKeys(Start_Time);
               // Thread.Sleep(1000);
               // driver.FindElement(By.XPath("//*[@id='PQForm']/tbody/tr/td/form/a/input[1]")).Click();
               // Thread.Sleep(10000);
               // var Returnbtn=driver.FindElements(By.XPath("//*[@id='Cancel']"));
               // Returnbtn[2].Click();             
                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                driver.Quit();
               
            }
        }

        public void GetCalcNo(string filepath,string deviceIP)
        {
            try
            {
                SeleniumDriverInitialise();
               // rdexcel = new Read_WriteExcel();
                driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");

                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]")).Click();  //Data Tab navigation to apply cabling
                //Data.Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe")));

                Thread.Sleep(1000);
                IWebElement pmp = driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[4]"));
                pmp.Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[4]/iframe")));  //Data Page
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));

                var pmp_Data = driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
                pmp_Data.Click();

                //Check pmp cabling
                var PMP_PQCabling = new SelectElement(driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[25]/tbody/tr/td[2]/span/a/select")));
                string PMP_PQCabling_Text = PMP_PQCabling.SelectedOption.Text.ToString();

                var PMP_FRCabling = new SelectElement(driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[26]/tbody/tr/td[2]/span/a/select")));
                string PMP_FRCabling_Text = PMP_FRCabling.SelectedOption.Text.ToString();
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[5]/tbody/tr/td[2]/a")).Click();

                
                for (int chnlnum = 0; chnlnum < 12; chnlnum++)
                {
                    dspchannelmap[chnlnum] = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[5]/tbody/tr/td[2]/div/table["+(chnlnum+1)+"]/tbody/tr/td[3]/span/a/input")).GetAttribute("value");
                }
                driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[6]/tbody/tr/td[2]/a")).Click();

                for (int chnlnum = 0; chnlnum < 12; chnlnum++)
                {

                    dspchannelmap[chnlnum+12] = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[6]/tbody/tr/td[2]/div/table[" + (chnlnum+1) + "]/tbody/tr/td[3]/span/a/input")).GetAttribute("value");
                }

                //Calc Number collection

                driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");
                Thread.Sleep(2000);
                driver.FindElement(By.XPath("//*[contains(text(),'Configuration')]")).Click();
                Thread.Sleep(1000);
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe"))); //switch to dfr frame
                Thread.Sleep(1000);
                //driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[21]")).Click();
                var pqp_get_attribute= (driver.FindElement(By.XPath("//a[contains(text(),'pqp')]")).GetAttribute("href"));
                var pqp_locate_number = pqp_get_attribute.Substring(pqp_get_attribute.Length - 4, 2);
                //var test1 = (test.GetAttribute("href");
                driver.FindElement(By.XPath("//a[contains(text(),'pqp')]")).Click();
                
                Thread.Sleep(10000);
               // driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']//div[2]//iframe[contain(@src,'cashelconfig.cgi?instance=pqp')]"))); //switch to pqpframe
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div["+ (Convert.ToUInt32(pqp_locate_number) +1)+ "]/iframe"))); //switch to pqpframe
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a")).Click(); //click on b1_config
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a")).Click(); //click on record config
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/a")).Click(); //click on paramid_list
                                                  ///html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div

                for (int calcnum = 0; calcnum < 1189; calcnum++)
                {
                    pq_10min_calcnum[calcnum] = driver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/div/table[" + (calcnum + 1) + "]/tbody/tr/td[3]/span/a/input")).GetAttribute("value");
                    if ("16777215" == pq_10min_calcnum[calcnum])
                    {
                        break;
                    }
                    
                }

                rdexcel.Dspchannelmap = this.dspchannelmap;
                rdexcel.Pq_10min_calcnum = this.pq_10min_calcnum;
                rdexcel.Pmp_PQcabling = PMP_PQCabling_Text;
                rdexcel.Pmp_FRcabling = PMP_FRCabling_Text;
                rdexcel.WriteExcel_DSPChannelmap(filepath);
                rdexcel.WriteExcel_CablingPQ_FR(filepath);
                rdexcel.WriteExcel_Standaloneparam(filepath);
                driver.Quit();
            }

            catch (Exception ex)

            {
                Console.WriteLine(ex.Message);
            }
        }


        public void DownloadPQ10min(string deviceIP,string StartTime,string Cabling)
        {
            try
            {
                SeleniumDriverinitialise_DownloadAtSpecificPath(Cabling);
                driver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
                Thread.Sleep(3000);
                driver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");

                driver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[4]")).Click();  //Click on Record tab

                driver.SwitchTo().Frame(driver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[4]/iframe"))); //switch to Record page frame
                driver.SwitchTo().Frame(driver.FindElement(By.XPath("/html/body/iframe")));
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/table[1]/tbody/tr/td/a")).Click();
                driver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a[3]")).Click(); //////*[@id="PQForm"]

                Thread.Sleep(1000);

                var StartTime_Editbox = driver.FindElements(By.XPath("//*[@id='StartTime']"));
                var EndTime_Editbox = driver.FindElements(By.XPath("//*[@id='EndTime']"));
                StartTime_Editbox[1].Clear();
                Thread.Sleep(2000);
                StartTime_Editbox[1].SendKeys(StartTime);
                EndTime_Editbox[1].Clear();
                EndTime_Editbox[1].SendKeys(StartTime);
                Thread.Sleep(1000);
                driver.FindElement(By.XPath("//*[@id='PQForm']/tbody/tr/td/form/a/input[1]")).Click();
                Thread.Sleep(10000);
                var Returnbtn = driver.FindElements(By.XPath("//*[@id='Cancel']"));
                Returnbtn[2].Click();
            }

            catch (Exception ex)
            {
                driver.Quit();
            }
        }

        public void SeleniumDriverinitialise_DownloadAtSpecificPath(string Cabling)
        {        
                try
                {
                UTC_Time = DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss");
                if (Directory.Exists(Directory.GetCurrentDirectory() + @"\PQFileDownload"+@"\"+ Cabling+"_"+ UTC_Time))
                {
                  
                }
                else
                {
                    Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\PQFileDownload" + @"\" + Cabling + "_" + UTC_Time);
                    
                }
                ChromeOptions options = new ChromeOptions();
                    options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content",
                "test-type","ignore-certificate-errors","disable-extensions"});
                    options.AddUserProfilePreference("credentials_enable_service", false);
                    options.AddUserProfilePreference("profile.password_manager_enabled", false);
                    options.AddUserProfilePreference("download.default_directory", Directory.GetCurrentDirectory() + @"\PQFileDownload" +@"\"+Cabling+"_"+UTC_Time);
                driver = new ChromeDriver(Directory.GetCurrentDirectory() + "\\Driver\\net40", options);
                    driver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(10));
                   // Log.addLine("Chromedriver instance initialised successfully", "Pass", "");
    
                }
                catch (Exception ex)
                {
                    //Log.addLine("Error in Chromedriver initialisation", "Fail", ex.Message);
     
                }
           
        }

    }
}
