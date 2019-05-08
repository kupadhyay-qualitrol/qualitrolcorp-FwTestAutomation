﻿using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.TestSuite
{
    public class FR_Dispatch_RMSDataSuite_18Channel: BaseTestSuite
    {
        public FR_Dispatch_Data dispatch_RMSData;
        public FirmwareCablingTest firmwareCablingTest;
        int[] TXRatioMultiplier;
        int[] NoTXRatio;
        string DataSetFolderPath = string.Empty;

        public FR_Dispatch_RMSDataSuite_18Channel() : base(DeviceInformation.glb_DeviceIP_18Channel)
        {
            dispatch_RMSData = new FR_Dispatch_Data();
            firmwareCablingTest = new FirmwareCablingTest();
            TXRatioMultiplier = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            NoTXRatio = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1 };
            DataSetFolderPath = DeviceInformation.BaseDirectoryPath + @"\TestDataFiles\FR_DISPATCHRMS_DATASET\";
        }
         [Test,Order(1)]
        public void FR_DIS_RMSDATA_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U Cabling");
            firmwareCablingTest.SetTXRationum(deviceIP, webdriver, InfovarStartTest, NoTXRatio);
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath, true);
            firmwareCablingTest.SetTXRationum(deviceIP, webdriver, InfovarStartTest, TXRatioMultiplier);
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U", DataSetFolderPath);
        }

        [Test, Order(2)]
        public void FR_DIS_RMSDATA_3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I",DataSetFolderPath);
        }

        [Test, Order(3)]
        public void FR_DIS_RMSDATA_3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I3I",DataSetFolderPath);
        }

        [Test, Order(4)]
        public void FR_DIS_RMSDATA_3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(5)]
        public void FR_DIS_RMSDATA_1U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I",DataSetFolderPath);
        }

        [Test, Order(6)]
        public void FR_DIS_RMSDATA_1U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(7)]
        public void FR_DIS_RMSDATA_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I",DataSetFolderPath);
        }

        [Test, Order(8)]
        public void FR_DIS_RMSDATA_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I3I",DataSetFolderPath);
        }

        [Test, Order(9)]
        public void FR_DIS_RMSDATA_1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I",DataSetFolderPath);
        }

        //[Test, Order(10)]
        //public void FR_DIS_RMSDATA_1U3U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3U3I Cabling");
        //    firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I", DataSetFolderPath,false);
        //    dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3U3I",DataSetFolderPath);
        //}

        [Test, Order(11)]
        public void FR_DIS_RMSDATA_1U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I",DataSetFolderPath);
        }

        [Test, Order(12)]
        public void FR_DIS_RMSDATA_1U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I",DataSetFolderPath);
        }

        //[Test, Order(13)]
        //public void FR_DIS_RMSDATA_1U4U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I Cabling");
        //    firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I", DataSetFolderPath,false);
        //    dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I",DataSetFolderPath);
        //}

        [Test, Order(14)]
        public void FR_DIS_RMSDATA_1U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I",DataSetFolderPath);
        }

        [Test, Order(15)]
        public void FR_DIS_RMSDATA_1U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(16)]
        public void FR_DIS_RMSDATA_1U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I",DataSetFolderPath);
        }

        [Test, Order(17)]
        public void FR_DIS_RMSDATA_1U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I",DataSetFolderPath);
        }

        [Test, Order(18)]
        public void FR_DIS_RMSDATA_2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U",DataSetFolderPath);
        }

        [Test, Order(19)]
        public void FR_DIS_RMSDATA_2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U3I",DataSetFolderPath);
        }

        [Test, Order(20)]
        public void FR_DIS_RMSDATA_2M3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I",DataSetFolderPath);
        }

        [Test, Order(21)]
        public void FR_DIS_RMSDATA_2M3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(22)]
        public void FR_DIS_RMSDATA_3U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(23)]
        public void FR_DIS_RMSDATA_3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U",DataSetFolderPath);
        }

        [Test, Order(24)]
        public void FR_DIS_RMSDATA_3U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I",DataSetFolderPath);
        }

        [Test, Order(25)]
        public void FR_DIS_RMSDATA_3U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I",DataSetFolderPath);
        }

        [Test, Order(26)]
        public void FR_DIS_RMSDATA_3U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(27)]
        public void FR_DIS_RMSDATA_3U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I",DataSetFolderPath);
        }

        [Test, Order(28)]
        public void FR_DIS_RMSDATA_3U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I",DataSetFolderPath);
        }

        [Test, Order(29)]
        public void FR_DIS_RMSDATA_4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U",DataSetFolderPath);
        }

        [Test, Order(30)]
        public void FR_DIS_RMSDATA_4U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I",DataSetFolderPath);
        }

        [Test, Order(31)]
        public void FR_DIS_RMSDATA_4U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(32)]
        public void FR_DIS_RMSDATA_4U1U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U1U4I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I",DataSetFolderPath);
        }

        [Test, Order(33)]
        public void FR_DIS_RMSDATA_4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I",DataSetFolderPath);
        }

        [Test, Order(34)]
        public void FR_DIS_RMSDATA_4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I3I",DataSetFolderPath);
        }

        [Test, Order(35)]
        public void FR_DIS_RMSDATA_4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(36)]
        public void FR_DIS_RMSDATA_4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U",DataSetFolderPath);
        }

        [Test, Order(37)]
        public void FR_DIS_RMSDATA_4U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U3I",DataSetFolderPath);
        }

        [Test, Order(38)]
        public void FR_DIS_RMSDATA_4U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I",DataSetFolderPath);
        }

        [Test, Order(39)]
        public void FR_DIS_RMSDATA_4U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I",DataSetFolderPath);
        }

        [Test, Order(40)]
        public void FR_DIS_RMSDATA_4U3U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U4I",DataSetFolderPath);
        }

        [Test, Order(41)]
        public void FR_DIS_RMSDATA_4U3U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U4I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I",DataSetFolderPath);
        }

        [Test, Order(42)]
        public void FR_DIS_RMSDATA_4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U4I",DataSetFolderPath);
        }

        [Test, Order(43)]
        public void FR_DIS_RMSDATA_4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U4I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U4I4I",DataSetFolderPath);
        }

        [Test, Order(44)]
        public void FR_DIS_RMSDATA_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for NOCIRCUIT Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT",DataSetFolderPath);
        }

        [Test, Order(45)]
        public void FR_DIS_RMSDATA_3U3I1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I3I",DataSetFolderPath);
        }

        [Test, Order(46)]
        public void FR_DIS_RMSDATA_3U3I1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I1U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I", DataSetFolderPath,false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I",DataSetFolderPath);
        }

        [Test, Order(47)]
        public void FR_DIS_RMSDATA_4U3I1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I1U3I3I", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I1U3I3I",DataSetFolderPath);
        }

    }
}