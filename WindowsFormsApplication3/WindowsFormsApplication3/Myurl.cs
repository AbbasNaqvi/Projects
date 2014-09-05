using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace WindowsFormsApplication3
{
    class Myurl
    {

        /*  private const string  angle1="/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
          private const string  angle2="/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
          private const string  angle3="/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
          private const string  angle4="/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
          */
        private string url1;

        public string Url1
        {
            get { return url1; }
            set { url1 = value; }
        }
        private string url2;

        public string Url2
        {
            get { return url2; }
            set { url2 = value; }
        }
        private string url3;

        public string Url3
        {
            get { return url3; }
            set { url3 = value; }
        }
        private string url4;

        public string Url4
        {
            get { return url4; }
            set { url4 = value; }
        }

        public Myurl()
        {/*
            url1 = "https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,182.03h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
            url2 = "https://www.google.com/maps/place/London,+UK/@51.478048,-0.00164,3a,75y,270h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
            url3 = "https://www.google.com/maps/place/London,+UK/@51.478048,-0.00164,3a,75y,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
            url4 = "https://www.google.com/maps/place/London,+UK/@51.478048,-0.00164,3a,75y,90h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
        */
        }

        public string Change_Angle(string url, string angle, string consoleText)
        {


            /*         New Url
           */

            string ResultString = null;
            string RefinedString = null;
            try
            {
                //   ResultString = Regex.Match(url, "http://maps.google.com/maps.q=.layer=c.cbll=.*?cbp=(?<data>.*)").Groups["data"].Value;
                ResultString = Regex.Match(url, "maps.google.com/maps\\?(?<data1>.*?)layer.*?cbp=(?<data>.*?)&.*").Groups["data"].Value;
                if (string.IsNullOrEmpty(ResultString))
                {
                    return "ERROR: ANGLE CAN NOT BE CHANGED";
                }
                string[] tokens = ResultString.Split(new char[] { ',' });

                string c1 = tokens[2];
                string c2 = tokens[3];
                string c3 = tokens[4];
                string JoinedString = tokens[0] + "," + angle + "," + c1 + "," + c2 + "," + c3;
                RefinedString = Regex.Replace(url, ResultString, JoinedString);
                ResultString = Regex.Replace(RefinedString, "q=.*?&", "");
            }
            catch (ArgumentException ex)
            {
                consoleText += "Invalid Url=  " + ex.Message + "\n";
            }
            return ResultString;

        }
    }
}         /*                        OLD URL

                string ResultString = null;
                string RefinedString = null;
                try
                {
                    ResultString = Regex.Match(url, "https://www.google.com/maps/.*?.*?(?<data>y.*?t).*?data").Groups["data"].Value;

                    if (ResultString.Contains("h"))
                    {
                        string[] tokens = ResultString.Split(new char[] { ',' });
                        string JoinedString = tokens[0] + "," + angle + "h," + tokens[2];
                        RefinedString = Regex.Replace(url, ResultString, JoinedString);
                    }
                    else
                    {
                        string[] tokens = ResultString.Split(new char[] { ',' });
                        string JoinedString = tokens[0] + "," + angle + "h," + tokens[1];
                        RefinedString = Regex.Replace(ResultString, "y.*?t", JoinedString);
                    }
                }
                catch (ArgumentException ex)
                {
                    consoleText += "Invalid Url=  " + ex.Message + "\n";
                }
                return RefinedString;
    */


