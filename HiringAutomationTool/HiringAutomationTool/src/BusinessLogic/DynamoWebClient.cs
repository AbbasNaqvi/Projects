using System;
using System.Net;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

/*Author :Abbas Naqvi
 * DynamoLogic Solution
 * 6/18/2014
 * 
 * Notes:
 * Exceptions are left blank in order to access proceed the next result if somwone fails as there are a lot of Records and downloading 
 * 
 * 
 */

namespace HiringAutomationTool
{
    public delegate void InformationDownloadHandler(object o, EventArguments e);

    [System.ComponentModel.DefaultEvent("InformationDownloadHandler")]
    class DynamoWebClient
    {

        public event InformationDownloadHandler InformationDownloadEvent;
        public static string Extension = null;
        string response_POSTEDJOBS;
        CookieAwareWebClient client = new CookieAwareWebClient();
        private readonly string InitialPath;

        public DynamoWebClient(string InitialPath)
        {
            this.InitialPath = InitialPath;
        }

        public virtual void OnInformationDownload(EventArguments e)
        {
            if (InformationDownloadEvent != null)
            {
                InformationDownloadEvent(this, e);

            }
        }
        private DateTime IsAppliedEarlier(string AppliedDate)
        {
            int m = 0; int y = 0; int d = 0;
            string year = null;
            string month = null;
            string date = null;
            int Date_length = 0;
                Date_length = AppliedDate.Trim().Length;
                if (Date_length <= 9)
                {
                    int xa = AppliedDate.Length;
                    AppliedDate = AppliedDate.Replace(",", "").Replace(" ", "");
                    month = AppliedDate.Substring(0, 3);
                    date = AppliedDate.Substring(3, 1);
                    year = AppliedDate.Substring(4, 2);
                }
                else
                {

                    int xa = AppliedDate.Length;
                    AppliedDate = AppliedDate.Replace(",", "").Replace(" ", "").Replace("-", "");
                    month = AppliedDate.Substring(0, 3);
                    date = AppliedDate.Substring(3, 2);
                    year = AppliedDate.Substring(5, 2);

                }
                int.TryParse(date, out d);
                int.TryParse(year, out y);

                if (month.Contains("Jan"))
                {
                    m = 1;
                }
                else if (month.Contains("Feb"))
                {
                    m = 2;
                }
                else if (month.Contains("Mar"))
                {
                    m = 3;
                }
                else if (month.Contains("Apr"))
                {
                    m = 4;
                }
                else if (month.Contains("May"))
                {
                    m = 5;
                }
                else if (month.Contains("Jun"))
                {
                    m = 6;
                }
                else if (month.Contains("Jul"))
                {
                    m = 7;
                }
                else if (month.Contains("Aug"))
                {
                    m = 8;
                }
                else if (month.Contains("Sep"))
                {
                    m = 9;
                }
                else if (month.Contains("Oct"))
                {
                    m = 10;
                }
                else if (month.Contains("Nov"))
                {
                    m = 11;
                }
                else if (month.Contains("Dec"))
                {
                    m = 12;
                }

                DateTime APPLIEDDATE = new DateTime(y, m, d);
                return APPLIEDDATE;
        }
        public void CreateDirectories()
        {
            PostedJobsLog log = PostedJobsLog.Create;
            foreach (var x in log.JobsList)
            {
                string Name = x.Name.Replace("&Irm", "");
                bool isExists = System.IO.Directory.Exists(System.IO.Path.Combine(InitialPath + @"\CV\" + x.Name + @"\"));

                if (!isExists)
                    System.IO.Directory.CreateDirectory(System.IO.Path.Combine(InitialPath + @"\CV\" + x.Name + @"\"));


            }
        }
        public void DownloadCVS(string path)
        {
            path = path + @"\CV\\";
            ApplicantLog log = ApplicantLog.Create;
            foreach (Applicant i in log.applicantsList)
            {
                try
                {
                    string FileType1 = System.IO.Path.Combine(path + @"\" + i.FolderName + @"\" + i.CVNumber + ".pdf");
                    string FileType2 = System.IO.Path.Combine(path + @"\" + i.FolderName + @"\" + i.CVNumber + ".doc");
                    string FileType3 = System.IO.Path.Combine(path + @"\" + i.FolderName + @"\" + i.CVNumber + ".docx");
           



                    if (System.IO.File.Exists(FileType1) || System.IO.File.Exists(FileType2)||System.IO.File.Exists(FileType3))
                    {
                        continue;
                    }

                    OnInformationDownload(new EventArguments() { Name = i.CVNumber, Date = DateTime.Now, Details = "Downloading CV of " + i.Name });
                    client.DownloadFile(i.CVLink, System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber));
                    string Extens = client.ResponseHeaders["Content-Disposition"].Replace("attachment; filename=\"ROZEE-CV-", "").Replace("\"", "");
                    i.OnlineFileName = System.IO.Path.Combine(path + i.FolderName + @"\" + Extens);
                    if (i.OnlineFileName.Contains(".pdf"))
                    {
                        System.IO.File.Move(System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber), System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber + ".pdf"));
                    }
                    else if (i.OnlineFileName.Contains(".docx"))
                    {
                        System.IO.File.Move(System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber), System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber + ".docx"));
                    }
                    else if (i.OnlineFileName.Contains(".doc"))
                    {
                        System.IO.File.Move(System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber), System.IO.Path.Combine(path + i.FolderName + @"\" + i.CVNumber + ".doc"));
                    }
                }
                catch (Exception)
                {
                    //Ignoring To get Performance
                }

            }
        }

        public void OpenAllLinks(DateTime date1, DateTime date2, bool radiocheck)
        {
            PostedJobsLog log = PostedJobsLog.Create;
            foreach (PostedJobs i in log.JobsList)
            {
                StringBuilder ApplicantsString = null;
                OnInformationDownload(new EventArguments() { Name = i.Name, Details = "Starting Fetching Information .... ", Date = DateTime.Now });
                try
                {
                    ApplicantsString = new StringBuilder(client.DownloadString(i.Link));
                }
                catch (Exception)
                {

                }
                int totalApplicants = 0;
                if (i.TotalApplicants.Equals("") || String.IsNullOrEmpty(i.TotalApplicants))
                {

                    i.TotalApplicants = ReadtotalApplicantsAgain(ApplicantsString.ToString());
                    int.TryParse(i.TotalApplicants, out totalApplicants);
                }
                else
                {
                    int.TryParse(i.TotalApplicants, out totalApplicants);
                }

                /*
                 * Foreach page of this job
                 * 
                 */

                StringBuilder ReservedLink = new StringBuilder();
                ReservedLink.Append("http://hiring.rozee.pk/job-applicantsCen.php?jid=" + i.EncryptedJobID + "ordBy=byDate&srtDir=desc&offset=");
                int totalPages = (int)Math.Ceiling((decimal)totalApplicants / 16);
                int pageCount = 0;

                while (pageCount != totalPages)
                {
                    ////////////*******************///////////////////
                    string pattern = @"<!--Single\sapplicant\sBox\sStart\s-->(?<data>.*?)<!--Single\sapplicant\sBox\sEnd\s-->";
                    Regex r = new Regex(pattern, RegexOptions.Singleline);
                    string Tempname = i.Name;

                    ApplicantLog alog = ApplicantLog.Create;
                    foreach (Match j in r.Matches(ApplicantsString.ToString()))
                    {
                        try
                        {





                            //SensitiVE Checking
                            Applicant applicant = new Applicant();





                            if (radiocheck == true&&String.IsNullOrEmpty(applicant.ApplyDate))
                            {
                                DateTime AppliedDate = IsAppliedEarlier(applicant.ApplyDate);

                                if (DateTime.Compare(AppliedDate, date1) < 0)
                                {
                                    continue;
                                }
                                if (DateTime.Compare(AppliedDate, date2) > 0)
                                {
                                    continue;
                                }
                            }

                            applicant.CVNumber = FindCVNumber(j.Groups["data"].Value);


                            // applicant.CVLink = "http://hiring.rozee.pk/"+FindCVLink( j.Groups["data"].Value);     --Old Style
                            applicant.CVLink = FindCVLink(j.Groups["data"].Value);

                            OnInformationDownload(new EventArguments() { Name = applicant.FolderName, Details = " Fetching information of" + i.Name + " at page" + (pageCount + 1) + "/" + (totalPages + 1), Date = DateTime.Now });
                            applicant.ApplyDate = FindApplyDate(j.Groups["data"].Value);


                            if (alog.Contains(applicant.CVNumber) == true)
                            {
                                continue;
                            }

                            applicant.FolderName = i.Name;
                            applicant.Name = FindName(j.Groups["data"].Value);

                            //             applicant.tags = FindAlltags(j.Groups["data"].Value);               --NOT WORKING
                            //             applicant.PictureLink = FindPictureLink(j.Groups["data"].Value);    --NOT WORKING
                            //             applicant.ProfessionalSummary=                                       --NotImplemented
                            //             applicant.Nationality = FindNationality(j.Groups["data"].Value);     --NOT WORKING 

                            applicant.MaritalStatus = FindMaritalStatus(j.Groups["data"].Value);

                            applicant.Experience = FindExperience(j.Groups["data"].Value);
                            applicant.CarrierLevel = FindCarrierLevel(j.Groups["data"].Value);
                            applicant.ExpectedSalary = FindExpectedSalary(j.Groups["data"].Value);
                            applicant.CurrentSalary = FindCurrentSalary(j.Groups["data"].Value);
                            applicant.DegreeLevel = FindDegreeLevel(j.Groups["data"].Value);
                            StringBuilder builder = new StringBuilder(FindEducation(j.Groups["data"].Value)).Replace("<b>", "");
                            builder = builder.Replace("<\b>", "");
                            builder = builder.Replace("@Irm;", "");
                            builder.Clear();
                            applicant.Education = builder.ToString();
                            applicant.Industry = FindIndustry(j.Groups["data"].Value);
                            applicant.FunctionalArea = FindFunctionalArea(j.Groups["data"].Value);
                            alog.applicantsList.Add(applicant);
                        }
                        catch (Exception)
                        {
                        }
                    }
                    pageCount++;
                    ApplicantsString.Clear();
                    StringBuilder newLink = new StringBuilder("");
                    newLink.Append(ReservedLink.ToString() + pageCount);
                    try
                    {
                        ApplicantsString.Append(client.DownloadString(newLink.ToString()));
                    }
                    catch (WebException)
                    {
                    }
                }
            }
        }



        private string ReadtotalApplicantsAgain(string p)
        {
            string result = null;
            string pattern = "<span id=\"spnTotAppCnt\" style=\"float:none; color:#333\">\\s*?\\((?<data>\\d*)\\)";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                result = i.Groups["data"].Value;
            }
            return result;
        }



        private string FindCVNumber(string p)
        {
            string pattern = @"<label>CV Number: <b>(?<data>.*?)</b></label>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string FindCarrierLevel(string p)
        {
            string pattern = @"<label>Career Level</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string FindExperience(string p)
        {
            string pattern = @"<label>Experience</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string FindCVLink(string p)
        {
            string pattern = "<a href='(?<data>http://hiringus.*?)'";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value.Replace("amp;", "");
            }
            return "";
        }
        private string FindCurrentSalary(string p)
        {

            string pattern = @"<label>Current Salary</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";


        }
        private string FindExpectedSalary(string p)
        {

            string pattern = @"<label>Expected Salary</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";


        }
        private string FindDegreeLevel(string p)
        {

            string pattern = @"<label>Degree Level</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";


        }
        private string FindIndustry(string p)
        {

            string pattern = @"<label>Industry</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string FindFunctionalArea(string p)
        {

            string pattern = @"<label>Functional Area</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string FindName(string p)
        {

            string pattern = "<b\\stitle=\\\".*?>(?<data>.*?)</b";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }

        private string FindEducation(string p)
        {

            string pattern = @"<label>Education</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value.Replace("<br>", "");
            }
            return "";
        }
        private string FindPictureLink(string p)
        {

            string pattern = "<td align=\"center\" valign=\"middle\"><img src=\"(?<data>.*?)\"></td>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return "http://hiring.rozee.pk/" + i.Groups["data"].Value;
            }
            return "";
        }
        private string FindMaritalStatus(string p)
        {

            string pattern = @"<label>Marital Status</label>(\s|\r)*?<p>(\s|\r)*(?<data>.*?)(\s|\r)*</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private List<string> FindAlltags(string p)
        {
            string patternone = "<a class=\\\"f\\\" rel=\\\"tipsyon\\\" title=\\\"< (?<exp>.*?)\\\">(?<tags>.*?)</a>";
            Regex r = new Regex(patternone, RegexOptions.Singleline);
            //Dictionary<string, string> tags = new Dictionary<string, string>();
            List<string> tags = new List<string>();
            foreach (Match i in r.Matches(p))
            {
                tags.Add(i.Groups["data"].Value + i.Groups["exp"].Value);
            }
            return tags;
        }
        private string FindNationality(string p)
        {
            string pattern = "<label>Nationality</label>(.|\\s|\\r)*?(?<data>.*?)</p>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(response_POSTEDJOBS))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string FindApplyDate(string p)
        {
            string pattern = @"<label>Apply Date: <b>(?<data>.*?)</b></label>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }

        public string DownloadAllPostedJobs(DateTime date1, DateTime date2, bool radiocheck)//(DateTime date1,DateTime date2,bool radiocheck
        {
            string text = null;
            string pattern = @"<tr\sclass(?<data>.*?)</tr>";
            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(response_POSTEDJOBS))
            {
                text = MatchTR(i.Groups["data"].Value, date1, date2, radiocheck);
            }
            return text;
        }

        private string MatchTR(string p, DateTime date1, DateTime date2, bool radiocheck)
        {
            string pattern = "<td (?<data>.*?)</td>";
            Regex r = new Regex(pattern, RegexOptions.Singleline);
            PostedJobsLog log = PostedJobsLog.Create;
            string EncryptedJID = MatchEncryptedJID(p);
            string text = null;
            int jobcount = 0;
            foreach (Match i in r.Matches(p))
            {
                jobcount++;
                PostedJobs job = new PostedJobs();
                string JobNameTemp = MatchName(i.Groups["data"].Value).Replace("/", "-").Trim();
                job.Date = MatchJobDate(p);
                if (radiocheck == true&&String.IsNullOrEmpty(job.Date)==false)
                {
                    DateTime AppliedDate=new DateTime();
                        AppliedDate = IsAppliedEarlier(job.Date);                    

                    if (DateTime.Compare(AppliedDate, date1) < 0)
                    {
                        continue;
                    }
                    if (DateTime.Compare(AppliedDate, date2) > 0)
                    {
                        continue;
                    }

                }
                if (JobNameTemp.Length > 3)
                {

                    job.Name = JobNameTemp.Replace("&lrm;", "").Trim();
                    if (log.Contains(job.Name.Trim()) == false)
                    {
                        OnInformationDownload(new EventArguments() { Name = "Counting Jobs" + jobcount + " ", Details = job.Name, Date = DateTime.Now });
                        job.Link = "http://hiring.rozee.pk/" + MatchLink(i.Groups["data"].Value);
                        job.TotalApplicants = MatchParticipants(p);
                        job.Status = MatchActivation(p);
                        job.EncryptedJobID = EncryptedJID + "=&";
                        log.JobsList.Add(job);
                        text = null;
                    }
                }

            }
            return text;
        }
        private string MatchEncryptedJID(string p)
        {
            string pattern = "<a href=\"javascript:;\" (onclick|onClick){1}=\"_fb\\('job-manage-fb.*?jid=(?<data>.*?)'";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return (i.Groups["data"].Value);
            }
            return "";

        }
        private string MatchLink(string p)
        {
            string pattern = "<a href=\"(?!javascript)(?<data>.*?)\">";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }
        private string MatchParticipants(string p)
        {
            string Firstpattern = "<td align=\"center\">(\\s|\\r)*?<a href=\"job-applicants.php.*?<b>(?<data>\\d*)</b></a>";
            Regex r = new Regex(Firstpattern, RegexOptions.Singleline);
            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value;
            }
            return "";
        }

        private string MatchName(string p)
        {
            string pattern = "<a href=.*?<b>(?<data>.*)</b>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                return i.Groups["data"].Value.Replace("&Irm", "");
            }
            return "";
        }
        private string MatchJobDate(string p)
        {
            string temp = null;
            string pattern = "Promote this Job(.|\\r|\\s)*?</td>(\\r|\\s|.)*?<td align=\"center\">(?<data>.*?)</td>";

            Regex r = new Regex(pattern, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                temp = i.Groups["data"].Value.Replace("&Irm", "");
            }
            return temp;
        }

        private string MatchActivation(string p)
        {
            string patternone = " <td align=";
            string patterntwo = "\"center\">";
            string patternthree = @"\s*?<img id=.*?src=";
            string patternfour = "\"http://www.rozee.pk/i/ico/icon.*?alt=\"(?<data>.*?)\".*?title";

            Regex r = new Regex(patternone + patterntwo + patternthree + patternfour, RegexOptions.Singleline);

            foreach (Match i in r.Matches(p))
            {
                if (i.Groups["data"].Value.Contains("Activated") == true)
                {
                    return "Activated";


                }
                else
                {
                    return "Deactivated";
                }
            }
            return "";
        }

        public string Login(string email, string password)
        {
            client.Headers["User-Agent"] =
       "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
       "(compatible; MSIE 6.0; Windows NT 5.1; " +
       ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";
            var values = new NameValueCollection();
            values["email"] = email;
            values["password"] = password;

            values["ss"] = "YUhSMGNEb3ZMM2QzZHk1eWIzcGxaUzV3YXk5emN5OXNiMmRwYmkxMlpYSXVZM056K0U";
            values["cb"] = "WVVoU01HTkViM1pNTW1od1kyMXNkVnA1TlhsaU0zQnNXbE0xZDJGNU9XaGtXRkp2VEZkT2FHSkhlR2xaVjA1eVRHNUNiMk5CUFQwPStT";
            values["rp"] = "V1ZWb1UwMUhUa1ZpTTFwTlRUSlJlbHBJYXpGbFYwbDZZMGQ0WVZWNlZqTlpXR3MxWWxkSmVsTnROV2xOTVVZeFdUQmtiMlIzUFQwPStB";
            values["type"] = "slim";
            values["slug"] = "en";
            values["button"] = "Sign In";
            values["appID"] = "c81e728d9d4c2f636f067f89cc14862c";
            var response = client.UploadValues("http://secure.chabee.pk/if.php", values);
            var responseString = Encoding.Default.GetString(response);
            if (responseString.Contains("!DOCTYPE") || responseString.Contains("body"))
            {
                return "Incorrect Password or Email";
            }
            else
            {
                response_POSTEDJOBS = client.DownloadString("http://hiring.rozee.pk/posted-jobs.php?frm=topNav");
                return null;
            }
        }
    }
}
