using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.Expression.Encoder;
using AForge.Video.FFMPEG;

namespace ZooplaExpression
{

    public partial class Form1 : Form
    {
        List<string> ImagesUrls = new List<string>();
        //     string MainVideoUrl = null;
        string outputDirectory = null;
        public Form1()
        {
            InitializeComponent();
            textBox1.Text = "Front House,Columbia Street ,Islamabad";
            textBox2.Text = "Beautiful Interior and Exterior";
            textBox3.Text = "2000000";
            textBox4.Text = "3000000";
            textBoxvideos.Text = @"C:\Users\jafar.baltidynamolog\Videos\videos\1.wmv";
            radioButton3.Checked = true;
            try
            {
                outputDirectory = System.Configuration.ConfigurationManager.AppSettings["outputDirectory"];
                if (System.IO.Directory.Exists(outputDirectory) == false)
                {
                    System.IO.Directory.CreateDirectory(outputDirectory);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void DrawImages(int redvalue, int greenvalue, int bluevalue)
        {

            SetLabelText("Adding text in Movie");

            Random rnd = new Random();
            redvalue = rnd.Next(0, 255);
            greenvalue = rnd.Next(0, 255);
            bluevalue = rnd.Next(0, 255);

            //bool redlock=false;
            //bool greenlock = false;
            //bool bluelock = false;


            int width = 1254;
            int height = 1000;

            for (int count = 0; count <= 100; count++)
            {
                //if (redvalue >= 255)
                //{
                //    redlock = false;
                //}
                //if (redvalue <= 0)
                //{
                //    redlock = true;
                //}
                //if (redlock == true)
                //{
                //    redvalue++;
                //}
                //else
                //{
                //    redvalue--;
                //}


                //if (greenvalue >= 255)
                //{
                //    greenlock = false;
                //}
                //if (greenvalue <= 0)
                //{
                //    greenlock = true;
                //}

                //if (greenlock == true)
                //{
                //    greenvalue++;
                //}
                //else
                //{
                //    greenvalue--;
                //}






                //if (bluevalue <= 0)
                //{
                //    bluelock = true;
                //}
                //if (bluevalue >= 255)
                //{
                //    bluelock = false;
                //}


                //if (bluelock == true)
                //{
                //    bluevalue++;
                //}
                //else
                //{
                //    bluevalue--;
                //}

                SolidBrush brush = null;
                SolidBrush Forebrush = null;
                SolidBrush Downbrush = null;
                try
                {

                    brush = new SolidBrush(Color.FromArgb(redvalue, greenvalue, bluevalue));
                    Forebrush = new SolidBrush(Color.FromArgb(bluevalue, redvalue, greenvalue));
                    Downbrush = new SolidBrush(Color.FromArgb(greenvalue, bluevalue, redvalue));
                }
                catch (Exception)
                {
                    MessageBox.Show("Can not Set your Color..Please Run again");
                    return;



                    /*In case of Exception we will continue if color changes ,with no effect untill it found color
                     */

                    //                    count--;
                    //                  continue;


                }
                try
                {
                    Bitmap flag = new Bitmap(width, height);

                    using (Graphics gfx = Graphics.FromImage(flag))
                    {
                        //                    gfx.FillRectangle(brush, 10+count, 10+count, width, height);
                        gfx.FillRectangle(brush, 0, 0, width, height);
                        float decimalcount = (float)(count * 10) / 100;
                        FontFamily family = new FontFamily("Times New Roman");



                        //gfx.FillRectangle(Forebrush,0,300,1200,400);
                        //gfx.DrawString(textBox1.Text, new Font(family, 40.0f , FontStyle.Bold | FontStyle.Underline), Forebrush, 50, 30 );
                        //gfx.DrawString("Description= " + textBox2.Text, new Font(family, 30.0f , FontStyle.Regular), Downbrush, 20 , 200 );
                        //gfx.DrawString("Average Sale Price= " + textBox3.Text, new Font(family, 30.0f , FontStyle.Regular), Downbrush, 20 , 300 );
                        //gfx.DrawString("Sales Price History=" + textBox4.Text, new Font(family, 30.0f , FontStyle.Regular), Downbrush, 20 , 400 );

                        gfx.FillRectangle(Forebrush, 0, 300, 1254, 500);
                        gfx.DrawString(textBox1.Text, new Font(family, 30.0f, FontStyle.Bold | FontStyle.Underline), Forebrush, 35 + decimalcount, 200);
                        gfx.DrawString("Description= " + textBox2.Text, new Font(family, 20.0f + decimalcount, FontStyle.Regular), Downbrush, 20 + decimalcount, 400);
                        gfx.DrawString("Average Sale Price= " + textBox3.Text, new Font(family, 20.0f + decimalcount, FontStyle.Regular), Downbrush, 20 + decimalcount, 500);
                        gfx.DrawString("Sales Price History=" + textBox4.Text, new Font(family, 20.0f + decimalcount, FontStyle.Regular), Downbrush, 20 + decimalcount, 600);

                        if (String.IsNullOrEmpty(textBoximages.Text) == false)
                        {
                            Image image = Bitmap.FromFile(textBoximages.Text);

                            gfx.DrawImage(image, width - (image.Width + 25), height - (image.Height + 25));
                        }

                        brush.Dispose();
                        Forebrush.Dispose();
                        Downbrush.Dispose();

                    }
                    flag.Save(outputDirectory + "//Tempe" + count + ".bmp");
                    flag.Dispose();
                }
                catch (Exception ex)
                {

                    MessageBox.Show(ex.Message);
                }
            }
            SetLabelText("Added text in Movie");
        }

        public Bitmap ReduceBitmap(Bitmap original, int reducedWidth, int reducedHeight)
        {
            var reduced = new Bitmap(reducedWidth, reducedHeight);
            using (var dc = Graphics.FromImage(reduced))
            {
                // you might want to change properties like
                dc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBilinear;
                dc.DrawImage(original, new Rectangle(0, 0, reducedWidth, reducedHeight), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }
            return reduced;
        }
        [STAThread]
        private void CreateMovieFromImages()
        {
         //   MessageBox.Show("OK");
            try
            {
                SetLabelText("Creating Movie");
                for (int i = 0; i < 50; i++)
                {
                    ImagesUrls.Add(outputDirectory+ "//Tempe" + i + ".bmp");
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }
            int width = 1254;
            int height = 1000;
            var framRate = 5;
            try
            {
                using (var vFWriter = new VideoFileWriter())
                {
                    vFWriter.Open(outputDirectory + "//TemperaryVideo.wmv", width, height, framRate, VideoCodec.WMV2);
                    foreach (var i in ImagesUrls)
                    {
                        Bitmap bmp = new Bitmap(i);
                        SetLabelText("Writing Image" + i + "\n");
                        var bmpReduced = ReduceBitmap(bmp, width, height);
                        vFWriter.WriteVideoFrame(bmpReduced);
                    }
                    vFWriter.Close();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter="Image files (*.jpg, *.jpeg, *.png) | *.jpg; *.jpeg; *.png";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBoximages.Text = dialog.FileName;
            }
            else
            {

            }
        }


        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Window Media Files (*.wmv)|*.wmv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //                MainVideoUrl = dialog.FileName;
                textBoxvideos.Text = dialog.FileName;
            }
            else
            {
                MessageBox.Show("Please select some video");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            bool Check = false;
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                errorProvider1.SetError(textBox1, "Can not be empty");
                Check = true;
            }
            if (String.IsNullOrEmpty(textBox2.Text))
            {
                errorProvider1.SetError(textBox2, "Can not be empty");
                Check = true;
            }
            if (String.IsNullOrEmpty(textBox3.Text))
            {
                errorProvider1.SetError(textBox3, "Can not be empty");
                Check = true;
            }
            if (String.IsNullOrEmpty(textBox4.Text))
            {
                errorProvider1.SetError(textBox4, "Can not be empty");
                Check = true;
            }

            if (String.IsNullOrEmpty(textBoxvideos.Text))
            {
                errorProvider1.SetError(textBoxvideos, "Can not be empty");
                Check = true;
            }

            if (Check == true)
            {
                return;
            }
            groupBox1.Enabled = false;
            button1.Enabled = false;
            backgroundWorker1.RunWorkerCompleted += new RunWorkerCompletedEventHandler(backgroundWorker1_RunWorkerCompleted);
            backgroundWorker1.RunWorkerAsync();
        }

        void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            groupBox1.Enabled = true;
            button1.Enabled = true;
        }
        void job_EncodeProgress(object sender, EncodeProgressEventArgs e)
        {
            SetLabelText("Combining Video, Progress: " + Math.Round(e.Progress,2));
        }



        #region Extra code
        private void button2_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < 100; i++)
            {
                System.IO.File.Copy("ABC.jpeg", "Temp" + i + ".bmp");
            }
        }


        private void buttonCreateImage_Click(object sender, EventArgs e)
        {
            DrawImages(255, 0, 0);
        }

        #endregion



        delegate void SetTextCallback(string text);
        private void SetLabelText(string text)
        {
            // InvokeRequired required compares the thread ID of the
            // calling thread to the thread ID of the creating thread.
            // If these threads are different, it returns true.
            if (this.labelNotify.InvokeRequired)
            {
                SetTextCallback d = new SetTextCallback(SetLabelText);
                this.labelNotify.Invoke(d, new object[] { text });
            }
            else
            {
                this.labelNotify.Text = text;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            DrawImages(0, 222, 145);
            CreateMovieFromImages();
                string adress = outputDirectory + "\\TemperaryVideo.wmv";
                if (System.IO.File.Exists(adress)==false)
                {
                    MessageBox.Show("Output Video is not there");
                    SetLabelText("Can not Find Video");
                    return;
                }

            MediaItem mediaItem1 = null;
            Job job = new Job();
            job.EncodeProgress += new EventHandler<EncodeProgressEventArgs>(job_EncodeProgress);

            if (radioButton1.Checked == true)
            {
                mediaItem1 = new MediaItem(textBoxvideos.Text);
                job.MediaItems.Add(mediaItem1);
                mediaItem1.Sources.Add(new Source(adress));
            }
            else if(radioButton3.Checked==true) {
                mediaItem1 = new MediaItem(adress);
                job.MediaItems.Add(mediaItem1);
                mediaItem1.Sources.Add(new Source(textBoxvideos.Text)); 
            }
            job.ApplyPreset(Presets.BestQuality);
            job.OutputDirectory = outputDirectory;
            job.Encode();
            SetLabelText("Completed");
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            MessageBox.Show("This functionality will lose audio so not implemented now");
        }
    }
}
