/*This file contains methods related to Cabling.
 * This is basically based on concept of Page Object Model(POM).
 */
using System;
using System.Threading;
using System.Net.NetworkInformation;
using NUnit.Framework;
using OpenQA.Selenium;
using Tabindex_Configuration.dfr;
using Tabindex_Configuration.pqp;
using Tabindex.Record;
using Tabindex_Data.pmp;
using Tabindex_Data.soh;
using Mfgindex.Calibration;
using CashelFirmware.Utility;
using RelevantCodes.ExtentReports;
using System.Resources;
using System.Xml;

namespace CashelFirmware.NunitTests
{

    public class FirmwareCablingTest     
    {
       Ping pinger = new Ping();
       PingReply GetDevicePingReplySuccess;
       string DataSetFileNameWithPath;
       ResourceManager resourceManager;
       public bool isCalibrationNeeded = true;
        string TXRatiobuilder = string.Empty;
        XmlDocument xmlDocument;
        /// <summary>
        /// This enum contains the data related to busbar and feeder map from DataSet in excel.
        /// </summary>
        enum busbarfeedermap
        {
            bb0_chnl_strt=0,
            bb1_chnl_strt=5,
            F0_chnl_strt=10,
            F1_chnl_strt = 16,
            F2_chnl_strt = 22,
            F3_chnl_strt = 28,
            F4_chnl_strt = 34
        }


        public FirmwareCablingTest()
        {
            resourceManager = new System.Resources.ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
            xmlDocument = new XmlDocument();
        }

