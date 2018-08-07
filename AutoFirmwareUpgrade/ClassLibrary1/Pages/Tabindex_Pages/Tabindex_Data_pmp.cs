using OpenQA.Selenium;
using CashelFirmware.Utility;
using OpenQA.Selenium.Support.UI;

namespace Tabindex_Data.pmp
{
    class Tabindex_Data_pmp
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;
      //  Read_WriteExcel rdexcel;

        public Tabindex_Data_pmp(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("ClassLibrary1.Resource", this.GetType().Assembly);
            //rdexcel = new Read_WriteExcel();
        }

        #region Object Repository

        private IWebElement Btn_Data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[2]"));
            }
        }

        private IWebElement Frame_pmp
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[2]/iframe"));
            }
        }

        private IWebElement Item_pmp
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[1]/a[4]"));
            }
        }

        private IWebElement Frame_pmp_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("//*[@id='TabView1']/div[2]/div[4]/iframe"));

            }
        }

        private IWebElement Sub_Frame_pmp_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("/html/body/iframe"));

            }
        }

        private IWebElement Item_pmp_data
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/a"));
            }
        }

        private IWebElement Item_pmp_data_PQCabling
        {
            get
            {
                return webDriver.FindElement(By.Name("pmp:pmp/data/pq_cabling_config"));

            }
        }

        private IWebElement Item_pmp_data_FRCabling
        {
            get
            {
                return webDriver.FindElement(By.Name("pmp:pmp/data/fr_cabling_config"));
            }
        }

        private IWebElement Item_pmp_data_dsp1_channels_map
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[5]/tbody/tr/td[2]/a"));
            }
        }

        private IWebElement Item_pmp_data_dsp2_channels_map
        {
            get
            {
                return webDriver.FindElement(By.XPath("html/body/form/table/tbody/tr/td/table[2]/tbody/tr/td/div/table[6]/tbody/tr/td[2]/a"));
            }
        }

        private string Item_pmp_data_dsp1_channels_map_dsp_channel
        {
            get
            {
                return resourceManager.GetString("XPATH_dsp1_channels_map_dsp_channel").ToString();
            }
        }

        private string Item_pmp_data_dsp2_channels_map_dsp_channel
        {
            get
            {
                return resourceManager.GetString("XPATH_dsp2_channels_map_dsp_channel").ToString();
            }
        }

        #endregion

        public bool Item_pmp_Click()
        {
            Item_pmp.Click();
            return true;
        }

        public bool SwitchFrame_FromParent_Topmp_item()
        {
            webDriver.SwitchTo().Frame(Frame_pmp);
            return true;
        }

        public bool SwitchFrame_Frompmp_Todata()
        {
            webDriver.SwitchTo().Frame(Frame_pmp_data);
            webDriver.SwitchTo().Frame(Sub_Frame_pmp_data);
            return true;
        }

        public bool Item_pmp_data_Click()
        {
            Item_pmp_data.Click();
            return true;
        }

        public string Get_PQCabling()
        {
            var PMP_PQCabling = new SelectElement(Item_pmp_data_PQCabling);
            return PMP_PQCabling.SelectedOption.Text.ToString();
        }

        public string Get_FRCabling()
        {
            var PMP_FRCabling = new SelectElement(Item_pmp_data_FRCabling);
            return PMP_FRCabling.SelectedOption.Text.ToString();
        }

        public bool Item_dsp1_channels_map_Click()
        {
            Item_pmp_data_dsp1_channels_map.Click();
            return true;
        }

        public bool Item_dsp2_channels_map_Click()
        {
            Item_pmp_data_dsp2_channels_map.Click();
            return true;
        }

        public string Get_DSP1_channel_map(int dspchannelnumber)
        {
            return webDriver.FindElement(By.XPath(string.Format(Item_pmp_data_dsp1_channels_map_dsp_channel, dspchannelnumber + 1).Replace("\"", ""))).GetAttribute("value");
            
        }

        public string Get_DSP2_channel_map(int dspchannelnumber)
        {
            return webDriver.FindElement(By.XPath(string.Format(Item_pmp_data_dsp2_channels_map_dsp_channel, dspchannelnumber + 1).Replace("\"", ""))).GetAttribute("value");

        }
    }
}
