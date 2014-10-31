using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AForge.Video.FFMPEG;

namespace ZooplaVideoMaker
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBox1.Text=@"C:\Users\jafar.baltidynamolog\Videos\images.jpg";
            textBox2.Text=@"C:\Users\jafar.baltidynamolog\Videos\vlc-record-2014-09-09-10h16m49s-- We Are Only Separated By A Border - - The Logical Indian - Facebook[via torchbrowser.com].mp4-.mp4";
        }


        public void Start()
        {

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


        private void CreateMovie(string videoName,string ImageName)
        {
            int width = 320;
            int height = 240;
            var framRate = 200;
            try
            {

                // create instance of video writer
                using (var vFWriter = new VideoFileWriter())
                {
                    // create new video file
                    vFWriter.Open(videoName, width, height, framRate, VideoCodec.Raw);
                    Bitmap map = new Bitmap(ImageName);
                    //what's the current image data?
                    //                       var bmp = ToBitmap(imageByteArray);
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

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav|All Files|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox2.Text = dialog.FileName;
            
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;
            dialog.Filter = "Media Files|*.mpg;*.avi;*.wma;*.mov;*.wav;*.mp2;*.mp3|All Files|*.*";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = dialog.FileName;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            CreateMovie(textBox2.Text, textBox1.Text);

        }
    }
}