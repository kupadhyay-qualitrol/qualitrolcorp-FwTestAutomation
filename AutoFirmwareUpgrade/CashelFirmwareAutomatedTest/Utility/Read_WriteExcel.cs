/*This file contains method related to Read data from excel sheet
 */

using System;
using Microsoft.Office.Interop.Excel;

namespace CashelFirmware.Utility
{
    public static class Read_WriteExcel
    {

        public static Application xlapp;
        
        public static string ReadExcel(string filename,string Sheetname,int i,string InputColumn)
        {
            Workbook wb;
            Worksheet Sheet;
            Range xlrange;
            try
            {
            wb = xlapp.Workbooks.Open(filename);                
            Sheet=wb.Sheets[Sheetname];
            xlrange = Sheet.UsedRange;
            xlapp.Visible = false;

            int rowcount = xlrange.Rows.Count;
            int columncount = xlrange.Columns.Count;
            int j = -1;
            for (int k = 1; k <= columncount; k++)
            {
                if ((string)(Sheet.Cells[1,k] as Microsoft.Office.Interop.Excel.Range).Value ==InputColumn) 
                {
                    j = k;
                    break;
                }                
            }
           string Datavalue = Convert.ToString((Sheet.Cells[i + 2, j] as Microsoft.Office.Interop.Excel.Range).Value);
           return Datavalue;                
            }
            catch(Exception ex)
            {
                return null;
            }
            finally
            {
                xlapp.Quit();
            }
        }
    }
}
