using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Net;
using System.Collections;
using System.Threading;
using System.Threading.Tasks;

namespace CSharpLearning
{
    public partial class Form1 : Form
    {

        string Log = null;
        public Form1()
        {
            InitializeComponent();
        }
        public int N = 50;
        public void TestMethod()
        {
            // Using a named method.
            Parallel.For(0, N, Method2);

            // Using an anonymous method.
            Parallel.For(0, N, delegate(int i)
            {
                Log += "\nusing anonmus method\n";
                // Do Work.
            });

            // Using a lambda expression.
            Parallel.For(0, N, i =>
            {
                Log+="\nusing lambda method\n";
                // Do Work.
            });
        }

        public void Method2(int i)
        {
            Log += "\nusing method 2\n";

           // richTextBox1.Text += "...";

            // Do work.
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            TestMethod();
            richTextBox1.Text = Log;
        }
    }
}








