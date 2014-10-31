using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ADOX;
using ADODB;
using System.Text.RegularExpressions;
using System.Data.OleDb;

namespace Listing_Date_Updater
{
    class AccessHandler
    {
        private string fileName;
        private string tableName;
        bool IsNewFile = false;


        /*
         * Author:Abbas Naqvi
         * This class can not be used independently ..It is linked with PropertyInfo
         * To use it for your code ..Replace insertProperty(PropertyInfo property) with forexample InsertCarInfo(CarInfo property) 
         */


        //public
        internal string IsRecordExist(string propertyid, string postaladress, string inputSource)
        {
            MakeFileName(postaladress, inputSource);

            string DBDate = null;
            OleDbCommand cmd = null;
            if (String.IsNullOrEmpty(fileName) || String.IsNullOrEmpty(tableName))
            {
                throw new Exception("File Name OR Table Name can not be null");
            }
            string ConnectionString = GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                string query = "SELECT ListingDate From [" + tableName + "] WHERE PostalCode='" + postaladress + "' AND PropertyID='" + propertyid + "' AND ListingDate IS NOT NULL";


                try
                {
                    conn.Open();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "While Updating In database...");
                }
                try
                {
                    cmd = new OleDbCommand(query, conn);

                    DBDate =(string) cmd.ExecuteScalar();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "While Checking whether Record Exists...");
                }

            }
            return DBDate;
        }
        internal void InsertOrUpdate(PropertyInfo p)
        {
            MakeFileName(p.PostCode, p.InputSource);
            if (IsNewFile == true)
            {
                try
                {
                    InsertProperty(p);
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " AT " + p.PropertyID);
                }
            }
            else
            {
                try
                {
                    if (UpdateProperty(p) == false)
                    {
                        InsertProperty(p);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + " at " + p.PropertyID);
                }
            }
        }

        //private

        private void InsertProperty(PropertyInfo property)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("PropertyID", property.PropertyID);
            parameters.Add("ListingDate", property.MarketedFrom);
            parameters.Add("FoundSource", property.FoundSource);
            parameters.Add("CurrentDate", DateTime.Now.ToString());
            parameters.Add("PostalCode", property.PostCode);
            parameters.Add("Address", property.Address);

            Insert(parameters);
        }
        private bool UpdateProperty(PropertyInfo property)
        {
            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("PropertyID", property.PropertyID);
            parameters.Add("ListingDate", property.MarketedFrom);
            parameters.Add("FoundSource", property.FoundSource);
            parameters.Add("CurrentDate", DateTime.Now.ToString());
            parameters.Add("PostalCode", property.PostCode);
            parameters.Add("Address", property.Address);

            return Update(parameters);
        }
        private string GetConnectionString()
        {
            string connectionString = null;

            if (fileName.Contains(".csv"))
            {
                connectionString = string.Format(@"Provider=Microsoft.Jet.OleDb.4.0; Data Source=" + Path.GetDirectoryName(fileName) + "" + @";Extended Properties=""Text;HDR=YES;FMT=Delimited""");
            }
            else if (fileName.Contains(".accdb"))
            {
                connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Persist Security Info=False";
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
        //MakeFileName also create the Access File
        private void MakeFileName(string postalcode,string inputsource)
        {

            string Suffix = null;
            //Argument Exception
            Suffix = Regex.Match(postalcode, "^(?<data>\\D*?)\\d", RegexOptions.IgnorePatternWhitespace | RegexOptions.Singleline).Groups["data"].Value;
            if (Directory.Exists("Database") == false)
            {
                throw new Exception("Kindly Create the database First ..You are not allowed to Go further");
//                Directory.CreateDirectory("Database");
            }

            /*
             * Dont touch this code ...This creates the database
             */


            if (Directory.Exists("Database\\" + inputsource) == false)
            {
                throw new Exception("Database for This input source is not present");
               // Directory.CreateDirectory("Database\\" + inputsource);
            }
            fileName = "Database\\" + inputsource +"\\" + Suffix + ".accdb";
            tableName = Suffix;
            //if (File.Exists(fileName) == false)
            //{

            //    ADOX.Catalog cat = new ADOX.Catalog();
            //    ADOX.Table table = new ADOX.Table();

            //    //Create the table and it's fields. 
            //    table.Name = Suffix;
            //    table.Columns.Append("PropertyID");
            //    table.Columns.Append("ListingDate");
            //    table.Columns.Append("FoundSource");
            //    table.Columns.Append("CurrentDate");
            //    table.Columns.Append("PostalCode");
            //    table.Columns.Append("Address");

            //    cat.Create("Provider=Microsoft.Jet.OLEDB.4.0;" + "Data Source=" + fileName + "; Jet OLEDB:Engine Type=5");
            //    cat.Tables.Append(table);

            //    //Now Close the database
            //    ADODB.Connection con = cat.ActiveConnection as ADODB.Connection;
            //    if (con != null)
            //        con.Close();

            //    cat = null;
            //    IsNewFile = true;

        //    }
        } 
        private bool Update(Dictionary<string, string> parameters)
        {
            //   KillProcesses("MSACCESS");
            OleDbCommand cmd = null;
            if (String.IsNullOrEmpty(fileName) || String.IsNullOrEmpty(tableName))
            {
                throw new Exception("File Name OR Table Name can not be null");
            }
            string ConnectionString = GetConnectionString();
            using (OleDbConnection conn = new OleDbConnection(ConnectionString))
            {
                StringBuilder Updatebuilder = new StringBuilder(@"UPDATE [" + tableName + "] SET ");
                foreach (var x in parameters.Keys)
                {
                    Updatebuilder.Append("[" + tableName + "].[" + x + "]=@" + x + ",");
                }
                Updatebuilder.Remove(Updatebuilder.Length - 1, 1);
                Updatebuilder.Append(" WHERE [" + tableName + "].[PropertyID]=@PropertyID AND [" + tableName + "].[PostalCode]=@PostalCode");
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
                    throw new Exception(ex.Message + "While Updating In database...");
                }
                try
                {
                    RowsEffected = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "While Updating In database...");
                }
                if (RowsEffected == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
        private void Insert(Dictionary<string, string> parameters)
        {
            if (String.IsNullOrEmpty(fileName) || String.IsNullOrEmpty(tableName))
            {
                throw new Exception("File Name OR Table Name can not be null");
            }

            //   KillProcesses("MSACCESS");
            OleDbCommand cmd = null;
            using (OleDbConnection conn = new OleDbConnection(GetConnectionString()))
            {
                StringBuilder builder = new StringBuilder("INSERT INTO [" + tableName + "] (");
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
                    throw new Exception(ex.Message + "While Updating In database...");

                }
                try
                {
                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message + "While Updating In database...");

                }
            }


        }
    }
}
