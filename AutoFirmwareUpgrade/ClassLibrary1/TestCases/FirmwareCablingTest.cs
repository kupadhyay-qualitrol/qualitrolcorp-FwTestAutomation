using System;
using System.Threading;
using System.Net.NetworkInformation;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using Tabindex_Configuration.dfr;
using Tabindex_Data.pmp;
using Tabindex_Data.soh;
using Mfgindex.Calibration;
using CashelFirmware.Utility;

namespace CashelFirmware.NunitTests
{
    [TestFixture]
    public class FirmwareCablingTest
    {
       IWebDriver webdriver;
       Ping pinger = new Ping();
       PingReply GetDevicePingReplySuccess;
       Read_WriteExcel rdexcel;
       string DataSetFileNameWithPath;
       System.Resources.ResourceManager resourceManager;
       public string deviceIP=string.Empty;


        public FirmwareCablingTest()
        {
            rdexcel = new Read_WriteExcel();
            resourceManager = new System.Resources.ResourceManager("ClassLibrary1.Resource", this.GetType().Assembly);
        }

        /// <summary>
        /// This method is used to initiate the selenium webdriver instance so that google chrome can be launched.
        /// </summary>
        [SetUp]
        public void TestSetup()
        {
            ChromeOptions options = new ChromeOptions();
            options.AddArguments(new[] {
                "start-maximized",
                "allow-running-insecure-content",
                "test-type","ignore-certificate-errors","disable-extensions"});
            options.AddUserProfilePreference("credentials_enable_service", false);
            options.AddUserProfilePreference("profile.password_manager_enabled", false);

            webdriver = new ChromeDriver(AppDomain.CurrentDomain.BaseDirectory, options);
            webdriver.Manage().Timeouts().ImplicitlyWait(TimeSpan.FromSeconds(25)); //Implicit wait is added so that selenium doesn't fail if any element is not loaded within specified time interval.
            //deviceIP = "10.75.58.51";
        }
        /// <summary>
        /// This method is called at the end of all the test so that google Chrome can be closed.
        /// </summary>
        [TearDown]
        public void TearDown()
        {
            webdriver.Close();
        }
        /// <summary>
        /// This method is used to test 3U cabling in the Firmware.
        /// a) Load the customized Calibration file
        /// b) Configure Cabling 3U
        /// c) Reboot the device
        /// d) Validate pmp_PQ/FR_Cabling
        /// </summary>
        [Test]
        public void Cabling3U()
        {
            DataSetFileNameWithPath = AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\CablingDataSet\3U_Standalone.xlsx";
            string deviceIP = rdexcel.ReadExcel(DataSetFileNameWithPath, "DeviceInfo", 0, "DeviceIP");

            Tabindex_Configuration_dfr Tabindex_Configuration_dfr = new Tabindex_Configuration_dfr(webdriver);
            Tabindex_Data_pmp Tabindex_Data_pmp = new Tabindex_Data_pmp(webdriver);
            Tabindex_Data_soh Tabindex_Data_soh = new Tabindex_Data_soh(webdriver);

            Assert.IsTrue(UploadCalbirationFile_3U_15I(deviceIP));

            Assert.AreEqual("Configuration", Tabindex_Configuration_dfr.OpenTabIndexPage(deviceIP), "Device is up/responding");
            Assert.IsTrue(Tabindex_Configuration_dfr.Item_Configuration_Click(), "Clicked on Configuration button on webpage");
            Assert.IsTrue(Tabindex_Configuration_dfr.SwitchFrame_FromParent_Todfr_item(), "Switched Frame from Default to dfr topology");
            Assert.IsTrue(Tabindex_Configuration_dfr.Item_Dfr_Click(), "Clicked dfr option under tabindex_configuration page");
            Assert.IsTrue(Tabindex_Configuration_dfr.SwitchFrame_FromDfr_Toanalog(), "Switched Frame from dfr topology to analog");
            Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_Click(), "Clicked on analog option to append it");

