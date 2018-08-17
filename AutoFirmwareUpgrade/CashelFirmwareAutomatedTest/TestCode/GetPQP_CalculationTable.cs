using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tabindex_Data.pqp;
using OpenQA.Selenium;
using NUnit.Framework;
using RelevantCodes.ExtentReports;
using System.IO;

namespace CashelFirmware.NunitTests
{
    public class GetPQP_CalculationTable
    {
        public void GetPQPDynamicTable(IWebDriver webDriver,string deviceIP, ExtentTest TestLog,string Cabling)
        {
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
            //File.Create(Filepath);
                        
            for (int calcindex = 0; calcindex < 3700; calcindex++)
            {
                string paramtype =string.Empty;
                string phase = string.Empty;
                string harmonicrank = string.Empty;
                string busbarfeeder_type = string.Empty;

                Assert.IsTrue(Tabindex_Data_Pqp.Item_Calc_type_num_Click(calcindex), "Clicked on Calc num[" + calcindex + "]");
                TestLog.Log(LogStatus.Pass, "Clicked on Calc num[" + calcindex + "]");

                paramtype = Tabindex_Data_Pqp.Get_paramtype(calcindex);
                harmonicrank = Tabindex_Data_Pqp.Get_harmonicrank(calcindex);
                busbarfeeder_type = Tabindex_Data_Pqp.Get_busbarfeedertype(calcindex);
                switch (Tabindex_Data_Pqp.Get_phase(calcindex))
                {
                    case "NONE":
                        {
                            phase = "0";
                            break;
                        }
                    case "A":
                        {
                            phase = "1";
                            break;
                        }
                    case "B":
                        {
                            phase = "2";
                            break;
                        }
                    case "C":
                        {
                            phase = "3";
                            break;
                        }
                    case "N":
                        {
                            phase = "4";
                            break;
                        }
                    default:
                        break;
                }

                using (StreamWriter Filewrite = File.AppendText(Filepath))
                {
                    Filewrite.WriteLine("{" + paramtype + "," + phase + "," + harmonicrank + "," + busbarfeeder_type + "},");
                }
            }
        }

    }
}
