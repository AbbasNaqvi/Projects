using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;

namespace ResizeImage
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            textBoxAX.Text = "450";
            textBoxAY.Text = "800";
            textBoxBX.Text = "450";
            textBoxBY.Text = "600";

        }
        private void DeleteAllPics()
        {
            string path = Path.GetDirectoryName(Application.ExecutablePath);
            string[] filesnames = System.IO.Directory.GetFiles(path);
            foreach (var i in filesnames)
            {
                try
                {
                    if (i.Contains("ResizeImage") == false || i.Contains("manifest") == false)
                        System.IO.File.Delete(i);
                }
                catch (Exception)
                {

                }
            }
        }
        int n = 0;
        private void PasteImage(Image img, Size size, string outputfilename)
        {
            Bitmap bitmap = new Bitmap(size.Width, size.Height);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                SolidBrush b = new SolidBrush(Color.White);
                GraphicsUnit units = GraphicsUnit.Point;
                g.FillRectangle(b, bitmap.GetBounds(ref units));

                int addedWidth = (bitmap.Width - img.Width);
                if (addedWidth != 0)
                {
                    addedWidth = addedWidth / 2;
                }


                int addedHeight = (bitmap.Height - img.Height);
                if (addedHeight != 0)
                {
                    addedHeight = addedHeight / 2;
                }

                g.DrawImage(img, addedWidth, addedHeight);
                //                g.DrawImage(image2, image1.Width, 0);
            }
            try
            {
                //                bitmap.Save(outputfilename, System.Drawing.Imaging.ImageFormat.Png);
                bitmap.Save(outputfilename);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not save Image" + ex.Message);
            }
        }

        private System.Drawing.Image resizeImage(System.Drawing.Image imgToResize, Size size)
        {
            //Get the image current width
            int sourceWidth = imgToResize.Width;
            //Get the image current height
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;
            //Calulate  width with new desired size
            nPercentW = ((float)size.Width / (float)sourceWidth);
            //Calculate height with new desired size
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;
            //New Width
            int destWidth = (int)(sourceWidth * nPercent);
            //New Height
            int destHeight = (int)(sourceHeight * nPercent);
            Bitmap b = new Bitmap(size.Width, size.Height);
            //            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((System.Drawing.Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            // Draw image with new width and height
            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();
            return (System.Drawing.Image)b;
        }

        public static Image resizeImage(Image imgToResize, Size size, int x)
        {
            return (Image)(new Bitmap(imgToResize, size));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DeleteAllPics();
            n = 0;

            foreach (string name in urls)
            {

                string fileName = System.IO.Path.GetFileName(name);

                if (fileName.Contains(".png") == false)
                {

                    fileName = fileName.Replace(".jpg", ".png");
                    fileName = fileName.Replace(".jpeg", ".png");
                    fileName = fileName.Replace(".bmp", ".png");

                }


                n++;
                Image image = Image.FromFile(name);
                //       Image newImage = resizeImage(image, new Size(1500, 300));
                //       newImage.Save("size.png", System.Drawing.Imaging.ImageFormat.Png);
                int h = 0;
                int w = 0;
                int bh = 0;
                int bw = 0;
                try
                {
                    Int32.TryParse(textBoxAX.Text, out h);
                    Int32.TryParse(textBoxAY.Text, out w);
                    Int32.TryParse(textBoxBX.Text, out bh);
                    Int32.TryParse(textBoxBY.Text, out bw);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Size must be in numbers\nDescription" + ex.Message);
                }
                try
                {
                    Image NEWImage = resizeImage(image, new Size(bw, bh), 2);

                    //                    NewImage = resizeImage(image, new Size(bw, bh));
                    //        image.Save(n+"m.png", System.Drawing.Imaging.ImageFormat.Png);
                    PasteImage(NEWImage, new Size(w, h), fileName);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error:    \n" + ex.Message);
                }
            }
            MessageBox.Show("Done");

        }
        List<string> urls = new List<string>();

        private void button2_Click(object sender, EventArgs e)
        {
            n = 0;
            urls.Clear();
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = true;
            file.Filter = "Image Files(*.BMP;*.JPEG;*.JPG;*.PNG)|*.BMP;*.JPEG;*.JPG;*.PNG|All files (*.*)|*.*";
            file.ShowDialog();
            foreach (string i in file.FileNames)
            {
                urls.Add(i);
                textBox1.Text += i;
            }

        }

    }
}
