﻿/*This file contains object repository for Webelements and method to perform action on them.
 * This is basically based on concept of Page Object Model(POM).
 * This file will contains objects related to Mfgindex_Calibration page.
 */

using OpenQA.Selenium;


namespace Mfgindex.Calibration
{
    class Mfgindex_Calibration
    {
        IWebDriver webDriver;
        System.Resources.ResourceManager resourceManager;

        public Mfgindex_Calibration(IWebDriver webDriver)
        {
            this.webDriver = webDriver;
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
        }

        #region ObjectRepository

        private IWebElement Btn_Calibration
        {
            get
            {
                return webDriver.FindElement(By.XPath("//a[text()='Calibration']"));
            }
        }
        private IWebElement SwitchFrame_FromCalibration_ToENGLISHBtn
        {
            get
            {
                return webDriver.FindElement(By.CssSelector("#TabView1>div.Pages>div:nth-child(3)>iframe"));
            }
        }
        private IWebElement Btn_ENGLISH
        {
            get
            {
                return webDriver.FindElement(By.CssSelector("input[type='submit'][value='ENGLISH']"));
               // return webDriver.FindElement(By.CssSelector("body>div>form:nth-child(3)>input[type='submit'][value='ENGLISH']"));
            }
        }

        private IWebElement Btn_LoadCalibration
        {
            get
            {
                return webDriver.FindElement(By.Id("load"));
            }
        }

        private IWebElement Btn_ChooseFile
        {
            get
            {
                return webDriver.FindElement(By.CssSelector("input[type='file'][name='filename']"));
            }
        }

        private IWebElement Btn_Load
        {
            get
            {
                return webDriver.FindElement(By.CssSelector("input[type='submit'][value='Load']"));
            }
        }

        #endregion

        #region Methods

        public string OpenMfgindexPage(string deviceIP)

        {
            webDriver.Navigate().GoToUrl(@"http://qualitrol:qualcorp_techSupport10@" + deviceIP + "/mfgindex");
            webDriver.Navigate().GoToUrl(@"http://" + deviceIP + "/mfgindex");
            return Btn_Calibration.Text;
        }

        public bool Btn_Calibration_Click()
        {
            Btn_Calibration.Click();
            return true;
        }

        public bool SwitchFrame_FromCalibration_ToENGLISH()
        {
            webDriver.SwitchTo().Frame(SwitchFrame_FromCalibration_ToENGLISHBtn);
            return true;
        }
        public bool Btn_ENGLISH_Click()
        {
            Btn_ENGLISH.Click();
            return true;
        }

        public bool Btn_LoadCalibration_Click()
        {
            Btn_LoadCalibration.Click();
            return true;
        }

        public bool Btn_ChooseFile_Click()
        {
            Btn_ChooseFile.Click();
            return true;
        }

        public bool Btn_Load_Click()
        {
            Btn_Load.Click();
            return true;
        }

        public bool UploadFile(string Filepathwithname)
        {
            Btn_ChooseFile.SendKeys(Filepathwithname);
            return true;
        }
        #endregion




    }
}
