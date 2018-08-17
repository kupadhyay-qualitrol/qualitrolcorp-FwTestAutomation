/*This file contains object repository for Webelements and method to perform action on them.
 * This is basically based on concept of Page Object Model(POM).
 * This file will contains objects related to Tabindex_Data_pqp page.
 */

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using CashelFirmware.Utility;
using System;

namespace Tabindex_Data.pqp
{
    class Tabindex_Data_pqp
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;

        public Tabindex_Data_pqp(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
        }

        #region ObjectRepository

        private IWebElement Btn_Data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]"));
            }
        }

        private IWebElement Frame_pqp
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe"));
            }
        }

        private IWebElement Item_pqp
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'pqp')]"));
            }
        }

        private IWebElement Frame_pqp_data
        {
            get
            {
                var pqp_get_attribute = Item_pqp.GetAttribute("href");
                var pqp_locate_number = pqp_get_attribute.Substring(pqp_get_attribute.Length - 4, 2);
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[" + (Convert.ToUInt32(pqp_locate_number) + 1) + "]/iframe"));
            }
        }
        
        private IWebElement SubFrame_pqp_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));
            }
        }

        private IWebElement Item_pqp_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
            }
        }

        private IWebElement Itemp_pqp_data_calculation_types
        {
            get
              {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'calculation_types')]"));
              }
        }

        private string Item_pqp_data_calculation_calctype
        {
            get
            {
                return resourceManager.GetString("LOCATOR_XPATH_pqp_data_calculation_type_calcno").ToString();                
            }
        }

        private string Item_pqp_data_calculation_calctype_paramtype
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_pqp_data_calculation_type_calcno_paramtype").ToString();
            }
        }

        private string Item_pqp_data_calculation_calctype_phase
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_pqp_data_calculation_type_calcno_phase").ToString();
            }
        }

        private string Item_pqp_data_calculation_calctype_harmonicrank
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_pqp_data_calculation_type_calcno_harmonicrank").ToString();
            }
        }

        private string Item_pqp_data_calculation_calctype_busbarfeedertype
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_pqp_data_calculation_type_calcno_busbarfeedertype").ToString();
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

        public bool SwitchFrame_Fromdefault_Topqp()
        {
            webDriver.SwitchTo().Frame(Frame_pqp);
            return true;
        }

        public bool SwitchFrame_Frompqp_Todata()
        {
            webDriver.SwitchTo().Frame(Frame_pqp_data);
            webDriver.SwitchTo().Frame(SubFrame_pqp_data);
            return true;
        }

        public bool Item_Data_pqp_Click()
        {
            Item_pqp.Click();
            return true;
        }

        public bool Item_Data_pqp_data_Click()
        {
            Item_pqp_data.Click();
            return true;
        }

        public bool Item_Calculation_Type_Click()
        {
            Itemp_pqp_data_calculation_types.Click();
            return true;
        }

        public bool Item_Calc_type_num_Click(int calcnumindex)
        {
            webDriver.FindElement(By.XPath(String.Format(Item_pqp_data_calculation_calctype, calcnumindex).Replace("\"", ""))).Click();
            return true;
        }

        public string Get_paramtype(int calcnumindex)
        {
            return webDriver.FindElement(By.Name(String.Format(Item_pqp_data_calculation_calctype_paramtype, calcnumindex).Replace("\"", ""))).GetAttribute("value").ToString();
        }

        public string Get_phase(int calcnumindex)
        {
            return webDriver.FindElement(By.Name(String.Format(Item_pqp_data_calculation_calctype_phase, calcnumindex).Replace("\"", ""))).GetAttribute("value").ToString();     
        }

        public string Get_harmonicrank(int calcnumindex)
        {
            return webDriver.FindElement(By.Name(String.Format(Item_pqp_data_calculation_calctype_harmonicrank, calcnumindex).Replace("\"", ""))).GetAttribute("value").ToString();
        }

        public string Get_busbarfeedertype(int calcnumindex)
        {
            return webDriver.FindElement(By.Name(String.Format(Item_pqp_data_calculation_calctype_busbarfeedertype, calcnumindex).Replace("\"", ""))).GetAttribute("value").ToString();
        }

        #endregion
    }
}
