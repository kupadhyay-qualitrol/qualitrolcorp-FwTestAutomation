//**************************************************************
// Class Name  :  ApplicationSpecific  
// Purpose     :  This class contains methods and class to run the Test function
// Modification History:
//  Ver #        Date         Author/Modified By       Remarks
//--------------------------------------------------------------
//   1.0        10-May-17      Rahuldev Gupta           Initial   

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Threading;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Remote;
using System.Windows.Forms;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Data;
using System.Linq;


namespace SeleniumProject
{
    public class ApplicationSpecific
    {
        
        public class AnalogChannel
        {
            public float fMultiplier { get; set; }
            public float fOffset { get; set; }
            public float fCalculatedRMS { get; set; }

            public AnalogChannel(float multiplier, float offset)
            {
                fMultiplier = multiplier;
                fOffset = offset;
            }
        }

        public class CFGData
        {
            public int iNoofTotalchannels { get; set; }
            public int iNoOfAnalogChannels { get; set; }
            public int iNoOfDigitalChannels { get; set; }
            public float frequency { get; set; }
            public List<AnalogChannel> lsAnalogChannelsList { get; set; }
            public int iNoOfSamples { get; set; }
            public float fSampleRate { get; set; }
            public DateTime dtStartDateTime { get; set; }
            public DateTime dtTriggerDateTime { get; set; }

            public CFGData()
            {
                lsAnalogChannelsList = new List<AnalogChannel>();
            }
        }
        
        public IWebDriver Chromedriver;        //Variable created to launch a session for chrome driver
        
        ObjectRepository ObjectRepository = new ObjectRepository();    //Variable contains locator ids     
        
        ChromeOptions options = new ChromeOptions(); //variable created for launching chrome with desired settings
        
        Ping PingDevice = new Ping(); //Variable created to check the ping status of device

        /// <summary>
        /// This function will execute all the Test function which are called inside it.
        /// </summary>
        public void TestcaseRun()
        {
            fn_upgrade_devices();    
            fn_ValidateDFR();       
        }       

        /// <summary>
        /// This function will launch the google chrome and start selenium session with a setting change of not to prompt 
        /// for "Save As" dialog for saving upgrade.cgi file.
        /// </summary>

        public void LaunchBrowserUpgrade()
        {
            try
            {
                //Changing the chrome settings as per requirement before launching the chrome browser.
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("browser.helperApps.neverAsksaveToDisk", "wwwserver/shellcgi");
                options.AddUserProfilePreference("browser.helperApps.alwaysAsk.force", false);
                options.AddUserProfilePreference("download.default_directory", "C:\\TestDownload");     //Setting default directory path of chrome        
                options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content", 
                "test-type","ignore-certificate-errors","disable-extensions"});


