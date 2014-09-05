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
        OleDbCommand cmd;
        DataTable dt = new DataTable();
        OleDbDataAdapter da = null;
        string FileName;

        PropertyLog PropertiesObj = PropertyLog.Create;
        PostalAdressLog PostalAdressObj = PostalAdressLog.Create;


        public OledbHandler(string filename)
        {
            FileName = filename;
        
        }


        private string GetConnectionString(string fileName)
        {
            string connectionString = null;

            if (fileName.Contains(".csv"))
            {
                connectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path.GetDirectoryName(fileName) + "" + @";Extended Properties=""Text;HDR=YES;FMT=Delimited""");
            }
            else if (fileName.Contains(".accdb"))
            {
                connectionString =
        @"Provider=Microsoft.Jet.OLEDB.4.0;" +
        @"Data Source=" + fileName + ""; 
            
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
            int count = 0;
            Property Record = new Property();
            string ConnectionString = GetConnectionString(FileName);
            using (OleDbConnection conn = new OleDbConnection())
            {
                conn.Open();
                cmd = new OleDbCommand("Select * FROM SPHFile WHERE PropertyID= @id");
                cmd.Parameters.AddWithValue("id", ID);
                OleDbDataReader reader = cmd.ExecuteReader();

                if (reader.HasRows == false)
                {
                    Record = null;
                }
                else
                {
                    while (reader.Read())
                    {
                        Record.Source = reader["Source"].ToString();
                        Record.FullAddress = reader["FullAdress"].ToString();
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
                throw new Exception("Primary Key is violated.\n" + --count + " Records contains same ID");
            }

            return Record;
        }
        private void Insert()
        {
 
        
        
        
        
        }
        public void AddToDB(Dictionary<string,string> parameters)
        {
            Property Record = new Property();
            if (String.IsNullOrEmpty(FileName))
            {
                throw new Exception("File Name can not be null ");
            }
            string ConnectionString = GetConnectionString(FileName);
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {

                conn.Open();

                StringBuilder Updatebuilder = new StringBuilder("UPDATE @FileName SET ");
                foreach (var x in parameters.Keys)
                {
                    Updatebuilder.Append(x + "=@"+x+",");
                }
                Updatebuilder.Remove(Updatebuilder.Length - 2, 1);
                cmd = new OleDbCommand(Updatebuilder.ToString());
                cmd.Parameters.AddWithValue("@FileName", FileName);
                foreach (var x in parameters.Keys)
                {
                    cmd.Parameters.AddWithValue("@" + x, parameters[x]);
                }
                

                #region Adding Update Parameters
                //if (parameters.ContainsKey("Source"))
                //{
                //    cmd.Parameters.AddWithValue("Source", parameters["Source"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("Source", null);
                //}

                //if (parameters.ContainsKey("FullAddress"))
                //{
                //    cmd.Parameters.AddWithValue("FullAddress", parameters["FullAddress"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("FullAddress", null);
                //}


                //if (parameters.ContainsKey("StreetAddress"))
                //{
                //    cmd.Parameters.AddWithValue("StreetAddress", parameters["StreetAddress"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("StreetAddress", null);
                //}



                //if (parameters.ContainsKey("PostalCode"))
                //{
                //    cmd.Parameters.AddWithValue("PostalCode", parameters["PostalCode"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("PostalCode", null);
                //}



                //if (parameters.ContainsKey("Price"))
                //{
                //    cmd.Parameters.AddWithValue("Price", parameters["Price"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("Price", null);
                //}



                //if (parameters.ContainsKey("MarketedDate"))
                //{
                //    cmd.Parameters.AddWithValue("MarketedDate", parameters["MarketedDate"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("MarketedDate", null);
                //}


                //if (parameters.ContainsKey("MarketedBy"))
                //{
                //    cmd.Parameters.AddWithValue("MarketedBy", parameters["MarketedBy"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("MarketedBy", null);
                //}


                //if (parameters.ContainsKey("SuccessURL"))
                //{
                //    cmd.Parameters.AddWithValue("SuccessURL", parameters["SuccessURL"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("SuccessURL", null);
                //}


                //if (parameters.ContainsKey("AddedOn"))
                //{
                //    cmd.Parameters.AddWithValue("AddedOn", parameters["AddedOn"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("AddedOn", null);
                //}


                //if (parameters.ContainsKey("LastSaleDate"))
                //{
                //    cmd.Parameters.AddWithValue("LastSaleDate", parameters["LastSaleDate"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("LastSaleDate", null);
                //}



                //if (parameters.ContainsKey("LastSalePrice"))
                //{
                //    cmd.Parameters.AddWithValue("LastSalePrice", parameters["LastSalePrice"]);
                //}
                //else
                //{
                //    cmd.Parameters.AddWithValue("LastSalePrice", null);
                //}
                #endregion
            
                
                int RowsEffected = 0;

                try
                {
                   RowsEffected=cmd.ExecuteNonQuery();
                }
                catch (OleDbException ex)
                {
                    throw new Exception("While ExecutingNonQuery in  Method=AddToDB(),  Class=OledbHandler",ex);
                }

                if (RowsEffected == 0)
                {
                    StringBuilder builder = new StringBuilder("INSERT INTO @FileName VALUES(");
                    foreach (var x in parameters.Keys)
                    {
                        builder.Append("@" + x+",");
                  
                    }
                    builder.Remove(builder.Length - 2, 1);
                    cmd=new OleDbCommand(builder.ToString());
                    cmd.Parameters.AddWithValue("@FileName", FileName);
                    foreach (var x in parameters.Keys)
                    {
                        cmd.Parameters.AddWithValue("@" + x,parameters[x]);
                    }

                    cmd.ExecuteNonQuery();
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

        public string ReadExcelFileSingle(string fileName, string sheetName, string columnNames)
        {
            string pid = null;
            string ConsoleLog = null;
            KillProcesses("EXCEL");
            string ConnectionString = GetConnectionString(fileName);
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
        private Process[] GetAllProcess(string processname)
        {
            Process[] aProc = Process.GetProcessesByName(processname);

            if (aProc.Length > 0)
                return aProc;

            else return null;
        }
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
