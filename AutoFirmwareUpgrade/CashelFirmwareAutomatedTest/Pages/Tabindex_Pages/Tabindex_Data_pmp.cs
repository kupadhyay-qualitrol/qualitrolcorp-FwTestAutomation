/*This file contains object repository for Webelements and method to perform action on them.
 * This is basically based on concept of Page Object Model(POM).
 * This file will contains objects related to Tabindex_Data_pmp page.
 */

using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace Tabindex_Data.pmp
{
    class Tabindex_Data_pmp
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;

        public Tabindex_Data_pmp(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
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

        private IWebElement Item_pmp_data_busbar_feeder_map
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[text()='busbar_feeder_map']"));
            }
        }

        private string Item_pmp_data_busbar
        {
            get
            {
                return resourceManager.GetString("LOCATOR_XPATH_BUSBAR").ToString();
                
            }
        }

        private string Edtbx_pmp_data_busbar_channel
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_BUSBAR_MAP_BUSBAR_CHANNEL").ToString();
                
            }
        }

        private string Edtbx_pmp_data_busbar_channel_cnt
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_BUSBAR_CHANNELCOUNT").ToString();                
            }
        }


        private string Item_pmp_data_feeder
        {
            get
            {
                return resourceManager.GetString("LOCATOR_XPATH_FEEDER").ToString();
            }
        }

        private string Edtbx_pmp_data_feeder_channel
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_BUSBAR_MAP_FEEDER_CHANNEL").ToString();             
            }
        }        

        private string Edtbx_pmp_data_feeder_channel_cnt
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_FEEDER_CHANNELCOUNT").ToString();
            }
        }

        private string Edtbx_pmp_data_feeder_channel_associated_busbar
        {
            get
            {
                return resourceManager.GetString("LOCATOR_NAME_FEEDER_ASSOCIATED_BUSBAR").ToString();
            }
        }      

        private IWebElement Item_pmp_data_dsp1_feeder_map
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[text()='dsp1_feeder_map']"));
            }
        }

        private string Edtbx_pmp_data_dsp1_feeder_map_dsp_feeder
        {
            get
            {
                return resourceManager.GetString("LOCATOR_XPATH_DSP1_FEEDERMAP_DSP_FEEDER").ToString();
            }
        }

        private IWebElement Item_pmp_data_dsp2_feeder_map
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[text()='dsp2_feeder_map']"));
            }
        }

        private string Edtbx_pmp_data_dsp2_feeder_map_dsp_feeder
        {
            get
            {
                return resourceManager.GetString("LOCATOR_XPATH_DSP2_FEEDERMAP_DSP_FEEDER").ToString();
            }
        }

        #endregion

        #region Methods

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

        public string Get_busbar_feeder_map_busbar(int bb_feed_num,int channelnum)
        {
            return webDriver.FindElement(By.Name(string.Format(Edtbx_pmp_data_busbar_channel, bb_feed_num, channelnum).Replace("\"", ""))).Text;
        }

        public string Get_busbar_feeder_map_feeder(int bb_feed_num, int channelnum)
        {
            return webDriver.FindElement(By.Name(string.Format(Edtbx_pmp_data_feeder_channel, bb_feed_num + 1, channelnum + 1).Replace("\"", ""))).Text;
        }

        public bool Item_pmp_data_busbar_feeder_map_Click()
        {
            Item_pmp_data_busbar_feeder_map.Click();
            return true;
        }

        public bool Item_pmp_data_busbar_Click(int bbnum)
        {
            webDriver.FindElement(By.XPath(string.Format(Item_pmp_data_busbar, bbnum).Replace("\"",""))).Click();
            return true;
        }

        public bool Item_pmp_data_feeder_Click(int feedernum)
        {
            webDriver.FindElement(By.XPath(string.Format(Item_pmp_data_feeder, feedernum).Replace("\"", ""))).Click();
            return true;
        }

        public bool Item_pmp_data_dsp1_feeder_map_Click()
        {
            Item_pmp_data_dsp1_feeder_map.Click();
            return true;
        }

        public bool Item_pmp_data_dsp2_feeder_map_Click()
        {
            Item_pmp_data_dsp2_feeder_map.Click();
            return true;
        }

        public string Get_pmp_data_dsp1_feeder_map_dsp(int dspfeedernum)
        {
            return webDriver.FindElement(By.XPath(string.Format(Edtbx_pmp_data_dsp1_feeder_map_dsp_feeder, dspfeedernum).Replace("\"", ""))).Text;
        }

        public string Get_pmp_data_dsp2_feeder_map_dsp(int dspfeedernum)
        {
            return webDriver.FindElement(By.XPath(string.Format(Edtbx_pmp_data_dsp2_feeder_map_dsp_feeder, dspfeedernum).Replace("\"", ""))).Text;
        }

        public string Get_pmp_data_busbar_channel_count(int busbarnum)
        {
            return webDriver.FindElement(By.XPath(string.Format(Edtbx_pmp_data_busbar_channel_cnt, busbarnum).Replace("\"", ""))).Text;
        }

        public string Get_pmp_data_feeder_channel_count(int feedernum)
        {
            return webDriver.FindElement(By.XPath(string.Format(Edtbx_pmp_data_feeder_channel_cnt, feedernum).Replace("\"", ""))).Text;
        }

        public string Get_pmp_data_feeder_assoc_busbar(int feedernum)
        {
            return webDriver.FindElement(By.XPath(string.Format(Edtbx_pmp_data_feeder_channel_associated_busbar, feedernum).Replace("\"", ""))).Text;
        }
        #endregion
    }
}
