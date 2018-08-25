
using OpenQA.Selenium;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using Tabindex_Configuration.dfr;

namespace CashelFirmware.NunitTests
{
    class ScalingFactorTag61850_ForPQStandalone
    {
        public void ValidateScalingfactorTag_PQStandalone(IWebDriver webDriver,string deviceIP,ExtentTest TestLog,string Cabling)
        {
            Tabindex_Configuration_dfr Tabindex_Configuration_Dfr = new Tabindex_Configuration_dfr(webDriver);

            Assert.AreEqual("Confniguration", Tabindex_Configuration_Dfr.OpenTabIndexPage(deviceIP), "Device is up/responding");
            TestLog.Log(LogStatus.Pass, "Device is up/responding");

            Assert.IsTrue(Tabindex_Configuration_Dfr.Item_Configuration_Click(), "Clicked on Configuration button on webpage");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked on Configuration button on webpage");

            Assert.IsTrue(Tabindex_Configuration_Dfr.SwitchFrame_FromParent_Todfr_item(), "Switched Frame from Default to dfr topology");
            TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from Default to dfr topology");

            Assert.IsTrue(Tabindex_Configuration_Dfr.Item_Dfr_Click(), "Clicked dfr option under tabindex_configuration page");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked dfr option under tabindex_configuration page");

            Assert.IsTrue(Tabindex_Configuration_Dfr.SwitchFrame_FromDfr_Toanalog(), "Switched Frame from dfr topology to analog");
            TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from dfr topology to analog");

            Assert.AreEqual("scale_factor_standalone_channels", Tabindex_Configuration_Dfr.Get_Scale_factor_standalone_channels_Tag());
            TestLog.Log(LogStatus.Pass, "Success:-ScaleFactor For Standalone channels Tag exists");

            Assert.IsTrue( Tabindex_Configuration_Dfr.Scale_factor_standalone_channels_Click(),"Clicked on scale factor for standalone channels tag");
            TestLog.Log(LogStatus.Info, "Success:-Clicked on scale factor for standalone channels tag");

            for (int channelnum = 0; channelnum < 18; channelnum++)
            {
                Assert.AreEqual("channel["+channelnum+"]",Tabindex_Configuration_Dfr.Get_Scale_factor_SC_channels_Tag(channelnum));
                TestLog.Log(LogStatus.Info, "Success:-Channel "+channelnum+" under scale factor standalone channel exists");

                Assert.IsTrue( Tabindex_Configuration_Dfr.Scale_factor_SC_channels_Click(channelnum));
                TestLog.Log(LogStatus.Info, "Success:-Clickec on Channel " + channelnum + " under scale factor standalone channel");

                Assert.AreEqual("scaling_factor", Tabindex_Configuration_Dfr.Get_Scale_factor_SC_channels_scalingfactor_Tag(channelnum));
                TestLog.Log(LogStatus.Info, "Success:-scaling_factor for Channel " + channelnum + " under scale factor standalone channel exists");
            }
            TestLog.Log(LogStatus.Info, "Success:-Tested Scale factor Tag for cabling:- " + Cabling);
        }
    }
}
