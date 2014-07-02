using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Reflection;
using System.Windows.Forms;
using System.Net.Mail;

namespace HiringAutomationTool
{
    public partial class Form1 : Form
    {
        Color CustomBackColor;
        Color CustomForeColor;
        Color CustomtOPColor;
        DynamoWebClient w;
        string RelativePath = null;
        DateTime Date1 = new DateTime();
        DateTime Date2 = new DateTime();

        public Form1()
        {
            InitializeComponent();


            Form1.CheckForIllegalCrossThreadCalls = false;
            textBoxFolderAdress.Text = "D:\\";
            RelativePath = textBoxFolderAdress.Text;
            w = new DynamoWebClient(RelativePath);
            w.InformationDownloadEvent += new InformationDownloadHandler(w_InformationDownloadEvent);
            button2.Enabled = false;
            button3.Enabled = false;
            SerializationHandler shandler = new SerializationHandler();
            shandler.Deserialize();
            shandler.DeserializeJobs();
            shandler.DeserializeApplicants();
            ProfileLog profilelog = ProfileLog.Create;
            foreach (var x in profilelog.ProfileList)
            {
                if (String.IsNullOrEmpty(x.Email) == false)
                    comboBoxEmails.Items.Add(x.Email);
                if (x.IsActive == true)
                {
                    comboBoxEmails.Text = x.Email;
                }
            }
            xmlHandler handler = new xmlHandler();
            try
            {
                comboBoxColors.Items.Clear();
                handler.ReadList();
                foreach (var x in ThemesCollection.themesList)
                    comboBoxColors.Items.Add(x.ThemeName);
                comboBoxColors.Update();
            }
            catch (Exception)
            {
                ThemesCollection.InitializeList();
                LabelErrorMessage.Text = "File Not Found";
            }

            LabelErrorMessage.Text = "";
            buttonSetColor.Enabled = false;
            radioButtonThemes.Checked = true;
            radioButtonSpecificDate.Checked = true;
            progressBar1.Visible = false;

            if (ThemesCollection.themesList.Count > 0)
            {
                ThemesCollection.InitializeList();
                // comboBoxColors.SelectedItem = "default";
                Themes x = ThemesCollection.GetActive();
                if (x != null && String.IsNullOrEmpty(x.ThemeName) == false)
                {
                    SetThemes(x.ThemeName);
                    comboBoxColors.SelectedText = x.ThemeName;
                    comboBoxColors.Text = x.ThemeName;
                }
            }
            else
            {
                SetThemes("default");
            }
            radioButtonRecent.Checked = true; ThemesCollection.themesList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(themesList_CollectionChanged);

        }

        void themesList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int index = e.NewStartingIndex;
            comboBoxColors.Items.Add(ThemesCollection.themesList[index].ThemeName);
            comboBoxColors.Update();

        }


