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

namespace CashelFirmware.NunitTests
{
    public class FR_Dispatch_Data
    {
        ResourceManager resourceManager;
        string DataSetFileNameWithPath;
        string[] FR_Dispactch_RMS_Data;
        Dictionary<string, string> CablingInfo;
        StringBuilder FR_Data_RMS_Value;
        int[] TXRatioMultiplier;
        double injectvoltage;
        double injectedcurrent;
        string[] channeltype;
        Dictionary<string, int> Channel_TXRatio;

        public FR_Dispatch_Data()
        {
            resourceManager = new ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);
            CablingInfo = new Dictionary<string, string>();
            Channel_TXRatio = new Dictionary<string, int>();
            FR_Data_RMS_Value = new StringBuilder();
            FR_Dispactch_RMS_Data = new string[18];
            injectvoltage = 50.0;
            injectedcurrent = 1.0;
            channeltype = new string[18];
            TXRatioMultiplier = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
        }

        public void Validate_FR_Dispatch_Data(IWebDriver webDriver, string deviceIP, ExtentTest TestLog, string Cabling)
        {
            Tabindex_Data_dispatch Tabindex_Data_Dispatch = new Tabindex_Data_dispatch(webDriver);
            Tabindex_Data_pmp Tabindex_Data_pmp = new Tabindex_Data_pmp(webDriver);

            TestLog.Log(LogStatus.Info, "Initialised the reference variable from classes");

            DataSetFileNameWithPath = AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\FR_DISPATCHRMS_DATASET\" + Cabling + ".xlsx";

            Assert.AreEqual("Data", Tabindex_Data_Dispatch.OpenTabIndexPage(deviceIP), "Device is up/responding");
            TestLog.Log(LogStatus.Info, "Device is up/responding");

            Assert.IsTrue(Tabindex_Data_Dispatch.Btn_Data_Click(),"Clicked on Data button on webpage");
            TestLog.Log(LogStatus.Info, "Success:-Clicked on Data button on webpage");

            Assert.IsTrue(Tabindex_Data_Dispatch.SwitchFrame_Fromdefault_Todispatch(), "Switched frame from Default to dispatch topology");
            TestLog.Log(LogStatus.Info, "Success:- Switched frame from Default to dispatch topology");

            Assert.IsTrue(Tabindex_Data_Dispatch.Item_Data_dispatch_Click(), "Clicked on dispatch under tabindex_data page");
            TestLog.Log(LogStatus.Info, "Success:- Clicked on dispatch under tabindex_data page");

            Assert.IsTrue(Tabindex_Data_Dispatch.SwitchFrame_Fromdispatch_Todata(), "Switched frame from dispatch to data");
            TestLog.Log(LogStatus.Info, "Success:- Switched frame from dispatch to data");

            Assert.IsTrue(Tabindex_Data_Dispatch.Item_dispatch_data_Click(), "Clicked on data");
            TestLog.Log(LogStatus.Info, "Success:- Clicked on data");

            Assert.IsTrue(Tabindex_Data_Dispatch.Item_fr_data_Click(), "Clicked on fr_data");
            TestLog.Log(LogStatus.Info, "Success:- Clicked on fr_data");

            Assert.IsTrue(Tabindex_Data_Dispatch.Item_fundamental_Click(), "Clicked on fundamental");
            TestLog.Log(LogStatus.Info, "Success:- Clicked on fundamental");

            Assert.IsTrue(Tabindex_Data_Dispatch.Item_rms_Click(), "Clicked on rms");
            TestLog.Log(LogStatus.Info, "Success:- Clicked on rms");

            for (int index = 0; index < 18; index++)
            {
                FR_Dispactch_RMS_Data[index] = Tabindex_Data_Dispatch.Get_Magnitude(index);
            }
            CablingInfo.Clear();
            Channel_TXRatio.Clear();
            for (int channelindex = 0; channelindex < 18; channelindex++)
            {
                channeltype[channelindex] = Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_si_units").ToString());
                if (!Channel_TXRatio.ContainsKey(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString())))
                {
                    Channel_TXRatio.Add(Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()), TXRatioMultiplier[channelindex]);
                }
                else
                {
                    TestLog.Log(LogStatus.Info,"Key is :- "+ Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_label").ToString()));
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
                    Read_WriteExcel.ReadExcel(DataSetFileNameWithPath, resourceManager.GetString("EXCELDATA_SHEETNAME_CABLING").ToString(), channelindex, resourceManager.GetString("EXCELDATA_CABLING_usage").ToString()));
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
            }

            //For Partial Circuit
                if (((CablingInfo.ElementAt(0).Value == "BUSBAR1")|| (CablingInfo.ElementAt(0).Value == "PQ")) && ((CablingInfo.ElementAt(1).Value == "BUSBAR1")|| (CablingInfo.ElementAt(1).Value == "PQ")) && ((CablingInfo.ElementAt(2).Value == "BUSBAR1")|| (CablingInfo.ElementAt(0).Value == "PQ")))
                {

                }
                else
                {
                    CablingInfo.Add("Channel 100", "BUSBAR1_1");
                    CablingInfo.Add("Channel 101","BUSBAR1_2");
                }
            if (CablingInfo.ElementAt(3).Key == "BUSBAR2")
            {
                if (((CablingInfo.ElementAt(3).Key == "BUSBAR2") || (CablingInfo.ElementAt(3).Value == "PQ")) && ((CablingInfo.ElementAt(4).Key == "BUSBAR2") || (CablingInfo.ElementAt(4).Value == "PQ")) && ((CablingInfo.ElementAt(5).Key == "BUSBAR2") || (CablingInfo.ElementAt(5).Value == "PQ")))
                {

                }
                else
                {
                    CablingInfo.Add("Channel 200", "BUSBAR2_1");
                    CablingInfo.Add("Channel 201", "BUSBAR2_2");
                }
            }
            var sortedDict = from entry in CablingInfo orderby entry.Value ascending select entry;

            for (int RMSValue = 0; RMSValue < 18; RMSValue++)
            {   
                FR_Data_RMS_Value.AppendLine(sortedDict.ElementAt(RMSValue).Value+" -- "+ sortedDict.ElementAt(RMSValue).Key+ " -- " + FR_Dispactch_RMS_Data[RMSValue]);
            }

            using (System.IO.StreamWriter file = new System.IO.StreamWriter(AppDomain.CurrentDomain.BaseDirectory + Cabling + ".txt"))
            {
                file.WriteLine(FR_Data_RMS_Value);
                file.Close();
            }

            Assert.Multiple(() =>
            {
                for (int test = 0; test < 18; test++)
                {
                    if (channeltype[test] == "VOLTS" && (sortedDict.ElementAt(test).Key.Length<=10))
                    {
                        Assert.AreEqual(injectvoltage * ChannelMultiplier(sortedDict.ElementAt(test).Key), Convert.ToDouble(FR_Dispactch_RMS_Data[test]), 1.0);
                    }
                    else  if((sortedDict.ElementAt(test).Key.Length <= 10))
                    {
                        Assert.AreEqual(injectedcurrent * ChannelMultiplier(sortedDict.ElementAt(test).Key), Convert.ToDouble(FR_Dispactch_RMS_Data[test]), 0.1);
                    }
                }      
            }
                );             
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
