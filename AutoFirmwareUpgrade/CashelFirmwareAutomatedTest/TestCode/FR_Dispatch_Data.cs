/* This file contains code to validate the FR Data at dispatch side.
  */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tabindex_Data.dispatch;
using Tabindex_Data.pmp;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System.Resources;
using NUnit.Framework;
using CashelFirmware.Utility;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.NunitTests
{
    public class FR_Dispatch_Data
    {
        ResourceManager resourceManager;
        string DataSetFileNameWithPath;
        double[] FR_Dispactch_RMS_Data;
        Dictionary<string, string> CablingInfo;
        StringBuilder FR_Data_RMS_Value;
        int[] TXRatioMultiplier;
        double injectvoltage;
        double injectedcurrent;
        Dictionary<string, int> Channel_TXRatio;
        Dictionary<string, string> ChannelType;
        Dictionary<string, string> sortedChannelType;
        public FR_Dispatch_Data()
        {
            resourceManager = new ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
            CablingInfo = new Dictionary<string, string>();
            Channel_TXRatio = new Dictionary<string, int>();
            ChannelType = new Dictionary<string, string>();
            sortedChannelType = new Dictionary<string, string>();
            
            FR_Data_RMS_Value = new StringBuilder();
            FR_Dispactch_RMS_Data = new double[18];
            injectvoltage = 50.0;
            injectedcurrent = 1.0;
            TXRatioMultiplier = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        }

        public void Validate_FR_Dispatch_Data(IWebDriver webDriver, string deviceIP, ExtentTest TestLog, string Cabling, string FRDataSetPath)
        {
            Tabindex_Data_dispatch Tabindex_Data_Dispatch = new Tabindex_Data_dispatch(webDriver);
            Tabindex_Data_pmp Tabindex_Data_pmp = new Tabindex_Data_pmp(webDriver);
            DataSetFileNameWithPath = FRDataSetPath + Cabling + ".xlsx";
            CablingInfo.Clear();
            Channel_TXRatio.Clear();
            sortedChannelType.Clear();
            ChannelType.Clear();
            FR_Data_RMS_Value.Clear();

            for (int channelindex = 0; channelindex < DeviceInformation.glb_deviceType; channelindex++)
            {
                if (!Channel_TXRatio.ContainsKey(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString())))
                {
                    Channel_TXRatio.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()), TXRatioMultiplier[channelindex]);
                }
                else
                {
                    TestLog.Log(LogStatus.Info, "Key is :- " + Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()));
                }

                if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_busbar").ToString()) == "BUSBAR1") &&
                    (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "NO_FEEDER") &&
                    ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_busbar").ToString()));
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_busbar").ToString()) == "BUSBAR2") &&
                        (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "NO_FEEDER") &&
                        ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                                Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_busbar").ToString()));
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "FEEDER1") &&
                        ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                            Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()));
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "FEEDER2") &&
                        ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()));
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "FEEDER3") &&
                        ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()));
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "FEEDER4") &&
                        ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()));
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "FEEDER5") &&
                        ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "BOTH")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()));
                }
                else if (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "PQ")
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                    "STANDALONE");
                }
                else if ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_feeder_number").ToString()) == "NO_FEEDER") &&
                     ((Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()) == "STANDALONE") ||
                     (Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_busbar").ToString()) == "NO_BUSBAR")))
                {
                    CablingInfo.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()),
                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()));
                }
                else
                {
                }

                ChannelType.Add(CablingInfo.ElementAt(channelindex).Key, Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_si_units").ToString()));
            }
            var sortedDict = from entry in CablingInfo orderby entry.Value ascending select entry;
            //For Partial Circuit
            if (sortedDict.ElementAt(0).Value == "BUSBAR1")
            {
                if (((sortedDict.ElementAt(0).Value == "BUSBAR1") || (sortedDict.ElementAt(0).Value == "PQ")) && ((sortedDict.ElementAt(1).Value == "BUSBAR1") || (sortedDict.ElementAt(1).Value == "PQ")) && ((sortedDict.ElementAt(2).Value == "BUSBAR1") || (sortedDict.ElementAt(0).Value == "PQ")))
                {

                }
                else
                {
                    CablingInfo.Add("Channel 100", "BUSBAR1_1");
                    CablingInfo.Add("Channel 101", "BUSBAR1_2");
                    ChannelType.Add("Channel 100", "VOLTS");
                    ChannelType.Add("Channel 101", "VOLTS");
                }
            }

            if (sortedDict.ElementAt(3).Value == "BUSBAR2")
            {
                if (((sortedDict.ElementAt(3).Value == "BUSBAR2") || (sortedDict.ElementAt(3).Value == "PQ")) && ((sortedDict.ElementAt(4).Value == "BUSBAR2") || (sortedDict.ElementAt(4).Value == "PQ")) && ((sortedDict.ElementAt(5).Value == "BUSBAR2") || (sortedDict.ElementAt(5).Value == "PQ")))
                {

                }
                else
                {
                    CablingInfo.Add("Channel 200", "BUSBAR2_1");
                    CablingInfo.Add("Channel 201", "BUSBAR2_2");
                    ChannelType.Add("Channel 200", "VOLTS");
                    ChannelType.Add("Channel 201", "VOLTS");
                }
            }


            foreach (var t in sortedDict)
            {
                if (ChannelType.ContainsKey(t.Key))
                {
                    sortedChannelType.Add(t.Key, ChannelType[t.Key]);
                }
            }

            TestLog.Log(LogStatus.Info, "Initialised the reference variable from classes");

            

            Assert.AreEqual("Data", Tabindex_Data_Dispatch.OpenTabIndexPage(deviceIP), "Device is up/responding");
            TestLog.Log(LogStatus.Info, "Device is up/responding");

            Assert.IsTrue(Tabindex_Data_Dispatch.Btn_Data_Click(), "Clicked on Data button on webpage");
            TestLog.Log(LogStatus.Info, "Success:-Clicked on Data button on webpage");

            Assert.IsTrue(Tabindex_Data_Dispatch.SwitchFrame_Fromdefault_Todispatch(), "Switched frame from Default to dispatch topology");
            TestLog.Log(LogStatus.Info, "Success:- Switched frame from Default to dispatch topology");

            Assert.IsTrue(Tabindex_Data_Dispatch.Item_Data_dispatch_Click(), "Clicked on dispatch under tabindex_data page");
            TestLog.Log(LogStatus.Info, "Success:- Clicked on dispatch under tabindex_data page");

            Assert.IsTrue(Tabindex_Data_Dispatch.SwitchFrame_Fromdispatch_Todata(), "Switched frame from dispatch to data");
            TestLog.Log(LogStatus.Info, "Success:- Switched frame from dispatch to data");
            System.Threading.Thread.Sleep(300000);

            for (int retrysum = 0; retrysum < 20; retrysum++)
            {
                Assert.IsTrue(Tabindex_Data_Dispatch.Item_dispatch_data_Click(), "Clicked on data");
                TestLog.Log(LogStatus.Info, "Success:- Clicked on data");

                Assert.IsTrue(Tabindex_Data_Dispatch.Item_fr_data_Click(), "Clicked on fr_data");
                TestLog.Log(LogStatus.Info, "Success:- Clicked on fr_data");

                Assert.IsTrue(Tabindex_Data_Dispatch.Item_fundamental_Click(), "Clicked on fundamental");
                TestLog.Log(LogStatus.Info, "Success:- Clicked on fundamental");

                Assert.IsTrue(Tabindex_Data_Dispatch.Item_rms_Click(), "Clicked on rms");
                TestLog.Log(LogStatus.Info, "Success:- Clicked on rms");

                for (int index = 0; index < DeviceInformation.glb_deviceType; index++)
                {
                    FR_Dispactch_RMS_Data[index] = Convert.ToDouble(Tabindex_Data_Dispatch.Get_Magnitude(index));
                }

                Assert.IsTrue(Tabindex_Data_Dispatch.SwitchToParentFrame(), "Switched frame to Refresh Button");
                TestLog.Log(LogStatus.Info, "Success:- Switched frame to Refresh Button");

                Assert.IsTrue(Tabindex_Data_Dispatch.Btn_refresh_click(), "Clicked on Refresh Button");
                TestLog.Log(LogStatus.Info, "Success:- Clicked on Refresh Button");

                Assert.IsTrue(Tabindex_Data_Dispatch.SwitchSubFrame_Todata(), "Switched frame from dispatch to data");
                TestLog.Log(LogStatus.Info, "Success:- Switched frame from dispatch to data");


                for (int RMSValue = 0; RMSValue < DeviceInformation.glb_deviceType; RMSValue++)
                {
                    FR_Data_RMS_Value.AppendLine(sortedDict.ElementAt(RMSValue).Value + " -- " + sortedDict.ElementAt(RMSValue).Key + " -- " + FR_Dispactch_RMS_Data[RMSValue]);
                }

                using (System.IO.StreamWriter file = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Cabling + ".txt"))
                {
                    file.WriteLine(FR_Data_RMS_Value);
                    file.Close();
                }

                Assert.Multiple(() =>
                {
                    for (int test = 0; test < DeviceInformation.glb_deviceType; test++)
                    {
                        if (sortedChannelType.ElementAt(test).Value == "VOLTS" && (sortedDict.ElementAt(test).Key.Length <= 10))
                        {
                            Assert.AreEqual(injectvoltage * ChannelMultiplier(sortedDict.ElementAt(test).Key), Convert.ToDouble(FR_Dispactch_RMS_Data[test]), 2.0);
                        }
                        else if ((sortedDict.ElementAt(test).Key.Length <= 10))
                        {
                            Assert.AreEqual(injectedcurrent * ChannelMultiplier(sortedDict.ElementAt(test).Key), Convert.ToDouble(FR_Dispactch_RMS_Data[test]), 0.2);
                        }
                    }
                }
                );
                
                FR_Data_RMS_Value.Clear();
            }
            sortedDict = null;
        }

        private int ChannelMultiplier(string channellabel)
        {
            int ret = -1;
            foreach (var c in Channel_TXRatio)
            {
                if (channellabel == c.Key)
                {
                  ret= c.Value;
                  break;
                }
            }
            return ret;
        }
    }
}
