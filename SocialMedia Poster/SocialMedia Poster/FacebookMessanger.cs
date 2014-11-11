using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Facebook;
using System.Dynamic;
using System.Net.Mail;

namespace SocialMediaPoster
{
    class FacebookMessanger
    {
        public void ShareOnFB(string caption, string description, string link, string accesstoken,string username,bool IsPage=true)
        {
            FacebookClient client = new FacebookClient(accesstoken);
            dynamic messagePost = new ExpandoObject();
            messagePost.access_token = accesstoken;
           // messagePost.picture = link;
            messagePost.link = "https://soundcloud.com/syeda-kazmi/buss-ya-hussain";
            messagePost.name = caption;
            messagePost.description = description;
            dynamic result = "";
                if (IsPage == false)
                {
                    result = client.Post("v2.1/me/feed", messagePost);
                }
                else
                {
                   // string PageTokens = client.Get("v2.1/me/accounts?access_token={0}"+ accesstoken);

//                    result = client.Post("v2.1/me/feed", messagePost);
                    result = client.Post(String.Format("v2.1/{0}/feed", username), messagePost);                    
                }
        }
 
        public void SendEmail(string username,string password,String to, String from, String caption, string description, List<string> link, string SmtpClient = "smtp.gmail.com", int port = 587)
        {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient(SmtpClient);

                mail.From = new MailAddress(from);
                mail.To.Add(to);
                mail.Subject = caption;
                mail.Body = description;

                SmtpServer.Port = port;
                SmtpServer.Credentials = new System.Net.NetworkCredential(username, password);
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);
        }
    }
}
