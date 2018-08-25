/* This file is used to fetch dynamic pqp table from tabindex and store in a .txt file
 */

using System;
using Tabindex_Data.pqp;
using OpenQA.Selenium;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.IO;
using System.Text;
using System.Xml;
using System.Collections.Generic;

namespace CashelFirmware.NunitTests
{
    public class GetPQP_CalculationTable
    {

        public void GetPQPDynamicTable(IWebDriver webDriver,string deviceIP, ExtentTest TestLog,string Cabling)
        {
            string paramtype = string.Empty;
            string phase = string.Empty;
            string harmonicrank = string.Empty;
            string busbarfeeder_type = string.Empty;
            int counter;
            StringBuilder pqpdataActual = new StringBuilder();
            StringBuilder pqpdataExpected = new StringBuilder();

            string DataSetFileName= AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\PQPDynamicParametersDataSet\" + Cabling + ".txt";

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
        }
    }
}
