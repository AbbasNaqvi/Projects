using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.Office.Interop.Excel;
//using Microsoft.Office.Tools.Excel;
using System.Runtime.InteropServices;
using System.Reflection;

namespace SplitandMerge
{
    public delegate void InformationDownloadHandler(object o, EventArguments e);
    class ExcelHandlercs
    {
        public event InformationDownloadHandler InformationDownloadEvent;
        private Application application;
        private Workbook workbook;
        private Worksheet worksheet;
        Brochures Header = new Brochures();

        public ExcelHandlercs()
        {
            application = new Application();
        }
        public virtual void OnInformationDownload(EventArguments e)
        {
            if (InformationDownloadEvent != null)
            {
                InformationDownloadEvent(this, e);

            }
        }
        public string Merge(string directory, string outputFile)
        {
            string Result=null;

            DirectoryInfo dInfo = new DirectoryInfo(directory);
            DirectoryInfo[] subdirs = dInfo.GetDirectories();
            List<Brochures> ResultSet = new List<Brochures>();
            for (int i = 0; i < subdirs.Length; i++)
            {
                string DirectoryName = subdirs[i].FullName + "\\"+subdirs[i]+"_Terraced.xls";
                List<Brochures> TarracedBrochures = GetBrochuresFromExcel(DirectoryName);
                DirectoryName = subdirs[i].FullName + "\\"+subdirs[i]+"_Non Terraced.xls";
                List<Brochures> NonTarracedBrochures = GetBrochuresFromExcel(DirectoryName);
                if (TarracedBrochures != null)
                {
                    ResultSet.AddRange(TarracedBrochures);
                }
                if (NonTarracedBrochures != null)
                {
                    ResultSet.AddRange(NonTarracedBrochures);
                }
            }
           Result= WriteAllBrochures(ResultSet, outputFile);
           return Result;
        }

        private string WriteAllBrochures(List<Brochures> brochures, string FilePath)
        {
            string Result=null;
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Application();
            xlApp.DisplayAlerts = false;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);

            int I = 1;
            foreach (Brochures b in brochures)
            {
                try
                {
                    if (I == 1)
                    {
                        if (Header != null)
                        {
                            xlWorkSheet.Cells[I, 1] = Header.FolderName;
                            xlWorkSheet.Cells[I, 2] = Header.PostCode;
                            xlWorkSheet.Cells[I, 3] = Header.Price;
                            xlWorkSheet.Cells[I, 4] = Header.PropertyType;
                        }
                        else {
                            xlWorkSheet.Cells[I, 1] = "FolderName";
                            xlWorkSheet.Cells[I, 2] = "PostCode";
                            xlWorkSheet.Cells[I, 3] = "Price";
                            xlWorkSheet.Cells[I, 4] = "PropertyType";                        
                        }
                    }
                    else if (b.PostCode.Equals("Postcode")==false || b.Price.Equals("Price")==false)
                    {


                        xlWorkSheet.Cells[I, 1] = b.FolderName;
                        xlWorkSheet.Cells[I, 2] = b.PostCode;
                        xlWorkSheet.Cells[I, 3] = b.Price;
                        xlWorkSheet.Cells[I, 4] = b.PropertyType;
                        OnInformationDownload(new EventArguments() { Name = "Writing all records", Time = DateTime.Now, Details = "Writing " + I + "/" + brochures.Count + " Record in " + FilePath });

                    }
                }
                catch (Exception ex)
                {

                    int hr = Marshal.GetHRForException(ex);
                    Result += "hr";

                } I++;
               
            }
            try
            {
                xlWorkBook.SaveAs(FilePath, XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);
            }
            catch (COMException)
            {
                Result += " -Can not write the File ";
            }
            finally
            {
                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                GC.Collect();
                KillProcesses("EXCEL");
            }
            Result+="successfully Written";
            return Result;
        }




