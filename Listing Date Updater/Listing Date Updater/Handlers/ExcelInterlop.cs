using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Office.Interop.Excel;
using System.Reflection;
namespace Listing_Date_Updater
{
    class ExcelInterlop
    {
        public void CreateNewExcelFile()
        {
            Microsoft.Office.Interop.Excel.Application xlApp = new Microsoft.Office.Interop.Excel.Application();

            if (xlApp == null)
            {
                Console.WriteLine("EXCEL could not be started. Check that your office installation and project references are correct.");
                return;
            }
            xlApp.Visible = true;

            Workbook wb = xlApp.Workbooks.Add(XlWBATemplate.xlWBATWorksheet);
            Worksheet ws = (Worksheet)wb.Worksheets[1];

            if (ws == null)
            {
                Console.WriteLine("Worksheet could not be created. Check that your office installation and project references are correct.");
            }										

            ws.Range["A1", Type.Missing].Value2 = "ADDRESS1";
            ws.Range["B1", Type.Missing].Value2 = "ADDRESS2";
            ws.Range["C1", Type.Missing].Value2 = "ADDRESS3";
            ws.Range["D1", Type.Missing].Value2 = "ADDRESS4";
            ws.Range["E1", Type.Missing].Value2 = "POSTCODE";
            ws.Range["F1", Type.Missing].Value2 = "PRICE";
            ws.Range["G1", Type.Missing].Value2 = "DATE MARKETED";
            ws.Range["H1", Type.Missing].Value2 = "MARKETED BY";
            ws.Range["I1", Type.Missing].Value2 = "NUMBERS OF DAY";
            ws.Range["J1", Type.Missing].Value2 = "Success URL";
            ws.Range["K1", Type.Missing].Value2 = "Homes URL";
            ws.Range["L1", Type.Missing].Value2 = "MARKETED BY2";
            ws.Range["M1", Type.Missing].Value2 = "Property Id";

            string xyz = System.IO.Directory.GetCurrentDirectory() + "\\ooutput.xlsx";
            
            ws.Name = "OOutput";
            wb.Close(true, xyz, Type.Missing);
           
            // Select the Excel cells, in the range c1 to c7 in the worksheet.
            //Range aRange = ws.get_Range("C1","R12");

            //if (aRange == null)
            //{
            //    Console.WriteLine("Could not get a range. Check to be sure you have the correct versions of the office DLLs.");
            //}

            //// Fill the cells in the C1 to C7 range of the worksheet with the number 6.
            //Object[] args = new Object[1];
            //args[0] = 6;
            //aRange.GetType().InvokeMember("Value", BindingFlags.SetProperty, null, aRange, args);

            //// Change the cells in the C1 to C7 range of the worksheet to the number 8.
            //aRange.Value2 = 8;
        
        
        }
    }
}
