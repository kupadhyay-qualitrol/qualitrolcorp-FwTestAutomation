using CashelFirmware.GlobalVariables;
using CashelFirmware.Reporting;
using NUnit.Framework;
using CashelFirmware.NunitTests;

namespace CashelFirmware.TestSuite
{
    public class CablingSuite_9Channel : BaseTestSuite
    {
        string DataSetFolderPath = string.Empty;
        public FirmwareCablingTest CablingTest;
        public CablingSuite_9Channel() : base(DeviceInformation.glb_DeviceIP_9Channel)
        {
            CablingTest = new FirmwareCablingTest();
            DataSetFolderPath = DeviceInformation.BaseDirectoryPath+ @"TestDataFiles\CablingDataSet_9Channel\";
            DeviceInformation.glb_deviceType = 9;
        }

        [Test, Order(1)]
        public void TestCabling3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U", DataSetFolderPath);
        } 

        [Test, Order(2)]
        public void TestCabling3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I", DataSetFolderPath);
        }

        [Test, Order(3)]
        public void TestCabling3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I", DataSetFolderPath);
        }

        [Test, Order(4)]
        public void TestCabling1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", DataSetFolderPath);
        }

        [Test, Order(5)]
        public void TestCabling1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", DataSetFolderPath);
        }

        [Test, Order(6)]
        public void TestCabling2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U", DataSetFolderPath);
        }

        [Test, Order(7)]
        public void TestCabling2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I", DataSetFolderPath);
        }

        [Test, Order(8)]
        public void TestCabling1U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I", DataSetFolderPath);
        }

        [Test, Order(9)]
        public void TestCabling4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U", DataSetFolderPath);
        }

        [Test, Order(10)]
        public void TestCabling4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I", DataSetFolderPath);
        }

        [Test, Order(11)]
        public void TestCabling4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I", DataSetFolderPath);
        }

        [Test, Order(12)]
        public void TestCabling2M4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U", DataSetFolderPath);
        }

        [Test, Order(13)]
        public void TestCabling4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U", DataSetFolderPath);
        }

        [Test, Order(14)]
        public void TestCabling3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U", DataSetFolderPath);
        }

        [Test, Order(15)]
        public void TestCablingNOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test NOCIRCUIT Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
        }
    }
}