        /*
         * There is no difference between this function and another ...This will be removed when getting all information
         * 
         */
        private List<Brochures> GetBrochuresFromExcel(string FileName)
        {
            List<Brochures> brochuresList = new List<Brochures>();
            application = new Application();
            application.DisplayAlerts = false;
            if (File.Exists(FileName))
            {
                workbook = application.Workbooks.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);
            }
            else
            {
                return null;
            }
            try
            {
                worksheet = (Worksheet)workbook.Sheets[1];
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            int lastRow;
            OnInformationDownload(new EventArguments() { Name = "Reading sub records", Time = DateTime.Now, Details = "Reading records in " + FileName });
            //Range excelRange = worksheet.UsedRange;
            try
            {
                lastRow = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);

            }
            BrochuresCollection bCollection = BrochuresCollection.Create();
            for (int i = 1; i < lastRow; i++)
            {
                Brochures brochure = new Brochures();
                System.Array valueArray = null;
                try
                {
                    valueArray = (System.Array)worksheet.get_Range("A" + i.ToString(), "D" + i.ToString()).Cells.EntireRow.Value2;
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
                try
                {
                    string FolderName = null;
                    string Price = null;
                    if (valueArray != null)
                    {

                        try
                        {
                            FolderName = valueArray.GetValue(1, 1).ToString();
                            Price = valueArray.GetValue(1, 3).ToString();
                        }
                        catch (NullReferenceException)
                        {


                        }
                        if (String.IsNullOrEmpty(FolderName) || String.IsNullOrEmpty(Price))
                        {
                            continue;
                        }
                        brochure.PostCode = valueArray.GetValue(1, 2).ToString();
                        brochure.PropertyType = valueArray.GetValue(1, 4).ToString();
                        brochure.Price = Price;
                        brochure.FolderName = FolderName;
                        brochuresList.Add(brochure);
                    }
                }
                catch (Exception)
                {

                }
            }

            try
            {

                Marshal.ReleaseComObject(worksheet);
                workbook.Close();
                Marshal.ReleaseComObject(workbook);
                application.Quit();
                Marshal.ReleaseComObject(application);
                GC.Collect();
                KillProcesses("EXCEL");

            }
            catch (Exception)
            {


            }
            return brochuresList;
        }
        public void KillProcesses(string fileType)
        {
            Process[] myprc = GetAllProcess(fileType);
            for (int i = 0; i < myprc.Length; i++)
            {
                myprc[i].Kill();
            }
        }
        private Process[] GetAllProcess(string processname)
        {
            Process[] aProc = Process.GetProcessesByName(processname);

            if (aProc.Length > 0)
                return aProc;

            else return null;
        }


        public string WriteBrochures(string directory)
        {
            string Result = null;
            if (Directory.Exists(directory) == false)
            {
                return Result;
            }
            BrochuresCollection bc = BrochuresCollection.Create();
            SpecificBrochuresCollection SBC = SpecificBrochuresCollection.Create();
            foreach (Brochures b in bc.BList)
            {
                if (b.FolderName == null)
                {
                    continue;
                } if (b.PostCode.Equals("Postcode")||b.Price.Equals("Price"))
                {
                    Header = b;
                    continue;
                }
                StringBuilder FolderPath = new StringBuilder(directory);
                FolderPath.Append("\\" + b.FolderName + "\\");
                try
                {
                    Directory.CreateDirectory(FolderPath.ToString());
                }
                catch (IOException e)
                {
                    Result += "Can not Create Folders ," + e.Message;
                    OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "CAN NOT CREATE FOLDERS" });

                }
                FolderPath.Clear();

