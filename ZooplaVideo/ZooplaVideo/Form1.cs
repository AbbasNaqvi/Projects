using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.FFMPEG;

namespace ZooplaVideo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            textBox1.Text = @"C:\Users\jafar.baltidynamolog\Videos\images.jpg";
            textBox2.Text = @"C:\Users\jafar.baltidynamolog\Videos\vlc-record-2014-09-09-10h16m49s-- We Are Only Separated By A Border - - The Logical Indian - Facebook[via torchbrowser.com].mp4-.mp4";

        }
        /*THIS CODE BLOCK IS COPIED*/

        public Bitmap ToBitmap(byte[] byteArrayIn)
        {
            var ms = new System.IO.MemoryStream(byteArrayIn);
            var returnImage = System.Drawing.Image.FromStream(ms);
            var bitmap = new System.Drawing.Bitmap(returnImage);

            return bitmap;
        }

        public Bitmap ReduceBitmap(Bitmap original, int reducedWidth, int reducedHeight)
        {
            var reduced = new Bitmap(reducedWidth, reducedHeight);
            using (var dc = Graphics.FromImage(reduced))
            {
                // you might want to change properties like
                dc.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                dc.DrawImage(original, new Rectangle(0, 0, reducedWidth, reducedHeight), new Rectangle(0, 0, original.Width, original.Height), GraphicsUnit.Pixel);
            }

            return reduced;
        }

        /*END OF COPIED CODE BLOCK*/

        private void CreateMovieFromImages(string VideoName)
        {
            int width = 400;
            int height = 224;
            var framRate = 1;
            try
            {
                // create instance of video writer
                using (var vFWriter = new VideoFileWriter())
                {
                    // create new video file
                    vFWriter.Open("nameOfMyVideoFile.wmv", width, height, framRate, VideoCodec.WMV2);

                    //loop throught all images in the collection

                    //foreach (var i in OldBitmaps)
                    //{
                    //    var bmpReduced = ReduceBitmap(i, width, height);

                    //    vFWriter.WriteVideoFrame(bmpReduced);
                    //}

                    foreach (var i in fileNames)
                    {
                        //what's the current image data?
                        //               var imageByteArray = image ;
                        //                    var bmp = ToBitmap(imageByteArray);
                        Bitmap bmp = new Bitmap(i);
                        richTextBox1.Text += "Writing Image" + i + "\n";
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
        private void CreateMovie(string videoName, string ImageName)
        {
            int width = 400;
            int height = 224;
            var framRate = 29;
            try
            {

                // create instance of video writer
                using (var vFWriter = new VideoFileWriter())
                {
                    // create new video file
                    vFWriter.Open(videoName, width, height, framRate, VideoCodec.MPEG4);
                    Bitmap map = new Bitmap(ImageName);
                    //what's the current image data?
                    //                       var bmp = ToBitmap(imageByte);
                    var bmpReduced = ReduceBitmap(map, width, height);

                    vFWriter.WriteVideoFrame(bmpReduced);
                    vFWriter.Close();
                }
            }
            catch (Exception e)
            {

                MessageBox.Show(e.Message);
            }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            //CreateMovie(textBox2.Text, textBox1.Text);
            List<string> images = new List<string>();

            CreateMovieFromImages(textBox2.Text);
            MessageBox.Show("Completed");
        }
        public List<string> fileNames = null;
        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
            //            dialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
               fileNames = dialog.FileNames.ToList();
               textBox1.Text += dialog.FileName + ";";
               textBox1.Text=textBox1.Text.Remove(textBox1.Text.Length-1,1);
            }
        }

        List<Bitmap> OldBitmaps = new List<Bitmap>();
        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            // dialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                System.IO.File.Copy(dialog.FileName, dialog.FileName + "temp");
                textBox2.Text = dialog.FileName;
            }
            VideoFileReader reader = new VideoFileReader();
            // open video file
            reader.Open(textBox2.Text);
            // check some of its attributes
            richTextBox1.Text += "width:  " + reader.Width + "\n";
            richTextBox1.Text += "height: " + reader.Height + "\n";
            richTextBox1.Text += "fps:    " + reader.FrameRate + "\n";
            richTextBox1.Text += "codec:  " + reader.CodecName + "\n";
            //for (int i = 0; i < reader.FrameCount; i++)
            //{
            //    Bitmap videoFrame = reader.ReadVideoFrame();
            //    // process the frame somehow
            //    // ...
            //    OldBitmaps.Add(videoFrame);
            //    // dispose the frame when it is no longer required
            //  //  videoFrame.Dispose();
            //}

            reader.Close();

        }


    }
}
