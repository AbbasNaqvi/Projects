using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp;
using iTextSharp.text.pdf;
using System.IO;
using AForge.Imaging;
using System.Drawing.Imaging;
using ImageMatchingLibrary;
namespace ExtractImages
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image files (*.pdf) | *.pdf;";  
                if (dialog.ShowDialog() == DialogResult.OK)
                    textBox1.Text = dialog.FileName;
            }

        }

        private void button2_Click(object sender, EventArgs e)
        {
            string text = null;
            try
            {
                bool Result = false;
                //Change the FileName Here
               System.Drawing.Image img=System.Drawing.Image.FromFile(textBox2.Text);
                Result =  ImageClass.IsImageExistInPDF(textBox1.Text, img,out text);
               text += "\n\nTemplate Path=" + textBox2.Text + "\n\n";
                richTextBox1.Text += text+"\n\n";
                if (Result)
                {
                    MessageBox.Show("Match Found");
                }
                else {
                    MessageBox.Show("Match Not Found");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
              ImageClass.WriteImageFile(textBox1.Text); // write image file
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonbone_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    textBoxone.Text = dialog.FileName;
            }
        }

        private void buttonbtwo_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    textBoxteo.Text = dialog.FileName;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            System.Drawing.Image img1 = System.Drawing.Image.FromFile(textBoxone.Text);
            System.Drawing.Image img2 = System.Drawing.Image.FromFile(textBoxteo.Text);

            try
            {
               label1.Text=  ImageClass.FindComparisonRatioBetweenImages(img1, img2).ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog dialog = new OpenFileDialog())
            {
                dialog.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                dialog.InitialDirectory = System.IO.Directory.GetCurrentDirectory();
                if (dialog.ShowDialog() == DialogResult.OK)
                    textBox2.Text = dialog.FileName;
            }

        }



    }
}
