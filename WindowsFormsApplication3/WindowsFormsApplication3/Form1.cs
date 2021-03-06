﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Web;
using System.Runtime.InteropServices;
using System.Threading;
using System.Drawing.Imaging;


namespace WindowsFormsApplication3
{
    #region Structures

    [StructLayout(LayoutKind.Explicit)]
    struct MouseKeybdhardwareInputUnion
    {
        [FieldOffset(0)]
        public MouseInputData mi;

        [FieldOffset(0)]
        public KEYBDINPUT ki;

        [FieldOffset(0)]
        public HARDWAREINPUT hi;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct KEYBDINPUT
    {
        public ushort wVk;
        public ushort wScan;
        public uint dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
    [StructLayout(LayoutKind.Sequential)]
    struct HARDWAREINPUT
    {
        public int uMsg;
        public short wParamL;
        public short wParamH;
    }
    struct MouseInputData
    {
        public int dx;
        public int dy;
        public uint mouseData;
        public MouseEventFlags dwFlags;
        public uint time;
        public IntPtr dwExtraInfo;
    }
    [Flags]
    enum MouseEventFlags : uint
    {
        MOUSEEVENTF_MOVE = 0x0001,
        MOUSEEVENTF_LEFTDOWN = 0x0002,
        MOUSEEVENTF_LEFTUP = 0x0004,
        MOUSEEVENTF_RIGHTDOWN = 0x0008,
        MOUSEEVENTF_RIGHTUP = 0x0010,
        MOUSEEVENTF_MIDDLEDOWN = 0x0020,
        MOUSEEVENTF_MIDDLEUP = 0x0040,
        MOUSEEVENTF_XDOWN = 0x0080,
        MOUSEEVENTF_XUP = 0x0100,
        MOUSEEVENTF_WHEEL = 0x0800,
        MOUSEEVENTF_VIRTUALDESK = 0x4000,
        MOUSEEVENTF_ABSOLUTE = 0x8000
    }
    enum SendInputEventType : int
    {
        InputMouse,
        InputKeyboard,
        InputHardware
    }
    [StructLayout(LayoutKind.Sequential)]
    struct INPUT
    {
        public SendInputEventType type;
        public MouseKeybdhardwareInputUnion mkhi;
    }
    #endregion
    public partial class Form1 : Form
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
        private System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();
        #region Variables
        public int pageCounts { get; set; }

        public bool LinkFound { get; set; }

        public string FormUrl { get; set; }

        public bool Invoked { get; set; }

        public bool IsLabelClicked { get; set; }
        #endregion
        System.Windows.Forms.Timer GUIclock;
        string ConsoleText = null;
        Myurl urls = new Myurl();
        bool Success = false;
        bool IsSVbuttonFound = false;
        string DirectoryName = "D://BED";
        string OldUrl = null;
        int currentUrl = 0;
        private System.Windows.Forms.Timer First_Tx = new System.Windows.Forms.Timer();
        int CountingNav = 0;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser2.ScriptErrorsSuppressed = true;
            //        var Element= webBrowser2.Document.GetElementFromPoint();
        }


