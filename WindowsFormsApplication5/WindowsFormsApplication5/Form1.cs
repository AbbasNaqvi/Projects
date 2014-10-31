using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Threading;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace WindowsFormsApplication5
{
    public partial class Form1 : Form
    {
        string text = "Lasagne Fried Rice~Automated Facebook Post~Do not like comment or share";
        string url = "http://upload.wikimedia.org/wikipedia/commons/0/04/Garfield_Building_Detroit.jpg";
        static System.Windows.Forms.Timer myTimer = new System.Windows.Forms.Timer();
        int timex = 0;
        string password = null;
        string emailid = null;
        string pageName = null;
        string PageLink = null;
        const int MK_LBUTTON = 0x0001;
        const int WM_LBUTTONDOWN = 0x0201;
        const int WM_LBUTTONUP = 0x0202;
        #region interop
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;
            public int y;
        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        static extern int PostMessage(IntPtr hwnd, int msg, IntPtr wparam, IntPtr lparam);

        [DllImport("User32.dll", ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern IntPtr GetFocus();

        [DllImport("User32.dll", SetLastError = true, ExactSpelling = true, CharSet = CharSet.Auto)]
        static extern bool IsChild(IntPtr hWndParent, IntPtr hWnd);

        #endregion
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
        [DllImport("user32.dll", SetLastError = true)]
        static extern uint SendInput(uint nInputs, ref INPUT pInputs, int cbSize);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetCursorPos(out MouseInputData lpPoint);

        public Form1()
        {
            InitializeComponent();
            myTimer.Interval = 5000;
            myTimer.Tick += new EventHandler(myTimer_Tick);
            webBrowser1.ScriptErrorsSuppressed = true;
          
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
        public static void ClickLeftMouseButton(int x, int y)
        {
            INPUT mouseInput = new INPUT();
            mouseInput.type = SendInputEventType.InputMouse;
            mouseInput.mkhi.mi.dx = x;
            mouseInput.mkhi.mi.dy = y;
            mouseInput.mkhi.mi.mouseData = 0;



            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTDOWN | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

            mouseInput.mkhi.mi.dwFlags = MouseEventFlags.MOUSEEVENTF_LEFTUP | MouseEventFlags.MOUSEEVENTF_ABSOLUTE;
            SendInput(1, ref mouseInput, Marshal.SizeOf(new INPUT()));

        }
        private void GetTextFromFile()
        {
            var temp=System.IO.File.ReadLines(System.IO.Directory.GetCurrentDirectory()+"\\Articles.txt");
            foreach (var i in temp)           
            {
                string[] tokens = i.Split(',');
                url=tokens[1];
                text = tokens[0];
            }
        
        
        }
        void myTimer_Tick(object sender, EventArgs e)
        {
            timex += 1;
        }



       // bool clickedLogin = false;
        bool clickedPage = false;
        bool Completed = false;
        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            bool IsRequiredPage = false;
            var PageElements = webBrowser1.Document.GetElementsByTagName("h2");
            foreach (HtmlElement elementy in PageElements)
            {
                if (elementy.InnerText.Contains(pageName))
                {

                    IsRequiredPage = true;
                }
            }

            var EmailElement = webBrowser1.Document.GetElementById("email");

            if (EmailElement != null)
            {
                webBrowser1.Document.GetElementById("email").InnerText = emailid;
                webBrowser1.Document.GetElementById("pass").InnerText = password;
                var element = webBrowser1.Document.GetElementById("loginbutton");
                element.Focus();
                element.InvokeMember("click");
                var hwnd = GetFocus();
                //                Thread.Sleep(1000);
              //  clickedLogin = true;
                if (!IsChild(this.webBrowser1.Handle, hwnd))
                    throw new ApplicationException("Unexpected focused window.");

                var rect = GetElementRect(element);
                IntPtr wParam = (IntPtr)MK_LBUTTON;
                IntPtr lParam = (IntPtr)(rect.Left | rect.Top << 16);
                PostMessage(hwnd, WM_LBUTTONDOWN, wParam, lParam);
                PostMessage(hwnd, WM_LBUTTONUP, wParam, lParam);
                //clickedLogin = true;

            }
            else if (clickedPage == false && EmailElement == null)
            {
                if (webBrowser1.Document.GetElementById("u_0_5").InnerText.Equals("Remember Browser"))
                {
                    var element = webBrowser1.Document.GetElementById("checkpointSubmitButton");
                    element.Focus();
                    element.InvokeMember("click");
                }
                else
                {
                    webBrowser1.Stop();
                    webBrowser1.Navigate(PageLink);
                    Print("\nNavigated to "+PageLink+"\n");
                    // Thread.Sleep(1000);
                    clickedPage = true;
                    return;
                }
            }
            if (clickedPage == true && IsRequiredPage == true && Completed == false)
            {
                HtmlElement elementTextArea = null;
                HtmlElementCollection elemss = webBrowser1.Document.GetElementsByTagName("textarea");
                foreach (HtmlElement elem in elemss)
                {
                    if (elem.GetAttribute("name") == "xhpc_message")
                    {
                        elementTextArea = elem;
                        break;
                    }
                }
                elementTextArea.Focus();
                Print("\nSet Focused\n");

                string id = elementTextArea.Id;

                SendText(url);
              
                webBrowser1.Update();
                DoWait(4);

                SendText("^(a)");
               SendText(text);
                webBrowser1.Update();

                elementTextArea = webBrowser1.Document.GetElementById(id);

                elementTextArea.InnerText += "\nINNER TEXT";
                Print("\nAdded Inner Text ABCD\n");





                //elementTextArea.SetAttribute("placeholder", text + "PLACEHOLDER");

                //Print("\nAdded place holder text abcd \n");



            //    Find Parrot

                HtmlElementCollection elems = webBrowser1.Document.GetElementsByTagName("input");
                foreach (HtmlElement elem in elems)
                {
                    if (elem.GetAttribute("className").Equals("mentionsHidden"))
                    {
                        Print("\nAdded Second Text\n");
                        elem.SetAttribute("value", text+" ...Parrot");
                        break;
                    }
                }
                DoWait(3);

                HtmlElementCollection elements = webBrowser1.Document.GetElementsByTagName("button");
                foreach (HtmlElement element in elements)
                {
                    if (element.GetAttribute("className") == "_42ft _4jy0 _11b _4jy3 _4jy1 selected _51sy")
                    {
                        Print("\nPost Button Found\n");
                        element.Focus();
                        object[] abj=new object[1];
                        abj[0] = (object)url;
                        webBrowser1.Document.InvokeScript("onsubmit",( abj));
                        string ide=element.Id;
                        webBrowser1.Document.GetElementById(ide).Focus();
                        Print("\nFocus1\n");

                        DoWait(2);

                        SendText("~");

                        //         element.InvokeMember("click");

                        Print("\nClicked1\n");

                        DoWait(2);

                        element.Focus();

                        Print("\nFocus2\n");

                        SendText("{ENTER}");

                        DoWait(2);

                        element.Focus();

                        SendText("~");

                        SendText("~");

                        element.Focus();
                        Print("\nFOCUS\n");

                        //                element.InvokeMember("click");
                        Print("\nClicked\n");

                        SendText("{ENTER}");
                        richTextBox1.Text += elementTextArea.Id;

                        elementTextArea.SetAttribute("title", "What's on your mind?");

                        element.Focus();
                        Print("\nFOCUS\n");

                        //          element.InvokeMember("click");
                        Print("\nCLICKED\n");
                        
                        
                        var hwnd = GetFocus();
                        if (!IsChild(this.webBrowser1.Handle, hwnd))
                            throw new ApplicationException("Unexpected focused window.");
                        var rect = GetElementRect(element);

                        MoveMouse(rect.X, rect.Y);
                        ClickLeftMouseButton(rect.X, rect.Y);

                        IntPtr wParam = (IntPtr)MK_LBUTTON;
                        IntPtr lParam = (IntPtr)(rect.Left | rect.Top << 16);
                        PostMessage(hwnd, WM_LBUTTONDOWN, wParam, lParam);
                        PostMessage(hwnd, WM_LBUTTONUP, wParam, lParam);
                        richTextBox1.Text += "clicked";
                        PostMessage(hwnd, WM_LBUTTONDOWN, wParam, lParam);
                        PostMessage(hwnd, WM_LBUTTONUP, wParam, lParam);



                        //clickedLogin = true;


                        myTimer.Dispose();

                        MessageBox.Show("Completed");
                        Completed = true;
                        return;
                    }

                    //SendText(text);

                    //var hwnd = GetFocus();
                    //Thread.Sleep(1000);
                    //clickedLogin = true;
                    //if (!IsChild(this.webBrowser1.Handle, hwnd))
                    //    throw new ApplicationException("Unexpected focused window.");

                    //var rect = GetElementRect(elementtemp);
                    //IntPtr wParam = (IntPtr)MK_LBUTTON;
                    //IntPtr lParam = (IntPtr)(rect.Left | rect.Top << 16);
                    //PostMessage(hwnd, WM_LBUTTONDOWN, wParam, lParam);
                    //PostMessage(hwnd, WM_LBUTTONUP, wParam, lParam);

                }
            }
            
        }// get the element rect in window client area coordinates
        static Rectangle GetElementRect(HtmlElement element)
        {
            var rect = element.OffsetRectangle;
            int left = 0, top = 0;
            var parent = element;
            while (true)
            {
                parent = parent.OffsetParent;
                if (parent == null)
                    return new Rectangle(rect.X + left, rect.Y + top, rect.Width, rect.Height);
                var parentRect = parent.OffsetRectangle;
                left += parentRect.Left;
                top += parentRect.Top;
            }
        }
        private void Print(string text)
        {
            richTextBox1.Text += "\n"+text+"\n";
            richTextBox1.Update();
        }
        private void SendText(string text)
        {
            string txt = Regex.Replace(text, "[+^%~()]", "{$0}");
            SendKeys.SendWait(text);
        Print("Printed: "+txt+"... By kEYS");
        }
        private void AfteDocumentLoads()
        {
            HtmlElementCollection textBox = webBrowser1.Document.GetElementsByTagName("textarea").GetElementsByName("xhpc_message");
            HtmlElementCollection button = webBrowser1.Document.GetElementsByTagName("button");

            foreach (HtmlElement element in textBox)
            {
                foreach (HtmlElement btnelement in button)
                {
                    if (btnelement.InnerText == "Post")
                    {
                        element.Focus();
                        element.InnerText = "FFF";
                        btnelement.InvokeMember("Click");
                    }
                }
            }
        }
        private void DoWait(int x)
        {
            richTextBox1.Text += "\nDoing Wait\n";
            timex = 0;
            myTimer.Start();
            while (timex < x)
            {
                Application.DoEvents();
            }
            myTimer.Stop();
        }
        private void button1_Click(object sender, EventArgs e)
        {

            pageName = textBox3.Text;
            PageLink = textBox4.Text;
            emailid = textBox1.Text;
            password = textBox2.Text;
            GetTextFromFile();
            webBrowser1.Visible = true;
            webBrowser1.Navigate("fb.com");
        }
    }
}
