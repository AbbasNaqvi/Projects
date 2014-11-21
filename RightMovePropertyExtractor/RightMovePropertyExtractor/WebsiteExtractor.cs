using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Text.RegularExpressions;

namespace RightMovePropertyExtractor
{
    class WebsiteExtractor
    {

        /*
         * This is Reusable Class, 
         * Only MakeUrlForPage Method needs to be changed
         */
        private int expectedRecordsPerPage;

        public int ExpectedRecordsPerPage
        {
            get { return expectedRecordsPerPage; }
            set { expectedRecordsPerPage = value; }
        }


        private string link;

        public string Link
        {
            get { return link; }
            set { link = value; }
        }

        private string log;

        public string Log
        {
            get { return log; }
            set { log = value; }
        }

        private int totalRecords;

        public int TotalRecords
        {
            get { return totalRecords; }
            set { totalRecords = value; }
        }

        private int currentRecord;

        public int CurrentRecord
        {
            get { return currentRecord; }
            set { currentRecord = value; }
        }

        private bool terminationNotice;

        public bool TerminationNotice
        {
            get { return terminationNotice; }
            set { terminationNotice = value; }
        }


        public string getRegexString(string document, string pattern, string groupname = "data")
        {
            string Result = null;
            if (string.IsNullOrEmpty(document) == false && string.IsNullOrEmpty(pattern) == false)
            {
                MatchCollection collection = Regex.Matches(document, pattern);
                if (collection.Count >= 1)
                {
                    Result = collection[0].Groups[groupname].Value;
                }
                else
                {
                    Result = null;
                }
            }
            return Result;
        }
        public List<string> getRegexStrings(string document, string pattern, string groupname = "data")
        {
            if (string.IsNullOrEmpty(document))
            {
                return null;
            }
            List<string> Result = new List<string>();
            MatchCollection collection = Regex.Matches(document, pattern);
            if (collection.Count >= 1)
            {
                foreach (Match m in collection)
                {
                    Result.Add(m.Groups[groupname].Value);
                }
            }
            else
            {
                Result = null;
            }
            return Result;
        }
        private bool DownloadSecureString(WebClient w, int trycount, out string document, string url = null)
        {
            bool Result = false;
            document = "";
            if (string.IsNullOrEmpty(url))
            {
                url = link;
            }
            for (int t = 1; t <= trycount; t++)
            {
                try
                {
                    document = w.DownloadString(url);
                    Result = true;
                    break;
                }
                catch (Exception)
                {
                    continue;
                }
            }
            return Result;
        }
        private bool getPageNumberCount(string pattern, string document, out int pagecount, string groupname = "data")
        {
            bool Result = false;
            pagecount = 0;
            string PageCount = getRegexString(document, pattern, groupname);
            if (PageCount != null)
            {
                if (int.TryParse(PageCount, out pagecount) == true)
                {
                    Result = true;
                }
            }
            return Result;
        }
        public IEnumerable<Record> ExtractValuesFromDocument(Dictionary<string, string> patterns, string spliterRegex = null)
        {
            terminationNotice = false;
            string Document = "";
            int totalpages = 0;
            using (WebClient w = new WebClient())
            {
                if (DownloadSecureString(w, 5, out Document) == false)
                {
                    log += "\nException:Error while getting the first page,";
                    TerminationNotice = true;
                }
                if (getPageNumberCount(patterns["TotalRecords"], Document, out totalRecords, "data") == false)
                {
                    log += "\nException: Total Records count is not found";
                    TerminationNotice = true;
                    yield break;
                }
                if (getPageNumberCount(patterns["PageNumbers"], Document, out totalpages) == false)
                {
                    log += "\nException: Total pages count is not found";
                    TerminationNotice = true;
                    yield break;
                }
                for (int i = 0; i < totalpages; i++)
                {
                    string Pageurl = null;
                    if (MakeUrlForPage(link, i, out Pageurl) == false)
                    {
                        log += "\nCan not make page url" + i;
                        terminationNotice = true;
                        yield break;
                    }
                    if (DownloadSecureString(w, 5, out Document, Pageurl) == false)
                    {
                        log += "\nException:Error while getting the " + i + " page,";
                    }
                    #region Extraction of Record
                    Record record = new Record();
                    if (spliterRegex != null)
                    {

                        List<string> Divs = getRegexStrings(Document, spliterRegex);
                        #region StrictFilters
                        if ((Divs == null || Divs.Count == 0))
                        {
                            log += "\nNo Record Found on Page " + i;
                            terminationNotice = true;
                            yield break;
                        }
                        else if (expectedRecordsPerPage != 0 && Divs.Count != ExpectedRecordsPerPage && i < totalpages-1 )
                        {
                            log += "\nRecords on page are not found as expected\nExpected= " + expectedRecordsPerPage + "\nFound=" + Divs.Count;
                            terminationNotice = true;
                            yield break;
                        }
                        #endregion
                        foreach (string x in Divs)
                        {
                            foreach (KeyValuePair<string, string> p in patterns)
                            {
                                //if (string.IsNullOrEmpty(record.PropertyID) == false)
                                //{
                                //    if (record.PropertyID.Equals("40277077"))
                                //    {
                                //        int c = 0;
                                //    }
                                //}
                                if (p.Key.Equals("TotalRecords") == false && p.Key.Equals("PageNumbers") == false)
                                {
                                    string abc = getRegexString(x, p.Value);
                                    record[p.Key] = getRegexString(x, p.Value);
                                }
                            }


                            #region Post Code Extraction Patch
                            string DetailPageUrl = "http://www.rightmove.co.uk/" + record["PostalCode"];
                            string DetailPageDocument = null;
                            if (DownloadSecureString(w, 2, out DetailPageDocument, DetailPageUrl) == false)
                            {
                                log += "\nCan not get Detail Page Url";
                            }
                            record.PostalCode = getRegexString(DetailPageDocument, "<a id=\"broadband-link\".*?#(?<data>.*?)\">");
                            #endregion
                            currentRecord++;
                            yield return record;
                        }
                    }
                    else
                    {
                        /*
                         * Assuming group name will be the same as of attributes,only possible if records are not earlier splited.
                         */
                        foreach (KeyValuePair<string, string> p in patterns)
                        {
                            string temp = getRegexString(Document, p.Value, p.Key);
                            if (string.IsNullOrEmpty(temp))
                            {
                                log += "\nValue of " + p.Key + " maynot be retrieved correctly";
                            }
                            log += "\n-------------------------------------------------------------------------------";
                            record[p.Key] = temp;
                        }
                        currentRecord++;
                        yield return record;
                    }

                    #endregion
                }
            }
        }
        private bool MakeUrlForPage(string link, int i, out string Pageurl)
        {
            string token = "&index=" + (i * 10);
            string match = Regex.Match(link, "&index=(\\d)*&").Groups[1].Value;
            if (string.IsNullOrEmpty(match) == false)
            {
                Pageurl = link.Replace(match, token);
            }
            else
            {
                Pageurl = link + token;
            }
            return true;
        }
    }
}