        /// <summary>
        /// This method is used to test cabling in the Firmware.
        /// a) Load the customized Calibration file
        /// b) Configure Cabling 
        /// c) Reboot the device
        /// d) Validate pmp_PQ/FR_Cabling
        /// e)Validate DSP Channel Mapping
        /// f)Validate BusbarFedder map
        /// g)Validate DSP Feeder map
        /// <param name="Cabling"> Cabling to be configured in device</param>
        /// <param name="deviceIP">Device IP Address under test</param>
        /// <param name="TestLog">Object of Extent Report for log info</param>
        /// <param name="webdriver"></param>
        /// </summary>
        public void TestCabling(IWebDriver webdriver,string deviceIP,ExtentTest TestLog,string Cabling,bool isCalibrationNeeded=true,string IsFolderPath=null)
        {  
            Tabindex_Configuration_dfr Tabindex_Configuration_dfr = new Tabindex_Configuration_dfr(webdriver);
            Tabindex_Data_pmp Tabindex_Data_pmp = new Tabindex_Data_pmp(webdriver);
            Tabindex_Data_soh Tabindex_Data_soh = new Tabindex_Data_soh(webdriver);
            TestLog.Log(LogStatus.Info, "Initialised the reference variable from classes");
            
            //created this so that shared Data Set can be used for Test Cases with no injection and injection

            if (IsFolderPath == null)
            {
                DataSetFileNameWithPath = AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\CablingDataSet\" + Cabling + ".xlsx";
            }
            else
            {
                DataSetFileNameWithPath = AppDomain.CurrentDomain.BaseDirectory + IsFolderPath + Cabling + ".xlsx";
            }

            if (isCalibrationNeeded)
            {
                switch (Cabling)
                {
                    case "NOCIRCUIT":
                        Assert.IsTrue(UploadCalbirationFile(deviceIP, webdriver, "sn409026540_DefaultCalibration.cal"));
                        TestLog.Log(LogStatus.Pass, "Successfully loaded calibration file:- DefaultCalibration.cal");
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

           // xmlDocument.Load("http://" + deviceIP + "/cgi-bin/ipcxml.cgi?dfr:dfr/config/analog");
            //xmlDocument.Save(AppDomain.CurrentDomain.BaseDirectory + Cabling + "_" + DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss") + "_BeforeSendingConfig.xml");

           // xmlDocument.Load("http://" + deviceIP + "/cgi-bin/ipcxml.cgi?pmp:pmp/data");
           // xmlDocument.Save(AppDomain.CurrentDomain.BaseDirectory + Cabling + "_" + DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss") + "_BeforeRebootpmp.xml");
            Thread.Sleep(60000);       //added so that cablin can be properly set
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

           // xmlDocument.Load("http://" + deviceIP + "/cgi-bin/ipcxml.cgi?dfr:dfr/config/analog");
           // xmlDocument.Save(AppDomain.CurrentDomain.BaseDirectory + Cabling +"_"+ DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss") + "_AfterRebootConfig.xml");

           // xmlDocument.Load("http://" + deviceIP + "/cgi-bin/ipcxml.cgi?pmp:pmp/data");
          //  xmlDocument.Save(AppDomain.CurrentDomain.BaseDirectory + Cabling + "_" + DateTime.Now.ToString(@"dd_MM_yyyy_hh_mm_ss") + "_AfterRebootpmp.xml");

            Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), 0, resourceManager.GetString("EXCELDATA_FRCABLING").ToString()), Tabindex_Data_pmp.Get_FRCabling());
            TestLog.Log(LogStatus.Pass, "Success:-Validated FR Cabling and found it correct:- " + Tabindex_Data_pmp.Get_FRCabling());

            Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), 0, resourceManager.GetString("EXCELDATA_PQCABLING").ToString()), Tabindex_Data_pmp.Get_PQCabling());
            TestLog.Log(LogStatus.Pass, "Success:-Validated PQ Cabling and found it correct:- " + Tabindex_Data_pmp.Get_PQCabling());

            if (IsFolderPath == null)
            {
                Assert.Multiple(() =>
                {
                    Assert.IsTrue(Tabindex_Data_pmp.Item_dsp1_channels_map_Click());
                    TestLog.Log(LogStatus.Pass, "Success:-Clicked on dsp1 channel map");

                    for (int dspchannelmap1 = 0; dspchannelmap1 < 12; dspchannelmap1++)
                    {
                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSPCHANNEL").ToString(), dspchannelmap1, resourceManager.GetString("EXCELDATA_Channel_Number_DSP1").ToString()), Tabindex_Data_pmp.Get_DSP1_channel_map(dspchannelmap1), "DSP 1 Channel Map for Channel:- " + dspchannelmap1 + " is " + Tabindex_Data_pmp.Get_DSP1_channel_map(dspchannelmap1));
                        TestLog.Log(LogStatus.Info, "DSP 1 Channel Map for Channel:- " + dspchannelmap1 + " is " + Tabindex_Data_pmp.Get_DSP1_channel_map(dspchannelmap1));
                    }
                    TestLog.Log(LogStatus.Pass, "Success:-Validated dsp1 channel mapping and it is correct");

                    Assert.IsTrue(Tabindex_Data_pmp.Item_dsp2_channels_map_Click());
                    TestLog.Log(LogStatus.Pass, "Success:-Clicked on dsp2 channel map");

                    for (int dspchannelmap2 = 0; dspchannelmap2 < 12; dspchannelmap2++)
                    {
                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSPCHANNEL").ToString(), dspchannelmap2, resourceManager.GetString("EXCELDATA_Channel_Number_DSP2").ToString()), Tabindex_Data_pmp.Get_DSP2_channel_map(dspchannelmap2), "DSP 2 Channel Map for Channel:- " + dspchannelmap2 + " is " + Tabindex_Data_pmp.Get_DSP2_channel_map(dspchannelmap2));
                        TestLog.Log(LogStatus.Info, "DSP 2 Channel Map for Channel:- " + dspchannelmap2 + " is " + Tabindex_Data_pmp.Get_DSP2_channel_map(dspchannelmap2));
                    }
                    TestLog.Log(LogStatus.Pass, "Success:-Validated dsp2 channel mapping and it is correct");

                    //Validate Busbar Feeder map

                    Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_busbar_feeder_map_Click(), "Clicked on Busbar Feeder map");
                    TestLog.Log(LogStatus.Pass, "Success:-Clicked on Busbar Feeder Map");

                    int[] busbar_map = new int[] { (int)busbarfeedermap.bb0_chnl_strt, (int)busbarfeedermap.bb1_chnl_strt };
                    int[] feeder_map = new int[] { (int)busbarfeedermap.F0_chnl_strt, (int)busbarfeedermap.F1_chnl_strt, (int)busbarfeedermap.F2_chnl_strt, (int)busbarfeedermap.F3_chnl_strt, (int)busbarfeedermap.F4_chnl_strt };

                    for (int busbarmap = 0; busbarmap < 2; busbarmap++)
                    {
                        Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_busbar_Click(busbarmap), "Clicked on busbar_" + busbarmap);
                        TestLog.Log(LogStatus.Pass, "Success:-Clicked on busbar_" + busbarmap);

                        for (int channel = 0; channel < 4; channel++)
                        {
                            Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_BUSBAR_FEEDER_MAP").ToString(), channel + busbar_map[busbarmap], resourceManager.GetString("EXCELDATA_BUSBAR_FEEDER_CHANNELNUMBER").ToString()), Tabindex_Data_pmp.Get_busbar_feeder_map_busbar(busbarmap, channel), "Checking busbar:- " + busbarmap + " channel :-" + channel + " value is " + Tabindex_Data_pmp.Get_busbar_feeder_map_busbar(busbarmap, channel));
                            TestLog.Log(LogStatus.Pass, "Success:-Checking busbar: -" + busbarmap + " channel: -" + channel + " value is " + Tabindex_Data_pmp.Get_busbar_feeder_map_busbar(busbarmap, channel));
                        }
                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_BUSBAR_FEEDER_MAP").ToString(), busbar_map[busbarmap] + 4, resourceManager.GetString("EXCELDATA_BUSBAR_FEEDER_CHANNELNUMBER").ToString()), Tabindex_Data_pmp.Get_pmp_data_busbar_channel_count(busbarmap), "Checking the busbar " + busbarmap + " channel count" + Tabindex_Data_pmp.Get_pmp_data_busbar_channel_count(busbarmap));
                        TestLog.Log(LogStatus.Pass, "Success:-The busbar " + busbarmap + " channel count is " + Tabindex_Data_pmp.Get_pmp_data_busbar_channel_count(busbarmap));
                    }

                    for (int feedermap = 0; feedermap < 5; feedermap++)
                    {
                        Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_feeder_Click(feedermap), "Clicked on Feeder_" + feedermap);
                        TestLog.Log(LogStatus.Pass, "Success:-Clicked on Feeder_" + feedermap);

                        for (int channel = 0; channel < 4; channel++)
                        {
                            Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_BUSBAR_FEEDER_MAP").ToString(), channel + feeder_map[feedermap], resourceManager.GetString("EXCELDATA_BUSBAR_FEEDER_CHANNELNUMBER").ToString()), Tabindex_Data_pmp.Get_busbar_feeder_map_feeder(feedermap, channel), "Checking feeder:- " + feedermap + " channel :-" + channel + " value is " + Tabindex_Data_pmp.Get_busbar_feeder_map_feeder(feedermap, channel));
                            TestLog.Log(LogStatus.Pass, "Success:-Checking feeder: -" + feedermap + " channel: -" + channel + " value is " + Tabindex_Data_pmp.Get_busbar_feeder_map_feeder(feedermap, channel));
                        }
                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_BUSBAR_FEEDER_MAP").ToString(), feeder_map[feedermap] + 4, resourceManager.GetString("EXCELDATA_BUSBAR_FEEDER_CHANNELNUMBER").ToString()), Tabindex_Data_pmp.Get_pmp_data_feeder_channel_count(feedermap), "Checking the feeder " + feedermap + " channel count" + Tabindex_Data_pmp.Get_pmp_data_feeder_channel_count(feedermap));
                        TestLog.Log(LogStatus.Pass, "Success:-The feeder " + feedermap + " channel count is " + Tabindex_Data_pmp.Get_pmp_data_feeder_channel_count(feedermap));

                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_BUSBAR_FEEDER_MAP").ToString(), feeder_map[feedermap] + 5, resourceManager.GetString("EXCELDATA_BUSBAR_FEEDER_CHANNELNUMBER").ToString()), Tabindex_Data_pmp.Get_pmp_data_feeder_assoc_busbar(feedermap), "Checking the feeder " + feedermap + " associated busbar is " + Tabindex_Data_pmp.Get_pmp_data_feeder_assoc_busbar(feedermap));
                        TestLog.Log(LogStatus.Pass, "Success:-The feeder " + feedermap + " associated busbar is " + Tabindex_Data_pmp.Get_pmp_data_feeder_assoc_busbar(feedermap));

                    }

                    //Validate DSP_FEEDER_MAP

                Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_dsp1_feeder_map_Click(), "Clicked on dsp1 feeder map");
                    TestLog.Log(LogStatus.Pass, "Success:-Clicked on dsp1 feeder map");

                    for (int dspfeeder = 0; dspfeeder < 3; dspfeeder++)
                    {
                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSP_FEEDER_MAP").ToString(), dspfeeder, resourceManager.GetString("EXCELDATA_DSP_FEEDER_NUMBER").ToString()), Tabindex_Data_pmp.Get_pmp_data_dsp1_feeder_map_dsp(dspfeeder), "Checking dsp1 feeder :-" + Tabindex_Data_pmp.Get_pmp_data_dsp1_feeder_map_dsp(dspfeeder));
                        TestLog.Log(LogStatus.Pass, "Success:-Dsp1 feeder number:- " + dspfeeder + ": " + Tabindex_Data_pmp.Get_pmp_data_dsp1_feeder_map_dsp(dspfeeder));
                    }

                    Assert.IsTrue(Tabindex_Data_pmp.Item_pmp_data_dsp2_feeder_map_Click(), "Clicked on dsp2 feeder map");
                    TestLog.Log(LogStatus.Pass, "Success:-Clicked on dsp2 feeder map");

                    for (int dspfeeder = 0; dspfeeder < 3; dspfeeder++)
                    {
                        Assert.AreEqual(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_DSP_FEEDER_MAP").ToString(), dspfeeder + 3, resourceManager.GetString("EXCELDATA_DSP_FEEDER_NUMBER").ToString()), Tabindex_Data_pmp.Get_pmp_data_dsp2_feeder_map_dsp(dspfeeder), "Checking dsp2 feeder :-" + Tabindex_Data_pmp.Get_pmp_data_dsp2_feeder_map_dsp(dspfeeder));
                        TestLog.Log(LogStatus.Pass, "Success:-Dsp2 feeder number:- " + dspfeeder + ": " + Tabindex_Data_pmp.Get_pmp_data_dsp2_feeder_map_dsp(dspfeeder));
                    }
                });
            }
            TestLog.Log(LogStatus.Info, "Ended Executing "+Cabling+" Cabling Test"); 
        }

        /// <summary>
        /// Upload Calibration file in the device
        /// </summary>
        /// <param name="deviceIP">Device IP</param>
        /// <param name="webdriver">Selenium driver instance</param>
        /// <param name="filepath">Path of the calibration file</param>
        /// <returns></returns>
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

        /// <summary>
        /// This method is used to Configure PQ 10min and PQ Free Interval Parameters
        /// </summary>
        /// <param name="deviceIP">IP Address of the device</param>
        /// <param name="webdriver">Selenium driver for chrome execution</param>
        /// <param name="TestLog">Object of Extent Report for log info</param>
        /// <param name="Cabling">Cabling to be configured</param>
        /// <param name="PQDuration">PQ Free interval time duration</param>
        /// <param name="PQDurationUnit">PQ Free interval duration unit</param>
        public void ConfigurePQData(string deviceIP,IWebDriver webdriver, ExtentTest TestLog,string Cabling,string PQDuration,string PQDurationUnit)
        {
            Tabindex_Configuration_dfr Tabindex_Configuration_dfr = new Tabindex_Configuration_dfr(webdriver);
            Tabindex_Configuration_pqp Tabindex_Configuration_pqp = new Tabindex_Configuration_pqp(webdriver);
            TestLog.Log(LogStatus.Info, "Initialised the reference variable from classes");

            DataSetFileNameWithPath = AppDomain.CurrentDomain.BaseDirectory + @"\TestData\PQStandalone_Ckt_ParamOnly\" + Cabling + ".xlsx";

            Assert.AreEqual("Configuration", Tabindex_Configuration_dfr.OpenTabIndexPage(deviceIP), "Device is up/responding");
            TestLog.Log(LogStatus.Pass, "Device is up/responding");

            Assert.IsTrue(Tabindex_Configuration_dfr.Item_Configuration_Click(), "Clicked on Configuration button on webpage");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked on Configuration button on webpage");

            Assert.IsTrue(Tabindex_Configuration_dfr.SwitchFrame_FromParent_Todfr_item(), "Switched Frame from Default to dfr topology");
            TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from Default to dfr topology");

            Assert.IsTrue(Tabindex_Configuration_pqp.Item_pqp_Click(), "Clicked on pqp");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on pqp");

            Assert.IsTrue(Tabindex_Configuration_pqp.SwitchFrame_Frompqp_Tob1_config(), "Switched Frame from pqp to b1_config");
            TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from pqp to b1_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b1_config_Click(), "Clicked on b1_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b1_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.record_config_Click(), "Clicked on record_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on record_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.param_id_list_Click(), "Clicked on param_id_list");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on param_id_list");
            string pq_param = string.Empty;
            int cnt = 0;
            for (int paramindex = 0; paramindex < 1189; paramindex++)
            {
                if (cnt <= 99)
                {
                    if (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo") == "16777215")
                    {
                        cnt++;
                        pq_param += Tabindex_Configuration_pqp.Configure_b1ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param);
                        break;
                    }
                    else
                    {
                        cnt++;
                        pq_param += Tabindex_Configuration_pqp.Configure_b1ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param);
                    }
                }
                else
                {
                    if (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo") == "16777215")
                    {                           
                        pq_param += Tabindex_Configuration_pqp.Configure_b1ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param);
                        break;
                    }
                    else
                    {                         
                        pq_param += Tabindex_Configuration_pqp.Configure_b1ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param);
                    }
                    Assert.IsTrue(Tabindex_Configuration_pqp.Switch_ToParentFrame(), "Switched to Parent Frame");
                    TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

                    Assert.IsTrue(Tabindex_Configuration_pqp.Insertpqp_param_javascript(pq_param), "Inserted pq parameters");
                    TestLog.Log(LogStatus.Pass, "Success:- Inserted pq parameters");

                    Assert.IsTrue(Tabindex_Configuration_pqp.Commit_Click(), "Configured pq 10 min param");
                    TestLog.Log(LogStatus.Pass, "Success:-Configured pq 10 min param");
                    pq_param = string.Empty;
                    Assert.IsTrue(Tabindex_Configuration_pqp.SwitchFrame_FromCommit_Tob1_config(), "Switched Frame from pqp to b1_config");
                    TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from pqp to b1_config");

                    Assert.IsTrue(Tabindex_Configuration_pqp.b1_config_Click(), "Clicked on b1_config");
                    TestLog.Log(LogStatus.Pass, "Success:- Clicked on b1_config");

                    Assert.IsTrue(Tabindex_Configuration_pqp.record_config_Click(), "Clicked on record_config");
                    TestLog.Log(LogStatus.Pass, "Success:- Clicked on record_config");

                    Assert.IsTrue(Tabindex_Configuration_pqp.param_id_list_Click(), "Clicked on param_id_list");
                    TestLog.Log(LogStatus.Pass, "Success:- Clicked on param_id_list");
                    cnt = 0;                      
                }
            }
            TestLog.Log(LogStatus.Pass, "Success:- Configured PQ param in string");

            Assert.IsTrue(Tabindex_Configuration_pqp.Switch_ToParentFrame(), "Switched to Parent Frame");
            TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

            Assert.IsTrue(Tabindex_Configuration_pqp.Insertpqp_param_javascript(pq_param), "Inserted pq parameters");
            TestLog.Log(LogStatus.Pass, "Success:- Inserted pq parameters");

            Assert.IsTrue(Tabindex_Configuration_pqp.Commit_Click(), "Configured pq 10 min param");
            TestLog.Log(LogStatus.Pass, "Success:-Configured pq 10 min param");

            Assert.IsTrue(Tabindex_Configuration_pqp.SwitchFrame_FromCommit_Tob1_config(), "Switched frame from Commit button to b2_config");
            TestLog.Log(LogStatus.Pass, "Success:-Switched frame from Commit button to b2_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_config_Click(), "Clicked on b2_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_record_config_click(), "Clicked on b2_record_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_record_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.Item_pqp_b2_config_sec_min_flag_Select(PQDurationUnit), "Selected unit as " + PQDurationUnit);
            TestLog.Log(LogStatus.Pass, "Success:- Selected unit as " + PQDurationUnit);

            Assert.IsTrue(Tabindex_Configuration_pqp.Switch_ToParentFrame(), "Switched to Parent Frame");
            TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

            Assert.IsTrue(Tabindex_Configuration_pqp.Commit_Click(), "Configured pq free interval param");
            TestLog.Log(LogStatus.Pass, "Success:-Configured pq free interval param");

            Assert.IsTrue(Tabindex_Configuration_pqp.SwitchFrame_FromCommit_Tob1_config(), "Switched frame from Commit button to b2_config");
            TestLog.Log(LogStatus.Pass, "Success:-Switched frame from Commit button to b2_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_config_Click(), "Clicked on b2_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_record_config_click(), "Clicked on b2_record_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_record_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.Switch_ToParentFrame(), "Switched to Parent Frame");
            TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

            Assert.IsTrue(Tabindex_Configuration_pqp.Insertpqp_param_javascript("pqp:pqp/config/b2_config/record_config/duration=" + PQDuration), "Inserted PQ Free Interval Duration");
            TestLog.Log(LogStatus.Pass, "Success:- Inserted PQ Free Interval Duration");

            Assert.IsTrue(Tabindex_Configuration_pqp.Commit_Click(), "Configured pq free interval duration");
            TestLog.Log(LogStatus.Pass, "Success:-Configured pq free interval duration");

            Assert.IsTrue(Tabindex_Configuration_pqp.SwitchFrame_FromCommit_Tob1_config(), "Switched frame from Commit button to b2_config");
            TestLog.Log(LogStatus.Pass, "Success:-Switched frame from Commit button to b2_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_config_Click(), "Clicked on b2_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_record_config_click(), "Clicked on b2_record_config");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_record_config");

            Assert.IsTrue(Tabindex_Configuration_pqp.b2_param_id_list_Click(), "Clicked on b2 param_id_list");
            TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2 param_id_list");
            cnt = 0;
            string pq_param_b2 = string.Empty;
            for (int paramindex = 0; paramindex < 257; paramindex++)
            {
                if (cnt <= 49)
                {
                    if (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo") == "16777215")
                    {
                        pq_param_b2 += Tabindex_Configuration_pqp.Configure_b2ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param_b2);
                        break;
                    }
                    else
                    {
                        cnt++;
                        pq_param_b2 += Tabindex_Configuration_pqp.Configure_b2ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param_b2);
                    }
                }
                else
                {
                    if (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo") == "16777215")
                    {
                        pq_param_b2 += Tabindex_Configuration_pqp.Configure_b2ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param_b2);
                        break;
                    }
                    else
                    {
                        pq_param_b2 += Tabindex_Configuration_pqp.Configure_b2ConfigParam(paramindex, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, "StandaloneParameters", paramindex, "CalcNo"), pq_param_b2);
                    }
                    Assert.IsTrue(Tabindex_Configuration_pqp.Switch_ToParentFrame(), "Switched to Parent Frame");
                    TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

                    Assert.IsTrue(Tabindex_Configuration_pqp.Insertpqp_param_javascript(pq_param_b2), "Inserted pq parameters");
                    TestLog.Log(LogStatus.Pass, "Success:- Inserted pq parameters");

                    Assert.IsTrue(Tabindex_Configuration_pqp.Commit_Click(), "Configured pq Free interval param");
                    TestLog.Log(LogStatus.Pass, "Success:-Configured pq 10 min param");

                    pq_param_b2 = string.Empty;

                    Assert.IsTrue(Tabindex_Configuration_pqp.SwitchFrame_FromCommit_Tob1_config(), "Switched frame from Commit button to b2_config");
                    TestLog.Log(LogStatus.Pass, "Success:-Switched frame from Commit button to b2_config");

                    Assert.IsTrue(Tabindex_Configuration_pqp.b2_config_Click(), "Clicked on b2_config");
                    TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_config");

                    Assert.IsTrue(Tabindex_Configuration_pqp.b2_record_config_click(), "Clicked on b2_record_config");
                    TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2_record_config");

                    Assert.IsTrue(Tabindex_Configuration_pqp.b2_param_id_list_Click(), "Clicked on b2 param_id_list");
                    TestLog.Log(LogStatus.Pass, "Success:- Clicked on b2 param_id_list");
                    cnt = 0;
                }
            }
            TestLog.Log(LogStatus.Pass, "Success:- Configured PQ param in string");

            Assert.IsTrue(Tabindex_Configuration_pqp.Switch_ToParentFrame(), "Switched to Parent Frame");
            TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

            Assert.IsTrue(Tabindex_Configuration_pqp.Insertpqp_param_javascript(pq_param_b2), "Inserted pq_b2 parameters");
            TestLog.Log(LogStatus.Pass, "Success:- Inserted pq_b2 parameters");

            Assert.IsTrue(Tabindex_Configuration_pqp.Commit_Click(), "Configured pq free interval param");
            TestLog.Log(LogStatus.Pass, "Success:-Configured pq free interval param");                      
        }

        /// <summary>
        /// Mehtod to download PQ Data from Record tab
        /// </summary>
        /// <param name="deviceIP">Device IP address under test</param>
        /// <param name="webdriver">Selenium driver instance</param>
        /// <param name="TestLog">Extent report Log object for reporting</param>
        /// <param name="Cabling">Cabling to be configured</param>
        /// <param name="RecordDownloadTime">Time to download 10 minute PQ record</param>
        public void DownloadPQData(string deviceIP, IWebDriver webdriver, ExtentTest TestLog, string Cabling,string RecordDownloadTime)
        {
            Tabindex_Configuration_dfr Tabindex_Configuration_dfr = new Tabindex_Configuration_dfr(webdriver);
            Tabindex_Record Tabindex_Record = new Tabindex_Record(webdriver);

            Assert.AreEqual("Configuration", Tabindex_Configuration_dfr.OpenTabIndexPage(deviceIP), "Device is up/responding");
            TestLog.Log(LogStatus.Pass, "Device is up/responding");

            Assert.IsTrue(Tabindex_Record.Item_Record_Click(), "Clicked on Record Tab");
            TestLog.Log(LogStatus.Pass, "Clicked on Record Tab");

            Assert.IsTrue(Tabindex_Record.SwitchFrame_ToRecordPage(), "Switched Frame from Parent to Record tab");
            TestLog.Log(LogStatus.Pass, "Switched Frame from Parent to Record tab");

            Assert.IsTrue(Tabindex_Record.Item_PQ10min_Click(), "Clicked on PQ 10 min Record Head");
            TestLog.Log(LogStatus.Pass, "Clicked on PQ 10 min Record Head");

            Assert.IsTrue(Tabindex_Record.Item_PQ10minRecord_Click(), "Clicked on PQ 10 min Record");
            TestLog.Log(LogStatus.Pass, "Clicked on PQ 10 min Record");

            Assert.IsTrue(Tabindex_Record.Item_StartTime_Edit(RecordDownloadTime), "Send Record Start time to be downloaded");
            TestLog.Log(LogStatus.Pass, "Send Record Start time to be downloaded");

            Assert.IsTrue(Tabindex_Record.Item_EndTime_Edit(RecordDownloadTime), "Send Record End time to be downloaded");
            TestLog.Log(LogStatus.Pass, "Send Record End time to be downloaded");

            Assert.IsTrue(Tabindex_Record.Item_DonwloadPQ10minRecord_Click(), "Clicked on Data to download record file");
            TestLog.Log(LogStatus.Pass, "Clicked on Data to download record");

            Assert.IsTrue(Tabindex_Record.Item_DonwloadPQ10minReturn_Click(), "Clicked on Return button");
            TestLog.Log(LogStatus.Pass, "Clicked on Return button");

            Assert.IsTrue(Tabindex_Record.Item_PQFreeInterval_Click(), "Clicked on PQ FreeInterval Record Head");
            TestLog.Log(LogStatus.Pass, "Clicked on PQ FreeInterval Record Head");

            Assert.IsTrue(Tabindex_Record.Item_PQFreeIntervalRecord_Click(), "Clicked on PQ FreeInterval Record");
            TestLog.Log(LogStatus.Pass, "Clicked on PQ FreeInterval Record");

            Assert.IsTrue(Tabindex_Record.Item_StartTime_Edit(RecordDownloadTime), "Send Record Start time to be downloaded");
            TestLog.Log(LogStatus.Pass, "Send Record Start time to be downloaded");

            Assert.IsTrue(Tabindex_Record.Item_EndTime_Edit(RecordDownloadTime), "Send Record End time to be downloaded");
            TestLog.Log(LogStatus.Pass, "Send Record End time to be downloaded");

            Assert.IsTrue(Tabindex_Record.Item_DonwloadPQ10minRecord_Click(), "Clicked on Data to download record file");
            TestLog.Log(LogStatus.Pass, "Clicked on Data to download record");

            Assert.IsTrue(Tabindex_Record.Item_DonwloadPQ10minReturn_Click(), "Clicked on Return button");
            TestLog.Log(LogStatus.Pass, "Clicked on Return button");
            Thread.Sleep(5000); //wait time added to download record
        }

        public void SetTXRationum(string deviceIP,IWebDriver webDriver, ExtentTest TestLog,int[] TXRatio)
        {
            Tabindex_Configuration_dfr Tabindex_Configuration_dfr = new Tabindex_Configuration_dfr(webDriver);
            Tabindex_Data_soh Tabindex_Data_soh = new Tabindex_Data_soh(webDriver);

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
            TXRatiobuilder = string.Empty;

            for (int i = 0; i < 18; i++)
            {
                Assert.IsTrue(Tabindex_Configuration_dfr.Item_dfr_analog_channel_Click(i + 1), "Clicked on Channel[" + i + "] option");
                TestLog.Log(LogStatus.Pass, "Success:-Clicked on Channel[" + i + "] option");
                TXRatiobuilder+=Tabindex_Configuration_dfr.Configure_TXRatio_num((i), Convert.ToString(TXRatio[i]));
            }

            TestLog.Log(LogStatus.Pass, "Success:- Configured TXRatioNum in string");

            Assert.IsTrue(Tabindex_Configuration_dfr.SwitchToParentFrame(), "Switched to Parent Frame");
            TestLog.Log(LogStatus.Pass, "Success:- Switched to Parent Frame");

            Assert.IsTrue(Tabindex_Configuration_dfr.InsertTXRation_Num_javascript(TXRatiobuilder), "Inserted TXRatioNum parameters");
            TestLog.Log(LogStatus.Pass, "Success:- Inserted TXRatioNum parameters");

            Assert.IsTrue(Tabindex_Configuration_dfr.Commit_Click(), "Configured TXRatioNum");
            TestLog.Log(LogStatus.Pass, "Success:-Configured TXRatioNum");
            Thread.Sleep(20000); //wait time so that TX ratio can be configured in device
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
        }
    }
}
