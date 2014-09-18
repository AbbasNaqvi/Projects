using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace CSVMatch
{
    public partial class Form1 : Form
    {

        HashSet<string> SugarFileList = new HashSet<string>();
        string SugarFilePath = null;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        string[] InputFileNames = null;
        int InputColumnNumber;
        private void buttonMatch_Click(object sender, EventArgs e)
        {
            SaveSugarFile(textBox1.Text, (int)numericUpDown1.Value);
            InputFileNames = richTextBox1.Text.Split('\n');
            InputColumnNumber = (int)numericUpDown2.Value;
            backgroundWorker1.RunWorkerAsync();
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);

        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            labelNotify.Text = "Completed";
        }

        private void buttonBrowse2_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = true;
            richTextBox1.Text += "";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                foreach (var x in ofd.FileNames)
                {
                    richTextBox1.Text += x.ToString() + "\n";
                }
            }
        }

        private void buttonBrowse1_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Multiselect = false;

            if (ofd.ShowDialog() == DialogResult.OK)
            {
                SugarFilePath = ofd.FileName;
                textBox1.Text = SugarFilePath;
            }

        }

        HashSet<string> Lines = new HashSet<string>();


        #region Read CSV file Code


        public void SaveSugarFile(string filename, int columnnumber)
        {
            SetLabelText("Reading Sugar FIle" + DateTime.Now);

            using (CsvFileReader handler = new CsvFileReader(filename))
            {
                CsvRow row = null;
                while (handler != null)
                {
                    row = new CsvRow();
                    handler.ReadRow(row);
                    if (row.Count == 0)
                    {
                        break;
                    }
                    else
                    {
                        SugarFileList.Add(row[columnnumber]);
                       // SetLabelText("Saving Sugar" + ++index);
                    }
                   
                }
            }
        }
       // List<string> MatchedStrings = new List<string>();
        public void ReadInputFiles(string filename, int columnnumber)
        {
            Lines.Clear();
            SetLabelText("Reading Input FIle" + DateTime.Now);
            CsvRow row = null;
            using (CsvFileReader handler = new CsvFileReader(filename))
            {
                while (handler != null)
                {
                    row = new CsvRow();
                    handler.ReadRow(row);
                    try
                    {
                        string line = row[0];
                        if (SugarFileList.Contains(row[columnnumber]))
                        {
                            row.Add("true");
                        }
                        Lines.Add(GetStringFromList(row));
                    }
                    catch (ArgumentException )
                    {
                        break;
                    }
                }
            }
            try
            {
                File.Delete(filename);

            }
            catch (Exception E)
            {

                MessageBox.Show("xas");
            }
            File.WriteAllLines(filename, Lines);          
            SetLabelText("Writing Input FIle" + DateTime.Now);
        }


        private string GetStringFromList(List<string> row)
        {
            string Result = null;
            foreach (var x in row)
            {
                Result += x+",";
            }
            Result.Remove(Result.Length-1, 1);
            return Result;
        }

        //    SetLabelText("Read Another File" + DateTime.Now);
         //   labelNotify.Update();
        
        #endregion
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
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            foreach (string i in InputFileNames)
            {
                ReadInputFiles(i, InputColumnNumber);
            }
            SetLabelText("Finished");
        }

    }

}
