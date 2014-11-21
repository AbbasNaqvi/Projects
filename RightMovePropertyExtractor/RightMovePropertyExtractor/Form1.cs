using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Reflection;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace RightMovePropertyExtractor
{
    public partial class Form1 : Form
    {
        WebsiteExtractor extractor = new WebsiteExtractor();
        string Log = null;
        public Form1()
        {
            InitializeComponent();
        }

        private void buttonRun_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerAsync();         
        }
        delegate void SetTextCallback(string text);
        private void SetLabelPageText(string text)
        {
            if (this.labelNotify.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelPageText);
                this.labelPageAverageTime.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelPageAverageTime.Text = text;
                this.labelPageAverageTime.Update();
            }
        }
        private void SetLabelRecordText(string text)
        {
            if (this.labelNotify.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelRecordText);
                this.labelAverageTime.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelAverageTime.Text = text;
                this.labelAverageTime.Update();
            }
        }
        private void SetLabelNotifyText(string text)
        {
            if (this.labelNotify.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelNotifyText);
                this.labelNotify.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelNotify.Text = text;
                this.labelNotify.Update();
            }
        }
        private void SetRichBoxText(string text)
        {
            if (this.labelNotify.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetRichBoxText);
                this.richTextBox1.Invoke(d, new object[] { text });
            }
            else
            {
                this.richTextBox1.Text += text;
                this.richTextBox1.Update();
            }
        }


        private bool getMyLink(string k, string SampleUrl, out string Url)
        {
            Url = "";
            bool Result = false;
            string sample = "locationIdentifier=OUTCODE%5E" + Regex.Match(SampleUrl, "locationIdentifier=OUTCODE%5E(?<data>\\d+)").Groups["data"].Value;
            string outcode = "locationIdentifier=OUTCODE%5E" + Regex.Match(k, "OUTCODE\\^(?<data>\\d+)").Groups["data"].Value;
            if (string.IsNullOrEmpty(sample) == false && string.IsNullOrEmpty(outcode) == false)
            {
                Url = SampleUrl.Replace(sample, outcode);
                Result = true;
            }
            return Result;
        }

        private void buttonLog_Click(object sender, EventArgs e)
        {
            Form f = new Form();
            f.Controls.Add(new RichTextBox() { Text = Log, Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Top | AnchorStyles.Bottom, Size = new Size(f.Width - 5, f.Width - 5) });
            f.ShowDialog();
        }

        string Document = null;
        private void SetDocument()
        {
            using (WebClient w = new WebClient())
            {
                Document = w.DownloadString("http://www.rightmove.co.uk/property-for-sale/find.html?searchType=SALE&locationIdentifier=OUTCODE%5E1859&insId=1&radius=0.0&displayPropertyType=&minBedrooms=&maxBedrooms=&minPrice=&maxPrice=&retirement=&partBuyPartRent=&maxDaysSinceAdded=&_includeSSTC=on&sortByPriceDescending=&primaryDisplayPropertyType=&secondaryDisplayPropertyType=&oldDisplayPropertyType=&oldPrimaryDisplayPropertyType=&newHome=&auction=false");
            }
        }


        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.FileName;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            double TotalMiliseconds = 0;
   //         labelStartingTime.Text = "Started Time:   " + DateTime.Now.ToShortTimeString();
            List<Record> GlobalList = new List<Record>();
            Dictionary<string, string> patterns = new Dictionary<string, string>();
            string FileName = textBox2.Text;
            string SampleUrl = "http://www.rightmove.co.uk/property-for-sale/find.html?searchType=SALE&locationIdentifier=OUTCODE%5E1859&insId=1&radius=0.0&displayPropertyType=&minBedrooms=&maxBedrooms=&minPrice=&maxPrice=&retirement=&partBuyPartRent=&maxDaysSinceAdded=&_includeSSTC=on&sortByPriceDescending=&primaryDisplayPropertyType=&secondaryDisplayPropertyType=&oldDisplayPropertyType=&oldPrimaryDisplayPropertyType=&newHome=&auction=false";
            System.IO.StreamWriter writer = new System.IO.StreamWriter("output.txt");

            #region Write Regex
            foreach (PropertyInfo i in typeof(Record).GetProperties())
            {
                try
                {
                    /*
                     * Regex for postal Code is still Not given
                     */
                    if (i.Name.Equals("PropertyID"))
                    {
                        patterns.Add(i.Name, "href=.*?-for-sale/property-(?<data>.*?).html");
                    }
                    else if (i.Name.Equals("Price"))
                    {
                        patterns.Add(i.Name, "<p class=\"price\">(.|\\n|\\r)*?<span>(?<data>.*?)</span>");
                    }
                    else if (i.Name.Equals("BedroomCount"))
                    {
                        patterns.Add(i.Name, "<a id=\"standardPropertySummary.*?<span class=\"\">.*?(?<data>.*?\\d) bedroom");
                    }
                    else if (i.Name.Equals("PropertyType"))
                    {
                        patterns.Add(i.Name, "<a id=\"standardPropertySummary.*?<span class=\"\">(?<data>.*?)</span>");
                    }
                    else if (i.Name.Equals("PostalCode"))
                    {
                        patterns.Add(i.Name, "<a id=\"standardPropertySummary.*?href=\"(?<data>.*?)\">");
                    }
                }
                catch (Exception)
                {
                    //item with same key is added
                }
            }
            patterns.Add("TotalRecords", "<span id='resultcount'>(?<data>\\d+)</span>");
            patterns.Add("PageNumbers", "\"pagenavigation pagecount\">page \\d of (?<data>\\d+)");
            #endregion
            int outercount = 0;
            //Stopwatch PageStopWatch = null;
            foreach (var k in System.IO.File.ReadLines(FileName))
            {
                List<Record> PageRecords = new List<Record>();
                string templink = null;
                int innercount = 0;
                #region LabelNotification
                Log += "\n\n\nLine= " + k;
                SetLabelNotifyText(outercount + ":   Processing " + k);
                outercount++;
     
                #endregion

                //#region StopWatch
                //if (PageStopWatch == null)
                //{
                //    PageStopWatch = new Stopwatch();
                //    PageStopWatch.Start();
                //}
                //else
                //{
                //    PageStopWatch.Stop();
                //    double PageRecordedMilisecond = PageStopWatch.ElapsedMilliseconds;
                //    TotalMiliseconds += PageRecordedMilisecond;
                //    double PageAverageMiliseconds = TotalMiliseconds / innercount;
                //    SetLabelPageText( "Approximate Page Fetch Time=  " + TimeSpan.FromMilliseconds(PageAverageMiliseconds).Minutes + " Minutes");
                //    PageStopWatch.Reset();
                //    PageStopWatch.Start();
                //}
                //#endregion

                if (getMyLink(k, SampleUrl, out templink))
                {
                    extractor.Link = templink;
                }
                else
                {
                    Log += "\nERROR Can not Page Link";
                }
                extractor.ExpectedRecordsPerPage = 10;
                Stopwatch RecordStopwatch = null;
                 SetRichBoxText( "\n\n\n\n");
                foreach (var x in extractor.ExtractValuesFromDocument(patterns, "(?<data><li class=\\\"(regular|premium)(\\r|\\n|.)*?id=\\\"dateWrapper)"))
                {
                    #region StopWatch
                    if (RecordStopwatch == null)
                    {
                        RecordStopwatch = new Stopwatch();
                        RecordStopwatch.Start();
                    }
                    else
                    {
                        RecordStopwatch.Stop();
                        double RecordedMilisecond = RecordStopwatch.ElapsedMilliseconds;
                        TotalMiliseconds += RecordedMilisecond;
                        double AverageMiliseconds = TotalMiliseconds / innercount;
                       SetLabelRecordText( "Approximate Record Fetch Time=  " + Math.Round((AverageMiliseconds / 1000),3) + " Seconds");
                        RecordStopwatch.Reset();
                        RecordStopwatch.Start();
                    }
                    #endregion

                    Log += extractor.Log;
                    if (extractor.TerminationNotice == true)
                    {
                        break;
                    }
                    Record r = new Record();
                    if (string.IsNullOrEmpty(x.PostalCode) == false)
                    {
                        r.PostalCode = x.PostalCode.Trim();
                    }
                    if (string.IsNullOrEmpty(x.Price) == false)
                    {
                        r.Price = x.Price.Trim();
                    }
                    if (string.IsNullOrEmpty(x.PropertyID) == false)
                    {
                        r.PropertyID = x.PropertyID.Trim();
                    }
                    if (string.IsNullOrEmpty(x.PropertyType) == false)
                    {
                        r.PropertyType = x.PropertyType.Trim();
                    }
                    if (string.IsNullOrEmpty(x.BedroomCount) == false)
                    {
                        r.BedroomCount = x.BedroomCount.Trim();
                    }
                    PageRecords.Add(r);
                    #region RichBoxShow
                    if (innercount % 10 == 0 && innercount != 0)
                    {
                        SetRichBoxText("\n\nNextPage\n");
                    }
                    else if (innercount == 0)
                    {
                         SetRichBoxText( "\nNewRecord");
                         SetRichBoxText( "\n" + k + "\n");
                    }
                    innercount++;

                     SetRichBoxText( "\n" + x.PropertyID + "    ,    " + x.PropertyType + "   .  " + x.BedroomCount + "     .    " + x.Price + "     ,  " + x.PostalCode);
                    #endregion
                }
                #region FinalCheck
                if (extractor.TerminationNotice == false)
                {
                    if (extractor.TotalRecords == PageRecords.Count)
                    {
                        foreach (var x in PageRecords)
                        {
                            writer.WriteLine(x.PropertyID + "," + x.PropertyType + "," + x.BedroomCount + "," + x.Price + "," + x.PostalCode);
                        }
                        //     GlobalList.AddRange(PageRecords);
                        Log += "\nThis Record is valid,Added in File";
                    }
                }
                else
                {
                    Log += extractor.Log;
                    Log += "\nRecord Processing is terminated";
                }
                #endregion
            }
            writer.Dispose();
            SetLabelNotifyText("Completed");
        }
    }
}
