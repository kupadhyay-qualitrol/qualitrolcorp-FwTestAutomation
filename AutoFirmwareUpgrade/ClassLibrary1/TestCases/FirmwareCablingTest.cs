using System;
using System.Threading;
using System.Net.NetworkInformation;
using NUnit.Framework;
using OpenQA.Selenium;
using Tabindex_Configuration.dfr;
using Tabindex_Data.pmp;
using Tabindex_Data.soh;
using Mfgindex.Calibration;
using CashelFirmware.Utility;
using RelevantCodes.ExtentReports;
using System.Resources;

namespace CashelFirmware.NunitTests
{

    public class FirmwareCablingTest     
    {
       Ping pinger = new Ping();
       PingReply GetDevicePingReplySuccess;
       Read_WriteExcel rdexcel;
       string DataSetFileNameWithPath;
       ResourceManager resourceManager;
      // public string deviceIP=string.Empty;

        public FirmwareCablingTest()
        {
            rdexcel = new Read_WriteExcel();
            resourceManager = new System.Resources.ResourceManager("ClassLibrary1.Resource", this.GetType().Assembly);
        }

        public void DemoReportPass(ExtentTest TestLog)
        {
            Assert.IsTrue(true);
            TestLog.Log(LogStatus.Pass, "Assert Pass as condition is True");
        }

        /// <summary>
        /// This method is used to test 3U cabling in the Firmware.
        /// a) Load the customized Calibration file
        /// b) Configure Cabling 3U
        /// c) Reboot the device
        /// d) Validate pmp_PQ/FR_Cabling
        /// e)Validate DSP Channel Mapping
        /// </summary>
        public void TestCabling(IWebDriver webdriver,string deviceIP,ExtentTest TestLog,string Cabling)
        {
            
            Tabindex_Configuration_dfr Tabindex_Configuration_dfr = new Tabindex_Configuration_dfr(webdriver);
            Tabindex_Data_pmp Tabindex_Data_pmp = new Tabindex_Data_pmp(webdriver);
            Tabindex_Data_soh Tabindex_Data_soh = new Tabindex_Data_soh(webdriver);
            TestLog.Log(LogStatus.Info, "Initialised the reference variable from classes");

            DataSetFileNameWithPath = AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\CablingDataSet\"+Cabling+".xlsx";
            switch (Cabling)
            {
                case "NOCIRCUIT":
                    break;
                case "3U":
                    {
                        Assert.IsTrue(UploadCalbirationFile(deviceIP, webdriver, "sn409026540_3U_15I.cal"));
                        TestLog.Log(LogStatus.Pass, "Successfully loaded calibration file:- 3U_15I");
                    }
                    break;
                case "2M3U":
                    {
                        Assert.IsTrue(UploadCalbirationFile(deviceIP, webdriver, "sn409026540_6U_12I.cal"));
                        TestLog.Log(LogStatus.Pass, "Successfully loaded calibration file:- 6U_12I");
                    }
                    break;
                case "4U":
                    {
                        Assert.IsTrue(UploadCalbirationFile(deviceIP, webdriver, "sn409026540_4U_14I.cal"));
                        TestLog.Log(LogStatus.Pass, "Successfully loaded calibration file:- 4U_14I");
                    }
                    break;

                case "2M4U":
                    {
                        Assert.IsTrue(UploadCalbirationFile(deviceIP, webdriver, "sn409026540_8U_10I.cal"));
                        TestLog.Log(LogStatus.Pass, "Successfully loaded calibration file:- 8U_10I");
                    }
                    break;
                case "4U3U":
                    {
                        Assert.IsTrue(UploadCalbirationFile(deviceIP, webdriver, "sn409026540_7U_11I.cal"));
                        TestLog.Log(LogStatus.Pass, "Successfully loaded calibration file:- 7U_11I");
                    }
                    break;
                default:
                    break;
            }


                Assert.AreEqual("Configuration", Tabindex_Configuration_dfr.OpenTabIndexPage(deviceIP), "Device is up/responding");
                TestLog.Log(LogStatus.Pass, "Device is up/responding");

                Assert.IsTrue(Tabindex_Configuration_dfr.Item_Configuration_Click(), "Clicked on Configuration button on webpage");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on Configuration button on webpage");

                Assert.IsTrue(Tabindex_Configuration_dfr.SwitchFrame_FromParent_Todfr_item(), "Switched Frame from Default to dfr topology");
                TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from Default to dfr topology");

                Assert.IsTrue(Tabindex_Configuration_dfr.Item_Dfr_Click(), "Clicked dfr option under tabindex_configuration page");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked dfr option under tabindex_configuration page");

                Assert.IsTrue(Tabindex_Configuration_dfr.SwitchFrame_FromDfr_Toanalog(), "Switched Frame from dfr topology to analog");
                TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from dfr topology to analog");

                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_Click(), "Clicked on analog option to append it");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on analog option to append it");

