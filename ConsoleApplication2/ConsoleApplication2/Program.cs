using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Data;
using System.Diagnostics;
namespace ConsoleApplication2
{
    class Program
    {

        DataSet ds = new DataSet();
        OleDbCommand cmd = new OleDbCommand();
        DataTable dt = new DataTable();
        OleDbDataAdapter da = null;
        public void PrintAllValues()
        {
            if (ds.Tables[0] != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r != null&&string.IsNullOrWhiteSpace(r["STM"].ToString())==false)
                    {
                        Console.WriteLine("--->" + r["STM"]);
                    }
                }
            }
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
        private void ReadExcelFileSingle(string fileName,string sheetName,string columnName)
        {
            string ConnectionString = GetConnectionString("D:\\Week185_SampleFile_Z5.xlsx");
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]  ";
                var x = cmd.ExecuteReader();
                while (x.Read())
                {
                    if (String.IsNullOrWhiteSpace(x[0].ToString()) == false)
                    {
                        Console.WriteLine(x[0]+" ,"+ x[9].ToString() +","+x[9].ToString().Remove(0,40));
                    }
                }
                cmd = null;
                conn.Close();
            }
        }
        private void ReadPAFFileSingle(string fileName, string sheetName, string PCD)
        {
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "] WHERE PCD = '"+PCD+"'";
                var x = cmd.ExecuteReader();
                while (x.Read())
                {
                    if (String.IsNullOrWhiteSpace(x["STM"].ToString()) == false)
                    {
                        Console.WriteLine(x[0] + " ," + x["STM"].ToString());
                    }
                }
                cmd = null;
                conn.Close();
            }
        }
        private DataSet ReadPAFFile(string fileName, string sheetName,string PCD)
        {
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]  WHERE PCD = '"+PCD+"'";
                dt.TableName = sheetName;
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                ds.Tables.Add(dt);
                Console.WriteLine(ds.Tables.Count);
                cmd = null;
                conn.Close();
                return ds;
            }
        }

        private DataSet ReadExcelFile(string fileName, string sheetName, string columnName)
        {
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                cmd.CommandText = "SELECT "+columnName+" FROM [" + sheetName + "]";
                dt.TableName = sheetName;
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                ds.Tables.Add(dt);
                Console.WriteLine(ds.Tables.Count);
                cmd = null;
                conn.Close();
                return ds;
            }
        }
        private string GetConnectionString(string fileName)
        {
           // XLSX - Excel 2007, 2010, 2012, 2013
            string ConnectionString=@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+fileName+@";Extended Properties=""Excel 12.0 XML;HDR=YES;IMEX=1""";
        
            
            
            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "C:\\MyExcel.xls";

            return ConnectionString.ToString();
        }

        static void Main(string[] args)
        {
            
        //    Program P = new Program();
        //    P.KillProcesses("EXCEL");
        //  //  P.ReadExcelFileSingle("D:\\Week185_SampleFile_Z5.xlsx","No Addresses Found$","POSTCODE' ,'ZOOPLA URL'");
        //  //  P.ReadPAFFile("D://Sample PAF File.xlsx", "sample_paf$", "AB10 1AJ");
        //    P.ReadPAFFileSingle("D://Sample PAF File.xlsx", "sample_paf$", "AB10 1AJ");
        //    //   P.ReadExcelFile("D:\\Week185_SampleFile_Z5.xlsx", "No Addresses Found$", "POSTCODE");
        ////    P.PrintAllValues();
            CsvFileReader handler = new CsvFileReader("D:\\PAF\\BD\\BD\\BD1.csv");
            
            
            CsvRow row = new CsvRow();
            int count = 0;
            while(handler!=null)
            {
                handler.ReadRow(row);
                if (row.Count == 0||count>=10)
                {
                    break;
                }
                for(int i=0;i<row.Count;i++)
                {
                    Console.WriteLine("row["+i+"]= "+row[i]);               
                }
/*                foreach (var x in row)
                {
                    Console.WriteLine(row[count])
                        
                }*/
                Console.WriteLine();
                count++;

            }
            
         
            




        }
    }
}
