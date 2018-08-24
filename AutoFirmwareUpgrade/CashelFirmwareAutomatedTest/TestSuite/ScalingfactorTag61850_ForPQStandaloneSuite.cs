using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CashelFirmware.NunitTests;

namespace CashelFirmware.TestSuite
{
    class ScalingfactorTag61850_ForPQStandaloneSuite:BaseTestSuite
    {
        FirmwareCablingTest CablingTest;
        ScalingFactorTag61850_ForPQStandalone ScalingFactorTag61850_ForPQStandalone;

        public ScalingfactorTag61850_ForPQStandaloneSuite()
        {
            ScalingFactorTag61850_ForPQStandalone = new ScalingFactorTag61850_ForPQStandalone();
            CablingTest = new FirmwareCablingTest();
        }
    }
}
