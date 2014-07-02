// POP3 manage class
// Karavaev Denis karavaev_denis@hotmail.com
// http://wasp.elcat.kg
///////////////////////////////////////////////////

using System;
using System.Net;
using System.Net.Sockets;
using System.IO;

namespace MMAC
{
    /// <summary>
    /// Summary description for waspPOP3.
    /// </summary>
    class waspPOP3
    {
        // variables for pop3 class
        public String pop3host;
        public int port;
        public String user;
        public String pwd;
        public String command;
        public TcpClient w_TcpClient;
        public NetworkStream w_NetStream;
        public StreamReader w_ReadStream;
        public byte[] bData;	//for the data, tat we'll recive

        public string DoConnect(String pop3host, int port, String user, String pwd)
        {

            string Result = null;
            // create POP3 connection
            w_TcpClient = new TcpClient(pop3host, port);           
            try
            {	// initialization
                w_NetStream = w_TcpClient.GetStream();
                w_NetStream.ReadTimeout = 500;
                w_ReadStream = new StreamReader(w_TcpClient.GetStream());
                try
                {
                    Result += w_ReadStream.ReadLine()+"\n";
                    Result += "No error while Making Connection";
                }
                catch (IOException e)
                {
                    Result+="Error while creating Connection" + e.Message+"\n";
//                    throw new Exception("Error while creating Connection" + e.Message);            
                }// send login
                command = "USER " + user + "\r\n";
                bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
                try
                {
                    w_NetStream.Write(bData, 0, bData.Length);
                    Result += w_ReadStream.ReadLine()+"\n";
                    Result += "No error while sending username";
                }
                catch (IOException e)
                {
                    Result += "Error while sending username" + e.Message+"\n";
                    //throw new Exception("exception while sending username" + e.Message);
                }
                //// send pwd
                command = "PASS " + pwd + "\r\n";
                bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
                try
                {

                    w_NetStream.Write(bData, 0, bData.Length);
                    w_ReadStream.ReadLine();
                    Result += "No error while sending password";
                }
                catch (IOException e)
                {
                    Result += "Error while sending Password" + e.Message + "\n";
                  //  throw new Exception("Exception while Sending Password" + e.Message);
                }

            }
            catch (InvalidOperationException err)
            {
                Result = "Invalid Operation Error" + err.Message;
///                return ("Error: " + err.ToString());
            }
            return Result;
        }
        public string GetStat()
        {
            string Result = null;

            try
            {
            // Send STAT command to get number of mail and total size
            command = "STAT\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
                w_NetStream.Write(bData, 0, bData.Length);
               Result+= w_ReadStream.ReadLine();
               Result += "STATS WORKS TOTALLY FINE\n";
            }
            catch (IOException e)
            {
                Result += "In GetStat" + e.Message;
              //  throw new Exception(e.Message);
            }
            return Result;

        }

        public string GetList()// Send LIST command with no parametrs to get all information
        {
            string Result=null;
            string sTemp=null;	// For saving 'list' results
            string sList = "";
            command = "LIST\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());

            try
            {
                w_NetStream.Write(bData, 0, bData.Length);
                sTemp = w_ReadStream.ReadLine();
                Result += "Read Stemp\n";
            }
            catch (Exception e)
            {
                Result += "\nNot Read sTemp..."+e.Message+sTemp+"...";
            } 
            if (sTemp != null)
            {

                Result += "STATS IS NOT NULL\n";
                if (sTemp[0] != '-')	// errors begins with '-'
                {
                    while (sTemp != ".")	//saving data to string while not found '.'
                    {
                        try
                        {
                            sList += sTemp + "\r\n";
                            sTemp = w_ReadStream.ReadLine();
                        }
                        catch (Exception e)
                        {
                            Result += "Error inside the loop\n"+e.Message;
                        }
                    }
                }
                else
                {
                    return sTemp+Result;
                }
            }
            return sList+Result;
        }
        public string GetList(int num)// Send LIST command with number of a letter
        {
            command = "LIST " + num + "\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            try
            {
                w_NetStream.Write(bData, 0, bData.Length);
                return w_ReadStream.ReadLine();
            }
            catch (Exception)
            {
                return "IO Exception in GetList";
            
            }
        }
        public string Retr(int num)
        {
            string sTemp=null;
            string sBody = "";
            string Result = "";
            try
            {
                command = "RETR " + num + "\r\n";
                bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());

                try
                {
                    w_NetStream.Write(bData, 0, bData.Length);
                    sTemp = w_ReadStream.ReadLine();
                }
                catch (IOException)
                {
                    Result += "Error while Reading Stemp in Retr(int num)\n";
                }
                
                if (String.IsNullOrEmpty(sTemp))
                {
                    Result += "sTemp is null in Retr(int num)\n";
                }
                
                else if(sTemp[0] != '-')	//errors begins with -
                {
                    while (sTemp != ".")	// . - is the end of the server response
                    {
                        try
                        {
                            sBody += sTemp + "\r\n";
                            sTemp = w_ReadStream.ReadLine();
                        }
                        catch (IOException)
                        {
                            Result += "Error in While Loop\n in Retr(num)";
                        }
                    }
                }
                else
                {
                    return sTemp+Result;
                }
            }
            catch (InvalidOperationException err)
            {
                Result+="Error: " + err.ToString()+"\n in Retr(num)";
            }
            return sBody+Result;
        }
        public string Dele(int num)
        {
            // Send DELE command to delete message with specified number
            command = "DELE " + num + "\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            w_NetStream.Write(bData, 0, bData.Length);
            return w_ReadStream.ReadLine();
        }
        public string Rset()
        {
            // Send RSET command to unmark all deleteting messages
            command = "RSET\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            w_NetStream.Write(bData, 0, bData.Length);
            return w_ReadStream.ReadLine();
        }

