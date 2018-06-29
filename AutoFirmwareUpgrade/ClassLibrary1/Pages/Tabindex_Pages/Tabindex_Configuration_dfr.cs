using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Threading;

namespace Tabindex.Pages
{
    class Tabindex_Configuration_dfr
    {
        IWebDriver webDriver;

        public Tabindex_Configuration_dfr(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
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
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[6]"));
            }
        }        

        private IWebElement Frame_Analog_fr_sensor
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[6]/iframe"));
            }
        }

        private IWebElement Sub_Frame_Analog_fr_sensor
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


        #endregion

        #region Methods
        public string OpenTabIndexPage(string deviceIP)
        {
            webDriver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/tabindex");
            
            webDriver.Navigate().GoToUrl(@"http://" + deviceIP + "/tabindex");
            return Btn_Configuration.Text;

        }

        public void Item_Configuration_Click()
        {

        }
        #endregion
    }
}
