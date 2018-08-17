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
        [Test,Order(1)]
        public void pqp_Dynamic_Calculation_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Get 3U pqp Calculation Table");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U");
            PQP_CalculationTable.GetPQPDynamicTable(webdriver, deviceIP, InfovarStartTest, "3U");            
        }
    }
}
