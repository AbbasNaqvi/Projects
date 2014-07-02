using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using OpenPop.Common.Logging;
using OpenPop.Mime;
using OpenPop.Mime.Decode;
using OpenPop.Mime.Header;
using OpenPop.Pop3;


namespace ConsoleApplication1
{
    class Program
    {


        public static void FindPlainTextInMessage(Message message)
        {
            MessagePart plainText = message.FindFirstPlainTextVersion();
            if (plainText != null)
            {
                // Save the plain text to a file, database or anything you like
                plainText.Save(new FileInfo("plainText.txt"));
            }
        }
        public static List<Message> FetchAllMessages(string hostname, int port, bool useSsl, string username, string password)
        {
            // The client disconnects from the server when being disposed
            using (Pop3Client client = new Pop3Client())
            {
                // Connect to the server
                client.Connect(hostname, port, useSsl);

                // Authenticate ourselves towards the server
                client.Authenticate(username, password);

                // Get the number of messages in the inbox
                int messageCount = client.GetMessageCount();

                // We want to download all messages
                List<Message> allMessages = new List<Message>(messageCount);

                // Messages are numbered in the interval: [1, messageCount]
                // Ergo: message numbers are 1-based.
                // Most servers give the latest message the highest number
                for (int i = messageCount; i > 0; i--)
                {
                    allMessages.Add(client.GetMessage(i));
                }

                // Now return the fetched messages
                return allMessages;
            }
        }

        static void Main(string[] args)
        {

            List<Message> allaEmail = FetchAllMessages("pop.gmail.com",995,true,"abbas.naqvi@dynamologic.com","Disneyjob");

    StringBuilder builder = new StringBuilder();
    foreach(OpenPop.Mime.Message message in allaEmail)
    {
         OpenPop.Mime.MessagePart plainText = message.FindFirstPlainTextVersion();
         if(plainText != null)
         {
             // We found some plaintext!
             builder.Append(plainText.GetBodyAsText());
         } else
         {
             // Might include a part holding html instead
             OpenPop.Mime.MessagePart html = message.FindFirstHtmlVersion();
             if(html != null)
             {
                 // We found some html!
                 builder.Append(html.GetBodyAsText());
             }
         }
    }
//    MessageBox.Show(builder.ToString());
    Console.WriteLine(builder.ToString());


     
        }
    }

}