using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tabindex_Configuration.pqp
{
    class Tabindex_Configuration_pqp
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;
        string config_param = string.Empty;
        
        public Tabindex_Configuration_pqp(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
        }

        #region ObjectRepository
        
        private IWebElement Item_pqp
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[contains(text(),'pqp')]"));
            }
        }

        private IWebElement Frame_b1_config
        {
            get
            {
                var pqp_get_attribute = Item_pqp.GetAttribute("href");
                var pqp_locate_number = pqp_get_attribute.Substring(pqp_get_attribute.Length - 4, 2);
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[" + (Convert.ToUInt32(pqp_locate_number) + 1) + "]/iframe"));
            }
        }

        private IWebElement SubFrame_b1_config
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));
            }
        }

        private IWebElement Item_b1_config
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
            }
        }

        private IWebElement Item_record_config
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/a"));
            }
        }

        private IWebElement Item_param_id_list
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[1]/tbody/tr/td[2]/div/table[1]/tbody/tr/td[2]/a"));
            }
        }

        private IWebElement Item_CommitChangesHidden
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[2]"));
            }
        }

        private IWebElement Item_CommitChanges
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/div/table/tbody/tr/td[1]/form/input[1]"));
            }
        }

        private string Edtbx_pqp_calcno
        {
            get
            {
               return resourceManager.GetString("XPATH_pqp_calcno_config").ToString();
            }
        }

        #endregion

        #region Methods

        public bool Item_pqp_Click()
        {
            Item_pqp.Click();
            return true;
        }

        public bool SwitchFrame_Frompqp_Tob1_config()
        {
            webDriver.SwitchTo().Frame(Frame_b1_config);
            webDriver.SwitchTo().Frame(SubFrame_b1_config);
            return true;
        }

        public bool b1_config_Click()
        {
            Item_b1_config.Click();
            return true;
        }

        public bool record_config_Click()
        {
            Item_record_config.Click();
            return true;
        }

        public bool param_id_list_Click()
        {
            Item_param_id_list.Click();
            return true;
        }

        

        public string Configure_b1ConfigParam(int paramindex,string calcnumfromexcel,string pqp_param)
        {
            if (webDriver.FindElement(By.XPath(String.Format(Edtbx_pqp_calcno, paramindex + 1).Replace("\"", ""))).GetAttribute("value") != calcnumfromexcel)
            {
                if (paramindex == 0 || pqp_param=="")
                {
                    config_param = "pqp:pqp/config/b1_config/record_config/paramid_list/calc_no[" + paramindex + "]=" + Convert.ToInt64(calcnumfromexcel);
                }
                else
                {
                    config_param = "&" + "pqp:pqp/config/b1_config/record_config/paramid_list/calc_no[" + paramindex + "]=" + Convert.ToInt64(calcnumfromexcel);
                }
                return config_param;
            }
            else
            {
                return null;
            }            
            
        }

        public bool Switch_ToParentFrame()
        {
            webDriver.SwitchTo().ParentFrame();
            return true;
        }

        public bool Insertpqp_param_javascript(string pqp_Calcnum)
        {
            IJavaScriptExecutor js = (IJavaScriptExecutor)webDriver;
            js.ExecuteScript("arguments[0].value='" + pqp_Calcnum + "';", Item_CommitChangesHidden);
            return true;
        }

        public bool Commit_Click()
        {
            Item_CommitChanges.Click();
            return true;
        }

        #endregion
    }
}
