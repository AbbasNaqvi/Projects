using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMAC
{
    class Email
    {
        private string emailId;

        public string EmailId
        {
            get { return emailId; }
            set { emailId = value; }
        }

        private string senderEmail;

        public string SenderEmail
        {
            get { return senderEmail; }
            set { senderEmail = value; }
        }

        private DateTime dateandTime;

        public DateTime DateandTime
        {
            get { return dateandTime; }
            set { dateandTime = value; }
        }
        

        private bool isRead;

        public bool IsRead
        {
            get { return isRead; }
            set { isRead = value; }
        }
        


    }
}
