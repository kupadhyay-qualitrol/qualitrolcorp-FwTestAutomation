using System;
using OpenQA.Selenium;
using NUnit.Framework;
using Tabindex_Data.soh;
using RelevantCodes.ExtentReports;

namespace CashelFirmware.NunitTests
{
    public static class FirmwareInformation
    {
        public static double FirmwareVersion;

        public static double GetFirmwareVersion(string deviceIP,IWebDriver webDriver,ExtentTest TestLog)
        {
            Tabindex_Data_soh Tabindex_Data_Soh = new Tabindex_Data_soh(webDriver);
            Assert.AreEqual("Data", Tabindex_Data_Soh.OpenTabIndexPage(deviceIP), "Open up the tabindex Page");
             TestLog.Log(LogStatus.Pass, "Device is up/responding");

            Assert.IsTrue(Tabindex_Data_Soh.Item_Data_Click(), "Click on data button");
             TestLog.Log(LogStatus.Pass, "Success:-Click on data button");

            Assert.IsTrue(Tabindex_Data_Soh.SwitchFrame_FromParent_Tosoh_item());
             TestLog.Log(LogStatus.Pass, "Success:-SwitchFrame_FromParent_Tosoh_item");

            Assert.IsTrue(Tabindex_Data_Soh.Item_soh_Click(), "Clicked on soh in topology");
             TestLog.Log(LogStatus.Pass, "Success:-Clicked on soh in topology");

            Assert.IsTrue(Tabindex_Data_Soh.SwitchFrame_Fromsoh_Tocontrol(), "Switch frame from soh to soh data");
            TestLog.Log(LogStatus.Pass, "Success:-Switch frame from soh to soh data");

            Assert.IsTrue(Tabindex_Data_Soh.Item_soh_data_Click(), "Clicked on soh data");
             TestLog.Log(LogStatus.Pass, "Success:-Clicked on soh data");

            Assert.IsTrue(Tabindex_Data_Soh.Item_soh_data_static_data_Click(), "Clicked on soh_static data");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked on soh static data");

            FirmwareVersion = Convert.ToDouble((Tabindex_Data_Soh.Get_CPU_Application_Version()).Split('_')[0]);   //[0] Major Version ; [1] Minor Version
            return FirmwareVersion;
        }
    }
}