        private void button1_Click(object sender, EventArgs e)
        {
            pageCounts = 0;

            LinkFound = false;

            FormUrl = null;

            Invoked = false;

            IsLabelClicked = false;


            OldUrl = textBox1.Text;
            webBrowser2.Navigate(OldUrl);

            First_Tx.Tick += new EventHandler(First_Tx_Tick);
            First_Tx.Interval = 1000;
            First_Tx.Start();
            while (First_tx_count < 10)
            {

                Application.DoEvents();
            
            }
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
                        Success = false;
                    }
                }
                else
                {

                    IsSVbuttonFound = true;
                }
            }

            while ((IsSVbuttonFound == false && pageCounts < 12) || (IsSVbuttonFound == true && pageCounts < 15))
            {

                Application.DoEvents();
            //    richTextBox1.Text += "Spining"+First_tx_count+","+pageCounts+"\n";
              
            }
            richTextBox1.Text += IsSVbuttonFound +"----"+ pageCounts;
            if (pageCounts > 11)
            {
                richTextBox1.Text += "Sucessfully Entered Street View\n";

                if (IsSVbuttonFound == true)
                {
                    richTextBox1.Text += "Change Angle By Link\n";

                    urls.Url1 = urls.Change_Angle(FormUrl, "90", ConsoleText);
                    urls.Url2 = urls.Change_Angle(FormUrl, "180", ConsoleText);
                    urls.Url3 = urls.Change_Angle(FormUrl, "270", ConsoleText);
                    urls.Url4 = urls.Change_Angle(FormUrl, "360", ConsoleText);

                }
                else
                {
                    richTextBox1.Text += "Change Angle\n";

                    urls.Url1 = "STREETVIEW";
                    urls.Url2 = "STREETVIEW";
                    urls.Url3 = "STREETVIEW";
                    urls.Url4 = "STREETVIEW";
                }
                if (WindowState != FormWindowState.Maximized)
                {
                    this.WindowState = FormWindowState.Maximized;
                }
                string url = null;

                while (currentUrl <= 3)
                {
                    currentUrl++;
                    richTextBox1.Text += "Finding Url" + currentUrl + "\n";
                    try
                    {
                        url = urls.GetType().GetProperty("Url" + currentUrl).GetValue(urls, null).ToString();
                    }
                    catch (Exception)
                    {
                        richTextBox1.Text += "Stuck Here---"+currentUrl+"\n";
                        continue;
                    }
                    if (url.Equals("STREETVIEW"))
                    {
                        try
                        {
                            Spin();
                            ClickLeft();
                            Spin();
                            ClickLeft();
                        }
                        catch (Exception )
                        {
                            richTextBox1.Text += "ERROR WHILE SPINING";
                        
                        }
                    }
                    else
                    {
                        try
                        {                           
                            webBrowser2.Navigate(url);
                            Spin();
                            Spin();
                        }
                        catch (Exception)
                        {

                            richTextBox1.Text += "Can not Navigate to this url";
                        }
                    }
             
               //     richTextBox1.Text += "Going on... " + url + "\n";
                    if (webBrowser2.ReadyState == WebBrowserReadyState.Complete)
                    {
                        int attempts = 0;
                        long size=10;
                        do
                        {
                            try
                            {
                                Spin();
                                richTextBox1.Text += "Capture :) " + url + "\n";
                                size = SaveImage(attempts);
                                richTextBox1.Text += "Size is " + size + "\n";
                                attempts++;
                            }catch(Exception)
                            {
                                richTextBox1.Text += "ERROR WHILE SAVING IMAGE\n";
                            }
                        } while (size < 600000 && attempts <= 8);

                        //     richTextBox1.Text += "Completed " + count + "," + previousUrl + "," + currentUrl + "\n";
                        //   previousUrl = currentUrl;
                        //  ++count;

                    }
                }
                //completed = true;
                //panel1.Enabled = true;
                //pictureBox1.Visible = true;
                //readyToClose = true;
                //this.Close();
                //PageReference.Activate();
                try
                {
                    GUIclock.Stop();                   
                }
                catch (Exception)
                {
                    richTextBox1.Text = "sTOPPING THE CLOCK\n";
                }
                try
                {
                    this.Close();
                }
                catch (Exception)
                { 
                richTextBox1.Text+="Exception while Close\n";
                }
            }
        }

        void Spin()
        {
            Second_tx_count = 0;
            System.Timers.Timer r3 = new System.Timers.Timer(700);
            r3.Elapsed += new System.Timers.ElapsedEventHandler(r_Elapsed);
            r3.Enabled = true;
            richTextBox1.Text += "Protective pause" + Second_tx_count + "," + currentUrl + "\n";
            while (Second_tx_count < 10)
            {
                Application.DoEvents();
            }
            r3.Enabled = false;
        }


        int Second_tx_count = 0;
        void r_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            Second_tx_count++;

        }
        int First_tx_count = 0;
        void First_Tx_Tick(object sender, EventArgs e)
        {
            if (First_tx_count >= 10)
            {
                First_Tx.Stop();

            }
            else {
                First_tx_count++;
            
            }
        }
        private long SaveImage(int attemps)
        {
            Bitmap newbitmap = null;
            long size = 0;
            try
            {
                Rectangle bounds = this.Bounds;
                Bitmap bitmap = new Bitmap(bounds.Width - 70, bounds.Height - 125);
                using (Graphics g = Graphics.FromImage(bitmap))
                {
                    g.CopyFromScreen(new Point(bounds.Left + 500, bounds.Top + 90), Point.Empty, bounds.Size);
                    newbitmap = new Bitmap(bitmap);
                }
                richTextBox1.Text += "Saving Image" + "... total attempts: " + attemps + "\n";

                string directory = null;
                try
                {
                    directory = DirectoryName;
                    Directory.CreateDirectory(directory);
                    bitmap.Save(directory + currentUrl + ".png", ImageFormat.Png);
                }
                catch (Exception)
                {
                    MessageBox.Show("cAN NOT SAVE IMAGE");
                }
                FileInfo info = new FileInfo(directory + currentUrl + ".png");
                size = info.Length;
                return size;
            }
            catch (IOException)
            {
                richTextBox1.Text += "IO Exception Unhandled";
            }
            return size;

        }
        private bool ClickLabel()
        {
            bool Result=false;
                HtmlElement e2 = webBrowser2.Document.GetElementById("one_A_1");
                if (e2 != null)
                {
                    e2.AttachEventHandler("onpropertychange", handler);
                    e2.InvokeMember("Click");
                    Timer_Lock = true;
                    IsLabelClicked = true;
                    richTextBox1.Text += "Label Is clicked\n\n";
                    Result = true;

                }
                else {
                    Result = false;
                }
            return Result;
        }

        private bool ClickSVButton()
        {
            bool Result = false;
            /*   Step 1
                 *  Find the StreetView Button in link and Click It
                 */
            if (Invoked == false && IsLabelClicked == false)
            {
                foreach (HtmlElement el in webBrowser2.Document.GetElementsByTagName("A"))
                {
                    if (el.GetAttribute("jsaction") == "app.loadVPageUrl")
                    {
                        el.InvokeMember("Click");
                        Invoked = true;
                        Result = true;
                        richTextBox1.Text += "Invoked at First Attempt\n\n";
                    }
                }

            }
            return Result;
        }
        bool Timer_Lock = false;
        bool Timer_Done = false;
        
        private void webBrowser2_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
      //      pageCounts++;


            #region stepFinal

            /*   Final Step
             *  Find the Link Requested after Clicking on Street View
             */

            if (Invoked == true && IshandlerInvoked == false && CountingNav == 0)
            {
                if (e.Url.OriginalString.Contains("layer"))
                {
                    FormUrl += e.Url.OriginalString.Replace("output=js", "").ToString();
                    LinkFound = true;
                    webBrowser2.Navigate(FormUrl);
                    richTextBox1.Text += "Navigating to link Successfully\n\n";
                    CountingNav = 1;
                    //                    return;
                }
            }
            #endregion


        
            if (IsLabelClicked == true && e.Url.ToString().Contains("javascript") == true && Timer_Done == false)
            {
                GUIclock = new System.Windows.Forms.Timer();
                GUIclock.Disposed += new EventHandler(GUIclock_Disposed);
                GUIclock.Enabled = true;
                GUIclock.Interval = (1000);
                GUIclock.Tick += GUIclock_Tick_1;
                richTextBox1.Text += "Timer Started  " + DateTime.Now.TimeOfDay + "\n";
                GUIclock.Start();
                Timer_Done = true;
            }



            //if (Invoked == true && IshandlerInvoked == true)
            //{
            //    ClickLeft();
            //}
        }
     
        void GUIclock_Disposed(object sender, EventArgs e)
        {
                richTextBox1.Text += "Timer Ended   " + DateTime.Now.TimeOfDay + "\n";
                Timer_Lock = false;
                ClickLeft();
                Timer_Done = true;
            
        }

        private void webBrowser2_Navigated(object sender, WebBrowserNavigatedEventArgs e)
        {
            richTextBox1.Text += e.Url.ToString()+"\n\n";
            pageCounts++;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            webBrowser2.Navigate(textBox1.Text);
        }

        private void webBrowser2_ProgressChanged(object sender, WebBrowserProgressChangedEventArgs e)
        {
          //  richTextBox1.Text +="Progress Changed "+ webBrowser2.Url.ToString() + "\n\n";            
        }

        private void webBrowser2_Navigating(object sender, WebBrowserNavigatingEventArgs e)
        {
            if (webBrowser2.Url != null)
            {
     //           richTextBox1.Text += "Navigating Url:  " + webBrowser2.Url.ToString() + "\n\n";
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = "";
        }

        private bool FindChildFrames(HtmlWindow n)
        {
            HtmlWindowCollection x = n.Document.Window.Frames;

            if (x.Count < 1)
            {
                richTextBox1.Text += "IN THE ROOT\n";
                return false;

            }
            else
            {
                richTextBox1.Text += "---->Diving\n";
                foreach (HtmlWindow w in x)
                {
                    if (FindChildFrames(w) == false)
                    {
                        PrintAllElements(w);
                    }

                }
                return true;
            }
        }
        public static void ClickLeftMouseButton(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = x;
            mouseInput.mkhi.mi.dy = y;
            mouseInput.mkhi.mi.mouseData = 0;



            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

        }
        private void ClickLeft()
        {
            richTextBox1.Text += "Clicking 350,350\n";
            MoveMouse(19900, 10500);
            ClickLeftMouseButton(19900, 10500);
                    }
        public void PrintAllElements(HtmlWindow n)
        {
            richTextBox1.Text += "Search Started--------\n";

            foreach (HtmlElement e in n.Document.All)
            {
                richTextBox1.Text += "---------Record Started--------\n";

                if (string.IsNullOrEmpty(e.OuterHtml) == false)
                {
                    richTextBox1.Text += e.OuterHtml + "\n\n";
                }
                if (string.IsNullOrEmpty(e.Id) == false)
                {
                    richTextBox1.Text += "ID= " + e.Id + "\n\n";
                } if (string.IsNullOrEmpty(e.Name) == false)
                {
                    richTextBox1.Text += "NAME= " + e.Name + "\n\n";
                } if (string.IsNullOrEmpty(e.TagName) == false)
                {
                    richTextBox1.Text += "TAGNAME= " + e.TagName + "\n";
                }
                richTextBox1.Text += "--------Record Ended--------\n";
            }
            richTextBox1.Text += "Search Ended--------\n\n\n\n";

        }

        private void MoveMouse(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dwExtraInfo = IntPtr.Zero;
            mouseInput.mkhi.mi.dx = x;
            mouseInput.mkhi.mi.dy = y;
            mouseInput.mkhi.mi.mouseData = 0;
            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_MOVE | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

        }


        bool IshandlerInvoked = false;
        private void handler(Object sender, EventArgs e)
        {
            if (IsLabelClicked == true)
            {
                HtmlElement e3 = webBrowser2.Document.GetElementById("svthumbnail");
                if (e3 != null)
                {
                    e3.InvokeMember("Click");
                    Invoked = true;
                    IshandlerInvoked = true;
                    richTextBox1.Text += "Invoked at Step handler Attempt\n\n";

                    richTextBox1.Text += "Click Happened\n\n";
                }
            }
            else
            {
                richTextBox1.Text += "Wrong event Invoke\n\n";
            }


        }
        int Count = 0;
        private void GUIclock_Tick_1(object sender, EventArgs e)
        {
            if (Count >= 100)
            {
                richTextBox1.Text += "End";
                GUIclock.Stop();
            }
            Count++;
            richTextBox1.Text += "+";
            this.Update();
        }

        public EventHandler timer_Tick { get; set; }

        public int Timer_Count { get; set; }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                GUIclock.Stop();
            }
            catch (Exception e1)
            {
                richTextBox1.Text = "pROBLEM sTOPING THE CLOCK   , "+e1.Message+"\n";
            
            
            }
        }
    }
}
