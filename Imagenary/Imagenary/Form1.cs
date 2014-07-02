using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Drawing.Imaging;


/*
 * gmap control is in this project disabled 
 * 
 */ 


namespace Imagenary
{
    public partial class Form1 : Form
    {
        int count = 0;
        public Form1()
        {
            InitializeComponent();
            webBrowser1.ScrollBarsEnabled = false;
            webBrowser1.AllowNavigation = true;

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            // initialize map:'''

            //  gmapcontrol1.mapprovider = gmap.net.mapproviders.opencyclemapprovider.instance ;
            //  gmapcontrol1.position = new pointlatlng(55.755786121111, 37.617633343333);
            //  gmapcontrol1.position = new pointlatlng(54.6961334816182, 25.2985095977783);
            //  gmapcontrol1.minzoom = 0;
            //  gmapcontrol1.maxzoom = 24;
            //  gmapcontrol1.zoom = 9;

              
            //  gmapcontrol1.mapprovider = gmap.net.mapproviders.googlemapprovider.instance;
            //  gmap.net.gmaps.instance.mode = gmap.net.accessmode.serverandcache;
            //  gmapcontrol1.setcurrentpositionbykeywords("london");
            //  gmapcontrol1.update();
            //  gmapcontrol1.position = new pointlatlng(-25.971684, 32.589759);
              
        }
        private void NavigateAgain(string url)
        {
            //&


            try
            {
                webBrowser1.Navigate(url);

            }
            catch (Exception a)
            {
                MessageBox.Show(a.GetBaseException().ToString());

            }
        
        }

        private void button1_Click(object sender, EventArgs e)
        {






            string url = "https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,182.03h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
            string url1 = "https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,182.03h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
            string url2 = "https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,182.03h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";
            string url3 = "https://www.google.com/maps/place/London/@51.478048,-0.00164,3a,75y,182.03h,86.95t/data=!3m5!1e1!3m3!1stHKiCQA1QB9j3CtWofKYFA!2e0!3e5!4m2!3m1!1s0x47d8a00baf21de75:0x52963a5addd52a99?hl=en-US";



            



            NavigateAgain(url);
            NavigateAgain();
            NavigateAgain();
            NavigateAgain();

            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            while (webBrowser1.ReadyState != WebBrowserReadyState.Complete)
            {
                System.Windows.Forms.Application.DoEvents();
            }
        }

        void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        { 
            try
            {

                Rectangle bounds = this.Bounds;
                using (Bitmap bitmap = new Bitmap(bounds.Width, bounds.Height))
                {
                    using (Graphics g = Graphics.FromImage(bitmap))
                    {
                        g.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
                    }
                    count++;
                    bitmap.Save("D://"+count+".png", ImageFormat.Png);
                    
                }
            }
            catch (Exception a)
            {
                MessageBox.Show(a.GetBaseException().ToString());
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
           
        }
    }
}
