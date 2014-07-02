using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;


namespace ConsoleApplication1
{
    class MaileHandler
    {


        public string SendGmail(string From, string to, string Subject, string body,string password)
        {

            try
            {
                MailMessage mail = new MailMessage(From,to);
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

//                mail.From = new MailAddress(From);
  //              mail.To.Add(to);
                mail.Subject = Subject;
                mail.Body = body;

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential(From,password);
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
