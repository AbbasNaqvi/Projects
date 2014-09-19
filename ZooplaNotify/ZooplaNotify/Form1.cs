using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Text.RegularExpressions;

namespace ZooplaNotify
{
    public partial class Form1 : Form
    {
        static ApplicationData applicationData = new ApplicationData();
        static public ApplicationData Create
        {
            get { return applicationData; }
        }

        public Form1()
        {
            InitializeComponent();
            SerializationHandler handler = new SerializationHandler();
            applicationData = handler.DeSerializeObject();
            textBoxURI.Text = "http://www.dezrez.com/DRApp/DotNetSites/WebEngine/property/Default.aspx?eaid=1047&apikey=1CA27664-D94A-4D87-BCB1-F9A5DEE44E8A&xslt=-1&page=1&perpage=100";
            ShowSettingsOnForm();
        }


        private string FirstPart = null;
        private string SecondPart = null;
        private string ThirdPart = null;



        private void ShowSettingsOnForm()
        {

            textBoxPassword.Text = applicationData.Password;
            textBoxHost.Text = applicationData.Host;
            textBoxEmail.Text = applicationData.EmailId;
            numericUpDownRunAfter.Value = applicationData.RunAfter;
            textBoxDBTableName.Text = applicationData.AccessTableName;
            textBoxDatabaseFileName.Text = applicationData.AccessDBFileName;
            textBoxURI.Text = applicationData.InitialUrl;
            textBoxPort.Text = applicationData.Port.ToString();
            numericUpDownRunAfter.Value = applicationData.RunAfter;
            foreach (string x in applicationData.To)
            {
                if (comboBoxTo.Items.Contains(x) == false)
                {
                    comboBoxTo.Items.Add(x);
                }
            }

        }
        private void SplitURl()
        {
            try
            {
                Match m = Regex.Match(textBoxURI.Text, "(?<first>http://www.dez.*?page=)(?<second>.*?)(?<third>&.*?)$");
                FirstPart = m.Groups["first"].Value;
                SecondPart = m.Groups["second"].Value;
                ThirdPart = m.Groups["third"].Value;
            }
            catch (ArgumentException ex)
            {
                // Syntax error in the regular expression
            }
        }
        private void buttonRun_Click(object sender, EventArgs e)
        {
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            label8.Text = "Completed";
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Filter = "Access 2000-2003 (*.mdb)|*.mdb|Access 2007 (*.accdb)|*accdb";
            dialog.Multiselect = false;
            if (DialogResult.OK == dialog.ShowDialog())
            {
                textBoxDatabaseFileName.Text = dialog.FileName;
            }
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            if (linkLabel2.Text.Equals("Show"))
            {
                textBoxPassword.UseSystemPasswordChar = false;
                linkLabel2.Text = "Hide";
            }
            else
            {
                textBoxPassword.UseSystemPasswordChar = true;
                linkLabel2.Text = "Show";
            }


        }

        private void button4_Click(object sender, EventArgs e)
        {

            applicationData.InitialUrl = textBoxURI.Text;
            applicationData.AccessDBFileName = textBoxDatabaseFileName.Text;
            applicationData.AccessTableName = textBoxDBTableName.Text;
            applicationData.EmailId = textBoxEmail.Text;
            applicationData.Password = textBoxPassword.Text;
            applicationData.RunAfter = (int)numericUpDownRunAfter.Value;
            applicationData.Host = textBoxHost.Text;
            int tempport;
            if (int.TryParse(textBoxPort.Text, out tempport))
            {
                applicationData.Port = tempport;
            }
            else
            {
                MessageBox.Show("Format of port is not correct");
            }

            for (int i = 0; i < comboBoxTo.Items.Count; i++)
            {
                if (applicationData.To.Contains(comboBoxTo.Items[i].ToString())==false)
                {
                    applicationData.To.Add(comboBoxTo.Items[i].ToString());
                }
            }


            label8.Text = "Settings are Saved";

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializationHandler handler = new SerializationHandler();
            handler.SerializeObject(applicationData);
        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            comboBoxTo.Items.Add(textBoxAddNew.Text);
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            int index = comboBoxTo.SelectedIndex;
            comboBoxTo.Items.RemoveAt(index);
        }

        private void button5_Click(object sender, EventArgs e)
        {

            Property p = new Property();
            p.PropertyID = "2922551";
            p.Title = "asdas";
            p.SetPrice(400,true);

        }
        delegate void SetTextCallback(string text);
        private void SetLabelText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.label8.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelText);
                this.label8.Invoke(d, new object[] { text });
            }
            else
            {
                this.label8.Text = text;
            }
        }
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            SplitURl();
            int PropertyCount = 0;
            XmlDocument doc = new XmlDocument();
            doc.Load(textBoxURI.Text);
            XmlNode PagesNode = doc.SelectSingleNode("/response/propertySearchSales/properties/pages");
            string tempString = PagesNode.Attributes.GetNamedItem("pageCount").Value.ToString();
            int PagesCount = 0;
            int.TryParse(tempString, out PagesCount);
            for (int i = 1; i < PagesCount; i++)
            {
                XmlDocument document = new XmlDocument();
                string uri = FirstPart + i + ThirdPart;
                document.Load(uri);
                XmlNode PropertiesNode = document.SelectSingleNode("/response/propertySearchSales/properties");
                XmlNodeList PropertiesXmlCollection = PropertiesNode.SelectNodes("property");
                foreach (XmlNode node in PropertiesXmlCollection)
                {
                    Property pobj = new Property();
                    pobj.PropertyID = node.Attributes.GetNamedItem("id").Value.ToString();
                    double tempPrice;

                    XmlNodeList childNodes = node.ChildNodes;
                    string title = null;
                    foreach (XmlNode x in childNodes)
                    {
                        title += x.InnerText + ",";
                    }
                    pobj.Title = title.Replace(",,", ",");
                    Double.TryParse(node.Attributes.GetNamedItem("priceVal").Value.ToString(), out tempPrice);
                    pobj.SetPrice(tempPrice, true);
                    SetLabelText( "Property Number: "+PropertyCount++.ToString());
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                this.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("Can not close");
            }
        }
    }
}
