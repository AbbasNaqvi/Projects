using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.OleDb;
using System.Diagnostics;
using System.IO;

namespace ZooplaNotify
{
    class AccessHandler
    {
        private string FileName { get; set; }

        private string TableName { get; set; }


       public AccessHandler()
        {
            ApplicationData data = Form1.Create;
           FileName= data.AccessDBFileName;
           TableName= data.AccessTableName;
        }



       private void Update(Dictionary<string, string> parameters)
       {
           KillProcesses("MSACCESS");
           OleDbCommand cmd = null;
           Property Record = new Property();
           if (String.IsNullOrEmpty(FileName) || String.IsNullOrEmpty(TableName))
           {
               throw new Exception("File Name OR Table Name can not be null");
           }
           string ConnectionString = GetConnectionString();
           using (OleDbConnection conn = new OleDbConnection(ConnectionString))
           {
               StringBuilder Updatebuilder = new StringBuilder(@"UPDATE [" + TableName + "] SET ");
               foreach (var x in parameters.Keys)
               {
                   Updatebuilder.Append("[" + TableName + "].[" + x + "]=@" + x + ",");
               }
               Updatebuilder.Remove(Updatebuilder.Length - 1, 1);
               Updatebuilder.Append(" WHERE [" + TableName + "].[PropertyID]=@PropertyID");
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
                   oLEDBLog = "Can not Open Connection because " + ex.Message + "\n";
                   return;
               }
               try
               {
                   RowsEffected = cmd.ExecuteNonQuery();
               }
               catch (OleDbException ex)
               {
                   //oLEDBLog = "While ExecutingNonQuery in  Method=AddToDB(),  Class=OledbHandler Message" + ex.Message + "\n";
                   throw new Exception("While ExecutingNonQuery ", ex);
                  // return;
               }
               if (RowsEffected == 0)
               {
                   throw new Exception( "Nothing to update !, Kindly check every thing .because program is not supposed to be here");
               }
               else
               {
                   oLEDBLog = "Row is updated with PropertyID = " + parameters["PropertyID"];
               }
           }
       }


        internal void InsertProperty(Property property)
        {
            property.LastUpdated = DateTime.Now;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add( "PropertyID",property.PropertyID);
            parameters.Add("Title",property.Title);
            parameters.Add( "Price",property.GetPrice().ToString());
            parameters.Add("LastUpdated",property.LastUpdated.ToString());

            Insert(parameters);
        }
        internal void UpdateProperty(Property property)
        {
            property.LastUpdated = DateTime.Now;
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("PropertyID", property.PropertyID);
            parameters.Add("Title", property.Title);
            parameters.Add("Price", property.GetPrice().ToString());
            parameters.Add("LastUpdated", property.LastUpdated.ToString());

            Update(parameters);
        }
        private void Insert(Dictionary<string, string> parameters)
        {
            if (String.IsNullOrEmpty(FileName) || String.IsNullOrEmpty(TableName))
            {
                throw new Exception("File Name OR Table Name can not be null");
            }

            KillProcesses("MSACCESS");
            OleDbCommand cmd=null;
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                StringBuilder builder = new StringBuilder("INSERT INTO [" + TableName + "] (");
                foreach (var x in parameters.Keys)
                {
                    builder.Append(x + ",");
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
                    cmd.Parameters.AddWithValue("@" + x, parameters[x]);
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
                    oLEDBLog = "Row is inserted with ID=" + parameters["PropertyID"] + "\n";
                }
                catch (Exception ex)
                {
                    oLEDBLog = "Can not Run this Query" + ex.Message + "\n";
                    return;
                    //throw new Exception(ex.Message);
                }
            }
        }

        private string GetConnectionString()
        {
            string connectionString = null;

            if (FileName.Contains(".csv"))
            {
                connectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path.GetDirectoryName(FileName) + "" + @";Extended Properties=""Text;HDR=YES;FMT=Delimited""");
            }
            else if (FileName.Contains(".accdb"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Persist Security Info=False";
            }
            else
            {
                connectionString = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + @";Extended Properties=""Excel 12.0 XML;HDR=YES;IMEX=1""";

            }
            // XLSX - Excel 2007, 2010, 2012, 2013
            // XLS - Excel 2003 and Older
            //props["Provider"] = "Microsoft.Jet.OLEDB.4.0";
            //props["Extended Properties"] = "Excel 8.0";
            //props["Data Source"] = "C:\\MyExcel.xls";

            return connectionString.ToString();
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
        public Property GetRecordByID(string ID)
        {

            OleDbCommand cmd;
            KillProcesses("MSACCESS");
            int count = 0;
            Property Record = new Property();
            string ConnectionString = GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                cmd = new OleDbCommand("Select * FROM [" + TableName + "] WHERE [" + TableName + "].[PropertyID]= @id", conn);
                cmd.Parameters.AddWithValue("@id", ID);
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
                        Record.Title = reader["Title"].ToString();
                        double tempprice;

                        if (Double.TryParse(reader["Price"].ToString(), out tempprice) == true)
                        {
                            Record.SetPrice(tempprice, false);
                        }
                        else {

                            throw new Exception("Datebase  is corrupted, You are not allowed to change it manuallyCan not retreive Price");
                        }

                        DateTime tempDateTime=DateTime.Now;
                        if (DateTime.TryParse(reader["LastUpdated"].ToString(), out tempDateTime))
                        {
                            Record.LastUpdated = tempDateTime;
                        }
                        else {
                            throw new Exception("Datebase  is corrupted, You are not allowed to change it manually, Can not retreive Date");                        
                        }

                        count++;
                    }
                }
            }
            if (count > 1)
            {
                oLEDBLog = "Primary Key is violated.\n" + --count + "Records contains same ID";
                //throw new Exception("Primary Key is violated.\n" + --count + " Records contains same ID");
            }
            return Record;
        }

        public string oLEDBLog { get; set; }


    }
}
