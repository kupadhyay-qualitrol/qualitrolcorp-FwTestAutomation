﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CashelFirmware.Utility;
using System.Threading;
using System;

namespace Tabindex_Configuration.dfr
{
    class Tabindex_Configuration_dfr
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;
        Read_WriteExcel rdexcel;
        public Tabindex_Configuration_dfr(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("ClassLibrary1.Resource", this.GetType().Assembly);
           // resourceManager = new System.Resources.ResourceManager("ClassLibrary1.Resource",Assembly.GetExecutingAssembly());
            rdexcel = new Read_WriteExcel();            
        }

        #region ObjectRepository
        private IWebElement Btn_Configuration
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[contains(text(),'Configuration')]"));
            }
        }

        private IWebElement Frame_dfr
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[1]/iframe"));
            }
        }


        private IWebElement Item_dfr
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'dfr')]"));
            }
        }        

        private IWebElement Frame_Analogfr
        {
            get
            {
                var dfr_get_attribute = webDriver.FindElement(By.XPath("//a[contains(text(),'dfr')]")).GetAttribute("href");
                var dfr_locate_number = dfr_get_attribute.Substring(dfr_get_attribute.Length - 3, 1);
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div["+ (Convert.ToUInt32(dfr_locate_number) + 1)+ "]/iframe"));
            }
        }

        private IWebElement Sub_Frame_Analogfr
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));
            }
        }

        private IWebElement Item_dfr_analog
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
            }
        }

        private string Item_dfr_analog_channel
        {
            get
            {
                return resourceManager.GetString("XPATH_CHANNELNUMBER").ToString();
            }
        }

        private string Item_dfr_analog_channel_busbar
        {
            get
            {
                return resourceManager.GetString("XPATH_BUSBAR").ToString();
            }
        }

        private string Item_dfr_analog_channel_feeder
        {
            get
            {
                return resourceManager.GetString("XPATH_FEEDER").ToString();
            }
        }

        private string Item_dfr_analog_channel_phase
        {
            get
            {
                return resourceManager.GetString("XPATH_PHASE").ToString();
            }
        }

        private string Item_dfr_analog_channel_usage
        {
            get
            {
                return resourceManager.GetString("XPATH_USAGE").ToString();
            }
        }

        private IWebElement CommitChanges
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]"));
            }
            
        }
        #endregion

        #region Methods
        /// <summary>
        /// This method is used to open the tabindex page of the device.
        /// </summary>
        /// <param name="deviceIP">Device IP which needs to be accessed</param>
        /// <returns>Returns name of the Configuration button if webpage is launched successfully.</returns>
        public string OpenTabIndexPage(string deviceIP)
        {
            webDriver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");            
            webDriver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");
            return Btn_Configuration.Text;
        }

        public bool Item_Configuration_Click()
        {
            Btn_Configuration.Click();
            return true;
        }

        public bool SwitchFrame_FromParent_Todfr_item()
        {
                webDriver.SwitchTo().Frame(Frame_dfr);
                return true;
        }

        public bool Item_Dfr_Click()
        {
                Item_dfr.Click();
                return true;
        }
        public bool SwitchFrame_FromDfr_Toanalog()
        {
            webDriver.SwitchTo().Frame(Frame_Analogfr);
            webDriver.SwitchTo().Frame(Sub_Frame_Analogfr);
            return true;
        }
        public bool Item_dfr_analog_Click()
        {
                Item_dfr_analog.Click();
                return true;
        }

        public bool Item_dfr_analog_channel_Click(int i)
        {
            webDriver.FindElement(By.XPath(String.Format(Item_dfr_analog_channel, i).Replace("\"",""))).Click();
            return true;
        }


        public bool Item_dfr_analog_channel_busbar_Select(int i,string DataSheetFilenamewithExtension)
        {
            var BusbarSelect= new SelectElement(webDriver.FindElement(By.XPath(string.Format(Item_dfr_analog_channel_busbar, i+1).Replace("\"", ""))));
            BusbarSelect.SelectByText(rdexcel.ReadExcel(DataSheetFilenamewithExtension, "Cabling",i,"busbar"));
            return true;
        }


        public bool Item_dfr_analog_channel_feeder_Click(int i,string DataSheetFilenamewithExtension)
        {
            var FeederSelect = new SelectElement(webDriver.FindElement(By.XPath(string.Format(Item_dfr_analog_channel_feeder, i+1).Replace("\"", ""))));
            FeederSelect.SelectByText(rdexcel.ReadExcel(DataSheetFilenamewithExtension, "Cabling", i, "feeder_number"));
            return true;
        }

        public bool Item_dfr_analog_channel_phase_Click(int i,string DataSheetFilenamewithExtension)
        {
            var PahseSelect = new SelectElement(webDriver.FindElement(By.XPath(string.Format(Item_dfr_analog_channel_phase, i+1).Replace("\"", ""))));
            PahseSelect.SelectByText(rdexcel.ReadExcel(DataSheetFilenamewithExtension, "Cabling", i, "phase"));
            return true;
        }

        public bool Item_dfr_analog_channel_usage_Click(int i, string DataSheetFilenamewithExtension)
        {
            var UsageSelect = new SelectElement(webDriver.FindElement(By.XPath(string.Format(Item_dfr_analog_channel_usage, i+1).Replace("\"", ""))));
            UsageSelect.SelectByText(rdexcel.ReadExcel(DataSheetFilenamewithExtension, "Cabling", i, "usage"));
            return true;
        }

        public bool SwitchToParentFrame()
        {
            webDriver.SwitchTo().ParentFrame();
            return true;
        }

        public bool SwitchToDefaultContent()
        {
            webDriver.SwitchTo().DefaultContent();
            return true;
        }

        public bool Commit_Click()
        {
                CommitChanges.Click();
                return true;
        }


        #endregion
    }
}
