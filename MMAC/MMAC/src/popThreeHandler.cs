using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;
namespace MMAC
{
    class popThreeHandler
    {

        public bool GetEmail(string userName, string password, string server, string port)
        {
            waspPOP3 emailsFactory = new waspPOP3();
            emailsFactory.DoConnect("pop.gmail.com",995, "abbas.naqvi@dynamologic.com", "DisneyJob");
             string EmailFactory= emailsFactory.GetStat();
            string EmailsText= emailsFactory.Retr(1);
            return false;
        }

    }
}
