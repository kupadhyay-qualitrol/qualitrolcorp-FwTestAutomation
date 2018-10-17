using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;

namespace CashelFirmware.TestSuite
{
    public class FR_Dispatch_RMSDataSuite: BaseTestSuite
    {
        public FR_Dispatch_Data dispatch_RMSData;
        public FirmwareCablingTest firmwareCablingTest;
        int[] TXRatioMultiplier;
        int[] NoTXRatio;

        public FR_Dispatch_RMSDataSuite()
        {
            dispatch_RMSData = new FR_Dispatch_Data();
            firmwareCablingTest = new FirmwareCablingTest();
            TXRatioMultiplier = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18 };
            NoTXRatio = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1 ,1, 1, 1, 1, 1, 1, 1, 1, 1 };
        }
         [Test,Order(1)]
        public void FR_DIS_RMSDATA_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U Cabling");
            firmwareCablingTest.SetTXRationum(deviceIP, webdriver, InfovarStartTest, NoTXRatio);
           // firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT",true, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            firmwareCablingTest.SetTXRationum(deviceIP, webdriver, InfovarStartTest, TXRatioMultiplier);
           // firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
           // dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U");
        }

        [Test, Order(2)]
        public void FR_DIS_RMSDATA_3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I");
        }

        [Test, Order(3)]
        public void FR_DIS_RMSDATA_3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I3I");
        }

        [Test, Order(4)]
        public void FR_DIS_RMSDATA_3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I", false,  @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I");
        }

        [Test, Order(5)]
        public void FR_DIS_RMSDATA_1U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I");
        }

        [Test, Order(6)]
        public void FR_DIS_RMSDATA_1U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I");
        }

        [Test, Order(7)]
        public void FR_DIS_RMSDATA_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I");
        }

        [Test, Order(8)]
        public void FR_DIS_RMSDATA_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I3I");
        }

        [Test, Order(9)]
        public void FR_DIS_RMSDATA_1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I");
        }

        //[Test, Order(10)]
        //public void FR_DIS_RMSDATA_1U3U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3U3I Cabling");
        //    firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
        //    dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3U3I");
        //}

        [Test, Order(11)]
        public void FR_DIS_RMSDATA_1U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I");
        }

        [Test, Order(12)]
        public void FR_DIS_RMSDATA_1U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I");
        }

        //[Test, Order(13)]
        //public void FR_DIS_RMSDATA_1U4U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I Cabling");
        //    firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
        //    dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I");
        //}

        [Test, Order(14)]
        public void FR_DIS_RMSDATA_1U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I");
        }

        [Test, Order(15)]
        public void FR_DIS_RMSDATA_1U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I");
        }

        [Test, Order(16)]
        public void FR_DIS_RMSDATA_1U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I");
        }

        [Test, Order(17)]
        public void FR_DIS_RMSDATA_1U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U4U3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I");
        }

        [Test, Order(18)]
        public void FR_DIS_RMSDATA_2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U");
        }

        [Test, Order(19)]
        public void FR_DIS_RMSDATA_2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U3I");
        }

        [Test, Order(20)]
        public void FR_DIS_RMSDATA_2M3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I");
        }

        [Test, Order(21)]
        public void FR_DIS_RMSDATA_2M3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 2M3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I");
        }

        [Test, Order(22)]
        public void FR_DIS_RMSDATA_3U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I");
        }

        [Test, Order(23)]
        public void FR_DIS_RMSDATA_3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U");
        }

        [Test, Order(24)]
        public void FR_DIS_RMSDATA_3U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I");
        }

        [Test, Order(25)]
        public void FR_DIS_RMSDATA_3U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I");
        }

        [Test, Order(26)]
        public void FR_DIS_RMSDATA_3U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I");
        }

        [Test, Order(27)]
        public void FR_DIS_RMSDATA_3U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I");
        }

        [Test, Order(28)]
        public void FR_DIS_RMSDATA_3U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U4U3I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I");
        }

        [Test, Order(29)]
        public void FR_DIS_RMSDATA_4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U");
        }

        [Test, Order(30)]
        public void FR_DIS_RMSDATA_4U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I");
        }

        [Test, Order(31)]
        public void FR_DIS_RMSDATA_4U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U1U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I");
        }

        [Test, Order(32)]
        public void FR_DIS_RMSDATA_4U1U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U1U4I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I");
        }

        [Test, Order(33)]
        public void FR_DIS_RMSDATA_4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I");
        }

        [Test, Order(34)]
        public void FR_DIS_RMSDATA_4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I3I");
        }

        [Test, Order(35)]
        public void FR_DIS_RMSDATA_4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I");
        }

        [Test, Order(36)]
        public void FR_DIS_RMSDATA_4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U");
        }

        [Test, Order(37)]
        public void FR_DIS_RMSDATA_4U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U3I");
        }

        [Test, Order(38)]
        public void FR_DIS_RMSDATA_4U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I");
        }

        [Test, Order(39)]
        public void FR_DIS_RMSDATA_4U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U3I3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I");
        }

        [Test, Order(40)]
        public void FR_DIS_RMSDATA_4U3U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U4I");
        }

        [Test, Order(41)]
        public void FR_DIS_RMSDATA_4U3U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3U4I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I");
        }

        [Test, Order(42)]
        public void FR_DIS_RMSDATA_4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U4I");
        }

        [Test, Order(43)]
        public void FR_DIS_RMSDATA_4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U4I4I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U4I4I");
        }

        [Test, Order(44)]
        public void FR_DIS_RMSDATA_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for NOCIRCUIT Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
        }

        [Test, Order(45)]
        public void FR_DIS_RMSDATA_3U3I1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I3I");
        }

        [Test, Order(46)]
        public void FR_DIS_RMSDATA_3U3I1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I1U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I1U3I");
        }

        [Test, Order(47)]
        public void FR_DIS_RMSDATA_4U3I1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 4U3I1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I1U3I3I", false, @"\TestDataFiles\FR_DISPATCHRMS_DATASET\");
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "4U3I1U3I3I");
        }

    }
}
