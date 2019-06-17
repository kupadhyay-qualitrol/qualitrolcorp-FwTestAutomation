using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.TestSuite
{
    public class PQPDynamicCalculationMap_18Channel:BaseTestSuite
    {
        ValidatePQP_CalculationTable validatePQP_CalculationTable;
        FirmwareCablingTest CablingTest;
        string DataSetFolderPath = string.Empty;
        string PQFile_DataSet = string.Empty;

        public PQPDynamicCalculationMap_18Channel() :base(DeviceInformation.glb_DeviceIP_18Channel)
        {
            validatePQP_CalculationTable = new ValidatePQP_CalculationTable();
            CablingTest = new FirmwareCablingTest();
            DataSetFolderPath = DeviceInformation.BaseDirectoryPath + @"TestDataFiles\CablingDataSet\";
            PQFile_DataSet = DeviceInformation.BaseDirectoryPath + @"TestDataFiles\PQPDynamicParametersDataSet\";
            DeviceInformation.glb_deviceType = 18;
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
        public void pqp_Dynamic_Calculation_3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(5)]
        public void pqp_Dynamic_Calculation_3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(6)]
        public void pqp_Dynamic_Calculation_3U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(7)]
        public void pqp_Dynamic_Calculation_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I", PQFile_DataSet);
        }

        [Test, Order(8)]
        public void pqp_Dynamic_Calculation_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I", PQFile_DataSet);
        }

        [Test, Order(9)]
        public void pqp_Dynamic_Calculation_1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(10)]
        public void pqp_Dynamic_Calculation_1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(11)]
        public void pqp_Dynamic_Calculation_1U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I", PQFile_DataSet);
        }


        [Test, Order(12)]
        public void pqp_Dynamic_Calculation_2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U", PQFile_DataSet);
        }

        [Test, Order(13)]
        public void pqp_Dynamic_Calculation_3U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(14)]
        public void pqp_Dynamic_Calculation_2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I", PQFile_DataSet);
        }

        [Test, Order(15)]
        public void pqp_Dynamic_Calculation_2M3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I", PQFile_DataSet);
        }

        [Test, Order(16)]
        public void pqp_Dynamic_Calculation_2M3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(17)]
        public void pqp_Dynamic_Calculation_2M3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I", PQFile_DataSet);
        }

        //[Test, Order(18)]
        //public void pqp_Dynamic_Calculation_1U3U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I pqp Calculation Table");
        //    CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I", DataSetFolderPath);
        //    validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I",PQFile_DataSet);
        //}

        [Test, Order(19)]
        public void pqp_Dynamic_Calculation_1U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I", PQFile_DataSet);
        }

        [Test, Order(20)]
        public void pqp_Dynamic_Calculation_1U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(21)]
        public void pqp_Dynamic_Calculation_1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(22)]
        public void pqp_Dynamic_Calculation_3U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(23)]
        public void pqp_Dynamic_Calculation_3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(24)]
        public void pqp_Dynamic_Calculation_1U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I", PQFile_DataSet);
        }

        [Test, Order(25)]
        public void pqp_Dynamic_Calculation_1U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(26)]
        public void pqp_Dynamic_Calculation_1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(27)]
        public void pqp_Dynamic_Calculation_S1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get S1U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(28)]
        public void pqp_Dynamic_Calculation_S1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get S1U3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(29)]
        public void pqp_Dynamic_Calculation_S3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get S3U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(30)]
        public void pqp_Dynamic_Calculation_4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U", PQFile_DataSet);
        }

        [Test, Order(31)]
        public void pqp_Dynamic_Calculation_4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3I", PQFile_DataSet);
        }

        [Test, Order(32)]
        public void pqp_Dynamic_Calculation_4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3I3I", PQFile_DataSet);
        }

        [Test, Order(33)]
        public void pqp_Dynamic_Calculation_4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(34)]
        public void pqp_Dynamic_Calculation_4U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I", PQFile_DataSet);
        }

        [Test, Order(35)]
        public void pqp_Dynamic_Calculation_4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U4I", PQFile_DataSet);
        }

        [Test, Order(36)]
        public void pqp_Dynamic_Calculation_4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U4I4I", PQFile_DataSet);
        }

        [Test, Order(37)]
        public void pqp_Dynamic_Calculation_4U4I4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I4I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I", PQFile_DataSet);
        }

        [Test, Order(38)]
        public void pqp_Dynamic_Calculation_2M4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U", PQFile_DataSet);
        }

        [Test, Order(39)]
        public void pqp_Dynamic_Calculation_2M4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U3I", PQFile_DataSet);
        }

        [Test, Order(40)]
        public void pqp_Dynamic_Calculation_2M4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I", PQFile_DataSet);
        }

        [Test, Order(41)]
        public void pqp_Dynamic_Calculation_2M4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(42)]
        public void pqp_Dynamic_Calculation_2M4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U4I", PQFile_DataSet);
        }

        [Test, Order(43)]
        public void pqp_Dynamic_Calculation_2M4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U4I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I", PQFile_DataSet);
        }

        [Test, Order(44)]
        public void pqp_Dynamic_Calculation_4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U", PQFile_DataSet);
        }

        [Test, Order(45)]
        public void pqp_Dynamic_Calculation_4U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U3I", PQFile_DataSet);
        }

        [Test, Order(46)]
        public void pqp_Dynamic_Calculation_4U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I", PQFile_DataSet);
        }

        [Test, Order(47)]
        public void pqp_Dynamic_Calculation_4U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(48)]
        public void pqp_Dynamic_Calculation_3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U", PQFile_DataSet);
        }

        [Test, Order(49)]
        public void pqp_Dynamic_Calculation_3U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I", PQFile_DataSet);
        }

        [Test, Order(50)]
        public void pqp_Dynamic_Calculation_3U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I", PQFile_DataSet);
        }

        [Test, Order(51)]
        public void pqp_Dynamic_Calculation_3U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(52)]
        public void pqp_Dynamic_Calculation_4U3U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U4I", PQFile_DataSet);
        }

        [Test, Order(53)]
        public void pqp_Dynamic_Calculation_4U3U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I", PQFile_DataSet);
        }

        [Test, Order(54)]
        public void pqp_Dynamic_Calculation_4U3U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U4I4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I", PQFile_DataSet);
        }

        [Test, Order(55)]
        public void pqp_Dynamic_Calculation_3U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I", PQFile_DataSet);
        }

        [Test, Order(56)]
        public void pqp_Dynamic_Calculation_3U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I", PQFile_DataSet);
        }

        [Test, Order(57)]
        public void pqp_Dynamic_Calculation_4U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I", PQFile_DataSet);
        }

        [Test, Order(58)]
        public void pqp_Dynamic_Calculation_4U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I", PQFile_DataSet);
        }

        //[Test, Order(59)]
        //public void pqp_Dynamic_Calculation_1U4U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I pqp Calculation Table");
        //    CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I", DataSetFolderPath);
        //    validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I",PQFile_DataSet);
        //}

        [Test, Order(60)]
        public void pqp_Dynamic_Calculation_1U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I", PQFile_DataSet);
        }

        [Test, Order(61)]
        public void pqp_Dynamic_Calculation_1U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I", PQFile_DataSet);
        }

        [Test, Order(62)]
        public void pqp_Dynamic_Calculation_4U1U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I", PQFile_DataSet);
        }

        [Test, Order(63)]
        public void pqp_Dynamic_Calculation_4U1U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U4I4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I", PQFile_DataSet);
        }

        [Test, Order(64)]
        public void pqp_Dynamic_Calculation_1U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I", PQFile_DataSet);
        }

        [Test, Order(65)]
        public void pqp_Dynamic_Calculation_1U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I", PQFile_DataSet);
        }

        [Test, Order(66)]
        public void pqp_Dynamic_Calculation_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get NOCIRCUIT pqp Calculation Table");
           // CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            validatePQP_CalculationTable.PQP_CalculationTable(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", PQFile_DataSet);
        }
    }
}
