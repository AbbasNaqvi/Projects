using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mshtml;
using System.Runtime.InteropServices;

namespace Imagenary
{
    public partial class SetSettings : Form
    {
        ApplicationData appdata = ApplicationData.Create;
        int XCoordinate = 0;
        int YCoordinate = 0;
        int HideX = 0;
        int HideY = 0;

        string MainFileName = null;
        string PAFFileName = null;
        string SPHFileName = null;
        string ImageOutPut = null;
        public SetSettings()
        {
            InitializeComponent();
            MainFileName = appdata.MainFileAdress;
            SPHFileName = appdata.SPHFileAdress;
            PAFFileName = appdata.PafFileAdress;
            XCoordinate = appdata.XCoordinate;
            YCoordinate = appdata.YCoordinate;
            HideX = appdata.XHide;
            HideY = appdata.YHide;
            ImageOutPut = appdata.ImageOutputDirectory;
            textBox1.Text = XCoordinate+","+YCoordinate;
            textBox2.Text = MainFileName;
            textBox3.Text = PAFFileName;
            textBox4.Text = SPHFileName;
            textBox5.Text = HideX + " , " + HideY;
            textBoxImageOutput.Text = ImageOutPut;
        }
        public event InformationDownloadHandler InformationDownloadEvent;
        public virtual void OnInformationDownload(EventArguments e)
        {
            if (InformationDownloadEvent != null)
            {
                InformationDownloadEvent(this, e);

            }
        }

        #region ClickStructs
        enum SystemMetric
        {
            SM_CXSCREEN = 0,
            SM_CYSCREEN = 1,
        }

        [DllImport("user32.dll")]
        static extern int GetSystemMetrics(SystemMetric smIndex);

        int CalculateAbsoluteCoordinateX(int x)
        {
            return (x * 65536) / GetSystemMetrics(SystemMetric.SM_CXSCREEN);
        }

        int CalculateAbsoluteCoordinateY(int y)
        {
            return (y * 65536) / GetSystemMetrics(SystemMetric.SM_CYSCREEN);
        }
        #endregion

   
        #region Navigating SV Functions and Variables

        int pageCounts = 0;
        System.Timers.Timer r3;
        int Second_tx_count = 0;

        WebBrowser browser = null;
        Form f1;
        private void InitializeWebBrowser()
        {
            browser = new WebBrowser();
            browser.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                           | System.Windows.Forms.AnchorStyles.Left)
                           | System.Windows.Forms.AnchorStyles.Right)));
            browser.Location = new System.Drawing.Point(0, 0);
            browser.MinimumSize = new System.Drawing.Size(20, 20);
            //  browser.Name = "browser";
            browser.Size = new System.Drawing.Size(903, 403);
            browser.TabIndex = 1;

        }



        private bool ClickSVButton()
        {
            bool Result = false;
            /*   Step 1
                 *  Find the StreetView Button in link and Click It
                 */
            if (Invoked == false && IsLabelClicked == false)
            {
                foreach (HtmlElement el in browser.Document.GetElementsByTagName("A"))
                {
                    if (el.GetAttribute("jsaction") == "app.loadVPageUrl")
                    {
                        el.InvokeMember("Click");
                        Invoked = true;
                        Result = true;
                    }
                }

            }
            return Result;
        }
        private bool ClickLabel()
        {
            bool Result = false;
            HtmlElement e2 = browser.Document.GetElementById("one_A_1");
            if (e2 != null)
            {
                e2.AttachEventHandler("onpropertychange", handler);
                e2.InvokeMember("Click");
                IsLabelClicked = true;
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;
        }


        private void handler(Object sender, EventArgs e)
        {
            if (IsLabelClicked == true)
            {
                HtmlElement e3 = browser.Document.GetElementById("svthumbnail");
                if (e3 != null)
                {
                    e3.InvokeMember("Click");
                }
            }
        }
        private int Spin(int time)
        {
            int Result = 0;
            r3 = new System.Timers.Timer();
            r3.Interval = time;
            Second_tx_count = 0;
            r3.Elapsed += new System.Timers.ElapsedEventHandler(r3_Elapsed);
            r3.Enabled = true;
            while (Second_tx_count < 10)
            {
                Application.DoEvents();
            }
            r3.Enabled = false;
            return Result;
        }

        void r3_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Second_tx_count++;
        }
        void f1_Load(object sender, EventArgs e)
        {

            Spin(300);

            if (pageCounts >= 2)
            {
                if (ClickSVButton() == false)
                {
                    if (ClickLabel() == true)
                    {
                        IsLabelClicked = true;
                    }
                    else
                    {
                        throw new Exception("1...");
                    }
                }
                else
                {
                    throw new Exception("2...");
                }
            }
        }
        void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            pageCounts++;
            browser.Document.Body.MouseDown += new HtmlElementEventHandler(Body_MouseDown);
        }