                SBC.Add(b);
            }

            foreach (BrochuresCollection fbc in SBC.sbcList)
            {
                Result+=WriteCollectionInExcel(directory, fbc);
            }
            return Result;
        }
        private string WriteCollectionInExcel(string FilePath, BrochuresCollection bc)
        {
            string Result = null;
            Application xlApp;
            Workbook xlWorkBook;
            Worksheet xlWorkSheet;
            object misValue = System.Reflection.Missing.Value;
            xlApp = new Application();
            xlApp.DisplayAlerts = false;
            xlWorkBook = xlApp.Workbooks.Add(misValue);
            xlWorkSheet = (Worksheet)xlWorkBook.Worksheets.get_Item(1);


            Application xlApp2;
            Workbook xlWorkBook2;
            Worksheet xlWorkSheet2;
            xlApp2 = new Application();
            xlApp2.DisplayAlerts = false;
            xlWorkBook2 = xlApp2.Workbooks.Add(misValue);
            xlWorkSheet2 = (Worksheet)xlWorkBook2.Worksheets.get_Item(1);

            int I = 1;
            int J = 1;
            int Count = 0;
            foreach (Brochures b in bc.BListSpecific)
            {
                Count++;
                if (I + J == 10)
                {
                    break;
                }
                if (b.PropertyType.Equals("terraced"))
                {
                    if (I == 1)
                    {
                        if (String.IsNullOrEmpty(Header.FolderName)==false&&String.IsNullOrEmpty(Header.PostCode)==false)
                        {
                            xlWorkSheet.Cells[I, 1] = Header.FolderName;
                            xlWorkSheet.Cells[I, 2] = Header.PostCode;
                            xlWorkSheet.Cells[I, 3] = Header.Price;
                            xlWorkSheet.Cells[I, 4] = Header.PropertyType;
                            I++;
                        }
                        else
                        {
                            xlWorkSheet.Cells[I, 1] = "FolderName";
                            xlWorkSheet.Cells[I, 2] = "PostCode";
                            xlWorkSheet.Cells[I, 3] = "Price";
                            xlWorkSheet.Cells[I, 4] = "PropertyType";
                            I++;
                        }
                        continue;
                    }


                    try
                    {
                        xlWorkSheet.Cells[I, 1] = b.FolderName;
                        xlWorkSheet.Cells[I, 2] = b.PostCode;
                        xlWorkSheet.Cells[I, 3] = b.Price;
                        xlWorkSheet.Cells[I, 4] = b.PropertyType;
                        I++;
                    }
                    catch (COMException)
                    {
                        Result = "\nCan not Fill Cells of Excels ,May be the File is opened somewhere else";
                        OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "Can not Fill Cells of Excels ,May be the File is opened somewhere else" });

                    }
                    OnInformationDownload(new EventArguments() { Name = "Writing Records", Time = DateTime.Now, Details = "Writing " + Count + "//" + bc.BListSpecific.Count + " Record in " + b.FolderName });
                }
                else
                {
                    try
                    {
                        if (J == 1)
                        {
                            if (Header != null)
                            {
                                xlWorkSheet.Cells[J, 1] = Header.FolderName;
                                xlWorkSheet.Cells[J, 2] = Header.PostCode;
                                xlWorkSheet.Cells[J, 3] = Header.Price;
                                xlWorkSheet.Cells[J, 4] = Header.PropertyType;
                                J++;
                            }
                            else
                            {
                                xlWorkSheet.Cells[J, 1] = "FolderName";
                                xlWorkSheet.Cells[J, 2] = "PostCode";
                                xlWorkSheet.Cells[J, 3] = "Price";
                                xlWorkSheet.Cells[J, 4] = "PropertyType";
                                J++;
                            }
                            continue;
                        }

                        xlWorkSheet2.Cells[J, 1] = b.FolderName;
                        xlWorkSheet2.Cells[J, 2] = b.PostCode;
                        xlWorkSheet2.Cells[J, 3] = b.Price;
                        xlWorkSheet2.Cells[J, 4] = b.PropertyType;
                        J++;
                        OnInformationDownload(new EventArguments() { Name = "Writing Records", Time = DateTime.Now, Details = "Writing " + Count + "//" + bc.BListSpecific.Count + " Record in " + b.FolderName });

                    }
                    catch (COMException)
                    {
                        Result+= "\nCan not Fill Cells of Excels ,May be the File is opened somewhere else";
                        OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "Can not Fill Cells of Excels ,May be the File is opened somewhere else" });

                    }
                }
            }

            try
            {
                xlWorkBook.SaveAs(FilePath + "\\" + bc.FolderName + "\\" + bc.FolderName + "_Terraced.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);
                xlWorkBook2.SaveAs(FilePath + "\\" + bc.FolderName + "\\" + bc.FolderName + "_Non Terraced.xls", XlFileFormat.xlWorkbookNormal, misValue, misValue, misValue, misValue, XlSaveAsAccessMode.xlShared, misValue, misValue, misValue, misValue, misValue);
            }
            catch (COMException)
            {
                Result += "\nCan not Save the Records in" + bc.FolderName + "_Non Terraced.xls, May be file is opened somewhere else";
                OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "Can not Save the Records in" + bc.FolderName + "_Non Terraced.xls, May be file is opened somewhere else" });

            }
            finally
            {

                xlWorkBook.Close(true, misValue, misValue);
                xlApp.Quit();
                Marshal.ReleaseComObject(xlWorkSheet);
                Marshal.ReleaseComObject(xlWorkBook);
                Marshal.ReleaseComObject(xlApp);
                GC.Collect();


                xlWorkBook2.Close(true, misValue, misValue);
                xlApp2.Quit();
                Marshal.ReleaseComObject(xlWorkSheet2);
                Marshal.ReleaseComObject(xlWorkBook2);
                Marshal.ReleaseComObject(xlApp2);
                GC.Collect();
                KillProcesses("EXCEL");
            }
            return Result;
        }
        public string ReadBrochures(string FileName, string WSName)
        {
            string Result = null;
            application.DisplayAlerts = false;
            workbook = application.Workbooks.Open(FileName, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing, Type.Missing);


            try
            {
                worksheet = (Worksheet)workbook.Sheets[1];
            }
            catch (Exception e)
            {
                Result += "\nCan not identify Worksheet"+e.Message;
                OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "Can not identify Worksheet" + e.Message });
                return Result;
            }
            int lastRow;
            //Range excelRange = worksheet.UsedRange;
            try
            {
                lastRow = worksheet.Cells.SpecialCells(XlCellType.xlCellTypeLastCell).Row;

            }
            catch (Exception e)
            {
                Result += "\nCan not Find Records in File, "+e.Message;
                OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "Can not Find Records" + e.Message });

                return Result;

            }
            BrochuresCollection bCollection = BrochuresCollection.Create();
            for (int i = 1; i <= lastRow; i++)
            {
                Brochures brochure = new Brochures();
                //                System.Array valueArray = (System.Array)excelRange.get_Value(XlRangeValueDataType.xlRangeValueDefault);
                System.Array valueArray = null;
                try
                {
                    valueArray = (System.Array)worksheet.get_Range("A" + i.ToString(), "L" + i.ToString()).Cells.Value2;
                }
                catch (Exception e)
                {
                    Result += "\nCan not Retrieve "+i+" Record.,"+e.Message;
                    OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "CAN NOT RELEASE RESOURCES" });
                    return Result;
                }
                try
                {
                    brochure.PostCode = valueArray.GetValue(1, 5).ToString();
                    brochure.Price = valueArray.GetValue(1, 6).ToString();
                    brochure.PropertyType = valueArray.GetValue(1, 10).ToString();
                    brochure.FindCode();
                    if(brochure.PostCode.Equals("Postcode")||brochure.Price.Equals("Price"))
                    {
                        Header.PostCode = brochure.PostCode;  
                        Header.Price = brochure.Price;
                        Header.PropertyType = brochure.PropertyType;
                    
                    }
                    OnInformationDownload(new EventArguments() { Name = "Reading Records", Time = DateTime.Now, Details = "Reading " + i + "//" + lastRow + " Record in " + brochure.FolderName });
                    bCollection.BList.Add(brochure);
                }
                catch (Exception)
                {
                    Result += "\nCan not Record Read Value at "+i+".";
                    OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details = "CAN NOT READ VALUE" });

                }
            }

            try
            {

                Marshal.ReleaseComObject(worksheet);
                workbook.Close();
                Marshal.ReleaseComObject(workbook);
                application.Quit();
                Marshal.ReleaseComObject(application);
                GC.Collect();
                KillProcesses("EXCEL");

            }
            catch (Exception)
            {

                Result += "\nCan not Release Resources.";
                OnInformationDownload(new EventArguments() { Name = "ERROR", Time = DateTime.Now, Details ="CAN NOT RELEASE RESOURCES"});

            }
            return Result;
        }
    }
}
