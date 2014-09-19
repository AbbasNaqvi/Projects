using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Mail;
using System.Net;

namespace ZooplaNotify
{
    class Property
    {
        private string propertyID;

        public string PropertyID
        {
            get { return propertyID; }
            set { propertyID = value; }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private double price;
        
        
        public bool SetPrice(double newprice,bool check)
        {
            bool Result = false;
            if (check == true)
            {                
                AccessHandler handler = new AccessHandler();
                Property OldProperty = new Property();
                if ((OldProperty = handler.GetRecordByID(this.propertyID)) != null)
                {
                    price = OldProperty.price;
               //     title = OldProperty.title;
                    LastUpdated = OldProperty.LastUpdated;

                    if (price > newprice)
                    {
                        //Price is decreased
                        double TenPercentOfOldPrice = (price * 10) / 100;
                        if (price - newprice >= TenPercentOfOldPrice)
                        {
                            SendMail(newprice);
                        }
                    }
                    price = newprice;
                    handler.UpdateProperty(this);
                }
                else
                {
                    if (propertyID != null)
                    {
                        price = newprice;
                        LastUpdated = DateTime.Now;
                        handler.InsertProperty(this);
                        Result = true;
                    }
                }
            }
            else {
                price = newprice;
                Result = true;
            }
            return Result;
        }
        private void SendMail(double newPrice)
        {

            ApplicationData data = Form1.Create;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress(data.EmailId);

                foreach (var x in data.To)
                {
                    mail.To.Add(x);                
                }
                mail.Subject = "Price of " + PropertyID + " is decreased from " + price + " to " + newPrice + " by more than or equal to 10 percent";


                string List = @"<ul>
	<li></b>PropertyID</b>=" + propertyID + @"</li>
	<li></b>Title</b>=" + title + @"</li>
	<li></b>OldPrice</b>=" + price + @"</li>
	<li></b>NewPrice</b>=" + newPrice + @"</li>
</ul>";

                string imagebar = "<img height=\"100\" width=\"300\" align=\"right\" src=\"http://images.vcpost.com/data/images/full/8105/zoopla.jpg?w=590\" alt=\"Google logo\">";
   
                mail.Body = "<h2>Price of " + PropertyID + " is decreased from " + price + " to " + newPrice + " by more than or equal to 10 percent</h2>"+ List+ imagebar;
               
                mail.IsBodyHtml = true;
                // Can set to false, if you are sending pure text.

/*                mail.Attachments.Add(new Attachment("C:\\SomeFile.txt"));
                mail.Attachments.Add(new Attachment("C:\\SomeZip.zip"));
                */
                using (SmtpClient smtp = new SmtpClient(data.Host, data.Port))
                {
                    smtp.Credentials = new NetworkCredential(data.EmailId, data.Password);
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                }
            }
        }
        public double GetPrice()
        {
            return price;
        }

        private DateTime lastUpdated;

        public DateTime LastUpdated
        {
            get { return lastUpdated; }
            set { lastUpdated = value; }
        }
        
    }
}
