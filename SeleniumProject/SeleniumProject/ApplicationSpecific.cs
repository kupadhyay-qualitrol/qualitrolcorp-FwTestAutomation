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
using System.Collections.Specialized;
using System.Threading;


namespace SeleniumProject
{
    public class ApplicationSpecific
    {
        //Variables Declarations

        string deviceIP = ""; //variable to assign the IP address 
        string userName = "qualitrol"; //device login username
        string password_upgrade = "qualcorp_Upgrade10"; //device loginpassword for upgrade
        string password__mfg_tabindex = "qualcorp_techSupport10"; //device loginpassword for tabindex,mfgindex
        string firmwareBinpathCPU = ""; //firmware files path CPU
        string firmwareBinpathDSP = ""; //firmware files path DSP
        string firmwareBinpathFPGA = ""; //firmware files path FPGA
        string firmwareBinpathPIC = ""; //firmware files path PIC
        string firmwareBinpathMMI = ""; //firmware files path MMI

        double InjectedNominalVoltage = 63.5;
        double InjectedNominalCurrent = 1.0;

        string CPUFileName = "";
        string DSPFileName = "";
        string FPGAFileName = "";
        string PICFilename = "";
        string MMIFileName = "";
        
        TestReport testReport; //

        public IWebDriver Chromedriver;        //Variable created to launch a session for chrome driver
        ObjectRepository ObjectRepository = new ObjectRepository();    //Variable contains locator ids     
        ChromeOptions options = new ChromeOptions(); //variable created for launching chrome with desired settings
        Ping PingDevice = new Ping(); //Variable created to check the ping status of device

        DataTable AnalogData = new DataTable(); // to store the RMS,Instantaneous,Phase Angle
        Dictionary<int, float[]> InstantaneousValue_Channel = new Dictionary<int, float[]>(); //to stroe the instantaneous values of voltage or current
        Dictionary<int,string> ChannelType=new Dictionary<int,string>();
        //Class Declarations
        
        internal class ValuePair
        {
            public float Sin, Cos;
        }

        internal class PhaseValueCache
        {
            private Dictionary<long, ValuePair> cache = new Dictionary<long, ValuePair>();

            public Dictionary<long, ValuePair> ValuePairs
            {
                get { return cache; }
            }
        }

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
            public float fSamplingFrequency { get; set; }
            public DateTime dtStartDateTime { get; set; }
            public DateTime dtTriggerDateTime { get; set; }
            public int SamplingRate { get; set; }
            public int iNoofVoltageChannel { get; set; }
            public int iNoofCurrentChannel { get; set; }
            public CFGData()
            {
                lsAnalogChannelsList = new List<AnalogChannel>();
                
            }
            
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

        public ApplicationSpecific()
        {
            TestReport testReport = new TestReport();
            if (!Directory.Exists(Directory.GetCurrentDirectory() + @"\TestDownload"))
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\TestDownload");                
            }            
        }
        
        //Methods

        /// <summary>
        /// This function will execute all the Test function which are called inside it.
        /// </summary>
        public void TestcaseRun()
        {
           // UpgradeCPUApplication();
          //  fn_upgrade_devices();    
            fn_ValidateDFR();       
        }       

        /// <summary>
        /// This function will launch the google chrome and start selenium session with a setting change of not to prompt 
        /// for "Save As" dialog for saving upgrade.cgi file.
        /// </summary>