        private void radioButtonSpecificDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSpecificDate.Checked == true)
            {
                labelFrom.Enabled = true;
                labelTo.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;

            }
            else
            {

                labelFrom.Enabled = false;
                labelTo.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {


            if (String.IsNullOrEmpty(textBoxFolderAdress.Text))
            {
                MessageBox.Show("Please set folder adress");
                return;
            }
            button1.Enabled = false;
            button2.Enabled = true;
            button3.Enabled = true;
            progressBar1.Visible = true;
            radioButtonSpecificDate.Enabled = false;
            radioButtonRecent.Enabled = false;
            dateTimePicker1.Enabled = false;
            dateTimePicker2.Enabled = false;

            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.ProgressChanged += new ProgressChangedEventHandler(backgroundWorker1_ProgressChanged);
            if (backgroundWorker1.IsBusy == false)
            {
                backgroundWorker1.RunWorkerAsync();
            }
            else
            {
                button2.Enabled = false;
                button3.Enabled = false;
                button1.Enabled = true;

                labelProgressReport.Text = "Previous Thread is not yet stopped";
            } backgroundWorker1.WorkerSupportsCancellation = true;
            backgroundWorker1.WorkerReportsProgress = true;

        }

        void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            progressBar1.Value = e.ProgressPercentage;
            LabelProgress.Text = e.ProgressPercentage + "%";
            progressBar1.Refresh();
            if (e.ProgressPercentage == 2)
            {
                labelProgressReport.Text = "    Creating Directories";
            }
            else if (e.ProgressPercentage == 3)
            {
                labelProgressReport.Text = "Downloading all information ..it will take a long time";
            }
            else if (e.ProgressPercentage == 1)
            {
                labelProgressReport.Text = "Fethching Jobs information";
            }
            else if (e.ProgressPercentage == 0)
            {
                labelProgressReport.Text = "Tryng to authenticate(Login)";
            }
            else if (e.ProgressPercentage == 12)
            {
                labelProgressReport.Text = "Writing the Information in File";
            }
            else if (e.ProgressPercentage == 18)
            {
                labelProgressReport.Text = "Downloading CV! please be patient";
            }
            else if (e.ProgressPercentage == 100)
            {
                labelProgressReport.Text = "Congratulation !All work is done";
            }


        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled == true)
            {
                labelProgressReport.Text = "Downloaded have Terminated Successfully";
                progressBar1.Value = 0;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "Terminated Successfully";
                notifyIcon1.BalloonTipText = "All downloading is terminated ";
                notifyIcon1.ShowBalloonTip(1000);
                InitializeComponent();

            }
            else
            {
                notifyIcon1.ShowBalloonTip(100, "Download Completed", "All CV's are Downloaded", new ToolTipIcon());
                labelProgressReport.Text = "Congratulations! Downloading is completed" + e.Result;
                progressBar1.Value = 100;
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon1.BalloonTipTitle = "Download Completed";
                notifyIcon1.BalloonTipText = "Congratulations! Downloading CV's is successfully completed\n ";
                notifyIcon1.ShowBalloonTip(1000);
                InitializeComponent();
            }
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            radioButtonSpecificDate.Enabled = true;
            radioButtonRecent.Enabled = true;
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
            //            progressBar1.Visible = false;
        }

        private void ButtonTabBrowseEmailTemplate_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void radioButtonThemes_CheckedChanged(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            if (radioButtonThemes.Checked == true)
            {
                comboBoxColors.Update();
                comboBoxColors.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
                buttonSetColor.Enabled = false;
                button6.Enabled = false;
                textBoxThemeName.Enabled = false;
            }
            else
            {
                comboBoxColors.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                buttonSetColor.Enabled = true;
                button6.Enabled = true;
                textBoxThemeName.Enabled = true;
            }
        }
        private string SetThemes(string Name, Color A, Color B, Color C)
        {
            if (A.IsNamedColor == false || B.IsNamedColor == false || C.IsNamedColor == false)
            {
                return "Unnamed color will not work";
            }

            Themes theme = new Themes();
            theme.ThemeName = Name;
            theme.TextColor = B.Name;
            theme.BackColor = A.Name;
            theme.TopColor = C.Name;
            TCDataFiltering.ForeColor = Color.FromName(theme.TextColor);
            TCDataFiltering.BackColor = Color.FromName(theme.BackColor);
            TCSettings.BackColor = Color.FromName(theme.BackColor);
            TCSettings.ForeColor = Color.FromName(theme.TextColor);
            TCEmailTemplate.BackColor = Color.FromName(theme.BackColor);
            TCEmailTemplate.ForeColor = Color.FromName(theme.TextColor);
            TCEmail.BackColor = Color.FromName(theme.BackColor);
            TCEmail.ForeColor = Color.FromName(theme.TextColor);
            panel1.BackColor = Color.FromName(theme.TopColor);
            panel1.ForeColor = Color.FromName(theme.TextColor);
            TabControl1.Update();
            var x = ThemesCollection.Contains(theme);
            ThemesCollection.themesList.Remove(x);
            ThemesCollection.themesList.Add(theme);

            return null;

        }
        private string SetThemes(string themeName)
        {

            Themes theme = ThemesCollection.Contains(themeName);
            if (theme == null)
            {
                return "This theme is not specified";
            }
            TCDataFiltering.ForeColor = Color.FromName(theme.TextColor);
            TCDataFiltering.BackColor = Color.FromName(theme.BackColor);
            TCSettings.BackColor = Color.FromName(theme.BackColor);
            TCSettings.ForeColor = Color.FromName(theme.TextColor);
            TCEmailTemplate.BackColor = Color.FromName(theme.BackColor);
            TCEmailTemplate.ForeColor = Color.FromName(theme.TextColor);
            TCEmail.BackColor = Color.FromName(theme.BackColor);
            TCEmail.ForeColor = Color.FromName(theme.TextColor);
            panel1.BackColor = Color.FromName(theme.TopColor);
            panel1.ForeColor = Color.FromName(theme.TextColor);

            TabControl1.Update();
            return "Theme is updated";

        }
        private void comboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            string message = SetThemes(comboBoxColors.SelectedItem.ToString());
            LabelErrorMessage.Text = message;
            ThemesCollection.TurnActive(comboBoxColors.SelectedItem.ToString());

        }

        private void buttonLoginReset_Click(object sender, EventArgs e)
        {
            textBoxuserID.Text = "";
            textBoxpassword.Text = "";
            textBoxErrorLogin.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            colorDialog1.ShowDialog();
            CustomBackColor = colorDialog1.Color;
            labelColorAlert1.Text = colorDialog1.Color.Name;
            if (labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert3.Text.Equals("NotChoosen") == false)
            {
                buttonSetColor.Enabled = true;
            }
            else
            {
                buttonSetColor.Enabled = false;
            }
        }

        private void buttonSetColor_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            string ThemeName = textBoxThemeName.Text.Trim();
            if (ThemeName.Length > 0)
            {
                string message;
                if ((message = SetThemes(ThemeName, CustomBackColor, CustomForeColor, CustomtOPColor)) != null)
                {
                    LabelErrorMessage.Text = message.ToString();
                }
                else
                {
                    LabelErrorMessage.Text = "";

                }
            }
            else
            {

                LabelErrorMessage.Text = "Please Enter the Theme Name";
            }

        }

        private void button5_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            colorDialog1.ShowDialog();
            CustomForeColor = colorDialog1.Color;
            labelColorAlert2.Text = colorDialog1.Color.Name;
            if (labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert3.Text.Equals("NotChoosen") == false)
            {

                buttonSetColor.Enabled = true;
            }
            else
            {
                buttonSetColor.Enabled = false;
            }

        }

        private void radioButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            if (radioButtonThemes.Checked == true)
            {
                comboBoxColors.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
                buttonSetColor.Enabled = false;
                button6.Enabled = false;
                textBoxThemeName.Enabled = false;

            }
            else
            {
                comboBoxColors.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                buttonSetColor.Enabled = true;
                button6.Enabled = true;
                textBoxThemeName.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            colorDialog1.ShowDialog();
            CustomtOPColor = colorDialog1.Color;
            labelColorAlert3.Text = colorDialog1.Color.Name;
            if (labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert3.Text.Equals("NotChoosen") == false)
            {

                buttonSetColor.Enabled = true;
            }
            else
            {
                buttonSetColor.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            xmlHandler handler = new xmlHandler();
            handler.WriteList();
            SerializationHandler shandler = new SerializationHandler();
            shandler.Serialize();
            shandler.SerializeJobs();
            shandler.SerializeApplicants();
        }

        private void buttonLoginSave_Click(object sender, EventArgs e)
        {
            ProfileLog log = ProfileLog.Create;
            Profile p = new Profile();
            try
            {
                var x = new MailAddress(textBoxuserID.Text);
                p.Email = x.Address;
                p.Password = textBoxpassword.Text;
            }
            catch (FormatException)
            {
                textBoxErrorLogin.Text = "Email Format not Accepted";
                return;
            }
            catch (ArgumentNullException)
            {
                textBoxErrorLogin.Text = "Email can not be empty";
                return;
            }
            if (log.IsContainsProfile(p.Email) == true)
            {
                textBoxErrorLogin.Text = "Email is already reistered";
            }
            else
            {
                log.Add(p);
                log.TurnActive(p.Email);
                comboBoxEmails.Items.Add(p.Email);
                textBoxErrorLogin.Text = "Succeded";
                textBoxuserID.Text = "";
                textBoxpassword.Text = "";

            }
        }

        private void textBoxuserID_TextChanged(object sender, EventArgs e)
        {

            //   textBoxErrorLogin.Text = "";
        }

        private void textBoxpassword_TextChanged(object sender, EventArgs e)
        {
            textBoxErrorLogin.Text = "";
        }

        private void textBoxuserID_TabIndexChanged(object sender, EventArgs e)
        {
            textBoxErrorLogin.Text = "";

        }

        private void buttonDoLogin_Click(object sender, EventArgs e)
        {


        }

        private void button7_Click(object sender, EventArgs e)
        {

        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                if (radioButtonSpecificDate.Checked == true)
                {
                    Date1 = dateTimePicker1.Value.Date;
                    Date2 = dateTimePicker2.Value.Date;
                    if (DateTime.Compare(Date1,Date2)>=0)
                    {
                        MessageBox.Show("Ambigious Dates Error\nDetails:\n\nEither selected Date is same or ambigious difference");
                        e.Cancel = true;
                        return;

                    }
                }
            }
            catch (Exception)
            {
                e.Cancel = true;
                return;
            }

            RelativePath = textBoxFolderAdress.Text;
            ProfileLog profilelog = ProfileLog.Create;
            CSVhandler handler = new CSVhandler();
            PostedJobsLog jobs = PostedJobsLog.Create;


            backgroundWorker1.ReportProgress(0);
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            string x = null;
            try
            {
                string password = null;
                string loginEmail = null;
                loginEmail = comboBoxEmails.Text;
                password = profilelog.FindPasswordofThisEmail(loginEmail);

                x = w.Login(loginEmail, password);

            }
            catch (System.Net.WebException)
            {
                notifyIcon1.BalloonTipIcon = ToolTipIcon.Error;
                notifyIcon1.BalloonTipTitle = "Connection Error";
                notifyIcon1.BalloonTipText = "Please connect to internet in order to download CV's\n ";
                notifyIcon1.ShowBalloonTip(1000);
                e.Cancel = true;
                return;
            }

            if (String.IsNullOrEmpty(x) == false)
            {
                MessageBox.Show(x);
                e.Cancel = true;
                return;
            }
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            } backgroundWorker1.ReportProgress(1);
           w.DownloadAllPostedJobs(Date1, Date2, radioButtonSpecificDate.Checked);
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            backgroundWorker1.ReportProgress(2);
            w.CreateDirectories();
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            backgroundWorker1.ReportProgress(3);
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            w.OpenAllLinks(Date1, Date2, radioButtonSpecificDate.Checked);
            backgroundWorker1.ReportProgress(12);
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }
            try
            {
                handler.WriteTheApplicantLists(System.IO.Path.Combine(RelativePath));
            }
            catch (Exception eX)
            {
                MessageBox.Show(eX.Message);

            }
            backgroundWorker1.ReportProgress(18);
            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            }

            x = handler.WriteFolderApplicantLists(RelativePath);
            if (String.IsNullOrEmpty(x) == false)
            {
                MessageBox.Show(x);
            }


            if (radioButtonRecent.Checked == false && radioButtonSpecificDate.Checked == true)
            {
                w.DownloadCVS(textBoxFolderAdress.Text);
            }
            else if (radioButtonRecent.Checked == true && radioButtonSpecificDate.Checked == false)
            {
                w.DownloadCVS(textBoxFolderAdress.Text);
            }


            if (backgroundWorker1.CancellationPending == true)
            {
                e.Cancel = true;
                return;
            } backgroundWorker1.ReportProgress(100);

        }
        void w_InformationDownloadEvent(object o, EventArguments e)
        {
            labelDetailedInformation.Text = e.Name + " " + e.Details + " at " + e.Date.ToLocalTime(); // runs on UI thread
        }


        private void button2_Click(object sender, EventArgs e)
        {
            labelProgressReport.Text = "Wait ..The process is not yet terminated";
            backgroundWorker1.CancelAsync();
            button2.Enabled = false;
            button3.Enabled = false;
        }

        private void comboBoxEmails_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProfileLog log = ProfileLog.Create;
            log.TurnActive(comboBoxEmails.SelectedItem.ToString());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            folderBrowserDialog1.ShowDialog();
            textBoxFolderAdress.Text = folderBrowserDialog1.SelectedPath;
        }




    }
}
