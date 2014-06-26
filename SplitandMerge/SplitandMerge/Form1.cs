using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SplitandMerge
{
   
    public partial class Form1 : Form
    {
        string LogResult = null;
        public Form1()
        {
            InitializeComponent();
            radioButtonSplit.Checked = true;
            backgroundWorker1.DoWork += new DoWorkEventHandler(backgroundWorker1_DoWork);
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            Form1.CheckForIllegalCrossThreadCalls = false;
            ExcelHandlercs handler = new ExcelHandlercs();
            handler.KillProcesses("EXCEL");
            buttonLShowLog.Visible = true;

           
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label5.Text = "Work is Completed at "+DateTime.Now;
            panel1.Enabled = true;
            notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon1.BalloonTipText = "Completed";
            notifyIcon1.BalloonTipTitle = "Work is Completed";
            notifyIcon1.ShowBalloonTip(100);
            buttonLShowLog.Visible = true;
        }

        void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            panel1.Enabled = false;
            ExcelHandlercs handler = new ExcelHandlercs();
            handler.InformationDownloadEvent += new InformationDownloadHandler(handler_InformationDownloadEvent);
            if (radioButtonSplit.Checked == true)
            {
                LogResult+="\n\n"+handler.ReadBrochures(textBoxInput.Text, textBoxWSName.Text);
                BrochuresCollection bc = BrochuresCollection.Create();
                LogResult+="\n\n"+ handler.WriteBrochures(textBoxOutput.Text);
            }
            else
            {
                LogResult+= handler.Merge(textBoxInput.Text, textBoxOutput.Text);
            }
        }

        void handler_InformationDownloadEvent(object o, EventArguments e)
        {
            label5.Text = e.Name + "    ," + e.Details + "      ," + e.Time;
            Update();
        }

        private void radioButtonMerge_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonMerge.Checked == true)
            {
                labelInput.Text = "Folder: ";
                labelOutput.Text = "File: ";
                buttonAction.Text = "Merge";
                textBoxInput.Text = "";
                textBoxOutput.Text = "";
            }
        }

        private void radioButtonSplit_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSplit.Checked == true)
            {
                labelInput.Text = "File: ";
                labelOutput.Text = "Folder: ";
                buttonAction.Text = "Split";
                textBoxInput.Text = "";
                textBoxOutput.Text = "";
            }
        }
        /*
 * 
 * iNPUT bROWSE
 * 
 * 
 */


        private void buttonInput_Click(object sender, EventArgs e)
        {
            if (radioButtonSplit.Checked == true)
            {
                openFileDialog1.ShowDialog();
                if (openFileDialog1.FileName.Contains(".xls") == false || openFileDialog1.FileName.Contains(".xlsx") == false)
                {
                    MessageBox.Show("You can only choose Excel File");
                     notifyIcon1.BalloonTipText = "You can only choose Excel File";
                     notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                    notifyIcon1.BalloonTipTitle = "ExcelError";
                    notifyIcon1.ShowBalloonTip(100);
                }
                else
                {
                    textBoxInput.Text = openFileDialog1.FileName;
                }
            }
            else
            {
                folderBrowserDialog1.ShowDialog();
                if (folderBrowserDialog1.SelectedPath == null)
                {
                    MessageBox.Show("Please select valid Folder");
                    notifyIcon1.BalloonTipText = "You can only choose Excel File";
                    notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning;
                    notifyIcon1.BalloonTipTitle = "ExcelError";
                    notifyIcon1.ShowBalloonTip(100);
                }
                else
                {
                    textBoxInput.Text = folderBrowserDialog1.SelectedPath;
                }
            }
        }


        /*
         * 
         * OutPUT bROWSE
         * 
         * 
         */

        private void buttonOutput_Click(object sender, EventArgs e)
        {

            if (radioButtonSplit.Checked == true)
            {
                folderBrowserDialog1.ShowDialog();
                if (folderBrowserDialog1.SelectedPath == null)
                {
                    MessageBox.Show("Please Select valid Folder");
                     notifyIcon1.BalloonTipText = "You can only choose Excel File";
                     notifyIcon1.BalloonTipIcon = ToolTipIcon.Warning; 
                    notifyIcon1.BalloonTipTitle = "ExcelError";
                     notifyIcon1.ShowBalloonTip(100);
                }
                else
                {
                    textBoxOutput.Text = folderBrowserDialog1.SelectedPath;
                }
            }
            else
            {
                saveFileDialog1.ShowDialog();
                if (saveFileDialog1.FileName.Contains(".xls") == true || saveFileDialog1.FileName.Contains(".xlsx") == true)
                {
                    textBoxOutput.Text = saveFileDialog1.FileName; 
                }
                else
                {
                    MessageBox.Show("You can only choose Excel File");
                }
            }
        }

        private void buttonAction_Click(object sender, EventArgs e)
        {
            buttonLShowLog.Visible = false;       
            backgroundWorker1.RunWorkerAsync();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            ExcelHandlercs handler = new ExcelHandlercs();
            handler.KillProcesses("EXCEL");
        }

        private void buttonLShowLog_Click(object sender, EventArgs e)
        {
            Form LogForm = new Form();
            LogForm.Load += new EventHandler(LogForm_Load);
            LogForm.Show();
        }

        void LogForm_Load(object sender, EventArgs e)
        {
            InitializeComponent();
            RichTextBox box = new RichTextBox();
            box.Text = LogResult;
            box.ReadOnly = true;
            box.WordWrap = false;
            box.ScrollBars = RichTextBoxScrollBars.ForcedBoth;
            box.Size = new Size(1250, 300);
            box.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            Form LogForm = (Form)sender;
            LogForm.Controls.Add(box);
        }
    }
}


