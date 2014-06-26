using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Runtime.InteropServices;
namespace ExcelPractice
{
    class Program
    {
        static void Main(string[] args)
        {   Application xlApp ;
            Workbook xlWorkBook ;
            Worksheet xlWorkSheet ;
            object misValue = System.Reflection.Missing.Value;

            xlApp = new Application();
            xlWorkBook = xlApp.Workbooks.Add(misValue);

            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);
           
            xlWorkSheet.Cells[1, 1] = "http://csharp.net-informations.com";
            xlWorkSheet.Cells[2, 1] = "http://csharp.net-informations.com";
            xlWorkSheet.Cells[3, 1] = "http://csharp.net-informations.com";
            xlWorkSheet.Cells[4, 1] = "http://csharp.net-informations.com";
            xlWorkSheet.Cells[1, 5] = "http://csharp.net-informations.com";

            xlWorkBook.SaveAs("csharp-Excel.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlExclusive, misValue, misValue, misValue, misValue, misValue);
            xlWorkBook.Close(true, misValue, misValue);
            xlApp.Quit();
            Marshal.ReleaseComObject(xlWorkSheet);
            Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                GC.Collect();
            /*    Process myprc = GetaProcess("EXCEL");
                myprc.Kill();
            */

    }
}}