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

            StringBuilder pqpdata = new StringBuilder();

            string DataSetFileName= AppDomain.CurrentDomain.BaseDirectory + @"\TestDataFiles\CablingDataSet\" + Cabling + ".xlsx";
            Tabindex_Data_pqp Tabindex_Data_Pqp = new Tabindex_Data_pqp(webDriver);
            Assert.AreEqual("Data", Tabindex_Data_Pqp.OpenTabIndexPage(deviceIP), "Device is up/responding");
            TestLog.Log(LogStatus.Pass, "Device is up/responding");

            Assert.IsTrue(Tabindex_Data_Pqp.Btn_Data_Click(), "Clicked on Data");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked on Data Tab");

            Assert.IsTrue(Tabindex_Data_Pqp.SwitchFrame_Fromdefault_Topqp(), "Switched Frame from Default to pqp topology");
            TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from Default to pqp topology");

            Assert.IsTrue(Tabindex_Data_Pqp.Item_Data_pqp_Click(), "Clicked pqp option under tabindex_data page");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked pqp option under tabindex_configuration page");

            Assert.IsTrue(Tabindex_Data_Pqp.SwitchFrame_Frompqp_Todata(), "Switched Frame from pqp topology to data");
            TestLog.Log(LogStatus.Pass, "Success:-Switched Frame from pqp topology to data");

            Assert.IsTrue(Tabindex_Data_Pqp.Item_Data_pqp_data_Click(), "Clicked on pqp data");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked on pqp data");

            Assert.IsTrue(Tabindex_Data_Pqp.Item_Calculation_Type_Click(), "Clicked on Calculation type");
            TestLog.Log(LogStatus.Pass, "Success:-Clicked on Calculation type");
            string Filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, Cabling + ".txt");

            XmlDocument xmlDocument = new XmlDocument();
            xmlDocument.Load("http://" + deviceIP + "/cgi-bin/ipcxml.cgi?pqp:pqp/data");

            XmlNodeList nodelist = xmlDocument.GetElementsByTagName("calc_type");

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
                pqpdata.AppendLine("{" + paramtype + "," + phase + "," + harmonicrank + "," + busbarfeeder_type + "},");
            }
 
            StreamWriter Filewrite = new StreamWriter(Filepath);            
            Filewrite.WriteLine(pqpdata.ToString());
            Filewrite.Close();
            
            TestLog.Log(LogStatus.Pass, "Created File Successfully for :-" + Cabling); 
        }
    }
}
