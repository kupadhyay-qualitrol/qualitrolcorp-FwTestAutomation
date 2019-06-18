using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.TestSuite
{
    public class PQPDynamicCalculationMap_9Channel : BaseTestSuite
    {
        ValidatePQP_CalculationTable validatePQP_CalculationTable;
        FirmwareCablingTest CablingTest;
        string DataSetFolderPath = string.Empty;
        string PQFile_DataSet = string.Empty;

        public PQPDynamicCalculationMap_9Channel() : base(DeviceInformation.glb_DeviceIP_9Channel)
        {
            validatePQP_CalculationTable = new ValidatePQP_CalculationTable();
            CablingTest = new FirmwareCablingTest();
            DataSetFolderPath = System.IO.Directory.GetParent(DeviceInformation.BaseDirectoryPath).ToString() + @"\TestDataFiles\CablingDataSet_9Channel\";
            PQFile_DataSet = System.IO.Directory.GetParent(DeviceInformation.BaseDirectoryPath).ToString() + @"\TestDataFiles\PQPDynamicParametersDataSet_9Channel\";
            DeviceInformation.glb_deviceType = 9;
        }

        [Test, Order(1)]
        public void pqp_Dynamic_Calculation_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U", PQFile_DataSet);
        }

        [Test, Order(2)]
        public void pqp_Dynamic_Calculation_3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U3I", PQFile_DataSet);
        }

        [Test, Order(3)]
        public void pqp_Dynamic_Calculation_3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I", PQFile_DataSet);
        }

        [Test, Order(4)]
        public void pqp_Dynamic_Calculation_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I", PQFile_DataSet);
        }

        [Test, Order(5)]
        public void pqp_Dynamic_Calculation_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I", PQFile_DataSet);
        }

        [Test, Order(6)]
        public void pqp_Dynamic_Calculation_2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U", PQFile_DataSet);
        }

        [Test, Order(7)]
        public void pqp_Dynamic_Calculation_2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I", PQFile_DataSet);
        }

        [Test, Order(8)]
        public void pqp_Dynamic_Calculation_1U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I", PQFile_DataSet);
        }

        [Test, Order(9)]
        public void pqp_Dynamic_Calculation_4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U", PQFile_DataSet);
        }

        [Test, Order(10)]
        public void pqp_Dynamic_Calculation_4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3I", PQFile_DataSet);
        }

        [Test, Order(11)]
        public void pqp_Dynamic_Calculation_4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U4I", PQFile_DataSet);
        }

        [Test, Order(12)]
        public void pqp_Dynamic_Calculation_2M4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U", PQFile_DataSet);
        }

        [Test, Order(13)]
        public void pqp_Dynamic_Calculation_4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U", PQFile_DataSet);
        }

        [Test, Order(14)]
        public void pqp_Dynamic_Calculation_3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U", PQFile_DataSet);
        }

        [Test, Order(15)]
        public void pqp_Dynamic_Calculation_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get NOCIRCUIT pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", PQFile_DataSet);
        }
    }
}
