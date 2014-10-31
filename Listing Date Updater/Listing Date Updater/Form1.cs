using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.Text.RegularExpressions;
using System.Net;
using System.Threading;


/*This is main project Entry
 * Auther ;Abbas Naqvi
 * (ver 1.1)All code is uncommented and it is in running Form
 * Instructions :
 * 1) Close Skype and torrent ..because it need internet bytes
 * 2)Delete cookies ,in internet options
 * 3)Change your IP if tool is not able to navigate
 * 4)output file is in Z2 format .There is a need to change little bit code when we get the Actuall Zoople File
 * 5)The code of this can be found under two for loop; if(Z1) else(Z2)
 */ 


namespace Listing_Date_Updater
{
    public partial class Form1 : Form
    {
        ArrayList RecordList = new ArrayList();
        int DownloadedPagesCount = 0;
        string googleUrl = "https://www.google.co.uk/webhp?biw=1280&bih=597&source=lnt&tbs=cdr:1,cd_min:1/1/1990,cd_max:9/30/2014&tbm=#tbs=cdr:1%2Ccd_min:1%2F1%2F1990%2Ccd_max:9%2F30%2F2014&q= ";
        int navigationCount = 0;
        // bool ResultFound = false;
        string SearchQuery = null;
        bool Loseit = false;
        bool NavigateAgain = false;
        string InputResourse = null;
        string FileName;
        bool safeStop = false;
        string WorkSheetName;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScriptErrorsSuppressed = true;
            //textBox1.Text = @"C:\Users\jafar.baltidynamolog\Documents\Visual Studio 2010\Projects\Listing Date Updater\Listing Date Updater\output\RM1_Week190_Set1.xlsx";
            //textBox2.Text = "Data With Sold Price (NMD)";
            textBox1.Text = @"C:\Users\jafar.baltidynamolog\Documents\Visual Studio 2010\Projects\Listing Date Updater\Listing Date Updater\output\SSPC_Week189_Set_2_4.xlsx";
            textBox2.Text = "Match Founds";
            comboBox1.SelectedItem = 2;
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }
          
        }
       
        #region browse buttons
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            if (DialogResult.OK == dialog.ShowDialog())
            {
                textBox1.Text = dialog.FileName;
            }
        }

        #endregion


        private void buttonExit_Click(object sender, EventArgs e)
        {
            safeStop = true;
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {
            InputResourse = comboBox1.SelectedItem.ToString();
            FileName = textBox1.Text;
            WorkSheetName = textBox2.Text;
            buttonRun.BackColor = Color.Green;
            buttonRun.Update();
            panel1.Enabled = false;
            button1.Enabled = false;
            FileName = textBox1.Text;
            FrontThreadWork();
       //   backgroundWorker1.WorkerReportsProgress = true;
       //     backgroundWorker1.RunWorkerAsync();
            panel1.Enabled = true;
            button1.Enabled = true;

        }
        private int getProgress(int i, int total)
        {
            double x = (double)((double)i /(double) total) ;
            int y = (int)(x * 100);
            return y;
        }
        // Not Needed anymore
        #region Illegal Thread Access Protection use of Notification Label and richtextbox1
        delegate void SetTextCallback(string text);
        private void SetLabelText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelNotify.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelText);
                this.labelNotify.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelNotify.Text = text;
                this.labelNotify.Update();
            }
        }

        private void SetRichboxText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.richTextBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetRichboxText);
                this.richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text += text;
                this.richTextBox1.Update();
            }
        }

        #endregion

        private void buttonRun_MouseEnter(object sender, EventArgs e)
        {
            buttonRun.ForeColor = Color.Red;
        }

        private void buttonRun_MouseLeave(object sender, EventArgs e)
        {
            buttonRun.ForeColor = Color.White;
        }

        public void FrontThreadWork()
        {
            safeStop = false;
            DataSet set = null;
            ExcelOledbHandler handler = new ExcelOledbHandler(textBox1.Text, textBox2.Text);
            try
            {
                SetLabelText("loading Excel File");
//                backgroundWorker1.ReportProgress(2);
                set = handler.ReadExcelFile();
                SetLabelText("loaded Excel File");
              //  backgroundWorker1.ReportProgress(10);
            }
            catch (InvalidOperationException ex)
            {
                MessageBox.Show(ex.Message + ", While Reading Excel File");
                return;
            }
            catch (System.Data.OleDb.OleDbException ex)
            {
                MessageBox.Show(ex.Message + ", While Reading Excel File");
                return;
            }



            PropertyInfo p = null;
            int count = 0;

            foreach (DataTable table in set.Tables)
            {
                int Rowscount = table.Rows.Count;
                SetRichboxText("Total Records= " + Rowscount + "\n");

                foreach (DataRow dr in table.Rows)
                {
                    SetLabelText("Reading Record " + ++count + "   out of  " + Rowscount);

                    progressBar1.Value = getProgress(count, Rowscount);
                    progressBar1.Update();
                    #region Read Property From DataTable

                    try
                    {
                        p = new PropertyInfo();
                        if (InputResourse.Contains("Right move") && (FileName.Contains("RM1") || FileName.Contains("rm1") || FileName.Contains("z1") || FileName.Contains("Z1")))
                        {
                            p.InputSource = InputResourse;
                            p.PostCode = dr[0].ToString();
                            p.Price = dr[1].ToString();
                            p.SoldPriceHistory = dr[2].ToString();

                            p.Address = dr[3].ToString();
                            p.MarketedFrom = dr[4].ToString();
                            p.MarketedBy = dr[5].ToString();

                            p.PropertyType = dr[6].ToString();
                            p.PropertyID = dr[7].ToString();
                            p.MarketedDateEstimate = dr[8].ToString();
                        }
                        else if (InputResourse.Contains("SSPC") || FileName.Contains("RM2") || FileName.Contains("rm2") || FileName.Contains("z2") || FileName.Contains("Z2"))
                        {
                            //This is always going to be RM2 Format
                            p.Address = dr[0].ToString() + "*" + dr[1].ToString() + "*" + dr[2].ToString() + "*" + dr[3].ToString();
                            p.PostCode = dr[4].ToString();
                            p.Price = dr[5].ToString();
                            p.MarketedBy = dr[7].ToString();
                            p.NumberOfDays = dr[8].ToString();
                            p.SuccessUrl = dr[9].ToString();   //Indirect
                            p.HomeUrl = dr[10].ToString();    //Direct
                            p.MarketedBy = dr[11].ToString();
                            p.PropertyID = dr[12].ToString();
                            p.InputSource = InputResourse;
                        }
                        else
                        {

                            MessageBox.Show("File Format Confusion ..Please Choose the correct Format..");
                            return;
                        }
                    }
                    catch (InvalidCastException ex)
                    {
                        MessageBox.Show(ex.Message + "While Extracting data from Adapter ," + count + "   out of  " + Rowscount);
                        return;
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        MessageBox.Show(ex.Message + "While Extracting data from Adapter ," + count + "   out of  " + Rowscount);
                        return;
                    }
                    catch (DeletedRowInaccessibleException ex)
                    {
                        MessageBox.Show(ex.Message + "While Extracting data from Adapter ," + count + "   out of  " + Rowscount);
                        return;
                    }
                    #endregion

                    if (String.IsNullOrEmpty(p.MarketedFrom) == true && String.IsNullOrEmpty(p.PropertyID) == false)// add more conditions in if
                    {
                        string DBDate = null;

                        #region stop if stop lock is released
                        if (safeStop == true)
                        {
                            return;
                        }
                        #endregion
                        #region Database Check
                        AccessHandler Ahandler = new AccessHandler();
                        try
                        {
                            DBDate = Ahandler.IsRecordExist(p.PropertyID, p.PostCode, InputResourse);
                        }
                        catch (Exception ex)
                        {
                            SetLabelText(ex.Message + "While Checking If Record Exists " + count + "   out of  " + Rowscount);

                        }
                        if (String.IsNullOrEmpty(DBDate) == false)
                        {
                            p.MarketedFrom = DBDate;
                            SetLabelText("Record Already Exists In DB for " + p.PostCode + "  " + count + "   out of  " + Rowscount);
                            continue;
                        }
                        #endregion
                     
                        #region MousePrice
                        SetRichboxText("\n\n\n..............Mouse Price Starts................\n\n\n");
                        MousePriceScrapper Mscrapper = new MousePriceScrapper();
                        try
                        {
                            SetLabelText("Checking Mouse Price of " + p.PostCode + ".    " + count + "   out of  " + Rowscount);
                            Mscrapper.CheckMousePrice(p);
                        }
                        catch (Exception ex)
                        {
                            if (ex.Message.Contains("IP_BLOCKED"))
                            {
                                DialogResult dialogResult = MessageBox.Show("IP is blocked ,Kindly Change proxy to continue.", "IP is blocked", MessageBoxButtons.RetryCancel);
                                if (dialogResult.Equals(DialogResult.Retry))
                                {
                                    continue;
                                }
                                else
                                {
                                    safeStop = true;
                                    SetLabelText("Stopped");
                                }
                                return;
                            }
                            SetLabelText(ex.Message + " at " + p.PostCode + "   , " + count + "   out of  " + Rowscount);
                            SetRichboxText(ex.Message + " at " + p.PostCode + count + "   , " + "   out of  " + Rowscount + "\n");
                            //   MessageBox.Show(ex.Message+" at "+p.PostCode+"\n Do you want to quit?","Missing Results",MessageBoxButtons.YesNo);
                        }
                        if (String.IsNullOrEmpty(p.MarketedFrom) == false)
                        {
                            #region InsertOrUpdate in database
                            SetRichboxText(p.MarketedFrom + "   Date Found For  " + p.PostCode + "   , " + count + "   out of  " + Rowscount + "\n");
                            try
                            {
                                p.FoundSource = "Mouse Price";
                                Ahandler.InsertOrUpdate(p);
                                SetLabelText("Added Record of  " + p.PostCode + "in Database" + "   , " + count + "   out of  " + Rowscount);
                            }
                            catch (Exception ex)
                            {
                                SetLabelText(ex.Message + "Update in Database Failed for " + p.PostCode + "   , " + count + "   out of  " + Rowscount); return;
                            }
                            #endregion
                        }
                        else
                        {

                        }
                        #endregion
                     

 

                        #region newgoogle

                        navigationCount = 0;
                        DownloadedPagesCount = 0;
                        RecordList.Clear();
                        if (webBrowser1.Document != null)
                        {
                            HtmlDocument doc = this.webBrowser1.Document;
                            doc.Write(string.Empty);
                            webBrowser1.Stop();
                            this.webBrowser1.Navigate("about:blank");
                        }
                    
                       
                        Loseit = false;

                        string InDirectUrl = p.SuccessUrl;
                        string DirectUrl = p.HomeUrl;
                        string Adress = p.Address.Replace("*",",");

                        if (String.IsNullOrEmpty(InDirectUrl) == false)
                        {

                            #region Static and Dynamic URL
                            if (InDirectUrl.Contains("?") || InDirectUrl.Contains("#") || InDirectUrl.Contains("&") || InDirectUrl.Contains("="))
                            {
                                MessageBox.Show("Indirect url can not be dynamic");
                                //Dynamic URL
                              //  SearchQuery = "http://www.spcmoray.com/property.php " + Adress + "  ,";
                            }
                            else
                            {
                                //Static URL
                                SearchQuery = InDirectUrl;
                            }
                            #endregion
                            richTextBox1.Text += "\n\n---------INPUT BOX----------\nINDirect url= " + InDirectUrl + "\n Direct Url=" + DirectUrl + "\n Search Query= " + SearchQuery + "\n---------------------\n";
                            ArrayList xcollection = GetAllRecords(SearchQuery);
                            if (xcollection.Count == 0)
                            {
                                #region Find DirectUrl
                                if (String.IsNullOrEmpty(DirectUrl))
                                {
                                    DirectUrl = FindDirectUrl(InDirectUrl);
                                    //     DirectUrl=FindDirectUrl(InDirectUrl);
                                }
                                #endregion
                                if (String.IsNullOrEmpty(DirectUrl))
                                {
                                    MessageBox.Show("Can not Process ,we can not Find Direct Url");
                                }
                                else
                                {
                          //          MessageBox.Show("Continue...");
                                    #region CheckStatic and Dynamic
                                    if (DirectUrl.Contains("?") || DirectUrl.Contains("#") || DirectUrl.Contains("&") || DirectUrl.Contains("="))
                                    {
                                        string ResultString = null;

                                        try
                                        {
                                            ResultString = Regex.Match(DirectUrl, "(?<data>http.*?)(\\?|#)").Groups["data"].Value;
                                        }
                                        catch (ArgumentException ex)
                                        {
                                            MessageBox.Show(ex.Message);
                                        }



                                        //Dynamic URL
                                        SearchQuery = ResultString +" "+ Adress + "  ,";

                                        //                                        SearchQuery = "http://www.spcmoray.com/property.php " + Adress + "  ,";
                                    }
                                    else
                                    {
                                        //Static URL
                                        SearchQuery = DirectUrl;
                                    }
                                    #endregion
                                    #region FindIndirect Url
                                    richTextBox1.Text += "\n\nNow going to Indirect Url\n\n";
                                    navigationCount = 0;
                                    DownloadedPagesCount = 0;
                                    RecordList.Clear();
                                    this.webBrowser1.Navigate("about:blank");
                                    Loseit = false;
                                    HtmlDocument document = this.webBrowser1.Document;
                                    //  CleanBrowserThread();
                                    document.Write(string.Empty);
                                    webBrowser1.Stop();
                                    ArrayList xmcollection = GetAllRecords(SearchQuery);
                                    p.MarketedFrom = PrintCollection(xmcollection,p.SuccessUrl);
                                    #endregion
                                }
                            }
                            else
                            {
                                p.MarketedFrom = PrintCollection(xcollection,p.HomeUrl);
                            }
                        }
                        else {
                            if (String.IsNullOrEmpty(DirectUrl))
                            {
                                MessageBox.Show("Both urls are empty");
                            }
                            else {
                                #region CheckStatic and Dynamic
                                if (DirectUrl.Contains("?") || DirectUrl.Contains("#") || DirectUrl.Contains("&") || DirectUrl.Contains("="))
                                {
                                    string ResultString = null;

                                    try
                                    {
                                        ResultString = Regex.Match(DirectUrl, "(?<data>http.*?)(\\?|#)").Groups["data"].Value;
                                    }
                                    catch (ArgumentException ex)
                                    {
                                        MessageBox.Show(ex.Message);
                                    }



                                    //Dynamic URL
                                    SearchQuery = ResultString +" "+Adress + "  ,";

                                    //                                        SearchQuery = "http://www.spcmoray.com/property.php " + Adress + "  ,";
                                }
                                else
                                {
                                    //Static URL
                                    SearchQuery = DirectUrl;
                                }
                                #endregion
                                #region FindDirect Url
                                richTextBox1.Text += "\n\nNow going to Direct Url\n\n";
                                navigationCount = 0;
                                DownloadedPagesCount = 0;
                                RecordList.Clear();
                                this.webBrowser1.Navigate("about:blank");
                                Loseit = false;
                                HtmlDocument document = this.webBrowser1.Document;
                                //  CleanBrowserThread();
                                document.Write(string.Empty);
                                webBrowser1.Stop();
                                ArrayList collection = GetAllRecords(SearchQuery);
                                p.MarketedFrom = PrintCollection(collection,p.HomeUrl);
                                #endregion
                            
                            }
                        }

                        #endregion
                    }
                    try
                    {
                        handler.WriteInDatabase(p);

                    }
                    catch (Exception ex)
                    {

                        MessageBox.Show(ex.Message);
                        return;
                    }
                    
                    webBrowser1.Navigate("about:blank");
                    webBrowser1.Stop();

        //            MessageBox.Show("Continue...");
               //     Thread.Sleep(60 * 60 * 1);

                }

           //     backgroundWorker1.ReportProgress(100);
            }
            labelNotify.Text = "Completed...";
        }
        
        private string FindDirectUrl(string Indirecturl)
        {
            string DirectUrl = null;
            using (WebClient c = new WebClient())
            {
                string WebPage = c.DownloadString(Indirecturl);

                MatchCollection collection = Regex.Matches(WebPage, "<a href=\"(?<data>.*?)\">See full details including the property schedule on the ");

                foreach (Match m in collection)
                {

                    DirectUrl = m.Groups["data"].Value;
                    if (String.IsNullOrEmpty(DirectUrl) == false)
                    {
                        break;
                    }
                }
                return DirectUrl;
            }
        }
     
        #region Google Methods

        public string PrintCollection(ArrayList xcollection,string Searchurl)
        {
            string Date = null;
            richTextBox1.Text += "\n\n-----------Output Box--------------\n";
            foreach (var x in xcollection)
            {
                GoogleResult result = (GoogleResult)x;
                richTextBox1.Text += "Google Record....\n Title=" + result.Title + " \n  Link= " + result.Link + "\n Date= " + result.Date + "\n\n";
                if (CompareStrings(result.Link, Searchurl))
                {
                    Date = result.Date;
                }
            }
            richTextBox1.Text += "\n\n-----------------------------------\n";
            return Date;
        }
        public ArrayList GetAllRecords(string query)
        {
            string MadeUrl = googleUrl + "site:" + query.Trim();
            webBrowser1.Navigate(MadeUrl);
            richTextBox1.Text += "\n\n\n GoogleURL=" + MadeUrl + "\n\n\n";
            while (Loseit == false)
            {
                if (NavigateAgain == true)
                {
                    if (navigationCount >= 4)
                    {
                        break;
                    }

                    webBrowser1.Navigate("");
                    webBrowser1.Stop();
                    Thread.Sleep(60*30*1);
                    webBrowser1.Stop();

                    navigationCount++;
                    webBrowser1.Navigate(googleUrl + "site:" + query.Trim());
                    DownloadedPagesCount = 0;
                    Thread.Sleep(60 * 60 * 1);
                    richTextBox1.Text += "\n\n\nNavigated....\n\n\n";
                    NavigateAgain = false;
                }
                else
                {
                    Application.DoEvents();
                }
            }
            richTextBox1.Text += "Out";
            webBrowser1.Stop();
            return RecordList;
        }

        private void CheckInBase(HtmlElement element)
        {
            richTextBox1.Text += "\n\n--------Base---------\n\n";
            GoogleResult result = new GoogleResult();
            string ResultString = null;
            try
            {
                #region Link

                MatchCollection collection = Regex.Matches(element.InnerHtml, "<a onmousedown=\"return.*?href=\"(?<data>.*?)\">",
                    RegexOptions.Multiline);

                foreach (Match m in collection)
                {
                    ResultString = m.Groups["data"].Value;
                    if (ResultString.Contains("cache") == false && ResultString.Contains("#") == false && ResultString.Contains("site") == false)
                    {
                        if (ResultString.Contains("search?q=related"))
                        {
                            return;
                        }
                        else
                        {
                            result.Link = ResultString;
                            richTextBox1.Text += "\n\nLink=" + result.Link + "\n\n";
                        }
                    }
                }
                #endregion
                #region title
                collection = Regex.Matches(element.InnerHtml, @"<h3.*?<a onmousedown=.*?>(?<data>.*?)</a></h3>",
RegexOptions.Multiline);

                foreach (Match m in collection)
                {
                    ResultString = m.Groups["data"].Value;
                    if (ResultString.Contains("cache") == false && String.IsNullOrEmpty(ResultString) == false)
                    {

                        result.Title = ResultString;
                        richTextBox1.Text += "\n\nTitle=" + ResultString + "\n\n";

                    }
                }
                #endregion
                #region Date
                collection = Regex.Matches(element.InnerHtml, "<span class=\"f\">(?<data>.*?)</span>",
RegexOptions.Multiline);

                foreach (Match m in collection)
                {
                    ResultString = m.Groups["data"].Value;
                    richTextBox1.Text += "\n\nDate=" + ResultString + "\n\n";
                    result.Date = ResultString;
                }
                #endregion
            }
            catch (ArgumentException ex)
            {
                throw new Exception("While Reading Google Record" + ex.Message);
            }
            richTextBox1.Text += "\n\n--------Base Ends---------\n\n";

            #region Add in List
            bool AlreadyExist = false;
            foreach (object x in RecordList)
            {
                GoogleResult TempCheckObj = (GoogleResult)x;
                if (TempCheckObj.Link.Equals(result.Link))
                {
                    AlreadyExist = true;
                }
            }
            if (AlreadyExist == false)
            {

                RecordList.Add(result);
            }
            #endregion
        }
        public void PrintAllElements(HtmlElementCollection elements)
        {
            foreach (HtmlElement e in elements)
            {
                /*check if no Record Found
                 */
                if (e.InnerText == null)
                {
                    continue;
                }
                else
                {
                    if (e.InnerText.Contains("About"))
                    {
                        NavigateAgain = true;
                        richTextBox1.Text += "This is not The right page ,this is google Page";
                        return;
                    }
                    if (e.InnerText.Contains("did not match any documents"))
                    {
                        RecordList.Clear();
                        richTextBox1.Text += "%%%%%%%%%%%%%%%%%%%%%%%%%NO RESULT FOUND%%%%%%%%%%%%%%%%%%%";
                    }
                    string className2 = e.GetAttribute("className");
                    if (String.IsNullOrEmpty(className2) == false)
                    {
                        if (className2.Equals("rc"))
                        {
                            CheckInBase(e);
                        }
                    }
                    if (e.Children.Count > 0)
                    {
                        PrintAllElements(e.Children);
                    }
                    else
                    {
                        //This is going to be the Base DIV, we don't need it .
                    }
                }
            }
        }
        private void webBrowser1_DocumentCompleted_1(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            bool SuccessResult = false;
            richTextBox1.Text += "\n\nEntered :Page Downloaded " + DownloadedPagesCount + "\n\n";
            HtmlElement element = webBrowser1.Document.GetElementById("rso");
            if (element != null)
            {


                if (element.InnerHtml.Contains(@"li class=""g"""))
                {

                    richTextBox1.Text += "\n\n\nnINNER HTML=" + element.InnerHtml + "\n\n\n";
                    PrintAllElements(element.Children);
                    SuccessResult = true;
                }
                else
                {
                    //It does not contain any Result.although
                    NavigateAgain = true;
                }
            }
            else
            {
                /* Result....NOT FOUND...CHECK FOR CASES 
                 * Only Navigate Again
                 */
                richTextBox1.Text += "\n\n............FAILED........\n\n";
                NavigateAgain = true;
            }
            DownloadedPagesCount++;
            if (SuccessResult == true)
            {
                Loseit = true;
            }
        }
        private bool CompareStrings(string a,string b)
        {
            bool Result = false;
            string[] aStrings = a.Split(',',' ');
            string[] bStrings = b.Split(',', ' ');
            int matches = 0;
            foreach (var i in aStrings)
            {
                foreach (var j in bStrings)
                {
                    if (i.Equals(j))
                    {

                        matches++;
                    }
                }
            }
            if (Math.Abs(matches - aStrings.Count()) < 3)
            {

                Result = true;

            }
            return Result;
        }

        #endregion

        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text.Equals("ShowLog"))
            {
                richTextBox1.Visible = true;
                button1.Text = "HideLog";
            }
            else {
                richTextBox1.Visible = false;
                button1.Text = "ShowLog";
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

    }
}
