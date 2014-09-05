using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication4
{
    public partial class Form1 : Form
    {
      static System.Windows.Forms.Timer time = new Timer();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            time.Enabled = true;
            time.Tick += new EventHandler(time_Tick);
            time.Interval = (3000);
            time.Start();
        }

        void time_Tick(object sender, EventArgs e)
        {
                      richTextBox1.Text += DateTime.Now.TimeOfDay+"\n";
                      Application.DoEvents();
        }

    }
}
