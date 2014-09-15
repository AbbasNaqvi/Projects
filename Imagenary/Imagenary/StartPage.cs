using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Imagenary
{
    public partial class StartPage : Form
    {
        NotifyIcon icon = null;
        public StartPage()
        {
            InitializeComponent(); 
        }
            
        void settings_InformationDownloadEvent(object o, EventArguments e)
        {
            if (e.Name.Equals("SET"))
            {
                SetTextBoxText(e.Details);
            }
        }
        void finder_InformationDownloadEvent(object o, EventArguments e)
        {
            if (e.Name.Equals("SETRICHBOX"))
            {
                SetRichboxText(e.Details);
            }
            else
            {
                SetLabelText(e.Name + "    ," + e.Details + "      ," + e.Time);
            }
            // Update();
        }
        private void StartPage_Load(object sender, EventArgs e)
        {
             icon= new NotifyIcon();
             icon.Icon =new Icon("C:\\Users\\jafar.baltidynamolog\\Documents\\Visual Studio 2010\\Projects\\Imagenary\\Imagenary\\Resources\\2.ico");
            icon.Text ="imaginary";
            SerializationHandler Serhandler = new SerializationHandler("D:");
            Serhandler.DeserializeProperties();
            Serhandler.DeserializePostalCodes();
            Serhandler.DeserializeApplicationData();
            ApplicationData data = ApplicationData.Create;
            textBox1.Text=data.MainFileAdress;


          /*  PrintAllProperties();
            PrintAllPostalCodes();*/
        }
        #region Printing
        public void PrintAllProperties()
        {
            int count = 0;
            PropertyLog log = PropertyLog.Create;
         //   richTextBox1.Text += "------------------------\n\n";
            foreach (KeyValuePair<string, Property> x in log.propertyList)
            {
                SetRichboxText(++count+": LMA= "+x.Value.IsLandMarkCertified+"  -SPH="+x.Value.IsSphCertified+"  -  "+ x.Value.PID  +" - "+x.Value.Adress+"\n");
                SetRichboxText("LandMarkAdress="+x.Value.LMA+"\n\n");
             //   richTextBox1.Update();
            }
        }
        public void PrintAllPostalCodes()
        {
            int count = 0;
            richTextBox1.Text += "------------------------\n\n";
            PostalAdressLog log = PostalAdressLog.Create;
           

            foreach (KeyValuePair<string, PostalAdress> x in log.adressList)
            {
                richTextBox1.Text += ++count + "\t" + x.Value.PCD + "\t" + x.Key.ToString() + "\n";
                richTextBox1.Update();
            }
        }
        #endregion
        private void buttonStart_Click(object sender, EventArgs e)
        {
            //PropertyFinder finder = new PropertyFinder();
            //finder.InformationDownloadEvent += new InformationDownloadHandler(finder_InformationDownloadEvent);
            //icon.BalloonTipText = "STARTED";
            //icon.ShowBalloonTip(5*60*1000);
            //string ConsoleText = null;
            //if (String.IsNullOrEmpty(textBox1.Text)==false)
            //{
            //    OledbHandler Excelhandler = new OledbHandler();
            //   ConsoleText= Excelhandler.ReadExcelFileSingle(textBox1.Text.Trim(), "No Addresses Found$", "POSTCODE");
            //}
            //richTextBox1.Text = ConsoleText;     
            //label2.Text = "Start time=" + DateTime.Now;
            //ConsoleText = finder.FindMultipleProperties();
            //richTextBox1.Text += ConsoleText;
            //this.Hide();
            //ConsoleText+=this.DownloadAllImages(this);
            //this.Show();
            //richTextBox1.Text = ConsoleText;
            string ConsoleText = null;
            backgroundWorker2.RunWorkerAsync();
            PropertyFinder finder = new PropertyFinder();
          //  ConsoleText += this.DownloadAllImages(this);
              ConsoleText +=  finder.DownloadAllImages(this);
            //  this.Show();
               SetRichboxText(ConsoleText);
            SetRichboxText("\n\n\n");
        }
        internal string DownloadAllImages(StartPage startPage)
        {
            ApplicationData data = ApplicationData.Create;
            PropertyLog log = PropertyLog.Create;
           StringBuilder builder = new StringBuilder();

            foreach (var p in log.propertyList)
            {
                string adress = "https://www.google.com/maps/place/" + p.Value.Adress.Replace(" ", "+").Replace(",", "") + "/";
                string TempDirectory = data.ImageOutputDirectory + "//" + p.Value.Adress;
                if (Directory.Exists(TempDirectory))
                {
                    string[] names = Directory.GetFiles(p.Value.Adress);
                    if (names.Count() >= 4)
                    {
                        continue;
                    }
                }
                builder.Clear();
                builder.Append(data.ImageOutputDirectory+"//"+p.Value.Adress+"//");
                SetRichboxText("Downloading..." + p.Value.Adress);
                GSVTool tool = new GSVTool(adress,builder.ToString(), startPage);
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
            this.Show();
            return null;
        }

        private void buttonBrowse_Click(object sender, EventArgs e)
        {
            PropertyFinder finder = new PropertyFinder();
            richTextBox1.Clear();
        }

        private void buttonStartInspection_Click(object sender, EventArgs e)
        {
            ImageMatch matchform = new ImageMatch(this);
            matchform.Show();
           // this.Hide();
        }

        private void StartPage_FormClosed(object sender, FormClosedEventArgs e)
        {
            SerializationHandler handler = new SerializationHandler("D:");
            handler.SerializePostalCodes(); 
            handler.SerializeProperties();
            handler.SerializeApplicationData();
            Environment.Exit(0);
        }

        private void buttonSettings_Click(object sender, EventArgs e)
        {
            SetSettings settings = new SetSettings();
            settings.InformationDownloadEvent += new InformationDownloadHandler(settings_InformationDownloadEvent);
            settings.Show();
        }

     
        private void buttonPrint_Click(object sender, EventArgs e)
        {
            label1.Text = "Printing All Properties ,This may take about 40 to 90 seconds";
            backgroundWorker1.RunWorkerAsync();
        }
        #region Toggle Richbox Visibility
        bool IsRichBox_Visible = true;
        private void buttonRichBox_Click(object sender, EventArgs e)
        {
            if (IsRichBox_Visible == true)
            {
                buttonRichBox.Text = "Show";
                richTextBox1.Visible = false;
                IsRichBox_Visible = false;
            }
            else
            {
                buttonRichBox.Text = "Hide";
                richTextBox1.Visible = true;
                IsRichBox_Visible = true;
            
            }
        }
        #endregion
       

        #region Cross Thread Access Fucntions
        
        delegate void SetTextCallback(string text);
        private void SetTextBoxText(string text) {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.textBox1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelText);
                this.textBox1.Invoke(d, new object[] { text });
            }
            else
            {
                this.textBox1.Text = text;
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
            }
        }
        private void SetLabelText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.label1.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelText);
                this.label1.Invoke(d, new object[] { text });
            }
            else
            {
                this.label1.Text = text;
            }
        }
        #endregion


        private void backgroundWorker1_DoWork_1(object sender, DoWorkEventArgs e)
        {
            //PrintAllPostalCodes();
            PrintAllProperties();
            SetLabelText("All Properties are Printed");
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            PropertyFinder finder = new PropertyFinder();
            finder.InformationDownloadEvent += new InformationDownloadHandler(finder_InformationDownloadEvent);
            
            icon.BalloonTipText = "STARTED";
            icon.ShowBalloonTip(5 * 60 * 1000);
            string ConsoleText = null;
            if (String.IsNullOrEmpty(textBox1.Text) == false)
            {
                ////            D:\\Week185_SampleFile_Z5.xlsx
                OledbHandler Excelhandler = new OledbHandler(textBox1.Text,null);
                ConsoleText = Excelhandler.ReadExcelFileSingle(textBox1.Text.Trim(), "No Addresses Found$", "POSTCODE");
            }
            SetRichboxText(ConsoleText);
            SetRichboxText("\n\n\n");

            SetLabelText("Start time=" + DateTime.Now);
        //    ConsoleText = finder.FindMultipleProperties();
            SetRichboxText(ConsoleText);
            SetRichboxText("\n\n\n");
            SetLabelText("Read PAF FILE");
           // this.Hide();
         
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OLEDBTEST test = new OLEDBTEST();
            test.ShowDialog();

        }

    }
}
