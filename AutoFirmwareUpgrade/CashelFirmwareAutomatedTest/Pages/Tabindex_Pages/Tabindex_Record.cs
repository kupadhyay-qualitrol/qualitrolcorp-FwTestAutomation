/*This file contains object repository for Webelements and method to perform action on them.
 * This is basically based on concept of Page Object Model(POM).
 * This file will contains objects related to Tabindex_Record page.
 */

using OpenQA.Selenium;

namespace Tabindex.Record
{
    class Tabindex_Record
    {
        IWebDriver webDriver;
        public Tabindex_Record(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
        }

        #region ObjectRepository

        private IWebElement Item_Record
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[contains(text(),'Records')]"));
            }
        }

        private IWebElement Frame_ToRecordPage
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[4]/iframe"));
            }
        }

        private IWebElement SubFrame_ToRecordPage
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));
            }
        }

        private IWebElement Item_PQ10min
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/table[1]/tbody/tr/td/a"));
            }
        }

        private IWebElement Item_PQ10min_Record
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/table[1]/tbody/tr/td/table[1]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a[3]"));
            }
        }

        private IWebElement Item_PQ10min_StartTime
        {
            get
            {
                return webDriver.FindElements(By.XPath("//*[@id='StartTime']"))[1];
            }
        }

        private IWebElement Item_PQ10min_EndTime
        {
            get
            {
                return webDriver.FindElements(By.XPath("//*[@id='EndTime']"))[1];
            }
        }

        private IWebElement Item_PQ10min_Data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='PQForm']/tbody/tr/td/form/a/input[1]"));
            }
        }

        private IWebElement Item_PQ10min_Cancel
        {
            get
            {
                return webDriver.FindElements(By.XPath("//*[@id='Cancel']"))[2];
              
            }
        }
        #endregion

        #region Methods

        public bool Item_Record_Click()
        {
            Item_Record.Click();
            return true;
        }

        public bool SwitchFrame_ToRecordPage()
        {
            webDriver.SwitchTo().Frame(Frame_ToRecordPage);
            webDriver.SwitchTo().Frame(SubFrame_ToRecordPage);
            return true;
        }

        public bool Item_PQ10min_Click()
        {
            Item_PQ10min.Click();
            return true;
        }

        public bool Item_PQ10minRecord_Click()
        {
            Item_PQ10min_Record.Click();
            return true;
        }

        public bool Item_StartTime_Edit(string StartTime)
        {
            Item_PQ10min_StartTime.Clear();
            Item_PQ10min_StartTime.SendKeys(StartTime);
            return true;
        }

        public bool Item_EndTime_Edit(string EndTime)
        {
            Item_PQ10min_EndTime.Clear();
            Item_PQ10min_EndTime.SendKeys(EndTime);
            return true;
        }

        public bool Item_DonwloadPQ10minRecord_Click()
        {
            Item_PQ10min_Data.Click();
            return true;
        }

        public bool Item_DonwloadPQ10minReturn_Click()
        {
            Item_PQ10min_Cancel.Click();
            return true;
        }

        #endregion
    }
}
