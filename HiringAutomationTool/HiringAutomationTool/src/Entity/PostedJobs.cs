using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
    [Serializable()]
    class PostedJobs
    {
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        private string date;

        public string Date
        {
            get { return date; }
            set { date = value; }
        }
        private string link;

        public string Link
        {
            get { return link; }
            set { link = value; }
        }
        private string totalApplicants;

        public string TotalApplicants
        {
            get { return totalApplicants; }
            set { totalApplicants = value; }
        }
        

        private string encryptedJobID;

        public string EncryptedJobID
        {
            get { return encryptedJobID; }
            set { encryptedJobID = value; }
        }

        private string status;

        public string Status
        {
            get { return status; }
            set { status = value; }
        }
        
    }
}
