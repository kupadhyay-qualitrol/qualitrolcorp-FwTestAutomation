using OpenQA.Selenium;
using CashelFirmware.Utility;

namespace Tabindex_Data.soh
{
    class Tabindex_Data_soh
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;
        //Read_WriteExcel rdexcel;

        public Tabindex_Data_soh(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
           // rdexcel = new Read_WriteExcel();
        }

        #region ObjectRepository

        private IWebElement Btn_Data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]"));
            }
        }

        private IWebElement Frame_soh
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe"));
            }
        }

        private IWebElement Item_soh
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[3]"));
            }
        }

        private IWebElement Frame_soh_control
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[3]/iframe"));

            }
        }

        private IWebElement Sub_Frame_soh_control
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));

            }
        }

        private IWebElement Item_soh_control
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/a"));
            }
        }

        private IWebElement Edtbx_soh_control_reset_cashel
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[1]/tbody/tr/td/div/table[4]/tbody/tr/td[2]/span/a/input"));
            }
        }

        private IWebElement Item_soh_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
            }
        }

        private IWebElement Item_CommitChangesHidden
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[2]"));
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

        public string OpenTabIndexPage(string deviceIP)
        {
            webDriver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
            webDriver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");
            return Btn_Data.Text;
        }

        public bool Item_Data_Click()
        {
            Btn_Data.Click();
            return true;
        }

        public bool SwitchFrame_FromParent_Tosoh_item()
        {
            webDriver.SwitchTo().Frame(Frame_soh);
            return true;
        }

        public bool Item_soh_Click()
        {
            Item_soh.Click();
            return true;
        }

        public bool SwitchFrame_Fromsoh_Tocontrol()
        {
            webDriver.SwitchTo().Frame(Frame_soh_control);
            webDriver.SwitchTo().Frame(Sub_Frame_soh_control);
            return true;
        }

        public bool Item_soh_control_Click()
        {
            Item_soh_control.Click();
            return true;
        }

        public bool Edtbx_soh_control_reset_cashel_Clear()
        {
            Edtbx_soh_control_reset_cashel.Clear();
            return true;
        }

        public bool Edtbx_soh_control_reset_cashel_SendKeys(string Sendkeys)
        {
            Edtbx_soh_control_reset_cashel.SendKeys(Sendkeys);
            return true;
        }

        public bool Item_soh_data_Click()
        {
            Item_soh_data.Click();
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

        public bool Commit_RebootDevice()
        {
            IJavaScriptExecutor executejavascript = (IJavaScriptExecutor)webDriver;
            executejavascript.ExecuteScript("arguments[0].value='soh:soh/control/reset_cashel=1';", Item_CommitChangesHidden);
            CommitChanges.Click();
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
