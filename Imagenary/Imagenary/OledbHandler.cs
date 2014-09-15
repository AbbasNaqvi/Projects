using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace Imagenary
{
    class OledbHandler
    {
      DataSet ds = new DataSet();
      private OleDbCommand cmd;
      private DataTable dt = new DataTable();
      private OleDbDataAdapter da = null;
      private string FileName;
      private string TableName;

        PropertyLog PropertiesObj = PropertyLog.Create;
        PostalAdressLog PostalAdressObj = PostalAdressLog.Create;
        private string oLEDBLog;

        public string OLEDBLog
        {
            get { return oLEDBLog; }
            set { oLEDBLog = value; }
        }
        
        /*
         * If Table name is same as FileName ,Second argument can be null
         * 
         */ 
        public OledbHandler(string filename,string tablename)
        {
            FileName = filename;
            if (string.IsNullOrEmpty(tablename))
            {
                TableName = filename;

            }
            else {
                TableName = tablename;            
            }
        }

        /*
         * Method takes filename and Returns ConnectionString
         * More ConnectionStrings can be added or changed for required application version
         */ 
        private string GetConnectionString(string fileName)
        {
            string connectionString = null;

            if (fileName.Contains(".csv"))
            {
                connectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path.GetDirectoryName(fileName) + "" + @";Extended Properties=""Text;HDR=YES;FMT=Delimited""");
            }
            else if (fileName.Contains(".accdb"))
            {
                connectionString ="Provider=Microsoft.ACE.OLEDB.12.0;Data Source="+fileName+";Persist Security Info=False"; 
            }
            else 
            {
                connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + @";Extended Properties=""Excel 12.0 XML;HDR=YES;IMEX=1""";

            }
            // XLSX - Excel 2007, 2010, 2012, 2013
            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "C:\\MyExcel.xls";

            return connectionString.ToString();
        }


        public Property GetRecordByID(string ID)
        {
            KillProcesses("MSACCESS");
            int count = 0;
            Property Record = new Property();
            string ConnectionString = GetConnectionString(FileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {              
                cmd = new OleDbCommand("Select * FROM ["+TableName+"] WHERE ["+TableName+"].[PropertyID]= @id",conn);
                cmd.Parameters.AddWithValue("@id",ID);
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    oLEDBLog = "Can not Open Connection because " + ex.Message + "\n";
                    return null;
                }                
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == false)
                {
                    Record = null;
                }
                else
                {

                    while (reader.Read())
                    {
                        Record.PropertyID = ID;
                        Record.Source = reader["Source"].ToString();
                        Record.FullAddress = reader["FullAddress"].ToString();
                        Record.StreetAddress = reader["StreetAddress"].ToString();
                        Record.PostalCode = reader["PostalCode"].ToString();
                        Record.Price = reader["Price"].ToString();
                        Record.MarketedDate = reader["MarketedDate"].ToString();
                        Record.MarketedBy = reader["MarketedBy"].ToString();
                        Record.SuccessURL = reader["SuccessURL"].ToString();
                        Record.AddedOn = reader["AddedOn"].ToString();
                        Record.LastSaleDate = reader["LastSaleDate"].ToString();
                        Record.LastSalePrice = reader["LastSalePrice"].ToString();
                        count++;
                    }
                }
            }
            if (count > 1)
            {
                oLEDBLog="Primary Key is violated.\n"+ --count + "Records contains same ID";
                //throw new Exception("Primary Key is violated.\n" + --count + " Records contains same ID");
            }
            return Record;
        }
        public void AddToDB(Dictionary<string, string> parameters)
        {
            KillProcesses("MSACCESS");
            Property Record = new Property();
            if (String.IsNullOrEmpty(FileName) || String.IsNullOrEmpty(TableName))
            {
                oLEDBLog = "File Name OR Table Name can not be null\n";
                return;
                //throw new Exception("File Name OR Table Name can not be null");
            }
            string ConnectionString = GetConnectionString(FileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                StringBuilder Updatebuilder = new StringBuilder(@"UPDATE [" + TableName + "] SET ");
                foreach (var x in parameters.Keys)
                {
                    Updatebuilder.Append("[" + TableName + "].[" + x + "]=@" + x + ",");
                }
                Updatebuilder.Remove(Updatebuilder.Length - 1, 1);
                Updatebuilder.Append(" WHERE ["+TableName+"].[PropertyID]=@PropertyID");
                cmd = new OleDbCommand(Updatebuilder.ToString(), conn);
                foreach (var x in parameters.Keys)
                {
                    cmd.Parameters.AddWithValue("@" + x, parameters[x]);
                }
                int RowsEffected = 0;
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    oLEDBLog = "Can not Open Connection because " + ex.Message+"\n";
                    return;
                }
                try
                {
                    RowsEffected = cmd.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    oLEDBLog="While ExecutingNonQuery in  Method=AddToDB(),  Class=OledbHandler Message"+ex.Message+"\n";
                    //throw new Exception("While ExecutingNonQuery in  Method=AddToDB(),  Class=OledbHandler", ex);
                    return;
                }
                if (RowsEffected == 0)
                {
                    Insert(parameters);
                }
                else {
                    oLEDBLog = "Row is updated with PropertyID = "+parameters["PropertyID"];
                }
           }            
        }
        private void Insert(Dictionary<string,string> parameters)
        {
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString(FileName)))
            {
                StringBuilder builder = new StringBuilder("INSERT INTO ["+ TableName +"] (");
                foreach (var x in parameters.Keys)
                {
                    builder.Append(x+",");
                }
                builder.Remove(builder.Length - 1, 1);                
                builder.Append(") VALUES(");
                foreach (var x in parameters.Keys)
                {
                    builder.Append("@" + x + ",");

                }
                builder.Remove(builder.Length - 1, 1);
                builder.Append(")");
               cmd = new OleDbCommand(builder.ToString(), conn);
                foreach (var x in parameters.Keys)
                {
                    cmd.Parameters.AddWithValue("@"+x ,parameters[x]);
                }
                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    oLEDBLog = "Can not Open Connection because " + ex.Message + "\n";
                    return;   
                }
                try
                {
                    cmd.ExecuteNonQuery();
                    oLEDBLog = "Row is inserted with ID="+parameters["PropertyID"]+"\n";
                }
                catch (Exception ex)
                {
                    oLEDBLog = "Can not Run this Query" + ex.Message + "\n";
                    return;
                    //throw new Exception(ex.Message);
                }
            }
        }
     
        #region CloseOpenFile
        private Process[] GetAllProcess(string processname)
        {
            Process[] aProc = Process.GetProcessesByName(processname);

            if (aProc.Length > 0)
                return aProc;

            else return null;
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
        #endregion
        public string ReadExcelFileSingle(string fileName, string sheetName, string columnNames)
        {
            string pid = null;
            string ConsoleLog = null;
            KillProcesses("EXCEL");
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                cmd = new OleDbCommand();
                cmd.Connection = conn;
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]  ";
                conn.Open();
                var x = cmd.ExecuteReader();
                while (x.Read())
                {
                    if (String.IsNullOrWhiteSpace(x[0].ToString()) == false)
                    {
                        string po = x["POSTCODE"].ToString().Replace(" ", "");
                        int length = po.Length;
                        string postalcode = po.Insert(length - 3, " ");
                        pid = x["ZOOPLA URL"].ToString();
                        if (PostalAdressObj.adressList.ContainsKey(pid) == false)
                        {
                            PostalAdressObj.Add(new PostalAdress() { PCD = postalcode, PID = pid });
                        }
                        else
                        {

                            PostalAdressObj.adressList[pid].PCD = postalcode;
                        }
                    }
                }
                cmd = null;
                conn.Close();
            }
            return ConsoleLog;
        }

        #region Reading PAF file Old Code
        [Obsolete]
        public DataSet ReadPAFFile(string fileName, string sheetName, string PCD)
        {
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                cmd.CommandText = "SELECT * FROM [" + sheetName + "]  WHERE PCD = '" + PCD + "'";
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
        [Obsolete]
        public string ReadPAFFileSingle(string fileName, string sheetName, PostalAdress poobj)
        {
            string ConsoleLog = null;
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                //+"."+ sheetName + 
                string query = "SELECT * FROM [" + Path.GetFileName(fileName) + "] WHERE  = '" + poobj.PCD.ToUpper() + "'";
                cmd = new OleDbCommand(query, conn);

                //                cmd.Parameters.AddWithValue("@p1",Path.GetFileName(fileName));
                //              cmd.Parameters.AddWithValue("@p2", poobj.PCD.ToUpper());

                var x = cmd.ExecuteReader();
                while (x.Read())
                {
                    if (String.IsNullOrWhiteSpace(x[11].ToString()) == false)
                    {
                        Property p = new Property();
                        p.DateAdded = DateTime.Now;
                        try
                        {
                            p.ORD = x[0].ToString();
                            ConsoleLog += "Added" + p.ORD + "\n";
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");
                        }
                        try
                        {
                            p.ORC = x[1].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.SBN = x[2].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.BNA = x[3].ToString();
                            //      ConsoleLog += "Added" + p.BNA + "\n";
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.POB = x[4].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.NUM = x[5].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DST = x[6].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.STM = x[7].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DDL = x[8].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DLO = x[9].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.PTN = x[10].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.PCD = x[11].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.CTA = x[12].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.CTP = x[13].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.CTT = x[14].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.SCD = x[15].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.CAT = x[16].ToString();
                            //             ConsoleLog += "Added" + p.CAT + "\n";

                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.NDP = x[17].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DPX = x[18].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");
                        }

                        try
                        {
                            p.URN = x[19].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");
                        }


                        try
                        {
                            p.PID = poobj.PID;
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }




                        PropertiesObj.Add(p);
                        PostalAdressObj.Add(p);
                        //adressList.Add(p.PID, p);
                        PostalAdressObj.adressList[p.PID].AddPropertyKey(p.PID);
                    }
                }
                cmd = null;
                conn.Close();
            }
            return ConsoleLog;
        }
        [Obsolete]
        public DataSet ReadExcelFile(string fileName, string sheetName, string columnNames)
        {
            KillProcesses("EXCEL");
            string ConnectionString = GetConnectionString(fileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                conn.Open();
                cmd.Connection = conn;
                DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                cmd.CommandText = "SELECT " + columnNames + " FROM [" + sheetName + "]";
                dt.TableName = sheetName;
                da = new OleDbDataAdapter(cmd);
                da.Fill(dt);
                ds.Tables.Add(dt);
                cmd = null;
                conn.Close();
                return ds;
            }
        }
        [Obsolete]
        public void PrintAllValues()
        {
            PostalAdressLog log = PostalAdressLog.Create;
            if (ds.Tables[0] != null)
            {
                foreach (DataRow r in dt.Rows)
                {
                    if (r != null && string.IsNullOrWhiteSpace(r[0].ToString()) == false)
                    {
                        string po = r[0].ToString();
                        if (log.adressList.ContainsKey(po))
                        {
                            log.Add(new PostalAdress() { PCD = po });
                        }
                    }
                }
            }
        }
        #endregion
    }
}
