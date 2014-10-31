using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace Listing_Date_Updater
{
    class MousePriceScrapper
    {
        private string TempUrl = @"http://www.mouseprice.com/property-for-sale/";


        //public
        internal void CheckMousePrice(PropertyInfo info)
        {
            string MainUrl = MakeUrl(info.PostCode);
            if (IsRegexResultFound("No results found for " + info.PostCode + ". Search area expanded", MainUrl))
            {
                //Unfortunately Result is not found
               throw new Exception("No Result On Page");
            }
            else
            {
                string RecordString = null;
                //Search Specific Record
                try
                {
                    RecordString = GetMyRecord(MainUrl, info.Price, null);
                }
                catch (Exception e)
                {

                    throw new Exception(e.Message + " ,While Reading Record With Price only");
                }

               
                if(String.IsNullOrEmpty(RecordString))
                {
                    throw new Exception("No Such Record For Prices is found");
                }
               else if (RecordString.Equals("MULTIPLE"))
                {
                    try
                    {
                        RecordString = GetMyRecord(MainUrl, info.Price, info.MarketedBy);
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(ex.Message + ", While Getting Record by Agent due to Multiple Records " + info.PostCode);                    
                    }
                    if (String.IsNullOrEmpty(RecordString))
                    {
                        throw new Exception("Can not get 50% agent name match in multiple prices");                    
                    }
                    SearchForDate(info, RecordString);
                }
                else
                {
                    SearchForDate(info, RecordString);
                }
            }
        }
        //private
        private void SearchForDate(PropertyInfo info, string RecordString)
        {
            string RecordDate = null;

            try
            {
                //RecordDate = GetRegexStringResult(new Uri(MainUrl), @"<span class=""updateinfo"">(?<data>.*?)</span>");
                RecordDate = GetRegexStringResult(RecordString, @"<span class=""updateinfo"">(?<data>.*?)</span>");
            }
            catch (Exception e)
            {
                throw new Exception(e.Message + " While Searching Date on page 1 ...Multiple Same Records");
            }

            int days = 0;
            DateTime MarketedDate = DateTime.Now;
            if (RecordDate.Contains("days"))
            {
                string temp = Regex.Match(RecordString, "Added (?<data>\\d*?) days ago", RegexOptions.Multiline).Groups["data"].Value;
                if (int.TryParse(temp, out days))
                {
                    MarketedDate = MarketedDate.Subtract(new TimeSpan(days, 0, 0, 0));
                    info.MarketedFrom = MarketedDate.ToShortDateString();
                }
                else
                {
                    throw new Exception("Unable to Find days in   Multiple Same Records");
                }
            }
            else if (RecordDate.Contains("over a"))
            {
                using (WebClient w = new WebClient())
                {
                    string NewPageString = w.DownloadString(GetNewPageUri(RecordString));
                    MatchCollection collection = Regex.Matches(NewPageString, "<span class=\"sr_dated\">(?<data>.*?)</span>");
                    foreach (Match m in collection)
                    {
                        RecordDate = m.Groups["data"].Value;
                        if (String.IsNullOrEmpty(RecordDate) == false)
                        {
                            break;
                        }
                    }
                    info.MarketedFrom = RecordDate;
                }
            }

        }
        private bool CompareStrings50Percent(string first, string second)
        {
            string[] onearray = first.Split(' ');
            string[] twoarray = second.Split(' ');

            int wordsinonearray = onearray.Count();
            int SuccessMathces = 0;
            foreach (string x in onearray)
            {
                foreach (string y in twoarray)
                {

                    if (x.Equals(y))
                    {
                        SuccessMathces++;
                    }
                }
            }

            if ((wordsinonearray / 2) - 3 < SuccessMathces)
            {
                return true;
            }
            else
            {
                return false;
            }


        }
        private string MakeUrl(string postalcode)
        {
            string url = TempUrl + postalcode.ToLower().Replace(" ", "+");
            return url;
        }
        private string MatchPriceFromSubjectString(string price, string SubjectString)
        {
            string Result = null;
            MatchCollection mCollection = Regex.Matches(SubjectString, "<span class=\"price\">.*?&pound;(?<data>.*?)</span>",
             RegexOptions.Multiline);

            if (mCollection.Count > 1)
            {
                throw new Exception("Multiple Prices in one Record");
            }
            foreach (Match m in mCollection)
            {
                string takenprice = null;
                try
                {
                    takenprice = m.Groups["data"].Value.Replace(",", "");
                }
                catch (ArgumentException e)
                {

                    throw new Exception(e.Message);
                }

                if (takenprice.Equals(price))
                {
                    Result = price;
                }
                else
                {

                    Result = null;
                }
            }
            return Result;
        }
        private bool IsRegexResultFound(string pattern, string url)
        {
            bool Result = false;
            WebClient client = new WebClient();
            string SubjectString = client.DownloadString(url);

            if (Regex.IsMatch(SubjectString, pattern, RegexOptions.Multiline))
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;
        }    
        private string GetRegexStringResult(string SubjectString, string pattern)
        {
            string Result = null;
            MatchCollection Matches = Regex.Matches(SubjectString, pattern);
            if (Matches.Count > 1)
            {
                throw new Exception("Multiple Results Found");
            }
            else if (Matches.Count == 1)
            {
                //One Result Found
                foreach (Match m in Matches)
                {
                    Result = m.Groups["data"].Value;
                }

            }
            else
            {
                //No Result Found            
                Result = null;
            }

            return Result;
        }
        private string GetMyRecord(string url, string price, string agent)
        {
            string ResultString = null;
            WebClient client = new WebClient();
            string SubjectString = client.DownloadString(url);
            if (SubjectString.Contains("IP address blocked"))
            {
                throw new Exception("IP_BLOCKED: Can not Access: Ip Address is Blocked");
            
            }
            MatchCollection collection = Regex.Matches(SubjectString, "<div class=\"srl_row\">(?<data>(.|\\r\\n)*?)<div class=\"srl_mapdesc", RegexOptions.Multiline);
            int count = 0;
            foreach (Match m in collection)
            {
                if (String.IsNullOrEmpty(agent))
                {
                    string ResultPrice = MatchPriceFromSubjectString(price, m.Groups["data"].Value);
                    if (String.IsNullOrEmpty(ResultPrice) == false)
                    {
                        count++;
                        ResultString = m.Groups["data"].Value;
                    }
                    else
                    {

                        throw new Exception("No Price Match Found");
                    }
                }
                else
                {//"<div class=\"srl_comp_sec\">(?<data>.*?)</div>"
                    //"<div class='srl_comp_sec'>(?<data>.*?)</div>"
                    string Agent = GetRegexStringResult(m.Groups["data"].Value, "<div class='srl_comp_sec'>(?<data>.*?)</div>");
                    if (CompareStrings50Percent(agent, Agent))
                    {
                        ResultString = m.Groups["data"].Value;
                    }
                }
            }
            if (count == 0 && String.IsNullOrEmpty(agent))
            {
                ResultString = null;
            }
            else if (count > 1 && String.IsNullOrEmpty(agent))
            {
                ResultString = "MULTIPLE";
            }
            return ResultString;
        }
        private Uri GetNewPageUri(string SubjectString)
        {
            string Result = null;
            MatchCollection collection = Regex.Matches(SubjectString, "<a href=\"(?<data>.*?)\">(.*?)</a>");
            foreach (Match m in collection)
            {
                if (m.Groups["data"].Value.Contains("javascript"))
                {
                    continue;
                }
                else
                {

                    Result = m.Groups["data"].Value;
                }
            }
            return new Uri("http://www.mouseprice.com/" + Result);
        }


        //unused
        private string GetRegexStringResult(Uri url, string pattern)
        {
            string Result = null;
            WebClient client = new WebClient();
            string SubjectString = client.DownloadString(url);

            MatchCollection Matches = Regex.Matches(SubjectString, pattern);
            if (Matches.Count > 1)
            {
                throw new Exception("Multiple Results Found");
            }
            else if (Matches.Count == 1)
            {
                //One Result Found
                foreach (Match m in Matches)
                {
                    Result = m.Groups["data"].Value;
                }

            }
            else
            {
                //No Result Found            
                Result = null;
            }

            return Result;
        }
    }
}
