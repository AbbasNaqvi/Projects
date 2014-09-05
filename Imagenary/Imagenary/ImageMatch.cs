using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Imagenary
{
    public partial class ImageMatch : Form
    {
        Form OldForm=null;
        PropertyLog log = PropertyLog.Create;

        public ImageMatch(Form X)
        {
            InitializeComponent();
            OldForm = X;
            buttonClose.Visible = false;
        }

        private void pictureBox7_MouseEnter(object sender, EventArgs e)
        {
            pictureBox7.BorderStyle = BorderStyle.FixedSingle;

        }
        private void pictureBox7_MouseLeave(object sender, EventArgs e)
        {
            pictureBox7.BorderStyle = BorderStyle.Fixed3D;

        }
        private void pictureBox6_MouseEnter(object sender, EventArgs e)
        {
            pictureBox6.BorderStyle = BorderStyle.FixedSingle;

        }

        private void pictureBox6_MouseLeave(object sender, EventArgs e)
        {
            pictureBox6.BorderStyle = BorderStyle.Fixed3D;

        }
        private void ButtonMPre_MouseEnter(object sender, EventArgs e)
        {
            ButtonMPre.BorderStyle = BorderStyle.FixedSingle;
        }
        private void ButtonMNext_MouseEnter(object sender, EventArgs e)
        {
            ButtonMNext.BorderStyle = BorderStyle.FixedSingle;
        }
        private void ButtonMNext_MouseLeave(object sender, EventArgs e)
        {
            ButtonMNext.BorderStyle = BorderStyle.Fixed3D;
        }
        private void ButtonMPre_MouseLeave(object sender, EventArgs e)
        {
            ButtonMPre.BorderStyle = BorderStyle.Fixed3D;
        }
        private void ButtonMatch_MouseEnter(object sender, EventArgs e)
        {
            ButtonMatch.BorderStyle = BorderStyle.Fixed3D;
        }

        private void ButtonMatch_MouseLeave(object sender, EventArgs e)
        {
            ButtonMatch.BorderStyle = BorderStyle.FixedSingle;
        }

        private void PictureBoxS1_Click(object sender, EventArgs e)
        {


            SuspendLayout();
            PictureBoxS2.Location = new Point(8, 8);
            PictureBoxS1.Size = new Size(panel2.Width, panel2.Height );
            PictureBoxS2.SendToBack();
            PictureBoxS3.SendToBack();
            PictureBoxS4.SendToBack();
            panel2.SendToBack();
            PictureBoxS1.BringToFront();
            buttonClose.Visible = true;
            buttonClose.BringToFront();
            ResumeLayout();
        }

        private void PictureBoxS2_Click(object sender, EventArgs e)
        {

            SuspendLayout();
            PictureBoxS2.Location = new Point(8, 8);
            PictureBoxS2.Size = new Size(panel2.Width , panel2.Height);
            panel2.SendToBack();
            PictureBoxS1.SendToBack();
            PictureBoxS3.SendToBack();
            PictureBoxS4.SendToBack();
            PictureBoxS2.BringToFront();
            buttonClose.Visible = true;
            buttonClose.BringToFront();
            ResumeLayout();
        }

        private void PictureBoxS3_Click(object sender, EventArgs e)
        {

            SuspendLayout();
            PictureBoxS3.Location = new Point(3,3);
            PictureBoxS3.Size = new Size(panel2.Width-1 , panel2.Height-1);
            PictureBoxS1.SendToBack();
            PictureBoxS2.SendToBack();
            PictureBoxS4.SendToBack();
            PictureBoxS3.BringToFront();
            buttonClose.Visible = true;
            buttonClose.BringToFront();
            ResumeLayout();
        }

        private void PictureBoxS4_Click(object sender, EventArgs e)
        {

            SuspendLayout();
            PictureBoxS4.Location = new Point(3,3);
            PictureBoxS4.Size = new Size(panel2.Width-1 , panel2.Height - 1);
            PictureBoxS1.SendToBack();
            PictureBoxS2.SendToBack();
            PictureBoxS3.SendToBack();        
            PictureBoxS4.BringToFront();
            buttonClose.Visible = true;
            buttonClose.BringToFront();
          
            ResumeLayout();
        }

        private void ImageMatch_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void buttonClose_Click(object sender, EventArgs e)
        {
            /*
          *No need to take shortcut 
          * 
          */


            //   InitializeComponent();

            buttonClose.Visible = false;
          
            this.PictureBoxS1.Size = new System.Drawing.Size(333, 308);
            this.PictureBoxS1.Location = new System.Drawing.Point(9, 3);
            this.PictureBoxS2.Size = new System.Drawing.Size(287, 308);
            this.PictureBoxS2.Location = new System.Drawing.Point(348, 3);
            this.PictureBoxS3.Size = new System.Drawing.Size(333, 274);
            this.PictureBoxS3.Location = new System.Drawing.Point(9, 326);
            this.PictureBoxS4.Size = new System.Drawing.Size(287, 274);
            this.PictureBoxS4.Location = new System.Drawing.Point(348, 326);
            this.pictureBox7.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.pictureBox6.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.WindowState = FormWindowState.Maximized;
        }

        private void ImageMatch_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y ;
            textBox1.Refresh();
        }

        private void PictureBoxS1_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();
        }

        private void PictureBoxS3_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();

        }

        private void PictureBoxS4_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();

        }

        private void PictureBoxS2_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();

        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();

        }

        private void pictureBox6_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();

        }

        private void pictureBox7_MouseMove(object sender, MouseEventArgs e)
        {
            textBox1.Text = MousePosition.X + "  & " + MousePosition.Y;
            textBox1.Refresh();

        }

        private void pictureBox7_Click(object sender, EventArgs e)
        {
        }

        private void pictureBox6_Click(object sender, EventArgs e)
        {
        }







        //private void PictureBoxS1_MouseEnter(object sender, EventArgs e)
        //{
        //    PictureBoxS1.Size = new Size(Convert.ToInt32(PictureBoxS1.Height * 1.5),Convert.ToInt32(PictureBoxS1.Width * 1.5));
        //    PictureBoxS1.BringToFront();
        //}

        //private void PictureBoxS1_MouseLeave(object sender, EventArgs e)
        //{
        //    PictureBoxS1.Size = new Size(Convert.ToInt32(PictureBoxS1.Height / 1.5), Convert.ToInt32(PictureBoxS1.Width / 1.5));
        //    PictureBoxS1.SendToBack();
        //}








        //private void PictureBoxS2_MouseEnter(object sender, EventArgs e)
        //{
        //    PictureBoxS2.Size = new Size(Convert.ToInt32(PictureBoxS2.Height * 1.5), Convert.ToInt32(PictureBoxS2.Width * 1.5));
        //    OldX = PictureBoxS2.Location.X;
        //    OldY = PictureBoxS2.Location.Y;
        //    PictureBoxS2.Location = new Point(PictureBoxS2.Location.X - (Convert.ToInt32(PictureBoxS2.Width * 0.5)), PictureBoxS2.Location.Y);
        //    PictureBoxS2.BringToFront();
        //}
        //private void PictureBoxS2_MouseLeave(object sender, EventArgs e)
        //{
        //    PictureBoxS2.Size = new Size(Convert.ToInt32(PictureBoxS2.Height / 1.5), Convert.ToInt32(PictureBoxS2.Width / 1.5));
        //    PictureBoxS2.Location = new Point(OldX, OldY);
        //    PictureBoxS2.SendToBack();
        //}


        //private void PictureBoxS3_MouseEnter(object sender, EventArgs e)
        //{
        //    PictureBoxS3.Size = new Size(Convert.ToInt32(PictureBoxS3.Height *1.5), Convert.ToInt32(PictureBoxS3.Width * 1.5));
        //    OldX = PictureBoxS3.Location.X;
        //    OldY = PictureBoxS3.Location.Y;
        //    PictureBoxS3.Location = new Point(PictureBoxS3.Location.X, PictureBoxS3.Location.Y-(Convert.ToInt32(PictureBoxS3.Height * 0.5)));
        //    PictureBoxS3.BringToFront();
        //}

        //private void PictureBoxS3_MouseLeave(object sender, EventArgs e)
        //{
        //    PictureBoxS3.Size = new Size(Convert.ToInt32(PictureBoxS3.Height / 1.5), Convert.ToInt32(PictureBoxS3.Width / 1.5));
        //    PictureBoxS3.Location = new Point(OldX, OldY);
        //    PictureBoxS3.SendToBack();
        //}


        //private void PictureBoxS4_MouseEnter(object sender, EventArgs e)
        //{
        //    PictureBoxS4.Size = new Size(Convert.ToInt32(PictureBoxS4.Height * 1.5), Convert.ToInt32(PictureBoxS4.Width * 1.5));
        //    OldX = PictureBoxS4.Location.X;
        //    OldY = PictureBoxS4.Location.Y;
        //    PictureBoxS4.Location = new Point(PictureBoxS4.Location.X-(Convert.ToInt32(PictureBoxS3.Width * 0.5)),Convert.ToInt32( PictureBoxS4.Location.Y -PictureBoxS4.Height * 0.5));
        //    PictureBoxS4.BringToFront();
        //}


        //private void PictureBoxS4_MouseLeave(object sender, EventArgs e)
        //{
        //    PictureBoxS4.Size = new Size(Convert.ToInt32(PictureBoxS4.Height / 1.5),Convert.ToInt32( PictureBoxS4.Width / 1.5));
        //    PictureBoxS4.Location = new Point(OldX, OldY);
        //    PictureBoxS4.SendToBack();

        //}
    }
}
