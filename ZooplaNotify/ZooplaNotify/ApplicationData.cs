using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ZooplaNotify
{
    [Serializable]
   public class ApplicationData
    {
        private string accessDB;

        public string AccessDBFileName
        {
            get { return "C://Users//jafar.baltidynamolog//Documents//PropertiesDB.accdb"; }
            set { accessDB = value; }
        }

        private string accessTableName;

        public string AccessTableName
        {
            get { return "Property"; }
            set { accessTableName = value; }
        }

        private string initialUrl;

        public string InitialUrl
        {
            get { return initialUrl; }
            set { initialUrl = value; }
        }

        private string emailId;

        public string EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

       public List<string> To = new List<string>();

        private string host;

        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        private int runAfter;

        public int RunAfter
        {
            get { return runAfter; }
            set { runAfter = value; }
        }

        private int port;

        public int Port
        {
            get { return port; }
            set { port = value; }
        }
        


    }
}
