using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Imagenary
{
    public delegate void InformationDownloadHandler(object o, EventArguments e);
    class PropertyFinder
    {
        public event InformationDownloadHandler InformationDownloadEvent;
        public virtual void OnInformationDownload(EventArguments e)
        {
            if (InformationDownloadEvent != null)
            {
                InformationDownloadEvent(this, e);

            }
        }
        PropertyLog log = PropertyLog.Create;
        PostalAdressLog POlog = PostalAdressLog.Create;



        public string FindMultipleProperties()
        {

            ApplicationData AppdataObj = ApplicationData.Create;
            string ConsoleText = null;
            foreach (KeyValuePair<string, PostalAdress> x in POlog.adressList)
            {
                /*
                 * Find Adress of Folder
                 * 
                 */
                OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Shifting File toS " + x.Value.PCD });
                if (String.IsNullOrEmpty(AppdataObj.PafFileAdress))
                {

                    Messagebox.Size = new Size(350, 150);
                    Messagebox.StartPosition = FormStartPosition.CenterScreen;



                    input.Size = new Size(200, 30);
                    input.Location = new Point(20, 50);



                    Label label = new Label();
                    label.Text = "Select the Directory Name, Pressing the browse Button";
                    label.Location = new Point(20, 20);




                    Button b = new Button();
                    b.Text = "Browse";
                    b.Click += new EventHandler(b_Click);
                    b.Location = new Point(250, 50);



                    Button ButtonSave = new Button();
                    ButtonSave.Text = "Save";
                    ButtonSave.Location = new Point(120, 80);
                    ButtonSave.Click += new EventHandler(ButtonSave_Click);

                    Messagebox.Controls.Add(label);
                    Messagebox.Controls.Add(b);
                    Messagebox.Controls.Add(input);
                    Messagebox.Controls.Add(ButtonSave);
                    Messagebox.ShowDialog();
                    input.Text = Messagebox_String;

                }
                string PafFileName = GenerateFileName(x.Value.PCD, AppdataObj.PafFileAdress);
                ReadPAFFile(PafFileName, x.Value);

                //  ConsoleText = ReadSPHFile("D://SPH Sample.csv", x.Value);
                //          ConsoleText += handler.ReadPAFFileSingle(PafFileName, x.Value.PCD.Remove(3, x.Value.PCD.Length - 3).ToUpper() + "$", x.Value);
                //FindAllProperties(...);
                OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Finding LandMarks" + x.Key });
                ConsoleText += FilterByLandMark(x.Value);
    
            }

            return ConsoleText;
        }
        #region Old Code for SearchByCoordinates
        [Obsolete]
        private int FindCoordinatesByGoogle(Property p, string adress)
        {
            int ErrorCode = 0;
            using (WebClient wc = new WebClient())
            {
                string wsUrl = "https://maps.googleapis.com/maps/api/geocode/json?address=" + adress + "&key=AIzaSyB2OcZcJBCNN8cIuntM4FHLG-mkYdz377A";
                string GoogleResponse = wc.DownloadString(wsUrl);
                try
                {
                    MatchCollection Gcollection = Regex.Matches(GoogleResponse, "\\\"location.*?\\n*?.*?lat.*?:(?<lat>.*?),(\\n|.).*?lng.*?:(?<lng>.*?)\\n");
                    //                        .lat.*?:(?<lat>.*?),(\\r\\n)*.*?lng.*?:(?<lng>.*?)(\\r\\n|})+");
                    if (Gcollection.Count < 1)
                    {
                        ErrorCode = 1;
                    }
                    foreach (Match m in Gcollection)
                    {
                        if (String.IsNullOrEmpty(m.Groups["lat"].Value) || String.IsNullOrEmpty(m.Groups["lng"].Value))
                        {
                            ErrorCode = 2;
                        }
                        else
                        {
                            p.Latitude = m.Groups["lat"].Value;
                            p.Longitude = m.Groups["lng"].Value;
                        }
                    }
                }
                catch (ArgumentException)
                {
                    ErrorCode = 3;
                }
            }
            return ErrorCode;
        }

        [Obsolete]
        private int FindCoordinatesByBing(Property p, string adress)
        {
            int ErrorCode = 0;

            string key = "Ah6u3flrOxfGT84qRft_XNQVgmNrqKwXm2ywctOX5bc5YIdHzAf6TFCaza2eGMqq";
            string wsUrl = "http://dev.virtualearth.net/REST/v1/Locations/UK/" + adress + "?&key=" + key;
            string RegexString = "coordinates\":\\[(?<lat>.*?),(?<lon>.*?)]";
            using (WebClient w = new WebClient())
            {
                string Response = w.DownloadString(wsUrl.Replace("%20?&key=", "?&key="));
                try
                {
                    MatchCollection collection = Regex.Matches(Response, RegexString);
                    if (collection.Count < 1)
                    {
                        ErrorCode = 1;
                    }
                    foreach (Match m in collection)
                    {
                        if (String.IsNullOrEmpty(m.Groups["lat"].Value) || String.IsNullOrEmpty(m.Groups["lon"].Value))
                        {
                            ErrorCode = 2;
                        }
                        else
                        {
                            p.Latitude = m.Groups["lat"].Value;
                            p.Longitude = m.Groups["lon"].Value;
                        }
                    }
                }
                catch (Exception)
                {
                    ErrorCode = 3;
                }
            }

            return ErrorCode;
        }
        [Obsolete]
        public string FindLongitude_Latitude()
        {
            string ConsoleLog = null;
            int ErrorCode;

            foreach (KeyValuePair<string, Property> p in log.propertyList)
            {
                if (String.IsNullOrEmpty(p.Value.Latitude) == false || String.IsNullOrEmpty(p.Value.Longitude) == false)
                {
                    continue;
                }
                else
                {
                    string localadd = p.Value.SimpleAdress + " " + p.Value.PCD;
                    string adress = p.Value.SimpleAdress.Replace(" ", "%20").Replace("!", "%21").Replace(".", "%2E").Replace("/", "%2F").Replace("(", "%28").Replace(")", "%29").Replace("+", "%2B").Replace("-", "%2D").Replace(".", "%2E").Replace("=", "%3D").Replace("_", "%5F").Replace(",", "%2C");

                    ErrorCode = FindCoordinatesByBing(p.Value, adress);

                    if (ErrorCode != 0)
                    {
                        if (ErrorCode == 1)
                        {
                            ConsoleLog += "No Coordinates Found by Bing for " + p.Key + "\n";
                        }
                        else if (ErrorCode == 2)
                        {
                            ConsoleLog += "Invalid Coordinates Found by Bing  for " + p.Key + "\n";
                        }
                        else if (ErrorCode == 3)
                        {
                            ConsoleLog += "Exception occured while Finding the Coordinates by Bing for " + p.Key + "\n";
                        }
                        else
                        {
                            ConsoleLog += "Bing-Error Not Detected. " + p.Key + "\n";
                        }

                        ErrorCode = FindCoordinatesByGoogle(p.Value, adress);

                        if (ErrorCode != 0)
                        {
                            if (ErrorCode == 1)
                            {
                                ConsoleLog += "No Coordinates Found by Google for " + p.Key + "\n";
                            }
                            else if (ErrorCode == 2)
                            {
                                ConsoleLog += "Invalid Coordinates Found By Google for " + p.Key + "\n";
                            }
                            else if (ErrorCode == 3)
                            {
                                ConsoleLog += "Exception occured while Finding the Coordinates by Google for " + p.Key + "\n";
                            }
                            else
                            {
                                ConsoleLog += "Google-Error Not Detected. " + p.Key + "\n";
                            }
                        }
                        else
                        {
                            ConsoleLog += "Google- Coordinates Detected. " + p.Key + "\n";

                        }
                    }
                    else
                    {
                        ConsoleLog += "Bing- Coordinates Detected. " + p.Key + "\n";
                    }
                }
            }
            return ConsoleLog;
        }
        [Obsolete]
        public string FindAllProperties(PostalAdress p)
        {
            string ConsoleLog = null;
            PostalAdress po = POlog.GetObject(p.PCD);

            /*If Properties are searched this month ,no need to process again
             */

            if (DateTime.Compare(po.SearchedProperties.AddMonths(1), DateTime.Now) >= 0 && po.SearchedProperties.Equals(new DateTime(0001, 1, 1)) == false)
            {
                ConsoleLog += "Already Searched this Postal Adress in this month\n";
                return ConsoleLog;
            }
            else
            {
                ConsoleLog += "Valid Properties Searched\n";
                string ScrappedString = null;
                using (WebClient webClientObj = new WebClient())
                {
                    try
                    {
                        ScrappedString = webClientObj.DownloadString("http://www.zoopla.co.uk/home-values/" + p.PCD);
                    }
                    catch (Exception e)
                    {
                        throw new Exception("Unable to Connect Zoople" + e.Message);
                    }
                    string b = "<a *?href=\"/property/(?<data>.*?)" + p.PCD.Replace(" ", "-").ToLower() + ".*?class=\"attrProperty\".*?>(?<data2>.*?)" + p.PCD.ToUpper() + "</a>";
                    string RequiredRegex = b;
                    try
                    {
                        MatchCollection collection = Regex.Matches(ScrappedString, RequiredRegex);
                        foreach (Match m in collection)
                        {
                            Property pobj = new Property();
                            pobj.SimpleAdress = m.Groups[2].Value.TrimEnd();
                            pobj.UrlAdress = @"C:\\Users\\jafar.baltidynamolog\\Documents\\Visual Studio 2010\\Projects\\Imagenary\\Imagenary\\bin\\" + m.Groups[2].Value.TrimEnd().Replace("\\\\", "\\").Replace(",", "") + "\\";
                            // log.Add(pobj, p.PCD);
                            po.AddPropertyKey(pobj.SimpleAdress);
                            ConsoleLog += "Property Addeds..." + pobj.SimpleAdress + "\n";
                        }
                    }
                    catch (ArgumentException ex)
                    {
                        throw new Exception("Exception while getting properties" + ex.Message);
                    }
                }
                po.SearchedProperties = DateTime.Now;
                ConsoleLog += "PostalAdress Added ...\n";
                POlog.Add(po);
            }
            return ConsoleLog;
        }
        #endregion

        #region EPC LandMarks Code
        public string FilterByLandMark(PostalAdress x)
        {
            string ConsoleLog = null;
            PostalAdress po = POlog.GetObject(x.PID);

            if (DateTime.Compare(x.SearchedLandMark.AddMonths(1), DateTime.Now) >= 0 || x.SearchedProperties.Equals(new DateTime(0001, 1, 1)) == false)
            {
                OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Trace:   Already Searched LandMark by this Postal Adress in this month\n" });
                ConsoleLog += "Trace:   Already Searched LandMark by this Postal Adress in this month\n";
                return ConsoleLog;
            }
            else
            {
                x.SearchedLandMark = DateTime.Now;
                HttpWebRequest request1 = null;
                Stream dataStream = null;
                string PostCode = "postcode=" + x.PCD.Replace(" ", "+").Trim();
                //          OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Creating LandMarkRequest\n" });
                request1 = (HttpWebRequest)HttpWebRequest.Create("https://www.epcregister.com/reportSearchAddressByPostcode.html");
                request1.UserAgent = " Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
                request1.ContentType = "application/x-www-form-urlencoded";
                request1.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
                request1.Referer = "https://www.epcregister.com/reportSearchAddressByPostcode.html";
                request1.Host = "www.epcregister.com";
                request1.Headers.Add("Accept-Encoding", " gzip,deflate,sdch");
                request1.Headers.Add("Accept-Language", " en-US,en;q=0.8");
                request1.Headers.Add("Origin", " https://www.epcregister.com");
                request1.Method = "POST";
                //   request.HttpMethod = WebRequestMethods.Http.Post;
                string postData = PostCode;
                byte[] byteArray = Encoding.UTF8.GetBytes(postData);
                dataStream = request1.GetRequestStream();


                // Write the data to the request stream.
                dataStream.Write(byteArray, 0, byteArray.Length);

                // Close the Stream object.
                //  dataStream.Close();
                WebResponse response = null;
                try
                {
                    response = request1.GetResponse();
                }
                catch (WebException)
                {
                    return "ERROR";
                    //                    throw new Exception("Error:  Can not Find Adress  --  " + ex.Message);
                }
                string responseFromServer = null;
                Uri responseUri = null;
                try
                {
                    string Status = ((HttpWebResponse)response).StatusDescription;

                    // Get the stream containing all content returned by the requested server.
                    dataStream = response.GetResponseStream();

                    // Open the stream using a StreamReader for easy access.
                    StreamReader reader = new StreamReader(dataStream);

                    // Read the content fully up to the end.
                    responseFromServer = reader.ReadToEnd();
                    responseUri = response.ResponseUri;
                    // Clean up the streams.
                    reader.Close();
                    dataStream.Close();
                    response.Close();
                }
                catch (Exception)
                {
                    return "ERROR2";
                }
                OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Link found for LandMarks\n" });
                List<Property> LandMarkCertifiedList = new List<Property>();

                using (WebClient c = new WebClient())
                {
                    string ResponseString = null;
                    try
                    {
                        ResponseString = c.DownloadString(responseUri);
                    }
                    catch (Exception)
                    {

                        OnInformationDownload(new EventArguments() { Name = "ERROR: ", Time = DateTime.Now, Details = "Exception while Reading File\n" });
                        ConsoleLog += "Exception Occured in Downloading LandMark string \n\n";
                        return ConsoleLog;
                    }

                    IsLocationThere2(x,ResponseString);

                    //try
                    //{
                    //    MatchCollection collection = Regex.Matches(ResponseString, "<a href=\\\"reportSearchAddressSelectAddress.*?>(?<data>.*?)</a>");

                    //    foreach (Match m in collection)
                    //    {
                    //        string localGroup = m.Groups["data"].Value;

                    //        IsLocationThere(x, localGroup, false);
                    //        //   OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "LandMark does not Matched\n" + x.PID + "\n\n" });


                    //    }
                    //}
                    //catch (ArgumentException ex)
                    //{
                    //    throw new Exception("Error: Can not Find LandMark Properties --" + ex.Message);
                    //}
                    return ConsoleLog;
                }
            }
        }

        private bool IsLocationThere2(PostalAdress x,string Document)
        {
            bool Result = false;

            List<string> keys = x.Propertykeys;
            foreach (string k in keys)
            {
                if (log.propertyList.ContainsKey(k))
                {
                    Property p = log.propertyList[k];
                    string adressTemp = p.Adress.ToLower().Replace(",", "").Replace(" ", "");
                    string DocumentHtml = Document.Replace(" ", "").Replace(",","").ToLower();
                    if (DocumentHtml.Contains(adressTemp))
                    {
                        p.IsLandMarkCertified = true;
                        p.IsLandMarkSearched = true;
                        Result = true;
                    }
    OnInformationDownload(new EventArguments() { Name = "SETRICHBOX", Time = DateTime.Now, Details = ": LMA= " + p.IsLandMarkCertified + "  -SPH=" + p.IsSphCertified + "  -  " + p.PID + " - " + p.Adress + "\n" });
                }
            }

            return Result;
        }
        [Obsolete]
        private bool IsLocationThere(PostalAdress x, string localGroup, bool IsSph)
        {
            List<string> keys = x.Propertykeys;
            foreach (string k in keys)
            {
                if (log.propertyList.ContainsKey(k))
                {
                    Property p = log.propertyList[k];
                    string adressTemp = p.Adress.ToLower().Replace(",", "").Replace(" ", "");
                    //    string adressTemp=p.Adress.ToLower().Remove(p.Adress.Length-p.PCD.Length,p.PCD.Length).Replace(",","").Replace(" ","");
                    string localGroupTemp = localGroup.ToLower().Replace(" ", "").Replace(",", "");

                    //   string adress = (p.ORD + p.ORC + p.SBN + p.BNA + p.POB + p.NUM + p.DST + p.STM + p.DDL + p.DLO + p.PTN + p.PCD).ToLower().Replace(" ", "");
                    p.LMA = localGroupTemp;
                    if (adressTemp.Equals(localGroupTemp))
                    {
                        if (IsSph == false)
                        {
                            p.IsLandMarkCertified = true;
                            p.IsLandMarkSearched = true;

                            return true;
                        }
                        else
                        {
                            p.IsSphCertified = true;
                            p.IsSphSearched = true;

                        }
                    }
                    else
                    {
                        if (IsSph == false)
                        {
                            p.IsLandMarkCertified = false;
                            p.IsLandMarkSearched = true;

                        }
                        else
                        {
                            p.IsSphCertified = false;
                            p.IsSphSearched = true;
                        }

                    }
                }
                else
                {
                    throw new Exception("Exception Here");
                }
            }

            return false;
        }
        #endregion


        internal string DownloadAllImages(StartPage startPage)
        {
            ApplicationData data = ApplicationData.Create;
            PropertyLog log = PropertyLog.Create;
            StringBuilder builder = new StringBuilder();

            foreach (var p in log.propertyList)
            {
                string adress = "https://www.google.com/maps/place/" + p.Value.Adress.Replace(" ", "+").Replace(",", "") + "/";
                if (Directory.Exists(data.ImageOutputDirectory + "//" + p.Value.Adress))
                {
                    string[] names = Directory.GetFiles(p.Value.Adress);
                    if (names.Count() >= 4)
                    {
                        continue;
                    }
                }
                builder.Clear();
                builder.Append(data.ImageOutputDirectory + "//" + p.Value.Adress + "//");
               // SetRichboxText("Downloading..." + p.Value.Adress);
                GSVTool tool = new GSVTool(adress, builder.ToString(), startPage);
                //    this.Hide();
                tool.Activate();
                tool.ShowDialog();

                if (tool.IsStopped == true)
                {
                    break;
                }
                if (tool.completed == true)
                {
                    // tool.Close();
                    continue;
                }
                //else
                //{
                //    tool.Activate();
                //    Application.DoEvents();
                //}


            }
//            this.Show();
            return null;
        }
        public string ReadSPHFile(string FileName, PostalAdress poobj)
        {
            OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Searching SPH file" });
            string ConsoleLog = null;
            CsvFileReader handler = new CsvFileReader(FileName);
            int count = 0;
            while (handler != null)
            {

                CsvRow row = new CsvRow();
                handler.ReadRow(row);
                if (row.Count == 0)
                {
                    break;
                }
                count++;
                if (count == 1)
                {
                    continue;
                }
                //If country is there
                if (row.Count >= 14)
                {
                    OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "SPH Record No: " + count });
                    string localGroupadress = row[8] + ", " + row[9] + ", " + row[10] + ", " + row[11] + ", " + row[12] + ", " + row[13].Replace(", ,", "");
                    while (localGroupadress.StartsWith(", "))
                    {
                        localGroupadress = localGroupadress.Remove(0, 2);
                    }

                    IsLocationThere(poobj, localGroupadress, true);
                }
                else
                {
                    ConsoleLog = "Row has less element than Required";
                }
            }
            return ConsoleLog;
        }


        #region Read PAF file Code
        public void ReadPAFFile(string FileName, PostalAdress poobj)
        {
            string ConsoleLog = null;
            CsvFileReader handler = new CsvFileReader(FileName);

            while (handler != null)
            {
                CsvRow row = new CsvRow();
                handler.ReadRow(row);
                if (row.Count == 0)
                {
                    break;
                }
                if (row[11].Equals(poobj.PCD.ToUpper()) == false)
                {
                    OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Searching " + poobj.PCD.ToUpper() + "==" + row[11].ToUpper() });
                    continue;
                }
                else
                {
                    OnInformationDownload(new EventArguments() { Name = "Trace: ", Time = DateTime.Now, Details = "Found Property " + poobj.PCD });

                    if (String.IsNullOrWhiteSpace(row[11].ToString()) == false)
                    {
                        Property p = new Property();
                        p.DateAdded = DateTime.Now;
                        try
                        {
                            p.ORD = row[0].ToString();
                            ConsoleLog += "Added" + p.ORD + "\n";
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");
                        }
                        try
                        {
                            p.ORC = row[1].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.SBN = row[2].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.BNA = row[3].ToString();
                            //      ConsoleLog += "Added" + p.BNA + "\n";
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.POB = row[4].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.NUM = row[5].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DST = row[6].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.STM = row[7].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DDL = row[8].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DLO = row[9].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.PTN = row[10].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.PCD = row[11].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.CTA = row[12].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.CTP = row[13].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.CTT = row[14].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.SCD = row[15].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.CAT = row[16].ToString();
                            //             ConsoleLog += "Added" + p.CAT + "\n";

                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }

                        try
                        {
                            p.NDP = row[17].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        try
                        {
                            p.DPX = row[18].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");
                        }

                        try
                        {
                            p.URN = row[19].ToString();
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");
                        }


                        try
                        {
                            p.PID = poobj.PID;
                        }
                        catch (Exception)
                        {
                            throw new Exception("Can not Find");

                        }
                        p.SetAdressAutomatically();
                        log.Add(p);

                        POlog.Add(p);
                        //adressList.Add(p.PID, p);

                        POlog.adressList[p.PID].AddPropertyKey(p.PCD + p.URN);
                    }
                }
            }
        }
        void b_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            Messagebox_String = dialog.SelectedPath;
            input.Text = Messagebox_String;
        }
        void ButtonSave_Click(object sender, EventArgs e)
        {
            ApplicationData data = ApplicationData.Create;
            data.PafFileAdress = Messagebox_String;
            Messagebox.Close();

        }

        string Messagebox_String = null;
        Form Messagebox = new Form();
        TextBox input = new TextBox();
        #endregion

        private string GenerateFileName(string pcd, string basedirectory)
        {
            string[] parameters = new string[6];
            string FileName = null;
            if (basedirectory.Contains("//"))
            {
                parameters[1] = basedirectory;
            }
            else
            {
                parameters[1] = basedirectory + "//";
            }

            parameters[2] = "//";
            try
            {
                parameters[3] = pcd.Remove(2, pcd.Length - 2).ToUpper() + "//";
                int length = pcd.Length;
                parameters[4] = pcd.Remove(length - 4, 4).ToUpper() + ".csv";
            }
            catch (Exception e)
            {
                throw new Exception("Index in Generating FileName for PAF file\nDescription :\t" + e.Message + "\nReason:\tWrong Input Postal Code");
            }
            FileName = parameters[1] + parameters[2] + parameters[3] + parameters[3] + parameters[4];
            return FileName;
        }

    }
}