            for (int i = 0; i < 18; i++)
            {
                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_Click(i + 1), "Clicked on Channel[" + i + "] option");
                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_busbar_Select((i), DataSetFileNameWithPath), "Set the busbar number for iteration " + i);
                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_feeder_Click((i), DataSetFileNameWithPath), "Set the feeder number for iteration " + i);
                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_phase_Click((i), DataSetFileNameWithPath), "Set the phase name for iteration " + i);
                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_usage_Click((i), DataSetFileNameWithPath), "Set the usage name for iteration " + i);
            }
            Assert.IsTrue(Tabindex_Configuration_dfr.SwitchToParentFrame(), "Successfully switched to parent frame");
            Assert.IsTrue(Tabindex_Configuration_dfr.Commit_Click(), "Clicked on Commit");
            Assert.IsTrue(Tabindex_Configuration_dfr.SwitchToDefaultContent(), "Successfully switched to default frame/main window");
            Assert.IsTrue(Tabindex_Data_soh.Item_Data_Click(), "Clicked on Data button");
            Assert.IsTrue(Tabindex_Data_soh.SwitchFrame_FromParent_Tosoh_item(), "Switched frame from main window to soh topology");
            Assert.IsTrue(Tabindex_Data_soh.Item_soh_Click(), "Clicked on soh item in topology");
            Assert.IsTrue(Tabindex_Data_soh.SwitchFrame_Fromsoh_Tocontrol(), "Switched frame from soh to soh_control");
            Assert.IsTrue(Tabindex_Data_soh.Item_soh_control_Click(), "Clicked on control button to append the child items");
            Assert.IsTrue(Tabindex_Data_soh.Edtbx_soh_control_reset_cashel_Clear(), "Clear the value in the Editbox");
            Assert.IsTrue(Tabindex_Data_soh.Edtbx_soh_control_reset_cashel_SendKeys("1"), "Send 1 int the edit box");
            Assert.IsTrue(Tabindex_Data_soh.Item_soh_data_Click(), "Expanded Data item under soh");
            Assert.IsTrue(Tabindex_Data_soh.SwitchToParentFrame(), "Switch from current frame to parent to click on commit");
            Assert.IsTrue(Tabindex_Data_soh.Commit_RebootDevice(), "Successfully inserted javascript to reboot device");
            
            Thread.Sleep(40000); //this wait is required so that system is up and running after reboot

            do
            {
                GetDevicePingReplySuccess = pinger.Send(deviceIP);
            }
            while (!GetDevicePingReplySuccess.Status.ToString().Equals("Success"));

            Thread.Sleep(40000); //this wait is required so that system is up and running after reboot

            Assert.AreEqual("Data", Tabindex_Data_soh.OpenTabIndexPage(deviceIP));
            Assert.IsTrue(Tabindex_Data_soh.Item_Data_Click(), "Click on data button");
            Assert.IsTrue(Tabindex_Data_pmp.SwitchFrame_FromParent_Topmp_item());
            Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_Click(), "Clicked on pmp in topology");
            Assert.IsTrue(Tabindex_Data_pmp.SwitchFrame_Frompmp_Todata(), "Switch frame from pmp to pmp data");
            Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_Click(), "Clicked on pmp data");
            Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), 0, resourceManager.GetString("EXCELDATA_PQCABLING").ToString()), Tabindex_Data_pmp.Get_PQCabling());
            Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), 0, resourceManager.GetString("EXCELDATA_FRCABLING").ToString()), Tabindex_Data_pmp.Get_FRCabling());
            Assert.IsTrue(Tabindex_Data_pmp.Item_dsp1_channels_map_Click());

            for (int dspchannelmap1 = 0; dspchannelmap1 < 12; dspchannelmap1++)
            {
                Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath,resourceManager.GetString("EXCELDATA_SHEETNAME_DSPCHANNEL").ToString(), dspchannelmap1, resourceManager.GetString("EXCELDATA_Channel_Number_DSP1").ToString()),Tabindex_Data_pmp.Get_DSP1_channel_map(dspchannelmap1));
            }

            Assert.IsTrue(Tabindex_Data_pmp.Item_dsp2_channels_map_Click());

            for (int dspchannelmap2 = 0; dspchannelmap2 < 12; dspchannelmap2++)
            {
                Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSPCHANNEL").ToString(), dspchannelmap2, resourceManager.GetString("EXCELDATA_Channel_Number_DSP2").ToString()), Tabindex_Data_pmp.Get_DSP2_channel_map(dspchannelmap2));
            }
        }

        
        private bool UploadCalbirationFile_3U_15I(string deviceIP)
        {
            string calibrationfilenamewithpath = AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\CablingDataSet\" + rdexcel.ReadExcel(DataSetFileNameWithPath, "DeviceInfo", 0, "CalibrationFileName");
            Mfgindex_Calibration Mfgindex_Calibration = new Mfgindex_Calibration(webdriver);
            Assert.AreEqual("Calibration", Mfgindex_Calibration.OpenMfgindexPage(deviceIP));
            Assert.IsTrue(Mfgindex_Calibration.Btn_Calibration_Click());
            Assert.IsTrue(Mfgindex_Calibration.SwitchFrame_FromCalibration_ToENGLISH());
            Assert.IsTrue(Mfgindex_Calibration.Btn_ENGLISH_Click());
            Assert.IsTrue(Mfgindex_Calibration.Btn_LoadCalibration_Click());
            Assert.IsTrue(Mfgindex_Calibration.UploadFile(calibrationfilenamewithpath));
            Assert.IsTrue(Mfgindex_Calibration.Btn_Load_Click());
            return true;
        }
    }
}