        public void LaunchBrowserUpgrade()
        {
            testReport.addLine("Method LaunchBrowserUpgrade Called", "","");
                //Changing the chrome settings as per requirement before launching the chrome browser.
                options.AddUserProfilePreference("download.prompt_for_download", false);
                options.AddUserProfilePreference("browser.helperApps.neverAsksaveToDisk", "wwwserver/shellcgi");
                options.AddUserProfilePreference("browser.helperApps.alwaysAsk.force", false);
                options.AddUserProfilePreference("download.default_directory", Directory.GetCurrentDirectory()+@"\TestDownload");     //Setting default directory path of chrome        
                options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content", 
                "test-type","ignore-certificate-errors","disable-extensions"});

                string driverPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

                Chromedriver = new ChromeDriver(driverPath+@"\selenium-dotnet-3.0.0\net40", options); //launching the chrome browser

                if (Chromedriver != null)
                {
                    testReport.addLine("Chrome Driver initialized failed", "Fail", "");
                    testReport.addLine("Method LaunchBrowserUpgrade Ends", "", "");
                }
                else
                {
                    testReport.addLine("Chrome Driver initialized failed", "Fail", "");
                    testReport.addLine("Method LaunchBrowserUpgrade Ends", "", "");
                }
        }
        
        /// <summary>
        /// This function will launch the google chrome and start selenium session with a setting change of not to prompt 
        /// for "Save As" dialog for saving upgrade.cgi file.
        /// </summary>
        
        public void LaunchBrowser()
        {
            testReport.addLine("Method LaunchBrowser Called", "", "");
                //Changing the chrome settings as per requirement before launching the chrome browser.
                options.AddUserProfilePreference("download.prompt_for_download", true);
                options.AddUserProfilePreference("browser.helperApps.neverAsksaveToDisk", "wwwserver/shellcgi");
                options.AddUserProfilePreference("browser.helperApps.alwaysAsk.force", false);
                options.AddUserProfilePreference("download.default_directory", Directory.GetCurrentDirectory() + @"\TestDownload");     //Setting default directory path of chrome    
                options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content", 
                "test-type","ignore-certificate-errors","disable-extensions"});

                string driverPath = Directory.GetParent(Directory.GetCurrentDirectory()).ToString();

                Chromedriver = new ChromeDriver(driverPath + @"\selenium-dotnet-3.0.0\net40", options); //launching the chrome browser

                if (Chromedriver != null)
                {
                    testReport.addLine("Chrome Driver initialized successfully", "Pass", "No Exception");
                    testReport.addLine("Method LaunchBrowser Called", "", "");
                } 
                else
                {
                testReport.addLine("Chrome Driver initialized failed", "Fail", "");
                testReport.addLine("Method LaunchBrowser Ends", "", "");
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
            testReport.addLine("Method LaunchBrowser Called", "", "");
            if (!(URL.ToString().Equals("")))
            {
                Chromedriver.Navigate().GoToUrl(URL);
                testReport.addLine("URL launched successfully", "Pass", "");
            }
            else
            {
                testReport.addLine("Invalid URL or Device is unresponsive", "Fail", "");
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
                string Finish;
                testReport.addLine("Method fn_AnalogData Called","","");
                AnalogData.Clear();
                string colName = string.Empty;
                string colExpr = string.Empty;
                string instValue=string.Empty;

                for (int i = 1; i <= cfg.iNoOfAnalogChannels; i++)
                {
                    colName = "Channel" + i;
                    colExpr = string.Format("(({0}*{1})+{2}) * (({0}*{1})+{2})", colName, cfg.lsAnalogChannelsList[i - 1].fMultiplier, cfg.lsAnalogChannelsList[i - 1].fOffset);
                    instValue = string.Format("(({0}*{1})+{2})", colName, cfg.lsAnalogChannelsList[i - 1].fMultiplier, cfg.lsAnalogChannelsList[i - 1].fOffset);
                    AnalogData.Columns.Add("Channel" + i,typeof(float)); //Channel1,Channel2,...
                    AnalogData.Columns.Add("InstantaneousValue_" + colName, typeof(float), instValue); //Instantaneous Values of Channel1,Channel2,...
                    AnalogData.Columns.Add("Calculated_"+ colName, typeof(float),colExpr);//Calculated_Channel1,Calculated_Channel2,...
                    AnalogData.Columns.Add("RMS_" + colName, typeof(float));//Calculated_Channel1,Calculated_Channel2,...
                    AnalogData.Columns.Add("PhaseAngle_" + colName, typeof(float));//Calculated_Channel1,Calculated_Channel2,...
                }
                testReport.addLine("Method fn_AnalogData: Table constructed successfully with column Names", "", "");
                string[] line;

                if (File.Exists(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + Record + ".DAT"))
                {                    
                    testReport.addLine("Method fn_AnalogData:DFR Record File exists", "Pass", "");
                }
                else
                {
                    testReport.addLine("Method fn_AnalogData:DFR Record File doesn't exist", "Fail", "");
                    goto Finish;
                }
                FileStream reader = File.OpenRead(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + Record + ".DAT");
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
                    
                    int sampleDataForRMSCalc = (int)(cfg.fSamplingFrequency / cfg.frequency) * 2;
                    float temp1;
                    float[] channelData;
                    for (int ch = 1; ch <= cfg.iNoOfAnalogChannels; ch++)
                    {
                        channelData = new float[cfg.iNoOfSamples];
                        for (int l = 0; l <= cfg.iNoOfSamples - 1; l++)
                        {
                            
                            channelData[l] = (float)AnalogData.Rows[l]["InstantaneousValue_Channel" + ch];

                        }
                        InstantaneousValue_Channel.Add(ch, channelData);
                    }

                    for (int j=1; j<=cfg.iNoOfAnalogChannels;j++)
                    {
                        string chtype;
                        ChannelType.TryGetValue(j,out chtype);
                        if(chtype=="V")
                        {
                            for (int i = 0; i < (AnalogData.Rows.Count - sampleDataForRMSCalc - 1); i++)
                            {
                                var result = AnalogData.AsEnumerable().Where((row, index) => index >= i && index < i + sampleDataForRMSCalc);
                                var temp = from row in result
                                           select row.Field<float>("Calculated_Channel" + j);
                                temp1 = (float)Math.Sqrt(temp.Cast<float>().Average());
                                AnalogData.Rows[i]["RMS_Channel" + j] = temp1;

                                if (temp1 >= (InjectedNominalVoltage+ ((InjectedNominalVoltage*0.15)/100)) || temp1 <= (InjectedNominalVoltage-((InjectedNominalVoltage*0.15)/100)))
                                {
                                    testReport.addLine("DFR record is invalid", "Fail", ""); //Condition when nominal injected voltage is 63.5V                               

                                }
                                else
                                {
                                    testReport.addLine("DFR record is Valid", "Pass", ""); //Condition when nominal injected voltage is 63.5V
                                }
                            }
                            
                        }

                        if (chtype == "A")
                        {
                            for (int i = 0; i < (AnalogData.Rows.Count - sampleDataForRMSCalc - 1); i++)
                            {
                                var result = AnalogData.AsEnumerable().Where((row, index) => index >= i && index < i + sampleDataForRMSCalc);
                                var temp = from row in result
                                           select row.Field<float>("Calculated_Channel" + j);
                                temp1 = (float)Math.Sqrt(temp.Cast<float>().Average());
                                AnalogData.Rows[i]["RMS_Channel" + j] = temp1;

                                if (temp1 >= (InjectedNominalCurrent + ((InjectedNominalCurrent * 0.1))) || temp1 <= (InjectedNominalVoltage - (InjectedNominalCurrent*0.1)))
                                {
                                    testReport.addLine("DFR record is invalid", "Fail", ""); //Condition when nominal injected current is 1.0A                               

                                }
                                else
                                {
                                    testReport.addLine("DFR record is Valid", "Pass", ""); //Condition when nominal injected current is 1.0A    
                                }
                            }
                        }
                    }
                    for (int j = 1; j <= cfg.iNoOfAnalogChannels; j++)
                    {
                        for (int i = 0; i < (AnalogData.Rows.Count); i++)
                        {
                            AnalogData.Rows[i]["PhaseAngle_Channel" + j] = PhaseAngleCalculate(cfg, j, i);                            
                        }
                    }

                    float[] diff = new float[cfg.iNoOfAnalogChannels];                             
                    
                    for (int arr = 0; arr < cfg.iNoOfSamples; arr++)
                    { 
                        for (int j=0; j<=cfg.iNoOfAnalogChannels-1;j++)
                        {   
                            diff[j]=(float)AnalogData.Rows[arr]["PhaseAngle_Channel" + (j+1)];
                        }
                        
                        if (!diff.ToString().Contains("NaN"))
                        {
                            if ((diff.Max() - diff.Min()) > 0.5)
                            {
                                testReport.addLine("Difference between phase angle is" + (diff.Max() - diff.Min()), "Fail", "");
                                break;                                
                            }
                        }
                    }
                    
             }
                

            Finish: ;

            }
            catch(Exception ex)
            {
                string err =ex.Message;
                testReport.addLine("Exception occurred in fn_AnalogData", "Fail", ex.Message);

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
                testReport.addLine("Method fn_ValidatePre_Postfault called", "", "");
                Chromedriver.SwitchTo().DefaultContent();       //need to put more check condition
                Chromedriver.FindElement(By.XPath(ObjectRepository.tab_Configuration)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Config_LDLicense)));
                Chromedriver.FindElement(By.XPath(ObjectRepository.Dfr)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Control_Data)));
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Sub_Control_Data)));

                Chromedriver.FindElement(By.XPath(ObjectRepository.ExterTrigger)).Click();

                int Recordlength = Convert.ToInt32(Chromedriver.FindElement(By.XPath(ObjectRepository.Prefault)).GetAttribute("value")) + Convert.ToInt32(Chromedriver.FindElement(By.XPath(ObjectRepository.Exter_Trigger_PostFault)).GetAttribute("value"));

                Samples.SamplingRate = (int)(Samples.fSamplingFrequency / Samples.frequency);

                if (Samples.frequency==50)
                {
                    if (((int)(Samples.iNoOfSamples / Samples.SamplingRate) * 20) == Recordlength)
                    {
                        testReport.addLine("DFR record length is correct", "Pass", "");
                        return true;
                    }
                    else
                    {
                        testReport.addLine("DFR record length is wrong", "Fail", "");
                        return false;
                    }
                }

                else if (Samples.frequency == 60)
                {
                    if (((int)(Samples.iNoOfSamples / Samples.SamplingRate) * 16.67) == Recordlength)
                    {
                        testReport.addLine("DFR record length is correct", "Pass", "");
                        return true;
                    }
                    else
                    {
                        testReport.addLine("DFR record length is wrong", "Fail", "");
                        return false;
                    }
                }
                else
                {
                    testReport.addLine("Sampling frequency is not 50 or 60 Hz", "Fail", "");
                    return false;
                }
            }
            catch (Exception ex)
            {
                testReport.addLine("Exception occured in fn_ValidatePre_PostFault", "Fail", ex.Message);
                return false;
            }
        }

        /// <summary>
        /// This method genertes the Manual Trigger and then download the record data and cfg file.
        /// After downloading the file in the folder it calls the fn_ValidatePre_Postfault & fn_AnalogData.
        /// </summary>
        public void fn_ValidateDFR()
        {           
            try
            {
                testReport.addLine("Method fn_ValidateDFR called", "", "");
                IJavaScriptExecutor js = (IJavaScriptExecutor)Chromedriver;

                LaunchBrowser();

                if (Chromedriver.FindElement(By.XPath(ObjectRepository.tab_Data)).Displayed)
                {
                    testReport.addLine("Launching the Google Chrome Browser", "Pass", "");
                }
                else
                {

                    PingReply Device_Reply = PingDevice.Send("192.168.1.19");
                    testReport.addLine("Unable to connect to device", "Fail", Device_Reply.ToString());
                    goto Finish;

                }                

                OpenURL("http://"+userName+":"+password__mfg_tabindex+"@"+deviceIP+"/tabindex");

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

                Chromedriver.FindElement(By.XPath(ObjectRepository.RecordNumber)).SendKeys(Convert.ToString(Record_num + 1));
                Chromedriver.FindElement(By.XPath(ObjectRepository.Rec_DFR_Config)).Click();


                Thread.Sleep(5000);
                SendKeys.SendWait(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + (Record_num + 1) + ".CFG");
                Thread.Sleep(2000);
                SendKeys.SendWait(@"{Enter}");

                while (!File.Exists(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + (Record_num + 1) + ".CFG"))
                {
                    Thread.Sleep(5000);
                }

                Chromedriver.FindElement(By.XPath(ObjectRepository.Rec_DFR_Data)).Click();

                Thread.Sleep(5000);
                SendKeys.SendWait(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + (Record_num + 1) + ".DAT");
                Thread.Sleep(2000);
                SendKeys.SendWait(@"{Enter}");

                while (!File.Exists(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + (Record_num + 1) + ".DAT"))
                {
                    Thread.Sleep(5000);
                }

                List<String[]> fileContent = new List<string[]>();
                CFGData cfg = new CFGData();

                int iLineNo = 0;
                int ChannelNo=0;
                string[] line;
                string[] dateSplit;
                string[] timeSplit;

                FileStream reader = File.OpenRead(Directory.GetCurrentDirectory() + @"\TestDownload\DFRRecord" + (Record_num + 1) + ".CFG");
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
                                ChannelNo++;                                
                                ChannelType.Add(ChannelNo,line[4].Trim());
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
                            cfg.fSamplingFrequency = float.Parse(line[0].Trim());
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
                    testReport.addLine("Record Length is correct","Pass","");
                }
                else
                {
                    testReport.addLine("Record Length is wrong","Fail","");
                }
                
                fn_AnalogData(cfg,(Record_num+1));


            Finish:
                Thread.Sleep(2000);
               
            }

            catch (Exception ex)

            {
                testReport.addLine("Exception ocurred in fn_ValidateDFR", "Fail", ex.Message);                
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
                OpenURL("http://qualitrol:qualcorp_Upgrade10@10.75.19.185/upgrade");
                               
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_lang)).Click();
                 bool canclose = false;
                 //CPU binary upgrade
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_CPU)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                 Thread.Sleep(5000);
                 SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.11\01-CPU-04.11_06-crc.bin");
                 Thread.Sleep(2000);
                 Thread.Sleep(5000); SendKeys.SendWait(@"{Enter}");
                 Thread.Sleep(5000);
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

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
                         canclose = true;
                         break;
                     }
                 }
                 if (canclose)
                 {
                     if (File.Exists(@"C:\TestDownload\CPU_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\CPU_upgrade.cgi");
                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\CPU_upgrade.cgi");                     
                 }
                 else
                 {
                 if (File.Exists(@"C:\TestDownload\CPU_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\CPU_upgrade.cgi");

                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\CPU_upgrade.cgi");
                     goto Finish;
                 }
                 
                 //DSP binary upgrade
                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_DSP)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                 Thread.Sleep(5000);
                 SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.11\01-DSP-04.11_04-crc.bin");
                 Thread.Sleep(2000);
                 Thread.Sleep(5000); SendKeys.SendWait(@"{Enter}");
                 Thread.Sleep(5000);
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

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
                         canclose = true;
                         break;
                     }
                 }
                 if (canclose)
                 {
                     if (File.Exists(@"C:\TestDownload\DSP_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\DSP_upgrade.cgi");
                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\DSP_upgrade.cgi");
                 }
                 else
                 {
                     if (File.Exists(@"C:\TestDownload\DSP_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\DSP_upgrade.cgi");

                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\DSP_upgrade.cgi");
                     goto Finish;
                 }


                 //FPGA binary upgrade
                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_FPGA)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                 Thread.Sleep(5000);
                 SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.11\01-FPGA-04.11_04-crc.bin");
                 Thread.Sleep(2000);
                 Thread.Sleep(5000); SendKeys.SendWait(@"{Enter}");
                 Thread.Sleep(5000);
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

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
                         canclose = true;
                         break;
                     }
                 }
                 if (canclose)
                 {
                     if (File.Exists(@"C:\TestDownload\FPGA_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\FPGA_upgrade.cgi");
                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\FPGA_upgrade.cgi");
                 }
                 else
                 {
                     if (File.Exists(@"C:\TestDownload\FPGA_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\FPGA_upgrade.cgi");

                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\FPGA_upgrade.cgi");
                     goto Finish;
                 }

                 //PIC binary upgrade
                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_PIC)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                 Thread.Sleep(5000);
                 SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.11\01-PIC-04.11_04-crc.bin");
                 Thread.Sleep(2000);
                 Thread.Sleep(5000); SendKeys.SendWait(@"{Enter}");
                 Thread.Sleep(5000);
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

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
                         canclose = true;
                         break;
                     }
                 }
                 if (canclose)
                 {
                     if (File.Exists(@"C:\TestDownload\PIC_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\PIC_upgrade.cgi");
                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\PIC_upgrade.cgi");
                 }
                 else
                 {
                     if (File.Exists(@"C:\TestDownload\PIC_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\PIC_upgrade.cgi");

                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\PIC_upgrade.cgi");
                     goto Finish;
                 }

                 //MMI binary upgrade
                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_MMI)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Select_File)).Click();
                 Thread.Sleep(5000);
                 SendKeys.SendWait(@"C:\My Documents\FW_Binaries\4.11\01-MMI-04.11_04-crc.bin");
                 Thread.Sleep(2000);
                 Thread.Sleep(5000); SendKeys.SendWait(@"{Enter}");
                 Thread.Sleep(5000);
                 Chromedriver.FindElement(By.XPath(ObjectRepository.Send)).Click();

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
                         canclose = true;
                         break;
                     }
                 }
                 if (canclose)
                 {
                     if (File.Exists(@"C:\TestDownload\MMI_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\MMI_upgrade.cgi");
                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\MMI_upgrade.cgi");
                 }
                 else
                 {
                     if (File.Exists(@"C:\TestDownload\MMI_upgrade.cgi"))
                     {
                         File.Delete(@"C:\TestDownload\MMI_upgrade.cgi");

                     }
                     File.Move(@"C:\TestDownload\upgrade.cgi", @"C:\TestDownload\MMI_upgrade.cgi");
                     goto Finish;
                 }

                 Chromedriver.SwitchTo().ParentFrame();
                 Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_toggle)));
                 Chromedriver.FindElement(By.XPath(ObjectRepository.CPU_toggle)).Click();
                 Chromedriver.FindElement(By.XPath(ObjectRepository.DSP_toggle)).Click();
                 Chromedriver.FindElement(By.XPath(ObjectRepository.FPGA_toggle)).Click();
                 Chromedriver.FindElement(By.XPath(ObjectRepository.PIC_toggle)).Click();
                 Chromedriver.FindElement(By.XPath(ObjectRepository.MMI_toggle)).Click();
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
                    DeviceReply = PingDevice.Send("10.75.19.185");
                }
                while (!DeviceReply.Status.ToString().Equals("Success"));
                Thread.Sleep(10000);

                OpenURL("http://qualitrol:qualcorp_techSupport10@10.75.19.185/mfgindex");
                
                Chromedriver.FindElement(By.XPath(ObjectRepository.Diagnostic_Info)).Click();
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe")));
                Chromedriver.SwitchTo().Frame(Chromedriver.FindElement(By.XPath(ObjectRepository.Frame_Diagnostic_info)));

                string CPU_Appln_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.CPU_Ver)).Text;
                string DSP_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.DSP_Ver)).Text;
                string FPGA_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.FPGA_Ver)).Text;
                string PIC_Ver = Chromedriver.FindElement(By.XPath(ObjectRepository.PIC_Ver)).Text;

                if (CPU_Appln_Ver.Equals("04.11_06"))
                {
                    //pass
                }
                else
                { 
                    //fails
                }

                if (DSP_Ver.Equals("04.11_04"))
                {
                    //pass
                }
                else
                {
                   // fails
                }

                if (FPGA_Ver.Equals("04.11_04"))
                {
                //pass
                }
                else
                {
                //fails
                }

                if (PIC_Ver.Equals("04.11_04"))
                {
                //pass
                }
                else
                {
                //fails
                }

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

        /// <summary>
        /// This method will kill the Selenium driver
        /// </summary>

        public void ExitSelenium()
        {
            Chromedriver.Quit();
        }

        /// <summary>
        /// This method is used to upgrade firmware in the device.
        /// </summary>
        public void UpgradeCPUApplication()
        {
            try
            {
                testReport.addLine("Method UpgradeCPUApplication called", "", "");
                UploadMultipart("application/octet-stream", "http://"+deviceIP+"/cgi-bin/upgrade.cgi?cmd=TOGGLE_ALL_FW&TOGGLE_CPU_APP&TOGGLE_DSP_FW&TOGGLE_FPGA_FW&TOGGLE_PIC_FW&TOGGLE_MMI_FW&");

                UploadMultipart(File.ReadAllBytes(firmwareBinpathCPU),
                CPUFileName, "application/octet-stream",
                "http://" + deviceIP + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_CPU_APP");

                UploadMultipart(File.ReadAllBytes(@"C:\My Documents\FW_Binaries\4.11\01-DSP-04.11_04-crc.bin"),
                DSPFileName, "application/octet-stream",
                "http://" + deviceIP + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_DSP_FW");

                UploadMultipart(File.ReadAllBytes(@"C:\My Documents\FW_Binaries\4.11\01-FPGA-04.11_04-crc.bin"),
                FPGAFileName, "application/octet-stream",
                "http://" + deviceIP + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_FPGA_FW");

                UploadMultipart(File.ReadAllBytes(@"C:\My Documents\FW_Binaries\4.11\01-PIC-04.11_04-crc.bin"),
                PICFilename, "application/octet-stream",
                "http://" + deviceIP + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_PIC_FW");
                
                
                UploadMultipart(File.ReadAllBytes(@"C:\My Documents\FW_Binaries\4.12\MMI\01-MMI-04.11_05-crc.bin"),
                MMIFileName, "application/octet-stream",
                "http://" + deviceIP + "/cgi-bin/upgrade.cgi?cmd=UPGRADE_MMI_FW");
                testReport.addLine("Method UpgradeCPUApplication Ends", "", "");
            }
            catch (Exception ex)
            {
                testReport.addLine("Exception in Method UpgradeCPUApplication", "Fail", ex.Message);
            }
        }
        

        /// <summary>
        /// This method is used to upload data to device using Webclient method
        /// </summary>
        /// <param name="file">Contains the firmware upgrade file path</param>
        /// <param name="filename">Name of the binary file</param>
        /// <param name="contentType"></param>
        /// <param name="url">Address of the device whose firmware to be upgraded</param>
        public void UploadMultipart(byte[] file, string filename, string contentType, string url)
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

            if (result.Contains("SUCCESS"))
            {
                testReport.addLine("Firmware Upgrade Successful", "Pass", "");
            }
            else
            {
                testReport.addLine("Firmware Upgrade Successful", "Pass", "");
            }
        }

        /// <summary>
        /// This method is used to toggle the image of the firmware
        /// </summary>
        /// <param name="contentType"></param>
        /// <param name="url">Address of the device whose firmware to be upgraded</param>

        public void UploadMultipart(string contentType, string url)
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
                    testReport.addLine("Toggle is Successful", "Pass", "");
                }
                else
                {
                    testReport.addLine("Toggle is not Successful", "Pass", "");
                }
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        public float PhaseAngleCalculate(CFGData DataSource,int iChannelIndex,int sampleIndex)
        {

                float fltResult = 0F;
                int intTotalsamples = DataSource.iNoOfSamples;

                //Cycle time
                double dblCycleTime = (double)(0.5f / DataSource.frequency);

                //Centre Time
                double dblCentreTime = TimeFromSample(DataSource, sampleIndex);
                double dblEndRecTime = TimeFromSample(DataSource, DataSource.iNoOfSamples);
                //Start Time
                double dblStartTime = (dblCentreTime - dblCycleTime);
                //End Time
                double dblEndTime = dblCentreTime + dblCycleTime;


                if (dblStartTime < 2 * dblCycleTime)
                {
                    return float.NaN;
                }
                if (dblEndTime > dblEndRecTime - 2 * dblCycleTime)
                {
                    return float.NaN;
                }

                float fltRatestart = 0, rateend = 0;
                double dblTimeErrSt = 0, timeerrend = 0;

                SampleFromTime(DataSource, dblStartTime, ref fltRatestart, ref dblTimeErrSt);
                SampleFromTime(DataSource, dblEndTime, ref rateend, ref timeerrend);

                int intIteration = (int)(fltRatestart / DataSource.frequency);

                float fltIterationValue = (float)(dblStartTime / (2 * dblCycleTime / intIteration));
                float fltIMod;
                float fltSin = 0;
                float fltCos = 0;

                float[] instChannelData;

                for (double dTime = dblStartTime; dTime + 0.0000000001 < dblEndTime;
                            dTime += ((dblEndTime - dblStartTime) / (intIteration)))
                {
                    float fltValue = float.NaN;
                    //Interpolation
                    long l = (long)(dTime * 10000000);
                    ValuePair p = null;
                    //  if (!cache.ValuePairs.TryGetValue(l, out p))
                    //  {
                    p = new ValuePair();
                    // cache.ValuePairs[l] = p;
                    if(InstantaneousValue_Channel.TryGetValue(iChannelIndex,out instChannelData))
                    { 
                        fltValue = (float)Interpolate(DataSource, instChannelData, dTime);
                    }
                    if (float.IsNaN(fltValue))
                    {
                        return float.NaN;
                    }

                    fltIMod = fltIterationValue;
                    if (fltIMod > intIteration / 2)
                    {
                        fltIMod -= intIteration;
                    }

                    //  if (!blnStaticNo)
                    //  {
                    //	   n					   	   n                        
                    //	   S =	E u(i)sin(2pi i/n)	    C =	E u(i)cos(2pi i/n)
                    //	 i=1					   	  i=1                       

                    //   where u(i) is the sample value, and n is the number of iterations.
                    p.Sin = fltValue * (float)(Math.Sin(2 * Math.PI * (FloatModulus(fltIMod, (float)(intIteration))) / intIteration));
                    p.Cos = fltValue * (float)(Math.Cos(2 * Math.PI * (FloatModulus(fltIMod, (float)(intIteration))) / intIteration));
                    //  }
                    //  else
                    //  {
                    //    p.Sin = fltValue * (float)(Math.Sin(2 * Math.PI * (FloatModulus(fltIMod, (float)(intIteration))) / intIteration));
                    //     p.Cos = fltValue * (float)(Math.Cos(2 * Math.PI * (FloatModulus(fltIMod, (float)(intIteration))) / intIteration));
                    //}


                    fltSin += p.Sin;
                    fltCos += p.Cos;

                    fltIterationValue = fltIterationValue + 1;
                }
                //Debug.WriteLine(string.Format("dTime {0} value {1} - {2}", dTime, fltSin2, fltCos2));
                fltResult = (180.0F * (float)(Math.Atan2((double)(fltSin), (double)(fltCos)) / Math.PI));
                return fltResult;
            
        }

        private float FloatModulus(float upper, float lower)
        {
            float result = upper / lower;
            return (result - (int)(result)) * lower;
        }

        public static double Interpolate(CFGData dataSource, float[] channelData, double Time)
        {
            double dblNewValue = float.NaN, dblDataPoint1, dblDataPoint2, dblDataPoint21;
            int intBefore, dblAfter;

            float fltRate = 0;
            double dblTimeError = 0;
            int intSample = SampleFromTime(dataSource, Time, ref fltRate, ref dblTimeError);

            if (intSample >= 0)
            {
                intBefore = intSample;
                dblAfter = intBefore + 1;
                if (dataSource.iNoOfAnalogChannels != null)
                {
                    int channelDataSize = channelData.Length;
                    if ((dblAfter + 1) >= channelDataSize ||
                        float.IsNaN(channelData[dblAfter]) ||
                        float.IsNaN(channelData[dblAfter + 1])
                        || float.IsNaN(channelData[intBefore]))
                    {
                        return float.NaN;
                    }

                    dblDataPoint1 = channelData[dblAfter] - channelData[intBefore];
                    dblDataPoint2 = channelData[dblAfter + 1] - channelData[dblAfter];

                    dblDataPoint21 = (dblDataPoint2 - dblDataPoint1) / 2.0;
                    dblNewValue = channelData[intBefore] +
                        ((double)(dblTimeError * fltRate)) *
                        (dblDataPoint1 + (1.0 - (double)(dblTimeError * fltRate)) * dblDataPoint21);
                }
            }
            return dblNewValue;
        }

        public static int SampleFromTime(CFGData dataSource, double Time, ref float Rate, ref double TimeError)
        {
            int intEndSample = 0, intSample = 0, intStartSample = 0, intPreviousSample = 0;

            // Startsample and end sample are the samples at the beginning and end of the rate
            double dblStartTime = 0.0, dblEndTime = 0.0;

            for (int intSamplingRateIndex = 0; intSamplingRateIndex < 1; intSamplingRateIndex++)
            {
                Rate = (float)dataSource.fSamplingFrequency;

                if (intSamplingRateIndex == dataSource.fSamplingFrequency - 1)
                {
                    intEndSample = dataSource.iNoOfSamples;
                    dblEndTime = (double)(dblStartTime + (intEndSample - intStartSample)) / Rate;
                }
                else
                {
                    intEndSample = intPreviousSample + (int)dataSource.iNoOfSamples;
                    dblEndTime = dblStartTime + (intEndSample - intStartSample) / Rate;
                }

                if (Time < dblEndTime || intSamplingRateIndex == dataSource.fSamplingFrequency - 1)
                {
                    intSample = (int)((intStartSample + (Time + 0.0000000001 - dblStartTime) * (double)Rate));
                    TimeError = Time - ((intSample - intStartSample) / Rate + dblStartTime);
                    break;
                }

                intPreviousSample += (int)dataSource.fSamplingFrequency;

                intStartSample = intEndSample;
                dblStartTime = dblEndTime;
            }

            return intSample;
        }

        public double TimeFromSample(CFGData DataSource, int Sample)
        {
            // Work out time from beginning of record for given sample
            // Startsample and end sample are the samples at the beginning and end of the rate            
            double dblRate = 0D, dblTime = 0D;
            int intEndSample = 0, intStartSample = 0, dblPreviousSample = 0;

            for (int intSamplingRateIndex = 0; intSamplingRateIndex < 1; intSamplingRateIndex++)
            {
                dblRate = DataSource.fSamplingFrequency;

                if (intSamplingRateIndex == DataSource.fSamplingFrequency - 1)
                {
                    intEndSample = DataSource.iNoOfSamples;
                }
                else
                {
                    intEndSample = dblPreviousSample + DataSource.iNoOfSamples;
                }

                if (Sample < intEndSample || intSamplingRateIndex == DataSource.fSamplingFrequency - 1)
                {
                    dblTime += (double)(Sample - intStartSample) / (double)dblRate;
                    break;
                }
                else
                {
                    dblTime += (double)(intEndSample - intStartSample) / (double)dblRate;
                }

                dblPreviousSample += DataSource.iNoOfSamples;
                intStartSample = intEndSample;
            }

            return (double)(dblTime + (0.0 / (100 * dblRate))); //0 -> Percent
        
        
        }
    }
}

