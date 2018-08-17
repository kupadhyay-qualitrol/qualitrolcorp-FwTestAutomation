/*This file contains all the test cases as per Nunit Framework for Cabling
 */

using NUnit.Framework;
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;

namespace CashelFirmware.TestSuite
{
    public class CablingSuite:BaseTestSuite
    {
        public FirmwareCablingTest CablingTest;

        public CablingSuite()
        {
            CablingTest = new FirmwareCablingTest();
        }

        [Test, Order(1)]
        public void TestCabling3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U");
        }

        [Test, Order(2)]
        public void TestCabling3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I");
        }

        [Test, Order(3)]
        public void TestCabling3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I");
        }

        [Test, Order(4)]
        public void TestCabling3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I");
        }

        [Test, Order(5)]
        public void TestCabling3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I");
        }

        [Test, Order(6)]
        public void TestCabling3U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3I3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I");
        }

        [Test, Order(7)]
        public void TestCabling1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I");
        }

        [Test, Order(8)]
        public void TestCabling1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I");
        }

        [Test, Order(9)]
        public void TestCabling1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I");
        }

        [Test, Order(10)]
        public void TestCabling1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I");
        }

        [Test, Order(11)]
        public void TestCabling1U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3I3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I");
        }


        [Test, Order(12)]
        public void TestCabling2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U");
        }

        [Test, Order(13)]
        public void TestCabling3U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I");
        }

        [Test, Order(14)]
        public void TestCabling2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I");
        }

        [Test, Order(15)]
        public void TestCabling2M3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I");
        }

        [Test, Order(16)]
        public void TestCabling2M3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I");
        }

        [Test, Order(17)]
        public void TestCabling2M3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I");
        }

        [Test, Order(18)]
        public void TestCabling1U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I");
        }

        [Test, Order(19)]
        public void TestCabling1U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I");
        }

        [Test, Order(20)]
        public void TestCabling1U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I");
        }

        [Test, Order(21)]
        public void TestCabling1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I");
        }

        [Test, Order(22)]
        public void TestCabling3U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I");
        }

        [Test, Order(23)]
        public void TestCabling3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I");
        }

        [Test, Order(24)]
        public void TestCabling1U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I");
        }

        [Test, Order(25)]
        public void TestCabling1U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I");
        }

        [Test, Order(26)]
        public void TestCabling1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I");
        }

        [Test, Order(27)]
        public void TestCablingS1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test S1U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I");
        }

        [Test, Order(28)]
        public void TestCablingS1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test S1U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I");
        }

        [Test, Order(29)]
        public void TestCablingS3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test S3U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I");
        }

        [Test, Order(30)]
        public void TestCabling4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U");
        }

        [Test, Order(31)]
        public void TestCabling4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I");
        }

        [Test, Order(32)]
        public void TestCabling4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I");
        }

        [Test, Order(33)]
        public void TestCabling4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I");
        }

        [Test, Order(34)]
        public void TestCabling4U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I");
        }

        [Test, Order(35)]
        public void TestCabling4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I");
        }

        [Test, Order(36)]
        public void TestCabling4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I");
        }

        [Test, Order(38)]
        public void TestCabling4U4I4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U4I4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I");
        }

        [Test, Order(39)]
        public void TestCabling2M4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U");
        }

        [Test, Order(40)]
        public void TestCabling2M4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I");
        }

        [Test, Order(41)]
        public void TestCabling2M4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I");
        }

        [Test, Order(42)]
        public void TestCabling2M4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I");
        }

        [Test, Order(43)]
        public void TestCabling2M4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I");
        }

        [Test, Order(44)]
        public void TestCabling2M4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 2M4U4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I");
        }

        [Test, Order(45)]
        public void TestCabling4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U");
        }

        [Test, Order(46)]
        public void TestCabling4U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I");
        }

        [Test, Order(47)]
        public void TestCabling4U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I");
        }

        [Test, Order(48)]
        public void TestCabling4U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I");
        }

        [Test, Order(49)]
        public void TestCabling3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U");
        }

        [Test, Order(50)]
        public void TestCabling3U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I");
        }

        [Test, Order(51)]
        public void TestCabling3U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I");
        }

        [Test, Order(52)]
        public void TestCabling3U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I");
        }

        [Test, Order(53)]
        public void TestCabling4U3U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I");
        }

        [Test, Order(54)]
        public void TestCabling4U3U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I");
        }

        [Test, Order(55)]
        public void TestCabling4U3U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U3U4I4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I");
        }

        [Test, Order(56)]
        public void TestCabling3U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I");
        }

        [Test, Order(57)]
        public void TestCabling3U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 3U4U3I3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I");
        }

        [Test, Order(58)]
        public void TestCabling4U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I");
        }

        [Test, Order(59)]
        public void TestCabling4U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I");
        }

        [Test, Order(60)]
        public void TestCabling1U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I");
        }

        [Test, Order(61)]
        public void TestCabling1U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I");
        }

        [Test, Order(62)]
        public void TestCabling1U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I");
        }

        [Test, Order(63)]
        public void TestCabling4U1U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U1U4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I");
        }

        [Test, Order(64)]
        public void TestCabling4U1U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 4U1U4I4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I");
        }

        [Test, Order(65)]
        public void TestCabling1U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U4U3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I");
        }

        [Test, Order(66)]
        public void TestCabling1U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test 1U4U3I3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I");
        }

        [Test, Order(67)]
        public void TestCablingNOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Test NOCIRCUIT Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
        }
    }
    
}