#endregion 
        int FormOpenedType = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            
            FormOpenedType = 1;
            f1 = new Form();
            InitializeWebBrowser();
            browser.SendToBack();
            f1.Size = new Size(903, 403);
            MessageBox.Show("Once the Form is loaded, Take the cursor to the Navigation Cicle on the Top left of the image and Double Click Left From the Mouse,Remember ,You have to Double Click on either Left or Right Arrow");
            f1.Controls.Add(browser);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            f1.WindowState = FormWindowState.Maximized;
            browser.Navigate("https://www.google.com/maps/place/Apartment+101+Landmark+House+11+Broadway++BRADFORD+bd1+1jb/");
            f1.Load += new EventHandler(f1_Load);

            f1.ShowDialog();
            FormOpenedType = 0;            
        }

        void ExitButton_Click(object sender, EventArgs e)
        {
    
            f1.Close();
        }
       


        void Body_MouseDown(object sender, HtmlElementEventArgs e)
        {
            
            Point MS = Cursor.Position;
            Point p1 = new Point();
            p1.X = CalculateAbsoluteCoordinateX(MS.X);
            p1.Y = CalculateAbsoluteCoordinateY(MS.Y);
            if (FormOpenedType == 1)
            {
                textBox1.Text = p1.X + "," + p1.Y;
                XCoordinate = p1.X;
                YCoordinate = p1.Y;
            }
            else if (FormOpenedType == 2)
            {
                textBox5.Text = p1.X + "," + p1.Y;
                HideX = p1.X;
                HideY = p1.Y;
            }
        }
       
        private void buttonSave_Click_1(object sender, EventArgs e)
        {
           
            if (XCoordinate==0||YCoordinate==0||HideX==0||HideY==0||String.IsNullOrEmpty(MainFileName)||String.IsNullOrEmpty(PAFFileName)||String.IsNullOrEmpty(SPHFileName))
            {
                labelError.Text = "Some Field is Missing, Complete all fields in order to proceed";
            }else{
                appdata.XCoordinate = XCoordinate;
                appdata.YCoordinate = YCoordinate;
                appdata.MainFileAdress = MainFileName;
                appdata.PafFileAdress = PAFFileName;
                appdata.SPHFileAdress = SPHFileName;
                appdata.ImageOutputDirectory = ImageOutPut;
                appdata.XHide = HideX;
                appdata.YHide = HideY;
                OnInformationDownload(new EventArguments() { Name = "SET", Time = DateTime.Now, Details =MainFileName });
                this.Close();
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            MainFileName= dialog.FileName;
            textBox2.Text = MainFileName;
        }

        private void button3_Click(object sender, EventArgs e)
        {

            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            PAFFileName = dialog.SelectedPath;
            textBox3.Text = PAFFileName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.ShowDialog();
            SPHFileName = dialog.FileName;
            textBox4.Text = SPHFileName;
        }

        public bool Invoked { get; set; }

        public bool IsLabelClicked { get; set; }

        private void buttonSetImageDownload_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog dialog = new FolderBrowserDialog();
            dialog.ShowDialog();
            ImageOutPut = dialog.SelectedPath;
            textBoxImageOutput.Text = ImageOutPut;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FormOpenedType = 2;
            f1 = new Form();
            InitializeWebBrowser();
            browser.SendToBack();
            f1.Size = new Size(903, 403);
            MessageBox.Show("Once the Form is loaded, Take the cursor to the Navigation Cicle on the Top left of the image and Double Click Left From the Mouse,Remember ,You have to Double Click on either Left or Right Arrow");
            f1.Controls.Add(browser);
            browser.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(browser_DocumentCompleted);
            f1.WindowState = FormWindowState.Maximized;
            browser.Navigate("https://www.google.com/maps/place/Apartment+101+Landmark+House+11+Broadway++BRADFORD+bd1+1jb/");
            f1.Load += new EventHandler(f1_Load);
            f1.ShowDialog();
            FormOpenedType = 0;

        }

    }
}
