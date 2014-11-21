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


        public bool SetPrice(double newprice, bool check, out string log)
        {
            /*check :whethter to simply set Price or do all the process
             * 
             */
               AccessHandler handler = new AccessHandler();
            log = "";
            bool Result = false;
            if (check == true)
            {
             
                Property OldProperty = new Property();
                //Fetch Previous Record In database
                if ((OldProperty = handler.GetRecordByID(this.propertyID)) != null)
                {
                    price = OldProperty.price;
                    //     title = OldProperty.title;
                    LastUpdated = OldProperty.LastUpdated;
                    if (price > newprice)
                    {

                        log += "\nRecord is Found and Price Fall is detected\nMail is Sent\nUpdated in database";
                        //Price is decreased
                        double TenPercentOfOldPrice = (price * 10) / 100;
                        if (price - newprice >= TenPercentOfOldPrice)
                        {
                            try
                            {
                                SendMail(newprice);
                            }
                            catch (Exception e)
                            {
                                log += e.Message;
                            
                            }
                        }
                    }
                    else
                    {

                        log += "\nPrice Fall is not Detected and Updated in Database";
                    }
                    price = newprice;
                    try
                    {
                        handler.UpdateProperty(this);
                    }
                    catch (Exception e)
                    {
                        log += "\n" + e.Message;
                    }

                }
                else
                {
                    if (propertyID != null)
                    {
                        log += "\nRecord is added for First time ,it was not Found in datebase";
                        price = newprice;
                        LastUpdated = DateTime.Now;
                        try
                        {
                            handler.InsertProperty(this);
                        }
                        catch (Exception e)
                        {
                            log += "\n" + e.Message;
                        }
                        Result = true;
                    }
                }
            }
            else
            {
                price = newprice;
                Result = true;
            }
            log += "OLEDB LOG={ "+ handler.oLEDBLog+" }";

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

                mail.Body = "<h2>Price of " + PropertyID + " is decreased from " + price + " to " + newPrice + " by more than or equal to 10 percent</h2>" + List + imagebar;

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
