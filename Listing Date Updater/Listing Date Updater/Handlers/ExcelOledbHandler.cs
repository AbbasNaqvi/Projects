using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;

namespace Listing_Date_Updater
{
    class ExcelOledbHandler
    {
        //Dont ignore the constructor
        public ExcelOledbHandler(string filename,string worksheetname)
        {
            filePath = filename;
            worksheetName = worksheetname;
        
        }

        //properties
        private string filePath;
        public string FilePath
        {
            get { return filePath; }
            set { filePath = value; }
        }
        private string worksheetName;
        public string WorkSheetName
        {
            get { return worksheetName; }
            set { worksheetName = value; }
        }
        public void KillProcesses(string fileType)
        {
            Process[] myprc = GetAllProcess(fileType);
            if (myprc != null)
            {
                for (int i = 0; i < myprc.Length; i++)
                {
                    myprc[i].Kill();
                }
            }
        }
        private Process[] GetAllProcess(string processname)
        {
            Process[] aProc = Process.GetProcessesByName(processname);

            if (aProc.Length > 0)
                return aProc;

            else return null;
        }

        //private
        private string GetConnectionString()
        {
            Dictionary<string, string> props = new Dictionary<string, string>();

            // XLSX - Excel 2007, 2010, 2012, 2013
            props["Provider"] = "Microsoft.ACE.OLEDB.12.0;";
            props["Extended Properties"] = "Excel 12.0 XML";
            props["Data Source"] = filePath;

            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "C:\\MyExcel.xls";

            StringBuilder sb = new StringBuilder();

            foreach (KeyValuePair<string, string> prop in props)
            {
                sb.Append(prop.Key);
                sb.Append('=');
                sb.Append(prop.Value);
                sb.Append(';');
            }

            return sb.ToString();
        }
       
        //public
        
        public DataSet ReadExcelFile()
        {
            DataSet ds = new DataSet();

            string connectionString = GetConnectionString();

            using (OleDbConnection conn = new OleDbConnection(connectionString))
            {
                conn.Open();
                OleDbCommand cmd = new OleDbCommand();
                cmd.Connection = conn;

                // Get all Sheets in Excel File
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                // Loop through all Sheets to get data

                if (worksheetName.EndsWith("$"))
                {
                    cmd.CommandText = "SELECT * FROM [" + worksheetName + "]";
                }
                else {

                    cmd.CommandText = "SELECT * FROM [" + worksheetName + "$]";                
                }

                    // Get all rows from the Sheet
                
                    DataTable dt = new DataTable();
                    dt.TableName = worksheetName;

                    OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                    da.Fill(dt);

                    ds.Tables.Add(dt);
                

                cmd = null;
                conn.Close();
            }

            return ds;
        }


        public void WriteInDatabase(PropertyInfo p,string outputFileName="ooutput.xlsx",string sheetName="OOutput")
        {
            KillProcesses("EXCEL");
            String xyz = System.IO.Directory.GetCurrentDirectory() + "\\" + outputFileName;
            if(System.IO.File.Exists(xyz)==false)
            {
                ExcelInterlop interop = new ExcelInterlop();
                interop.CreateNewExcelFile();
            }

                System.Data.OleDb.OleDbConnection MyConnection;
                System.Data.OleDb.OleDbCommand myCommand = new System.Data.OleDb.OleDbCommand();
                string sql = null;

      
 string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + outputFileName + ";Extended Properties=Excel 12.0;";
                MyConnection = new System.Data.OleDb.OleDbConnection(connStr);
                MyConnection.Open();
                myCommand.Connection = MyConnection;
      

                if (String.IsNullOrEmpty(p.HomeUrl))
                {
                    p.HomeUrl += " ";
                
                }
                if (String.IsNullOrEmpty(p.SuccessUrl))
                {
                    p.SuccessUrl += " ";

                }
                if (String.IsNullOrEmpty(p.PostCode))
                {
                    p.PostCode += " ";

                }
                if (String.IsNullOrEmpty(p.MarketedFrom))
                {
                    p.MarketedFrom += " ";

                }
                if (String.IsNullOrEmpty(p.PropertyID))
                {
                    p.PropertyID += " ";

                }
                string[] AdressTokens=null;

                if (String.IsNullOrEmpty(p.Address) == false)
                {
                    if (p.Address.Contains("*"))
                    {
                        AdressTokens = p.Address.Split('*');


                        for (int i = 0; i < AdressTokens.Count(); i++)
                        {
                            if (String.IsNullOrEmpty(AdressTokens[i]))
                            {
                                AdressTokens[i] = "-";
                            }
                        }

                            sql = "Insert into [" + sheetName + "$] ([ADDRESS1],[ADDRESS2],[ADDRESS3],[ADDRESS4],[POSTCODE],[PRICE],[DATE MARKETED],[MARKETED BY],[NUMBERS OF DAY],[Success URL],[Homes URL],[MARKETED BY2],[Property Id]) values('" + AdressTokens[0] + "','" + AdressTokens[1] + "','" + AdressTokens[2] + "','" + AdressTokens[3] + "','" + p.PostCode + "','" + p.Price + "','" + p.MarketedFrom + "','" + p.MarketedBy + "','" + p.NumberOfDays + "','" + p.SuccessUrl + "','" + p.HomeUrl + "','" + p.MarketedBy + "','" + p.PropertyID + "')";
                    }
                    else {
                        AdressTokens = p.Address.Split('*');
                        string address1 = AdressTokens[0];
                        string address2 = AdressTokens[1];
                        string address3 = AdressTokens[2];
                        string address4="";
                        for (int i = 2; i < AdressTokens.Length; i++)
                        {
                            address4 += AdressTokens[i] + " ";
                        }
                        sql = "Insert into [" + sheetName + "$] ([ADDRESS1],[ADDRESS2],[ADDRESS3],[ADDRESS4],[POSTCODE],[PRICE],[DATE MARKETED],[MARKETED BY],[NUMBERS OF DAY],[Success URL],[Homes URL],[MARKETED BY2],[Property Id]) values('" + AdressTokens[0] + "','" + AdressTokens[1] + "','" + AdressTokens[2] + "','" + AdressTokens[3] + "','" + p.PostCode + "','" + p.Price + "','" + p.MarketedFrom + "','" + p.MarketedBy + "','" + p.NumberOfDays + "','" + p.SuccessUrl + "','" + p.HomeUrl + "','" + p.MarketedBy + "','" + p.PropertyID + "')";


                    }



                }


                sql = "Insert into [" + sheetName + "$] ([ADDRESS1],[ADDRESS2],[ADDRESS3],[ADDRESS4],[POSTCODE],[PRICE],[DATE MARKETED],[MARKETED BY],[NUMBERS OF DAY],[Success URL],[Homes URL],[MARKETED BY2],[Property Id]) values('" + AdressTokens[0] + "','" + AdressTokens[1] + "','" + AdressTokens[2] + "','" + AdressTokens[3] + "','" + p.PostCode + "','" + p.Price + "','" + p.MarketedFrom + "','" + p.MarketedBy + "','" + p.NumberOfDays + "','" + p.SuccessUrl + "','" + p.HomeUrl + "','" + p.MarketedBy + "','" + p.PropertyID + "')";
                sql = sql.Replace("','','", "','");

                myCommand.CommandText = sql;
                myCommand.ExecuteNonQuery();
                MyConnection.Close();
        }
    }
}
