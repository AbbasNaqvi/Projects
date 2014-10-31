using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Windows.Media.Imaging;
using System.Windows;
using Vlc;


namespace FreeGifCreator
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = true;
           // dialog.Filter = "";
            if (DialogResult.OK == dialog.ShowDialog())
            {
                foreach(var x in dialog.FileNames)
                {
                  richTextBox1.Text += x+" , ";
                }
                richTextBox1.Text.Remove(richTextBox1.Text.Length-1, 1);
                richTextBox1.Update();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
                string FileName = "D://FileName.gif";
                var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
                string []FileNames=richTextBox1.Text.Split(',');
    
            System.Windows.Media.Imaging.GifBitmapEncoder gEnc = new GifBitmapEncoder();

            foreach (var x in FileNames)
            {
                System.Drawing.Bitmap bmpImage = new Bitmap(x);
                var src = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(
                    bmpImage.GetHbitmap(),
                    IntPtr.Zero,
                    Int32Rect.Empty,
                    BitmapSizeOptions.FromEmptyOptions());
                gEnc.Frames.Add(BitmapFrame.Create(src));
            }
            gEnc.Save(new FileStream(FileName, FileMode.Create));











        //    string FileName = "D://FileName.gif";
        //    var enc = new System.Windows.Media.Imaging.PngBitmapEncoder();
        //    string []FileNames=richTextBox1.Text.Split(',');
        //    foreach (var x in FileNames)
        //    {
        //      //  var jpgFile = File.OpenRead(x);
        //         var uriBitmap = BitmapDecoder.Create(new Uri(x,UriKind.Absolute) ,BitmapCreateOptions.None,BitmapCacheOption.Default);

        //            foreach (var frame in uriBitmap.Frames)
        //            {
        //                enc.Frames.Add(frame);
        //            }
        // }
        //    using (var stm = System.IO.File.Create(FileName))
        //    {
        //        enc.Save(stm);
        //    }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
