using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mailbox_Monitoring_and_Auto_Call
{
    class Email
    {
        private string id;

        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string subject;

        public string Subject
        {
            get { return subject; }
            set { subject = value; }
        }


        private string senderName;

        public string SenderName
        {
            get { return senderName; }
            set { senderName = value; }
        }

        private DateTime receivingTime;

        public DateTime ReceivingTime
        {
            get { return receivingTime; }
            set { receivingTime = value; }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private bool  isCalled;

        public bool  IsCalled
        {
            get { return isCalled; }
            set { isCalled = value; }
        }
        
        
    }
}