        public string Quit()
        {
            // Send QUIT
            command = "QUIT\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            w_NetStream.Write(bData, 0, bData.Length);
            String tmp = w_ReadStream.ReadLine();
            w_NetStream.Close();
            w_ReadStream.Close();
            return tmp;
        }
        public string GetTop(int num)
        {
            string Result = null;
            string sTemp=null;
            string sTop = "";
            try
            {
                // retrieve mail with number mail parameter
                command = "TOP " + num + " n\r\n";
                bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
                try
                {
                    w_NetStream.Write(bData, 0, bData.Length);
                    sTemp = w_ReadStream.ReadLine();
                }
                catch (IOException)
                {
                    Result += "IO error while Reading Stream\n";
                }
                if (String.IsNullOrEmpty(sTemp))
                {
                    Result += "sTemp is null in GetTop\n";                
                }
                else if (sTemp[0] != '-')
                {
                    while (sTemp != ".")
                    {
                        try
                        {
                            sTop += sTemp + "\r\n";
                            sTemp = w_ReadStream.ReadLine();
                        }
                        catch (IOException e)
                        {
                            Result +="ioexception in loop"+e.Message+"\n";                        
                        }
                    }
                }
                else
                {
                    return sTemp+Result;
                }
            }
            catch (InvalidOperationException err)
            {
                return ("Error: " + err.ToString());
            }
            return sTop;
        }
        public string GetTop(int num_mess, int num_strok)
        {
            string sTemp;
            string sTop = "";
            try
            {
                // retrieve mail with number mail parameter
                command = "TOP " + num_mess + " " + num_strok + "\r\n";
                bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
                w_NetStream.Write(bData, 0, bData.Length);

                sTemp = w_ReadStream.ReadLine();
                if (sTemp[0] != '-')
                {
                    while (sTemp != ".")
                    {
                        sTop += sTemp + "\r\n";
                        sTemp = w_ReadStream.ReadLine();
                    }
                }
                else
                {
                    return sTemp;
                }
            }
            catch (InvalidOperationException err)
            {
                return ("Error: " + err.ToString());
            }
            return sTop;
        }
        public string GetUidl()
        {
            string Result = null;
            string sTemp=null;
            string sUidl = "";
            command = "UIDL\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            try
            {
                w_NetStream.Write(bData, 0, bData.Length);
                sTemp = w_ReadStream.ReadLine();
            }
            catch (IOException)
            {
                Result += "Exception while Reading UUIDTYpe,initialization\n";
            }
            if (String.IsNullOrEmpty(sTemp))
            {
                Result += "sTemp is null in UUId\n";
            }
            else if (sTemp[0] != '-')	// errors begins with '-'
            {
                while (sTemp != ".")	//saving data to string while not found '.'
                {
                    try
                    {
                        sUidl += sTemp + "\r\n";
                        sTemp = w_ReadStream.ReadLine();
                    }
                    catch (IOException)
                    {
                        Result += "Exception in loop UUIDTYpe\n";
                    }
                }
            }
            else
            {
                return sTemp+Result;
            }
            return sUidl+Result;
        }
        public string GetUidl(int num)
        {
            command = "UIDL " + num + "\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            w_NetStream.Write(bData, 0, bData.Length);
            return w_ReadStream.ReadLine();
        }
        public string GetNoop()
        {
            // Send NOOP command to check if we are connected
            command = "NOOP\r\n";
            bData = System.Text.Encoding.ASCII.GetBytes(command.ToCharArray());
            try
             {
                 w_NetStream.Write(bData, 0, bData.Length);
                 return w_ReadStream.ReadLine();
            }
            catch (IOException)
            {
                return "Exception while Reading GetNoop()";
            }
        }
    }
}
