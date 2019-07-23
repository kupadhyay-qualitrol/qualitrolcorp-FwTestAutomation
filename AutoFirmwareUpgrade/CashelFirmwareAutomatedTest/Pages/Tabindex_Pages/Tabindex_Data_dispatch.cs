using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tabindex_Data.dispatch
{
    class Tabindex_Data_dispatch
    {
        IWebDriver webDriver;
        WebDriverWait explicitWait;
        System.Resources.ResourceManager resourceManager;

        public Tabindex_Data_dispatch(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
            explicitWait = new WebDriverWait(webDriver, TimeSpan.FromSeconds(100));
        }

        #region Object Repository

        private IWebElement Btn_Data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]"));
            }
        }
        private IWebElement Frame_dispatch
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe"));
            }
        }

        private IWebElement Item_dispatch
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'dis')]"));
            }
        }

        private IWebElement Frame_dispatch_data
        {
            get
            {
                var dispatch_get_attribute = Item_dispatch.GetAttribute("href");
                var dispatch_locate_number = dispatch_get_attribute.Substring(dispatch_get_attribute.Length - 4, 2);
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[" + (Convert.ToUInt32(dispatch_locate_number) + 1) + "]/iframe"));
            }
        }

        private IWebElement SubFrame_dispatch_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));
            }
        }

        private IWebElement Item_dispatch_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table/tbody/tr/td/a"));
            }
        }

        private IWebElement Item_dispatch_data_fr_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'fr_data')]"));
            }
        }

        private IWebElement Item_dispatch_data_fr_data_fundamental
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'fundamental')]"));
            }
        }

        private IWebElement Item_dispatch_data_fr_data_fundamental_rms
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'rms')]"));
            }
        }

        private string Item_dispatch_data_fr_data_fundamental_rms_magnitude
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_Dispatch_Data_Fr_Data_Fundamental_RMS_Magnitude").ToString();
            }
        }

        private IWebElement Btn_refresh
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[2]/form/input"));
            }
        }

        #endregion

        #region Methods

        public string OpenTabIndexPage(string deviceIP)
        {
            webDriver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
            webDriver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");
            return Btn_Data.Text;
        }

        public bool Btn_Data_Click()
        {
            Btn_Data.Click();
            return true;
        }

        public bool SwitchFrame_Fromdefault_Todispatch()
        {
            webDriver.SwitchTo().Frame(Frame_dispatch);
            return true;
        }

        public bool Item_Data_dispatch_Click()
        {
            Item_dispatch.Click();
            return true;
        }

        public bool SwitchFrame_Fromdispatch_Todata()
        {
            webDriver.SwitchTo().Frame(Frame_dispatch_data);
            webDriver.SwitchTo().Frame(SubFrame_dispatch_data);
            return true;
        }

        public bool SwitchSubFrame_Todata()
        {
            webDriver.SwitchTo().Frame(SubFrame_dispatch_data);
            return true;
        }


        public bool Item_dispatch_data_Click()
        {
            explicitWait .Until(ExpectedConditions.ElementToBeClickable(Item_dispatch_data));
            Item_dispatch_data.Click();              
            return true;
        }

        public bool Item_fr_data_Click()
        {
            Item_dispatch_data_fr_data.Click();
            return true;
        }

        public bool Item_fundamental_Click()
        {
            Item_dispatch_data_fr_data_fundamental.Click();
            return true;
        }

        public bool Item_rms_Click()
        {
            Item_dispatch_data_fr_data_fundamental_rms.Click();
            return true;
        }

        public string Get_Magnitude(int index)
        {
            return webDriver.FindElement(By.Name(String.Format(Item_dispatch_data_fr_data_fundamental_rms_magnitude, index).Replace("\"", ""))).GetAttribute("value").ToString();
        }  

        public bool Btn_refresh_click()
        {
            Btn_refresh.Click();
            return true;
        }

        public bool SwitchToParentFrame()
        {
            webDriver.SwitchTo().ParentFrame();
            return true;
        }



        #endregion
    }
}
