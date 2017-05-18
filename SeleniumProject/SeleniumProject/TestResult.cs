//**************************************************************
// Class Name  :  TestReport  
// Purpose     :  This class is used to generate Test Report for the Test function executed.
// Modification History:
//  Ver #        Date         Author/Modified By       Remarks
//--------------------------------------------------------------
//   1.0        10-May-17      Rahuldev Gupta           Initial 

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeleniumProject
{
    public class TestReport
    {
        private string BrowserType;
        private string url;
        private DateTime date;
        private FileStream fs;
        private StringBuilder reportcsv;
        private string filePath;
        private string fileName;
        public TestReport()
        {
            date = DateTime.Now;
            fileName = date.Date.Date.ToShortDateString() + date.TimeOfDay.Hours.ToString() + date.TimeOfDay.Minutes.ToString();
            reportcsv = new StringBuilder();
            if (Directory.Exists(Directory.GetCurrentDirectory()+@"\TestResult"))
            {
            filePath =  Directory.GetCurrentDirectory()+ fileName + ".csv";
            }
            else
            {
                Directory.CreateDirectory(Directory.GetCurrentDirectory() + @"\TestResult");
                filePath = Directory.GetCurrentDirectory() +@"\TestResult\"+ fileName +".csv";
            }
            createCsvFile();
        }
        private void createCsvFile()
        {
            reportcsv.Append("StepDescription,");
            reportcsv.Append("Pass/Fail,");
            reportcsv.Append("Exception");
            File.AppendAllText(filePath, reportcsv.ToString());
        }
        public void addLine(string description, string result, string exception)
        {
            reportcsv.Append(Environment.NewLine);
            reportcsv.Append(description + ",");
            reportcsv.Append(result + ",");
            reportcsv.Append(exception + ",");
            reportcsv.Append(Environment.NewLine);
            File.WriteAllText(filePath, reportcsv.ToString());
        }
    }
}

