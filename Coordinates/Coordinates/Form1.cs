using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Coordinates
{
    public partial class Form1 : Form
    {

        #region Structures
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


        [DllImport("User32.dll")]
        static extern IntPtr GetDC(IntPtr hwnd);

        [DllImport("gdi32.dll")]
        static extern int GetDeviceCaps(IntPtr hdc, int nIndex);

        [DllImport("user32.dll")]
        static extern bool ReleaseDC(IntPtr hWnd, IntPtr hDC);

        private Point ConvertPixelsToUnits(int x, int y)
        {
            // get the system DPI
            IntPtr dDC = GetDC(IntPtr.Zero); // Get desktop DC
            int dpi = GetDeviceCaps(dDC, 88);
            bool rv = ReleaseDC(IntPtr.Zero, dDC);

            // WPF's physical unit size is calculated by taking the 
            // "Device-Independant Unit Size" (always 1/96)
            // and scaling it by the system DPI
            double physicalUnitSize = (1d / 96d) * (double)dpi;
            Point wpfUnits = new Point(Convert.ToInt32(physicalUnitSize * (double)x),Convert.ToInt32(physicalUnitSize * (double)y));
            return wpfUnits;
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
        public Form1()
        {
            InitializeComponent();
        }
        Timer tx = new Timer();

        private void Form1_Load(object sender, EventArgs e)
        {
            tx.Interval = 10;
            tx.Tick += new EventHandler(tx_Tick);
            tx.Start();
            webBrowser1.Navigate("https://www.google.com/maps/place/Apartment+101+Landmark+House+11+Broadway++BRADFORD+bd1+1jb/");

            this.WindowState = FormWindowState.Maximized;
       //     MoveMouse(19526,17066);
            webBrowser1.ScriptErrorsSuppressed = true;
         //   ClickLeftMouseButton(19526, 17066);
          

        }
        void tx_Tick(object sender, EventArgs e)
        {
            MouseInputData p;
            if (GetCursorPos(out p))
            {
                Point pc = PointToScreen(new Point() { X = p.dx, Y = p.dy });
                Point pc2 = PointToClient(new Point() { X = p.dx, Y = p.dy });
                Point p1 = new Point();
                Point p2 = new Point();
                Point p3 = new Point();
                p1.X = CalculateAbsoluteCoordinateX(pc.X);
                p1.Y = CalculateAbsoluteCoordinateY(pc.Y);
                p2.X = CalculateAbsoluteCoordinateX(pc.X);
                p2.Y = CalculateAbsoluteCoordinateY(pc.Y);
                p3.X = CalculateAbsoluteCoordinateX(Cursor.Position.X);
                p3.Y = CalculateAbsoluteCoordinateY(Cursor.Position.Y);
                Point px = ConvertPixelsToUnits(p.dx, p.dy);

                textBox1.Text = p3.X + "," + p3.Y;
                //    textBox2.Text = p2.X + "," + p2.Y;
                //   ClickLeftMouseButton(p1.X, p1.Y);
                //             MoveMouse(p1.X, p1.Y);
            }
        }        
    

        //private void Form1_MouseMove(object sender, MouseEventArgs e)
        //{

        //}

    }
}