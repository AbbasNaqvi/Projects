﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Text.RegularExpressions;
using System.Diagnostics;


/*
 * Author: Abbas Naqvi
 * Version 1.0 contains all implementation of tool
 * Version 1.1 contains adding the variable in it and correction of output file name ----    Changes= 1 to 8.
 * version 1.2      Saved time by disabling TextWriterFile and added some new Code and commented old code Changes: 8 to 10
 * version 2.1       Make it Function by eradicating remaining errors ,Making it multithreaded 
 * Form1.cs is .Net form so it can not be used as a utility class ,all methods are private
 */

namespace Right_Move___Bedroom_Count
{
    public partial class Form1 : Form
    {
        DateTime StartingTime;
        string InputFileName = null;
        string OutputFileName = null;
        double AverageTime = 0;
        double LocalTime = 0;
        //(10)new way
        List<string> OutputLines = new List<string>();

        public Form1()
        {
            InitializeComponent();
        }
        
        private int? GetPagesCount(string document)
        {
            int? Result = null;
            string temp = GetStringFromRegex(document, "span class=\"pagenavigation pagecount.*?>(?<data>.*?)</span>");
            if (temp != null)
            {
                string[] splits = temp.Split(' ');
                int count = 0;
                if (int.TryParse(splits[3], out count) == false)
                {
                    Result = null;
                    throw new Exception("\nCan not parse the count of all pages\n");
                }
                if (count == 0)
                {
                    Result = null;
                }
                else
                {
                    Result = count;
                }
            }
            else
            {
                Result = null;
            }
            return Result;
        }
        private string GetStringFromRegex(string SubjectString, string pattern)
        {
            MatchCollection collection = Regex.Matches(SubjectString, pattern);
            foreach (Match x in collection)
            {
                return x.Groups["data"].Value;
            }
            return null;
        }
        private void ReadInputFile()
        {
            StartingTime = DateTime.Now;
            List<PropertyRecord> collection = new List<PropertyRecord>();
            var lines = System.IO.File.ReadLines(InputFileName);
            //        TextFileWriter handler = new TextFileWriter(OutputFileName);     (old way)
            int i = 0;
            int count = 0;
            int RecordsCount = lines.Count();
            // (2) changes line instead of only postal code in below line
            foreach (var line in lines)                              //(n)
            {
                count++;
                double localTime = 0;
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();
                if (string.IsNullOrEmpty(line))
                {
                    continue;
                }
                // (1)
                string[] tokens = line.Split(',');
                string postalcode = tokens[0];
                string country = null;
                //2.1 correction length to count
                if (tokens.Count() >= 2)
                {
                    country = tokens[1];
                }//2.1
                else
                {
                    country = "england";
                }//(1 ends)

                #region Notification
                i++;
                labelNotify2.Text = "Reading PostCode " + i + " out of " + lines.Count();
                labelNotify2.Update();
                decimal value = (i * 100) / RecordsCount;
                progressBar2.Value = (int)value;
                progressBar2.Update();
                #endregion

                try
                {//(8) added country because we have country variable 
                    List<PropertyRecord> temp = GetAllAddesses(postalcode, country);
                    if (temp != null)
                    {
                        collection.AddRange(temp);
                    }
                }
                catch (Exception ex)
                {
                    richTextBox1.Text += "\n" + line + "\n";
                    #region exceptionHandling
                    //if (MessageBox.Show("Error While Reading Input Record\n\nDetails=" + ex.Message, "Error", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    //{
                    //    labelNotify.Text = "Stopped";
                    //    labelNotify2.Text = "stopped";
                    //    progressBar1.Value = 0;
                    //    progressBar2.Value = 0;
                    //  //  return;
                    //}
                    //else
                    //{
                    //    continue;
                    //}
                    #endregion
                    
                }
                stopwatch.Stop();
                localTime = stopwatch.ElapsedMilliseconds;
                AverageTime += localTime;
                double time = (AverageTime / count);
                labelTime.Text = "AverageTime=  " + TimeSpan.FromMilliseconds(time).TotalSeconds + " s";
                labelTime.Update();
                double estimatedTime = (time * lines.Count());
                double milisecondsLeft = TimeSpan.FromMilliseconds(estimatedTime).TotalMinutes;
                labelEstimated.Text = "Total Minutes=    " + milisecondsLeft + "           ,Estimated Time of completion" + StartingTime.AddMilliseconds(estimatedTime).TimeOfDay;
                labelEstimated.Update();
            }
            if (collection != null)
            {
                System.IO.File.WriteAllLines(OutputFileName, OutputLines);
                //using Stream Writer is not a good option ,if we are using loop (it can be good if we add in serial wise)
                #region commented Unefficient Code
                //foreach (var x in collection)
                //{
                //    /*
                //     * Method to increase performance is by not using TextFileWriter
                //     *insted use System.IO.File.WriteAllLines() you will have to add another property   
                //     *set this line to address+","+bedcount, make list of each line and a
                //     */ 
                //    try
                //    {
                //        string temp=x.address.Replace(",", " ");
                //        handler.WriteRecordInCSV( temp+ ","+x.BedroomCount);               //(old way)
                //    }
                //    catch (Exception e)
                //    {
                //        #region exception Handling
                //        if (MessageBox.Show("Error While Eriting Record\nDetails= "+e.Message, "Error", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                //        {
                //            labelNotify.Text = "Stopped";
                //            labelNotify2.Text = "stopped";
                //            progressBar1.Value = 0;
                //            progressBar2.Value = 0;
                //            return;
                //        }
                //        #endregion
                //    }
                //    }
                #endregion
            }
            //      handler.Dispose();            (old way)
            string textFileName = OutputFileName;
            string tempName = OutputFileName.Replace(".txt", "");
            tempName += ".csv";
            CheckNameValidity(tempName);
            RenameFileFormat(textFileName);
            try
            {
                System.IO.File.WriteAllText("LogFile.txt", richTextBox1.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Can not Create Log File");            
            }
        }
        private void RenameFileFormat(string texttFileName)
        {

            //      string FullPath = System.IO.Directory.GetCurrentDirectory()+"\\"+OutputFileName+".txt";
            //  string DestinationPath = System.IO.Directory.GetCurrentDirectory()+"\\"+OutputFileName+".csv";
            string DestinationPath = OutputFileName;
            //  string DestinationPath = OutputFileName.Replace(".txt","") + ".csv";
            try
            {
                if (System.IO.File.Exists(texttFileName))
                {
                    System.IO.File.Move(texttFileName, DestinationPath);
                }
                else
                {

                    MessageBox.Show("Due to some Reason OutFile is not created ");
                }
            }
            catch (Exception e)
            {
                labelNotify2.Text="We are unable to create CSV output File, You can Find the output in notepad file\n\n\nDetails: " + e.Message;
            }
        }
        //(7) country is added as an optional parameter if country was not there in previous version
        private List<PropertyRecord> GetAllAddesses(string postalcode, string country = "england")
        {
            labelNotify.Text = "";
            progressBar1.Value = 0;

            labelNotify.Update();
            progressBar1.Update();

            richTextBox1.Text += "\n PostalCode=   " + postalcode;
            List<PropertyRecord> Records = new List<PropertyRecord>();
            using (WebClient w = new WebClient())
            {
                string url = CreateUrl(postalcode, country);
                string Document = w.DownloadString(url);
                int? pagescount = GetPagesCount(Document);
                richTextBox1.Text += "\nPages=" + pagescount;

                if (pagescount == null)
                {
                    return null;
                }
                else
                {
                    for (int i = 0; i <= pagescount; i++)             //(n*m)
                    {
                        int recordsOnPage = 0;
                        #region Notification
                        labelNotify.Text = "Reading Page " + i + " out of " + (pagescount);
                        labelNotify.Update();
                        decimal value = (i * 100) / (decimal)pagescount;
                        progressBar1.Value = (int)value;
                        progressBar1.Update();
                        #endregion

                        url = CreateUrl(postalcode, country, i);
                        Document = w.DownloadString(url);
                        //matches are added in Records(List)
                        #region Extract Record From Page
                        MatchCollection collection = Regex.Matches(Document, "<div class=\"soldDetails\">(.|\\n|\\r)*?class=\"soldAddress\".*?>(?<address>.*?)(</a>|</div>)(.|\\n|\\r)*?class=\\\"noBed\\\">(?<noBed>.*?)</td>|</tbody>");

                        foreach (Match x in collection)                     //(n*m*k)...we are saving another loop by using only one Regex here
                        {
                            PropertyRecord r = new PropertyRecord();
                            try
                            {
                                string BedroomCount = x.Groups["noBed"].Value;
                                if (String.IsNullOrEmpty(BedroomCount) == false)
                                {
                                    if (BedroomCount.Contains("bedrooms"))
                                    {
                                        r.BedroomCount = BedroomCount.Replace("bedrooms", "");
                                    }
                                    else if (BedroomCount.Contains("bedroom"))
                                    {
                                        r.BedroomCount = BedroomCount.Replace("bedroom", "");
                                    }
                                }
                                else
                                {
                                    r.BedroomCount = "-1";
                                }
                                r.address = x.Groups["address"].Value;
                                string postalCode = ExtractAndRemovePostalCode(ref r.address).Trim();
                                //(9)added to increase efficiencty           new way
                                //                         string temp = r.address.Replace(",", @""",""");

                                string temp = @"""" + r.address + @"""";
                                string line = postalCode + "," + temp + "," + r.BedroomCount;
                                OutputLines.Add(line);
                                //(9)ends
                            }
                            catch (Exception ex)
                            {
                                richTextBox1.Text = ex.Message;
                            }
                            Records.Add(r);
                            recordsOnPage++;
                        }
                        #endregion
                        richTextBox1.Text += "\n----Page=" + i +"   ,Records="+recordsOnPage;
                       
                        
                    }
                }
            }
            return Records;
        }
        private string ExtractAndRemovePostalCode(ref string address)
        {
            //AN -ANN-AAN-AANN
            string postalCode = Regex.Match(address, "(?<postalcode> [A-Z]{1,2}\\d{1,2} \\d{1}[A-Z]{2})", RegexOptions.RightToLeft).Groups["postalcode"].Value;
            //ANA- AANA
            if (string.IsNullOrEmpty(postalCode))
            {
                postalCode = Regex.Match(address, "(?<postalcode> [A-Z]{1,2}\\d{1}[A-Z]{1} \\d{1}[A-Z]{2})", RegexOptions.RightToLeft).Groups["postalcode"].Value;
            }

           //string postalCode = address.Substring(address.Length - 7);
           // address = address.Remove(address.Length - 7);
            address = address.Replace(postalCode, "");
            return postalCode.Trim();

        }
        //(4)added a country parameter
        private string CreateUrl(string postalcode, string country, int i = 0)
        {
            string urlMade = null;
            //(3) embedded a country variable in urlMade 
            string ConstructedPostalCode = postalcode.Trim().Replace(" ", "+");
            urlMade = @"http://www.rightmove.co.uk/house-prices/detail.html?country=" + country + "&locationIdentifier=PARTIAL%5E" + ConstructedPostalCode + "&searchLocation=" + ConstructedPostalCode + "&referrer=landingPage&index=" + (i * 25);

            return urlMade;
        }
        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            // dialog.Filter=(.txt)|
            if (DialogResult.OK == dialog.ShowDialog())
            {
                textBox1.Text = dialog.FileName;
            }
        }
        private void CheckNameValidity(string name)
        {
            int count = 1;
            while (System.IO.File.Exists(name))
            {
                Match m = Regex.Match(name, "\\(.*?\\).csv");
                if (string.IsNullOrEmpty(m.Value) == false)
                {//(6)Immutable characteristic of string name was ignored causing problem in naming output File
                    name = name.Replace(m.Value, "");
                    name += "(" + count + ").csv";
                }
                else
                {
                    name = name.Replace(".csv", "");
                    name += "(" + count + ").csv";
                }
                count++;
            }

            OutputFileName = name;

        }
        private void buttonRead_Click(object sender, EventArgs e)
        {
            InputFileName = textBox1.Text;

            OutputFileName = System.IO.Directory.GetCurrentDirectory() + "\\output.txt";

            //            OutputFileName = 

            ReadInputFile();

            //(5)Added Following code
            progressBar1.Value = 100;
            progressBar2.Value = 100;
            labelNotify.Text = "completed";
            labelNotify2.Text = "completed";
            //(5)ends
        }
        private void button1_Click(object sender, EventArgs e)
        {
            string text=textBox1.Text;
        string postalcode=  ExtractAndRemovePostalCode(ref text);
        richTextBox1.Text = postalcode;
        richTextBox1.Text += "\n\n" + text;
        }
    }
}
