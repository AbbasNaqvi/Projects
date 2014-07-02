using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Mail;

namespace Mailbox_Monitoring_and_Auto_Call
{
    class RebexMailHandler
    {

        public string SendGmail(string From, string to, string Subject, string body)
        {

            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress(From);
                mail.To.Add(to);
                mail.Subject = Subject;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("username", "password");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);
                return "SUCCESS---Mail is Sent";
            }
            catch (Exception ex)
            {

                return "FAILED---Mail is not Sent" + ex.ToString();
            }



        }


    }
}