                for (int i = 0; i < 18; i++)
                {
                    Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_Click(i + 1), "Clicked on Channel[" + i + "] option");
                    TestLog.Log(LogStatus.Pass, "Success:-Clicked on Channel[" + i + "] option");

                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_si_unit_Select((i), DataSetFileNameWithPath), "Set the si_unit for iteration "+ i);
               

                    Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_busbar_Select((i), DataSetFileNameWithPath), "Set the busbar number for iteration " + i);
                    TestLog.Log(LogStatus.Pass, "Success:-Set the busbar number for iteration " + i);

                    Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_feeder_Click((i), DataSetFileNameWithPath), "Set the feeder number for iteration " + i);
                    TestLog.Log(LogStatus.Pass, "Success:-Set the feeder number for iteration " + i);

                    Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_phase_Click((i), DataSetFileNameWithPath), "Set the phase name for iteration " + i);
                    TestLog.Log(LogStatus.Pass, "Success:-Set the phase number for iteration " + i);

                    Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_usage_Click((i), DataSetFileNameWithPath), "Set the usage name for iteration " + i);
                    TestLog.Log(LogStatus.Pass, "Success:-Set the usage number for iteration " + i);
                }
                Assert.IsTrue(Tabindex_Configuration_dfr.SwitchToParentFrame(), "Successfully switched to parent frame");
                TestLog.Log(LogStatus.Pass, "Success:-switched to parent frame");

                Assert.IsTrue(Tabindex_Configuration_dfr.Commit_Click(), "Clicked on Commit");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on Commit");

                Assert.IsTrue(Tabindex_Configuration_dfr.SwitchToDefaultContent(), "Successfully switched to default frame/main window");
                TestLog.Log(LogStatus.Pass, "Success:-switched to default frame/main window");

                Assert.IsTrue(Tabindex_Data_soh.Item_Data_Click(), "Clicked on Data button");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on Data button");

                Assert.IsTrue(Tabindex_Data_soh.SwitchFrame_FromParent_Tosoh_item(), "Switched frame from main window to soh topology");
                TestLog.Log(LogStatus.Pass, "Success:-Switched frame from main window to soh topology");

                Assert.IsTrue(Tabindex_Data_soh.Item_soh_Click(), "Clicked on soh item in topology");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on soh item in topology");

                Assert.IsTrue(Tabindex_Data_soh.SwitchFrame_Fromsoh_Tocontrol(), "Switched frame from soh to soh_control");
                TestLog.Log(LogStatus.Pass, "Success:-Switched frame from soh to soh_control");

                Assert.IsTrue(Tabindex_Data_soh.Item_soh_control_Click(), "Clicked on control button to append the child items");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on control button to append the child items");

                Assert.IsTrue(Tabindex_Data_soh.Edtbx_soh_control_reset_cashel_Clear(), "Clear the value in the Editbox");
                TestLog.Log(LogStatus.Pass, "Success:-Clear the value in the Editbox");

                Assert.IsTrue(Tabindex_Data_soh.Edtbx_soh_control_reset_cashel_SendKeys("1"), "Send 1 int the edit box");
                TestLog.Log(LogStatus.Pass, "Success:-Send 1 int the edit box");

                Assert.IsTrue(Tabindex_Data_soh.Item_soh_data_Click(), "Expanded Data item under soh");
                TestLog.Log(LogStatus.Pass, "Success:-Expanded Data item under soh");

                Assert.IsTrue(Tabindex_Data_soh.SwitchToParentFrame(), "Switch from current frame to parent to click on commit");
                TestLog.Log(LogStatus.Pass, "Success:-Switch from current frame to parent to click on commit");

                Assert.IsTrue(Tabindex_Data_soh.Commit_RebootDevice(), "Successfully inserted javascript to reboot device");
                TestLog.Log(LogStatus.Pass, "Success:-Successfully inserted javascript to reboot device");

                Thread.Sleep(40000); //this wait is required so that device can go for reboot

                do
                {
                    GetDevicePingReplySuccess = pinger.Send(deviceIP);
                }
                while (!GetDevicePingReplySuccess.Status.ToString().Equals("Success"));

                Thread.Sleep(40000); //this wait is required so that system is up and running after reboot
                TestLog.Log(LogStatus.Info, "Device is up");
                Assert.AreEqual("Data", Tabindex_Data_soh.OpenTabIndexPage(deviceIP));
                TestLog.Log(LogStatus.Pass, "Success:-Device is up.Opened Tabindex page");

                Assert.IsTrue(Tabindex_Data_soh.Item_Data_Click(), "Click on data button");
                TestLog.Log(LogStatus.Pass, "Success:-Click on data button");

                Assert.IsTrue(Tabindex_Data_pmp.SwitchFrame_FromParent_Topmp_item());
                TestLog.Log(LogStatus.Pass, "Success:-SwitchFrame_FromParent_Topmp_item");

                Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_Click(), "Clicked on pmp in topology");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on pmp in topology");

                Assert.IsTrue(Tabindex_Data_pmp.SwitchFrame_Frompmp_Todata(), "Switch frame from pmp to pmp data");
                TestLog.Log(LogStatus.Pass, "Success:-Switch frame from pmp to pmp data");

                Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_Click(), "Clicked on pmp data");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on pmp data");

                Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), 0, resourceManager.GetString("EXCELDATA_PQCABLING").ToString()), Tabindex_Data_pmp.Get_PQCabling());
                TestLog.Log(LogStatus.Pass, "Success:-Validated PQ Cabling and found it correct");

                Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), 0, resourceManager.GetString("EXCELDATA_FRCABLING").ToString()), Tabindex_Data_pmp.Get_FRCabling());
                TestLog.Log(LogStatus.Pass, "Success:-Validated FR Cabling and found it correct");

                Assert.IsTrue(Tabindex_Data_pmp.Item_dsp1_channels_map_Click());
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on dsp1 channel map");

                for (int dspchannelmap1 = 0; dspchannelmap1 < 12; dspchannelmap1++)
                {
                    Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSPCHANNEL").ToString(), dspchannelmap1, resourceManager.GetString("EXCELDATA_Channel_Number_DSP1").ToString()), Tabindex_Data_pmp.Get_DSP1_channel_map(dspchannelmap1));
                }
                TestLog.Log(LogStatus.Pass, "Success:-Validated dsp1 channel mapping and it is correct");

                Assert.IsTrue(Tabindex_Data_pmp.Item_dsp2_channels_map_Click());
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on dsp2 channel map");

                for (int dspchannelmap2 = 0; dspchannelmap2 < 12; dspchannelmap2++)
                {
                    Assert.AreEqual(rdexcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSPCHANNEL").ToString(), dspchannelmap2, resourceManager.GetString("EXCELDATA_Channel_Number_DSP2").ToString()), Tabindex_Data_pmp.Get_DSP2_channel_map(dspchannelmap2));
                }
                TestLog.Log(LogStatus.Pass, "Success:-Validated dsp2 channel mapping and it is correct");

                TestLog.Log(LogStatus.Info, "Ended Executing "+Cabling+" Cabling Test");

        }

        
        private bool UploadCalbirationFile(string deviceIP,IWebDriver webdriver,string filepath)
        {
            string calibrationfilenamewithpath = AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\CablingDataSet\" + filepath;
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