                Chromedriver = new ChromeDriver("C:\\Users\\rdev\\Downloads\\selenium-dotnet-3.0.0\\net40", options); //launching the chrome browser
            }
            catch (Exception ex)
            { 
            
            }
        }
        
        /// <summary>
        /// This function will launch the google chrome and start selenium session with a setting change of not to prompt 
        /// for "Save As" dialog for saving upgrade.cgi file.
        /// </summary>
        
        public void LaunchBrowser()
        {
            try
            {
                //Changing the chrome settings as per requirement before launching the chrome browser.
                options.AddUserProfilePreference("download.prompt_for_download", true);
                options.AddUserProfilePreference("browser.helperApps.neverAsksaveToDisk", "wwwserver/shellcgi");
                options.AddUserProfilePreference("browser.helperApps.alwaysAsk.force", false);
                options.AddUserProfilePreference("download.default_directory", "C:\\TestDownload");     //Setting default directory path of chrome    
                options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content", 
                "test-type","ignore-certificate-errors","disable-extensions"});

                Chromedriver = new ChromeDriver("C:\\Users\\rdev\\Downloads\\selenium-dotnet-3.0.0\\net40", options);   //launching the chrome browser                  
            }
            catch (Exception ex)
            { 
            
            }
        }

         /// <summary>
        /// This method is used to Open the device page(mfgindex,tabindex or any webpage)
        /// </summary>
        /// <param name="URL"> 
        /// string URl is needs to have Username & password if authentication is required for the page
        /// Format to be http://Username:password@webaddress for ex:- http://qualitrol:qualcorp_techSupport10@192.168.1.19/tabindex
        /// </param>
        public void OpenURL(string URL)
        {
            try
            {
                
                if (!(URL.ToString().Equals("")))
                {
                    Chromedriver.Navigate().GoToUrl(URL);
                }
                else
                {
                    //Print Message Invalid URL
                }
            }

            catch (Exception ex)
            {
                string Err = ex.Message;
                //Print Err
            }
        }
        /// <summary>
        /// This method is used to extract the data from DFR .DAT file and then store it in a table. 
        /// Calculate the instantaneous values as per (ax+b) where a is multiplier & b is offset for particular channel in cfg file.
        /// Then it calculate the RMS values considering samples equivalent to 2 cycles till (last sample -2*samplingrate -1)
        /// Pass/Fail it depending upon the RMS values is within range or not.
        /// </summary>
        /// <param name="cfg">cfg variable contains the values multiplier,offser present in config file of DFR</param>
        /// <param name="Record">Record is the value of the latest DFR trigger generated</param>
        public void fn_AnalogData(CFGData cfg,int Record)
        {
            try
            {
                DataTable AnalogData = new DataTable();
                AnalogData.Clear();
                string colName = string.Empty;
                string colExpr = string.Empty;
                ;
                for (int i = 1; i <= cfg.iNoOfAnalogChannels; i++)
                {
                    colName = "Channel" + i;
                    colExpr = string.Format("(({0}*{1})+{2}) * (({0}*{1})+{2})", colName, cfg.lsAnalogChannelsList[i - 1].fMultiplier, cfg.lsAnalogChannelsList[i - 1].fOffset);
                    AnalogData.Columns.Add("Channel" + i,typeof(float)); //Channel1,Channel2,...
                    AnalogData.Columns.Add("Calculated_"+ colName, typeof(float),colExpr);//Calculated_Channel1,Calculated_Channel2,...
                    AnalogData.Columns.Add("RMS_" + colName, typeof(float));//Calculated_Channel1,Calculated_Channel2,...
                }
                
                string[] line;
                FileStream reader = File.OpenRead(@"C:\TestDownload\DFRRecord"+Record+".DAT");
                //float temp;
                using (TextFieldParser parser = new TextFieldParser(reader)) //lOOPS THROUGH FILE
                {
                    parser.TrimWhiteSpace = true; // if you want
                    parser.Delimiters = new[] { "," };
                    parser.HasFieldsEnclosedInQuotes = true;
                  
                    while (!parser.EndOfData)
                    {
                        line = parser.ReadFields(); //EACH STRING IN CURRENT LINE SEPEARATED BY DELIMITER
                        DataRow RMSValues = AnalogData.NewRow();
                        
                        for (int Value = 1; Value <= cfg.iNoOfAnalogChannels; Value++)
                        {
                            RMSValues["Channel" + Value] = Convert.ToInt32(line[Value + 1]);

                        }
                        AnalogData.Rows.Add(RMSValues);
                    }

                    int sampleDataForRMSCalc = (int)(cfg.fSampleRate / cfg.frequency) * 2;
                    float temp1;

                    for (int j=1; j<=cfg.iNoOfAnalogChannels;j++)
                    {
                        for (int i = 0; i < (AnalogData.Rows.Count - sampleDataForRMSCalc - 1); i++)
                        {
                            var result = AnalogData.AsEnumerable().Where((row, index) => index >= i && index < i + sampleDataForRMSCalc);
                            var temp = from row in result
                                       select row.Field<float>("Calculated_Channel"+j);
                            temp1 = (float)Math.Sqrt(temp.Cast<float>().Average());
                            AnalogData.Rows[i]["RMS_Channel" + j] = temp1;
                            if (temp1 >= 0.5 || temp1 <= -0.5)
                            {
                                var temp2= false;

                            }
                            else
                            {
                                var temp2= true;
                            }
                        }
                    }
                }
                
            }
            catch(Exception ex)
            {
                string err =ex.Message;

            }
            
        }
        /// <summary>
        /// This method extract the pre fault & post fault value from the tabindex.
        /// Also calculate the record length based on samples (total number of samples/Sampling rate)*cycle timeperiod
        /// Depending upon value calculated from tabindex and calculated from samples pass/fail the fucntion
        /// </summary>
        /// <param name="Samples">this contains information of the data from DFR config file.</param>
        /// <returns>Returns true/false depending upon Test Case Pass/Fail</returns>
        public bool fn_ValidatePre_Postfault(CFGData Samples)
        {
            try
            {
                
                //  LaunchBrowser();
               // OpenURL("http://qualitrol:qualcorp_techSupport10@192.168.1.19/tabindex");
                Chromedriver.SwitchTo().DefaultContent();
                Chromedriver.FindElement(By.XPath(ObjectRepository.tab_Configuration)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Config_LDLicense)));
                Chromedriver.FindElement(By.XPath(ObjectRepository.Dfr)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Control_Data)));
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Sub_Control_Data)));

                Chromedriver.FindElement(By.XPath(ObjectRepository.ExterTrigger)).Click();

                int Recordlength = Convert.ToInt32(Chromedriver.FindElement(By.XPath(ObjectRepository.Prefault)).GetAttribute("value")) + Convert.ToInt32(Chromedriver.FindElement(By.XPath(ObjectRepository.Exter_Trigger_PostFault)).GetAttribute("value"));

                int SamplingRate = (int)(Samples.fSampleRate / Samples.frequency);

                if (Samples.frequency==50)
                {
                    if (((int)(Samples.iNoOfSamples / SamplingRate) * 20) == Recordlength)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }

                else if (Samples.frequency == 60)
                {
                    if (((int)(Samples.iNoOfSamples / SamplingRate) * 16.67) == Recordlength)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
                return false;
            }
        }

        /// <summary>
        /// This method genertes the Manual Trigger and then download the record data and cfg file.
        /// After downloading the file in the folder it calls the fn_ValidatePre_Postfault & fn_AnalogData.
        /// </summary>
        public void fn_ValidateDFR()
        {
            TestReport TestReport = new TestReport("Google Chrome", "http://qualitrol:qualcorp_techSupport10@192.168.1.19/tabindex");
            try
            {
                LaunchBrowser();               
               
                if (Chromedriver.FindElement(By.XPath(ObjectRepository.tab_Data)).Displayed)
                {
                    TestReport.addLine("Launching the Google Chrome Browser", "Pass", "");
                }
                else
                {

                    PingReply Device_Reply = PingDevice.Send("192.168.1.19");
                    TestReport.addLine("Unable to connect to device", "Fail", Device_Reply.ToString());
                    goto Finish;

                }
                
                IJavaScriptExecutor js = (IJavaScriptExecutor)Chromedriver;
                
                OpenURL("http://qualitrol:qualcorp_techSupport10@192.168.1.19/tabindex");

                
                Chromedriver.FindElement(By.XPath(ObjectRepository.tab_Data)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_LDLicense)));
                Chromedriver.FindElement(By.XPath(ObjectRepository.Dfr)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Control_Data)));
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Sub_Control_Data)));

                Chromedriver.FindElement(By.XPath(ObjectRepository.Expand_Data)).Click();
                Chromedriver.FindElement(By.XPath(ObjectRepository.Runtimeinfo)).Click();  //Expand Runtimeinfo 
                
                string Record = Chromedriver.FindElement(By.XPath(ObjectRepository.Runtime_RecordNumber)).GetAttribute("value");
                int Record_num = Convert.ToInt32(Record);
                Chromedriver.FindElement(By.XPath(ObjectRepository.Expand_Control)).Click();
                Chromedriver.FindElement(By.XPath(ObjectRepository.Manual_Trigger)).Click();  //Expand Manual Trigger

                Chromedriver.SwitchTo().ParentFrame();
                js.ExecuteScript("arguments[0].value='dfr:dfr/control/manual_trigger/no_triggers=1';", Chromedriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[2]")));
                Thread.Sleep(2000);
                Chromedriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]")).Click();


                Chromedriver.SwitchTo().DefaultContent();


                Chromedriver.FindElement(By.XPath(ObjectRepository.tab_Records)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Rec)));
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame__Rec_Sub)));
                Chromedriver.FindElement(By.XPath(ObjectRepository.Frame__Rec_DFR)).Click();
                Chromedriver.FindElement(By.XPath(ObjectRepository.Rec_No)).Click();
                Chromedriver.FindElement(By.XPath(ObjectRepository.RecordNumber)).Clear();
                
                Chromedriver.FindElement(By.XPath(ObjectRepository.RecordNumber)).SendKeys(Convert.ToString(Record_num+1));
                Chromedriver.FindElement(By.XPath(ObjectRepository.Rec_DFR_Config)).Click();             


                Thread.Sleep(5000);
                SendKeys.SendWait(@"C:\TestDownload\DFRRecord" + (Record_num + 1) + ".CFG");
                Thread.Sleep(2000);
                SendKeys.SendWait(@"{Enter}");

                while (!File.Exists(@"C:\TestDownload\DFRRecord"+(Record_num+1)+".CFG"))
                {
                Thread.Sleep(5000);
                }

                Chromedriver.FindElement(By.XPath(ObjectRepository.Rec_DFR_Data)).Click();

                Thread.Sleep(5000);
                SendKeys.SendWait(@"C:\TestDownload\DFRRecord"+(Record_num+1)+".DAT");
                Thread.Sleep(2000);
                SendKeys.SendWait(@"{Enter}");

                while (!File.Exists(@"C:\TestDownload\DFRRecord"+(Record_num+1)+".DAT"))
                {
                Thread.Sleep(5000);
                }

                List<String[]> fileContent = new List<string[]>();
                CFGData cfg = new CFGData();

                int iLineNo = 0;
                string[] line;
                string[] dateSplit;
                string[] timeSplit;

                FileStream reader = File.OpenRead(@"C:\TestDownload\DFRRecord"+(Record_num+1)+".CFG"); // mind the encoding - UTF8
                using (TextFieldParser parser = new TextFieldParser(reader)) //lOOPS THROUGH FILE
                {
                    parser.TrimWhiteSpace = true; // if you want
                    parser.Delimiters = new[] { "," };
                    parser.HasFieldsEnclosedInQuotes = true;
                    while (!parser.EndOfData)
                    {
                        iLineNo++;
                        line = parser.ReadFields(); //EACH STRING IN CURRENT LINE SEPEARATED BY DELIMITER

                        switch (iLineNo)
                        {
                            case 2:
                                if (line.Length == 3)
                                {
                                    cfg.iNoofTotalchannels = Convert.ToInt32(line[0].Trim());
                                    cfg.iNoOfAnalogChannels = Convert.ToInt32(line[1].Trim().Substring(0, line[1].Trim().Length - 1));
                                    cfg.iNoOfDigitalChannels = Convert.ToInt32(line[2].Trim().Substring(0, line[2].Trim().Length - 1));
                                }
                                break;
                            default:
                                break;
                        }

                        //Analog Channels
                        if (iLineNo > 1 && iLineNo <= cfg.iNoOfAnalogChannels + 2)
                        {
                            if (line.Length == 13)
                            {
                                AnalogChannel ac = new AnalogChannel(float.Parse(line[5].Trim()), float.Parse(line[6].Trim()));
                                cfg.lsAnalogChannelsList.Add(ac);
                            }
                        }
                        // Frequency
                        else if (iLineNo == cfg.iNoofTotalchannels + 3)
                        {
                            cfg.frequency = float.Parse(line[0].Trim());
                        }
                        //Samples Info
                        else if (iLineNo == cfg.iNoofTotalchannels + 5)
                        {
                            cfg.fSampleRate = float.Parse(line[0].Trim());
                            cfg.iNoOfSamples = Convert.ToInt32(line[1].Trim());
                        }
                        //Record Start Date Time
                        else if (iLineNo == cfg.iNoofTotalchannels + 6)
                        {
                            dateSplit = line[0].Trim().Split('/');
                            timeSplit = line[1].Trim().Split(':');
                            int ms = Convert.ToInt32(timeSplit[2].Split('.')[1]) / 1000;
                            cfg.dtStartDateTime = new DateTime(Convert.ToInt32(dateSplit[2]),//Year
                                Convert.ToInt32(dateSplit[1]),//Month
                                Convert.ToInt32(dateSplit[0]),//Date
                                Convert.ToInt32(timeSplit[0]),//Hour
                                Convert.ToInt32(timeSplit[1]),//Minute
                                Convert.ToInt32(timeSplit[2].Split('.')[0]),//Second
                                Convert.ToInt32(timeSplit[2].Split('.')[1]) / 1000);//Millisecond
                        }
                        //Trigger Date Time
                        else if (iLineNo == cfg.iNoofTotalchannels + 7)
                        {
                            dateSplit = line[0].Trim().Split('/');
                            timeSplit = line[1].Trim().Split(':');
                            cfg.dtTriggerDateTime = new DateTime(Convert.ToInt32(dateSplit[2]),//Year
                                Convert.ToInt32(dateSplit[1]),//Month
                                Convert.ToInt32(dateSplit[0]),//Date
                                Convert.ToInt32(timeSplit[0]),//Hour
                                Convert.ToInt32(timeSplit[1]),//Minute
                                Convert.ToInt32(timeSplit[2].Split('.')[0]),//Second
                                Convert.ToInt32(timeSplit[2].Split('.')[1]) / 1000);//Millisecond
                        }
                    }
                }
                if (fn_ValidatePre_Postfault(cfg))
                {
                    bool err = true;
                }
                else
                {
                    bool err = false;
                }
                fn_AnalogData(cfg,(Record_num+1));


            Finish:
                Thread.Sleep(2000);
               
            }

            catch (Exception ex)

            {
                TestReport.addLine("Some exception occured", "Fail", ex.Message);                
            }  
            
            finally
            {
                ExitSelenium();
            }
        
        }

        /// <summary>
        /// This method is used to take the snapshot of the webpage
        /// </summary>
        public void Snapshot()
        {
            try
            {
                Screenshot Snapshot = ((ITakesScreenshot)Chromedriver).GetScreenshot();
                string SnapshotFile = "C:\\Snapshot\\" + "TestResult_" + DateTime.Now.ToString().Replace('-', '_').Replace(':', '_') + ".png";
                Snapshot.SaveAsFile(SnapshotFile, System.Drawing.Imaging.ImageFormat.Png);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// This method is used to upgrade the firmware file.
        /// Check the upgrade status from the mfgindex page.
        /// </summary>
        public void fn_upgrade_devices()
        {
            try
            {
                LaunchBrowserUpgrade();
                OpenURL("http://qualitrol:qualcorp_Upgrade10@192.168.1.21/upgrade");
                               
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_lang)).Click();

                ////CPU binary upgrade
                // Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_CPU)));
                // Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                // Thread.Sleep(5000);
                // SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.12\04.12_73\01-CPU-04.12_73-crc.bin");
                // Thread.Sleep(2000);
                // Thread.Sleep(5000);SendKeys.SendWait(@"{Enter}");
                // Thread.Sleep(5000);
                // Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

                // do
                // {

                //     Thread.Sleep(2000);
                // }
                // while (!File.Exists(@"C:\TestDownload\upgrade.cgi"));
                // File.OpenRead(@"C:\TestDownload\upgrade.cgi");

                // foreach (var line in File.ReadAllLines(@"C:\TestDownload\upgrade.cgi"))
                // {
                //     if (line.Contains("SUCCESS"))
                //     {
                //         if (File.Exists(@"C:\TestDownload\CPU_upgrade.cgi"))
                //         {
                //             File.Delete(@"C:\TestDownload\CPU_upgrade.cgi");
                             
                //         }
                //         File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\CPU_upgrade.cgi");
                //         break;
                //     }
                //     else
                //     {
                //         File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\CPU_upgrade.cgi");
                //         goto Finish;
                //     }
                // }

                 //DSP binary upgrade
                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_DSP)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                 Thread.Sleep(5000);
                 SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.12\DSP\01-DSP-04.12_03_crc.bin");
                 Thread.Sleep(2000);
                 Thread.Sleep(5000); SendKeys.SendWait(@"{Enter}");
                 Thread.Sleep(5000);
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

                 do
                 {

                     Thread.Sleep(2000);
                 }
                 while (!File.Exists(@"C:\TestDownload\upgrade.cgi"));
                 //File.OpenRead(@"C:\TestDownload\upgrade.cgi");

                 bool canClose = false;
                 string[] filecontents = File.ReadAllLines(@"C:\TestDownload\upgrade.cgi");
                 foreach (string line in filecontents)
                 {
                     if (line.Contains("SUCCESS"))
                     {                         
                         canClose = true;
                         bool isexists = File.Exists(@"C:\TestDownload\DSP_upgrade.cgi");
                         isexists = File.Exists(@"C:\TestDownload\upgrade.cgi");
                         if (File.Exists(@"C:\TestDownload\DSP_upgrade.cgi"))
                         {
                             File.Delete(@"C:\TestDownload\DSP_upgrade.cgi");
                         }                         
                         break;
                     }
                 }
                  if (canClose)
                    {                        
                        File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\DSP_upgrade.cgi");
                        
                     }
                  else
                  {
                      File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\DSP_upgrade.cgi");
                      goto Finish;
                  }
                 
                    

                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_toggle)));
                // Chromedriver.FindElement(By.XPath(ObjectRepository.CPU_toggle)).Click();
                 Chromedriver.FindElement(By.XPath(ObjectRepository.DSP_toggle)).Click();

                 Chromedriver.FindElement(By.XPath(ObjectRepository.ToggleAll)).Click();

                 do
                 {

                     Thread.Sleep(2000);
                 }
                 while (!File.Exists(@"C:\TestDownload\upgrade.cgi"));
                // File.OpenRead(@"C:\TestDownload\upgrade.cgi");

                 foreach (var line in File.ReadAllLines(@"C:\TestDownload\upgrade.cgi"))
                 {
                     if (line.Contains("SUCCESS"))
                     {
                         if (File.Exists(@"C:\TestDownload\ToggleAll.cgi"))
                         {
                             File.Delete(@"C:\TestDownload\ToggleAll.cgi");

                         }
                         File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\ToggleAll.cgi");
                         break;
                     }
                     else
                     {
                         File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\ToggleAll.cgi");
                         goto Finish;
                     }
                 }

                 Thread.Sleep(10000);

                 PingReply DeviceReply;

                do
                {
                   DeviceReply = PingDevice.Send("192.168.1.21");
                }
                while (!DeviceReply.Status.ToString().Equals("Success"));
                Thread.Sleep(10000);

                OpenURL("http://qualitrol:qualcorp_techSupport10@192.168.1.21/mfgindex");
                
                Chromedriver.FindElement(By.XPath(ObjectRepository.Diagnostic_Info)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe")));
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Diagnostic_info)));

                string CPU_Appln_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.CPU_Ver)).Text;
                string DSP_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.DSP_Ver)).Text;
                string FPGA_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.FPGA_Ver)).Text;
                string PIC_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.PIC_Ver)).Text;

                //if (CPU_Appln_Ver.Equals("04.12_04"))
                //{
                //    //pass
                //}
                //else
                //{ 
                //    //fails
                //}

                if (DSP_Ver.Equals("04.12_03"))
                {
                    //pass
                }
                else
                {
                    //fails
                }

                //if (FPGA_Ver.Equals("04.12_02"))
                //{
                //    //pass
                //}
                //else
                //{
                //    //fails
                //}

                //if (PIC_Ver.Equals("04.11_06"))
                //{
                //    //pass
                //}
                //else
                //{
                //    //fails
                //}

            Finish: ;  
            }
            catch (Exception ex)
            {
                string err=ex.Message;
            }
            finally
            {
                ExitSelenium();
            }
        }

        public void ExitSelenium()
        {
            Chromedriver.Quit();
        }

    }
}

