﻿
using CashelFirmware.Reporting;
using CashelFirmware.NunitTests;
using NUnit.Framework;
using CashelFirmware.GlobalVariables;

namespace CashelFirmware.TestSuite
{
    class ScalingfactorTag61850_ForPQStandaloneSuite:BaseTestSuite
    {
        FirmwareCablingTest CablingTest;
        ScalingFactorTag61850_ForPQStandalone ScalingFactorTag61850_ForPQStandalone;
        string DataSetFolderPath = string.Empty;

        public ScalingfactorTag61850_ForPQStandaloneSuite():base(DeviceInformation.glb_DeviceIP_18Channel)
        {
            ScalingFactorTag61850_ForPQStandalone = new ScalingFactorTag61850_ForPQStandalone();
            CablingTest = new FirmwareCablingTest();
            DataSetFolderPath = System.IO.Directory.GetParent(DeviceInformation.BaseDirectoryPath).ToString() + @"\TestDataFiles\CablingDataSet_18Channel\";
        }
        [Test, Order(1)]
        public void ScalingFactorTag_3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U");
        }

        [Test, Order(2)]
        public void ScalingFactorTag_3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U3I");
        }

        [Test, Order(3)]
        public void ScalingFactorTag_3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U3I3I");
        }

        [Test, Order(4)]
        public void ScalingFactorTag_3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I");
        }

        [Test, Order(5)]
        public void ScalingFactorTag_3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I");
        }

        [Test, Order(6)]
        public void ScalingFactorTag_3U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U3I3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U3I3I3I3I3I");
        }

        [Test, Order(7)]
        public void ScalingFactorTag_1U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3I");
        }

        [Test, Order(8)]
        public void ScalingFactorTag_1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3I3I");
        }

        [Test, Order(9)]
        public void ScalingFactorTag_1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I");
        }

        [Test, Order(10)]
        public void ScalingFactorTag_1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I");
        }

        [Test, Order(11)]
        public void ScalingFactorTag_1U3I3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3I3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3I3I3I3I3I");
        }


        [Test, Order(12)]
        public void ScalingFactorTag_2M3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M3U");
        }

        [Test, Order(13)]
        public void ScalingFactorTag_3U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U3U3I3I3I3I");
        }

        [Test, Order(14)]
        public void ScalingFactorTag_2M3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M3U3I");
        }

        [Test, Order(15)]
        public void ScalingFactorTag_2M3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I");
        }

        [Test, Order(16)]
        public void ScalingFactorTag_2M3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I");
        }

        [Test, Order(17)]
        public void ScalingFactorTag_2M3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M3U3I3I3I3I");
        }

        //[Test, Order(18)]
        //public void ScalingFactorTag_1U3U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3U3I Cabling");
        //    CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I", DataSetFolderPath);
        //    ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3U3I");
        //}

        [Test, Order(19)]
        public void ScalingFactorTag_1U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I");
        }

        [Test, Order(20)]
        public void ScalingFactorTag_1U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I");
        }

        [Test, Order(21)]
        public void ScalingFactorTag_1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U3U3I3I3I3I");
        }

        [Test, Order(22)]
        public void ScalingFactorTag_3U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I");
        }

        [Test, Order(23)]
        public void ScalingFactorTag_3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U1U3I3I3I3I");
        }

        [Test, Order(24)]
        public void ScalingFactorTag_1U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I");
        }

        [Test, Order(25)]
        public void ScalingFactorTag_1U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I");
        }

        [Test, Order(26)]
        public void ScalingFactorTag_1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U1U3I3I3I3I");
        }

        [Test, Order(27)]
        public void ScalingFactorTag_S1U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  S1U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "S1U1U3I3I3I3I");
        }

        [Test, Order(28)]
        public void ScalingFactorTag_S1U3U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  S1U3U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "S1U3U3I3I3I3I");
        }

        [Test, Order(29)]
        public void ScalingFactorTag_S3U1U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  S3U1U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "S3U1U3I3I3I3I");
        }

        [Test, Order(30)]
        public void ScalingFactorTag_4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U");
        }

        [Test, Order(31)]
        public void ScalingFactorTag_4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3I");
        }

        [Test, Order(32)]
        public void ScalingFactorTag_4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3I3I");
        }

        [Test, Order(33)]
        public void ScalingFactorTag_4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I");
        }

        [Test, Order(34)]
        public void ScalingFactorTag_4U3I3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3I3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3I3I3I3I");
        }

        [Test, Order(35)]
        public void ScalingFactorTag_4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U4I");
        }

        [Test, Order(36)]
        public void ScalingFactorTag_4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U4I4I");
        }

        [Test, Order(37)]
        public void ScalingFactorTag_4U4I4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U4I4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U4I4I4I");
        }

        [Test, Order(38)]
        public void ScalingFactorTag_2M4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M4U Cabling Testing");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M4U");
        }

        [Test, Order(39)]
        public void ScalingFactorTag_2M4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M4U3I");
        }

        [Test, Order(40)]
        public void ScalingFactorTag_2M4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I");
        }

        [Test, Order(41)]
        public void ScalingFactorTag_2M4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M4U3I3I3I");
        }

        [Test, Order(42)]
        public void ScalingFactorTag_2M4U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M4U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M4U4I");
        }

        [Test, Order(43)]
        public void ScalingFactorTag_2M4U4I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  2M4U4I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "2M4U4I4I");
        }

        [Test, Order(44)]
        public void ScalingFactorTag_4U3U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U");
        }

        [Test, Order(45)]
        public void ScalingFactorTag_4U3U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U3I");
        }

        [Test, Order(46)]
        public void ScalingFactorTag_4U3U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I");
        }

        [Test, Order(47)]
        public void ScalingFactorTag_4U3U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U3I3I3I");
        }

        [Test, Order(48)]
        public void ScalingFactorTag_3U4U()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U4U Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U4U");
        }

        [Test, Order(49)]
        public void ScalingFactorTag_3U4U3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U4U3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U4U3I");
        }

        [Test, Order(50)]
        public void ScalingFactorTag_3U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I");
        }

        [Test, Order(51)]
        public void ScalingFactorTag_3U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I3I");
        }

        [Test, Order(52)]
        public void ScalingFactorTag_4U3U4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U4I");
        }

        [Test, Order(53)]
        public void ScalingFactorTag_4U3U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U4I3I");
        }

        [Test, Order(54)]
        public void ScalingFactorTag_4U3U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U3U4I4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U3U4I4I3I");
        }

        [Test, Order(55)]
        public void ScalingFactorTag_3U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U4U3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U4U3I4I");
        }

        [Test, Order(56)]
        public void ScalingFactorTag_3U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  3U4U3I3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "3U4U3I3I4I");
        }

        [Test, Order(57)]
        public void ScalingFactorTag_4U1U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U1U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I");
        }

        [Test, Order(58)]
        public void ScalingFactorTag_4U1U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U1U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U1U3I3I3I");
        }

        //[Test, Order(59)]
        //public void ScalingFactorTag_1U4U3I()
        //{
        //    InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U4U3I Cabling");
        //    CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I", DataSetFolderPath);
        //    ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U4U3I");
        //}

        [Test, Order(60)]
        public void ScalingFactorTag_1U4U3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U4U3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I");
        }

        [Test, Order(61)]
        public void ScalingFactorTag_1U4U3I3I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U4U3I3I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I3I");
        }

        [Test, Order(62)]
        public void ScalingFactorTag_4U1U4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U1U4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U1U4I3I");
        }

        [Test, Order(63)]
        public void ScalingFactorTag_4U1U4I4I3I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  4U1U4I4I3I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "4U1U4I4I3I");
        }

        [Test, Order(64)]
        public void ScalingFactorTag_1U4U3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U4U3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U4U3I4I");
        }

        [Test, Order(65)]
        public void ScalingFactorTag_1U4U3I3I4I()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  1U4U3I3I4I Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "1U4U3I3I4I");
        }

        [Test, Order(66)]
        public void ScalingFactorTag_NOCIRCUIT()
        {
            InfovarStartTest = ReportGeneration.extent.StartTest("Validate Scale Factor Tag in Standalone channel for  NOCIRCUIT Cabling");
            CablingTest.TestCabling(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT", DataSetFolderPath);
            ScalingFactorTag61850_ForPQStandalone.ValidateScalingfactorTag_PQStandalone(webdriver, deviceIP, InfovarStartTest, "NOCIRCUIT");
        }


    }
}
