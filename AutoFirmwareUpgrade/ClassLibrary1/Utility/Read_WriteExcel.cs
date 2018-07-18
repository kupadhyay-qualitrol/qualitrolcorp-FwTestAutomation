using System;
using Microsoft.Office.Interop.Excel;


namespace CashelFirmware.Utility
{
    public class Read_WriteExcel
    {
        Application xlapp = new Application();
        Workbook wb;
        Worksheet Sheet;
        Range xlrange;

        string[] dspchannelmap = new string[25];
        string[] pq_10min_calcnum = new string[1190];
        string pmp_PQcabling = string.Empty;
        string pmp_FRcabling = string.Empty;
        public string[] Dspchannelmap { get => dspchannelmap; set => dspchannelmap = value; }
        public string[] Pq_10min_calcnum { get => pq_10min_calcnum; set => pq_10min_calcnum = value; }
        public string Pmp_PQcabling { get => pmp_PQcabling; set => pmp_PQcabling = value; }
        public string Pmp_FRcabling { get => pmp_FRcabling; set => pmp_FRcabling = value; }

        public int GetUsedRowCount(string filename, string Sheetname)
        {
                wb = xlapp.Workbooks.Open(filename);
                Sheet = wb.Sheets[Sheetname];
                xlrange = Sheet.UsedRange;
                int rowcount = xlrange.Rows.Count;
                return rowcount;
        }

        public string ReadExcel(string filename,string Sheetname,int i,string InputColumn)
        {
            try
            { 
            wb = xlapp.Workbooks.Open(filename);
            Sheet = wb.Sheets[Sheetname];
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

        public void WriteExcel_DSPChannelmap(string filename)
        {
            try
            {
                wb = xlapp.Workbooks.Open(filename);
                Sheet = wb.Sheets["DSPChannelMap"];
                xlapp.Visible = false;
                xlapp.DisplayAlerts = false;
                for (int cellnum = 1; cellnum <= 12; cellnum++)
                {
                    (Sheet.Cells[cellnum + 1, 2] as Microsoft.Office.Interop.Excel.Range).Value = Dspchannelmap[cellnum - 1];
                }
                for (int cellnum = 1; cellnum <= 12; cellnum++)
                {
                    (Sheet.Cells[cellnum + 1, 4] as Microsoft.Office.Interop.Excel.Range).Value = Dspchannelmap[12 + cellnum - 1];
                }
                wb.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                wb.Close();
            }
            finally
        
            {
                xlapp.Quit();
                
            }
            }

        public void WriteExcel_CablingPQ_FR(string filename)
        {
            try
            {
                wb = xlapp.Workbooks.Open(filename);
                Sheet = wb.Sheets["Cabling"];
                xlapp.Visible = false;
                xlapp.DisplayAlerts = false;

                (Sheet.Cells[11, 2] as Microsoft.Office.Interop.Excel.Range).Value = pmp_FRcabling;
                (Sheet.Cells[12, 2] as Microsoft.Office.Interop.Excel.Range).Value = pmp_PQcabling;

                wb.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
                wb.Close();
            }
            finally
            {
                xlapp.Quit();
                
            }
        }

        public void WriteExcel_Standaloneparam(string filename)
        {
         
            wb = xlapp.Workbooks.Open(filename);
            Sheet = wb.Sheets["StandaloneParameters"];
            xlapp.Visible = false;
            xlapp.DisplayAlerts = false;
            for (int cellnum = 1; cellnum <= 1189; cellnum++)
            {
                if (pq_10min_calcnum[cellnum - 1] != null)
                {
                    (Sheet.Cells[cellnum + 1, 3] as Microsoft.Office.Interop.Excel.Range).Value = Pq_10min_calcnum[cellnum - 1];
                }
                else
                {
                    break;
                }
            }
            wb.SaveAs(filename, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Microsoft.Office.Interop.Excel.XlSaveAsAccessMode.xlNoChange, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            wb.Close();
            xlapp.Quit();
        }
    }
}
