using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;

namespace CashelFirmware.TestSuite
{
    public class PQPDynamicCalculationMap:BaseTestSuite
    {
        GetPQP_CalculationTable PQP_CalculationTable;
        FirmwareCablingTest CablingTest;

        public PQPDynamicCalculationMap()
        {
            PQP_CalculationTable = new GetPQP_CalculationTable();
            CablingTest = new FirmwareCablingTest();
        }
        [Test,Order(68)]
        public void pqp_Dynamic_Calculation_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U");            
        }

        [Test, Order(69)]
        public void pqp_Dynamic_Calculation_3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U3I");
        }

        [Test, Order(70)]
        public void pqp_Dynamic_Calculation_3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I");
        }

        [Test, Order(71)]
        public void pqp_Dynamic_Calculation_3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I");
        }

        [Test, Order(72)]
        public void pqp_Dynamic_Calculation_3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I");
        }

        [Test, Order(73)]
        public void pqp_Dynamic_Calculation_3U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3I3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I");
        }

        [Test, Order(74)]
        public void pqp_Dynamic_Calculation_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3I");
        }

        [Test, Order(75)]
        public void pqp_Dynamic_Calculation_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I");
        }

        [Test, Order(76)]
        public void pqp_Dynamic_Calculation_1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I");
        }

        [Test, Order(77)]
        public void pqp_Dynamic_Calculation_1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I");
        }

        [Test, Order(78)]
        public void pqp_Dynamic_Calculation_1U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3I3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I");
        }


        [Test, Order(79)]
        public void pqp_Dynamic_Calculation_2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M3U");
        }

        [Test, Order(80)]
        public void pqp_Dynamic_Calculation_3U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I");
        }

        [Test, Order(81)]
        public void pqp_Dynamic_Calculation_2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I");
        }

        [Test, Order(82)]
        public void pqp_Dynamic_Calculation_2M3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I");
        }

        [Test, Order(83)]
        public void pqp_Dynamic_Calculation_2M3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I");
        }

        [Test, Order(84)]
        public void pqp_Dynamic_Calculation_2M3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I");
        }

        [Test, Order(85)]
        public void pqp_Dynamic_Calculation_1U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I");
        }

        [Test, Order(86)]
        public void pqp_Dynamic_Calculation_1U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I");
        }

        [Test, Order(87)]
        public void pqp_Dynamic_Calculation_1U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I");
        }

        [Test, Order(88)]
        public void pqp_Dynamic_Calculation_1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I");
        }

        [Test, Order(89)]
        public void pqp_Dynamic_Calculation_3U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I");
        }

        [Test, Order(90)]
        public void pqp_Dynamic_Calculation_3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I");
        }

        [Test, Order(91)]
        public void pqp_Dynamic_Calculation_1U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I");
        }

        [Test, Order(92)]
        public void pqp_Dynamic_Calculation_1U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I");
        }

        [Test, Order(93)]
        public void pqp_Dynamic_Calculation_1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I");
        }

        [Test, Order(94)]
        public void pqp_Dynamic_Calculation_S1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get S1U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I");
        }

        [Test, Order(95)]
        public void pqp_Dynamic_Calculation_S1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get S1U3U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I");
        }

        [Test, Order(96)]
        public void pqp_Dynamic_Calculation_S3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get S3U1U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I");
        }

        [Test, Order(97)]
        public void pqp_Dynamic_Calculation_4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U");
        }

        [Test, Order(98)]
        public void pqp_Dynamic_Calculation_4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3I");
        }

        [Test, Order(99)]
        public void pqp_Dynamic_Calculation_4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3I3I");
        }

        [Test, Order(100)]
        public void pqp_Dynamic_Calculation_4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I");
        }

        [Test, Order(101)]
        public void pqp_Dynamic_Calculation_4U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3I3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I");
        }

        [Test, Order(102)]
        public void pqp_Dynamic_Calculation_4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U4I");
        }

        [Test, Order(103)]
        public void pqp_Dynamic_Calculation_4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U4I4I");
        }

        [Test, Order(104)]
        public void pqp_Dynamic_Calculation_4U4I4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U4I4I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I");
        }

        [Test, Order(105)]
        public void pqp_Dynamic_Calculation_2M4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M4U");
        }

        [Test, Order(106)]
        public void pqp_Dynamic_Calculation_2M4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M4U3I");
        }

        [Test, Order(107)]
        public void pqp_Dynamic_Calculation_2M4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I");
        }

        [Test, Order(108)]
        public void pqp_Dynamic_Calculation_2M4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I");
        }

        [Test, Order(109)]
        public void pqp_Dynamic_Calculation_2M4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M4U4I");
        }

        [Test, Order(110)]
        public void pqp_Dynamic_Calculation_2M4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 2M4U4I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I");
        }

        [Test, Order(111)]
        public void pqp_Dynamic_Calculation_4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U");
        }

        [Test, Order(112)]
        public void pqp_Dynamic_Calculation_4U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U3I");
        }

        [Test, Order(113)]
        public void pqp_Dynamic_Calculation_4U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I");
        }

        [Test, Order(114)]
        public void pqp_Dynamic_Calculation_4U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I");
        }

        [Test, Order(115)]
        public void pqp_Dynamic_Calculation_3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U4U");
        }

        [Test, Order(116)]
        public void pqp_Dynamic_Calculation_3U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I");
        }

        [Test, Order(117)]
        public void pqp_Dynamic_Calculation_3U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I");
        }

        [Test, Order(118)]
        public void pqp_Dynamic_Calculation_3U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I");
        }

        [Test, Order(119)]
        public void pqp_Dynamic_Calculation_4U3U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U4I");
        }

        [Test, Order(120)]
        public void pqp_Dynamic_Calculation_4U3U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I");
        }

        [Test, Order(121)]
        public void pqp_Dynamic_Calculation_4U3U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U3U4I4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I");
        }

        [Test, Order(122)]
        public void pqp_Dynamic_Calculation_3U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I");
        }

        [Test, Order(123)]
        public void pqp_Dynamic_Calculation_3U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U4U3I3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I");
        }

        [Test, Order(124)]
        public void pqp_Dynamic_Calculation_4U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I");
        }

        [Test, Order(125)]
        public void pqp_Dynamic_Calculation_4U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I");
        }

        [Test, Order(126)]
        public void pqp_Dynamic_Calculation_1U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I");
        }

        [Test, Order(127)]
        public void pqp_Dynamic_Calculation_1U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I");
        }

        [Test, Order(128)]
        public void pqp_Dynamic_Calculation_1U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I3I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I");
        }

        [Test, Order(129)]
        public void pqp_Dynamic_Calculation_4U1U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I");
        }

        [Test, Order(130)]
        public void pqp_Dynamic_Calculation_4U1U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 4U1U4I4I3I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I");
        }

        [Test, Order(131)]
        public void pqp_Dynamic_Calculation_1U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I");
        }

        [Test, Order(132)]
        public void pqp_Dynamic_Calculation_1U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 1U4U3I3I4I pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I");
        }

        [Test, Order(133)]
        public void pqp_Dynamic_Calculation_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get NOCIRCUIT pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
        }
    }
}
