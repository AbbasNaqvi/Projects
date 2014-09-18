using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ItextSharp
{
    public partial class TestingForm : Form
    {
        public TestingForm()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog() ; 
            if (DialogResult.OK == dialog.ShowDialog())
            {
                textBox1.Text = dialog.FileName;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            PdfTron tron = new PdfTron();
            //tron.RunPdfTron(textBox1.Text);
            //richTextBox1.Text = tron.ConsoleLog;



            tron.ReadTextFromCoordinates(textBox1.Text, 1, int.Parse(textBoxURX.Text), int.Parse(textBoxURY.Text), int.Parse(textBoxLLX.Text), int.Parse(textBoxLLY.Text));
            richTextBox1.Text = tron.ConsoleLog;



 

        }

        private void button3_Click(object sender, EventArgs e)
        {
            PdfTron tron = new PdfTron();
            tron.example1_basic = false;
            tron.example2_xml = true;
            tron.example3_wordlist = false;
            tron.example4_advanced = false;
            tron.ReadAdvanced(textBox1.Text);
            richTextBox1.Text = tron.ConsoleLog;
        }



    }
}
