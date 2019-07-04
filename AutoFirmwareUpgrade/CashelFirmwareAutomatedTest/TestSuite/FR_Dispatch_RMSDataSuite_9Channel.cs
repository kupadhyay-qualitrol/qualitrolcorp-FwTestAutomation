using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.TestSuite
{
    public class FR_Dispatch_RMSDataSuite_9Channel : BaseTestSuite
    {
        public FR_Dispatch_Data dispatch_RMSData;
        public FirmwareCablingTest firmwareCablingTest;
        int[] TXRatioMultiplier;
        int[] NoTXRatio;
        string DataSetFolderPath = string.Empty;

        public FR_Dispatch_RMSDataSuite_9Channel() : base(DeviceInformation.glb_DeviceIP_9Channel)
        {
            dispatch_RMSData = new FR_Dispatch_Data();
            firmwareCablingTest = new FirmwareCablingTest();
            TXRatioMultiplier = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9};
            DeviceInformation.glb_deviceType = 9;
            NoTXRatio = new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1};
            DataSetFolderPath = System.IO.Directory.GetParent(DeviceInformation.BaseDirectoryPath).ToString() + @"\TestDataFiles\FR_DISPATCHRMS_DATASET_9Channel\";
            DeviceInformation.glb_deviceType = 9;
        }

        [Test, Order(1)]
        public void FR_DIS_RMSDATA_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U Cabling");
            firmwareCablingTest.SetTXRationum(deviceIP, webdriver, InfovarStartTest, NoTXRatio);
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath, true);
            firmwareCablingTest.SetTXRationum(deviceIP, webdriver, InfovarStartTest, TXRatioMultiplier);
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U",DataSetFolderPath);
        }

        [Test, Order(2)]
        public void FR_DIS_RMSDATA_3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I",DataSetFolderPath);
        }

        [Test, Order(3)]
        public void FR_DIS_RMSDATA_3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 3U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "3U3I3I",DataSetFolderPath);
        }

        [Test, Order(7)]
        public void FR_DIS_RMSDATA_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I",DataSetFolderPath);
        }

        [Test, Order(8)]
        public void FR_DIS_RMSDATA_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for 1U3I3I Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "1U3I3I",DataSetFolderPath);
        }

        [Test, Order(9)]
        public void FR_DIS_RMSDATA_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate FR_DISPATCH_RMSDATA for NOCIRCUIT Cabling");
            firmwareCablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath, false);
            dispatch_RMSData.Validate_FR_Dispatch_Data(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
        }
    }
}
