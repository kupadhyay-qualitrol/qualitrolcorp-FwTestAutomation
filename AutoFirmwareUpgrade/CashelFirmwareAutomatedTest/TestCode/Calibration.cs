using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Resources;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using NUnit.Framework;

namespace CashelFirmware.NunitTests
{
    public class Calibration
    {

        ResourceManager resourceManager;

        public Calibration()
        {
            resourceManager = new ResourceManager("CashelFirmwareAutomatedTest.Resource", this.GetType().Assembly);

        }

        public void CalibrateDevice()
        {
            
        }
    }
}
