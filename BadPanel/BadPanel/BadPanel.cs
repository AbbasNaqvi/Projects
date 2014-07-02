using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BadPanel
{
    public partial class BadPanel : Panel
    {
        public BadPanel()
        {
            InitializeComponent();
            
            this.BackColor = Color.GhostWhite;
            hEADING.Location = new Point(90, 10);
            input.Size = new Size(100, 100);
            label1.Location = new Point(10, 40);
            HeadingName = "Login";
            this.Font = new System.Drawing.Font(FontFamily.GenericSansSerif,12,FontStyle.Bold);
            LabelOneName = "UserName";
            input.Location =  new Point(100, 40);
            label2.Location = new Point(10, 80);
            LabelTwoName = "Password";
            output.UseSystemPasswordChar = true;
            output.Location = new Point(100, 80);
            submit.Location = new Point(110, 120);
            output.Size = new Size(100, 100);
            submit.Size = new Size(80, 40);

            ButtonName = "Login";
          
            this.ForeColor = Color.Green;
            this.Controls.Add(input);
            this.Controls.Add(output);
            this.Controls.Add(submit);
            this.Controls.Add(label1);
            this.Controls.Add(label2);
            this.Controls.Add(hEADING);

     
        }

        Label hEADING = new Label();
        TextBox input = new TextBox();
        TextBox output = new TextBox();
        Button submit = new Button();
        Label label1 = new Label();
        Label label2 = new Label();


        private string headingName;

        public string HeadingName
        {
            get { return headingName; }
            set { headingName = value; hEADING.Text = value; }
        }
        


        private string labeloneName;

        public string LabelOneName
        {
            get { return labeloneName; }
            set { labeloneName = value; label1.Text = value; }
        }


        private string labeltwoName;

        public string LabelTwoName
        {
            get { return labeltwoName; }
            set { labeltwoName = value; label2.Text = value; }
        }
        
        private string buttonName;

        public string ButtonName
        {
            get { return buttonName; }
            set { buttonName = value; submit.Text = buttonName; }
        }
        
        

        private Color buttonfrontColor;

        public Color ButtonFrontColor
        {
            get { return buttonfrontColor; }
            set { buttonfrontColor = value; Invalidate(); submit.ForeColor = ButtonFrontColor; }
        }

        private Color buttonBackColor;

        public Color ButtonBackColor
        {
            get { return buttonBackColor; }
            set { buttonBackColor = value; submit.BackColor = buttonBackColor; }
        }

        private Color textboxBackColor;

        public Color TextboxBackColor
        {
            get { return textboxBackColor; }
            set { textboxBackColor = value; Invalidate(); }
        }


        private Color textboxFrontColor;

        public Color TextBoxFrontColor
        {
            get { return textboxFrontColor; }
            set { textboxFrontColor = value; Invalidate(); }
        }
        
        
        protected override void OnPaint(PaintEventArgs pe)
        {
            base.OnPaint(pe);
          
       

            //Invalidate();
            
        }

    }
}
