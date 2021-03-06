﻿/* This file is used to compare dynamic pqp table from tabindex with dataset .txt file
 */

using System;
using OpenQA.Selenium;
using RelevantCodes.ExtentReports;
using System.IO;
using System.Text;
using System.Xml;
using NUnit.Framework;


namespace CashelFirmware.NunitTests
{
    public class ValidatePQP_CalculationTable
    {          
        public void PQP_CalculationTable(IWebDriver webDriver,string deviceIP, ExtentTest TestLog,string Cabling,string PQDynamicFilePath)
        {
            string paramtype = string.Empty;
            string phase = string.Empty;
            string harmonicrank = string.Empty;
            string busbarfeeder_type = string.Empty;
            int counter;
            StringBuilder pqpdataActual = new StringBuilder();
            StringBuilder pqpdataExpected = new StringBuilder();

            string DataSetFileName= PQDynamicFilePath + Cabling + ".txt";

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("http://" + deviceIP + "/cgi-bin/ipcxml.cgi?pqp:pqp/data");               
            XmlNodeList nodelist = xmlDocument.GetElementsByTagName("calc_type");
            counter = 0;

            foreach (XmlNode xml in nodelist)
            {
                paramtype = string.Empty;
                phase = string.Empty;
                harmonicrank = string.Empty;
                busbarfeeder_type = string.Empty;
                paramtype = xml.ChildNodes[0].InnerText.Trim();
                phase= xml.ChildNodes[1].InnerText.Trim();
                harmonicrank= xml.ChildNodes[2].InnerText.Trim();
                busbarfeeder_type= xml.ChildNodes[3].InnerText.Trim();
                counter++;
                if (counter != nodelist.Count)
                {
                    pqpdataActual.AppendLine("{" + paramtype + "," + phase + "," + harmonicrank + "," + busbarfeeder_type + "},");
                }
                else
                {
                    pqpdataActual.AppendLine("{" + paramtype + "," + phase + "," + harmonicrank + "," + busbarfeeder_type + "}");
                }                
            }

            StreamReader streamReader = new StreamReader(DataSetFileName);
            while (!streamReader.EndOfStream)
            {
                pqpdataExpected.AppendLine(streamReader.ReadLine()); 
            }

            if (pqpdataExpected.Equals(pqpdataActual))
            {
                TestLog.Log(LogStatus.Pass, "Dynamic PQP Calculation Parameters are correct");
            }
            else
            {
                TestLog.Log(LogStatus.Pass, "Dynamic PQP Calculation Parameters are not correct");
            }

            Assert.IsTrue(pqpdataExpected.Equals(pqpdataActual), "Checking Dynamic PQP Calculation Parameters");
            TestLog.Log(LogStatus.Pass, "Dynamic PQP Calculation Parameters are correct");
        }
    }
}
