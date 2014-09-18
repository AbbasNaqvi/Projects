namespace ItextSharp
{
    partial class TestingForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.textBoxURX = new System.Windows.Forms.TextBox();
            this.textBoxLLX = new System.Windows.Forms.TextBox();
            this.textBoxURY = new System.Windows.Forms.TextBox();
            this.textBoxLLY = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(728, 8);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(122, 52);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.Location = new System.Drawing.Point(13, 66);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(863, 268);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(62, 37);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(299, 20);
            this.textBox1.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(367, 37);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "browse";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // textBoxURX
            // 
            this.textBoxURX.Location = new System.Drawing.Point(463, 13);
            this.textBoxURX.Name = "textBoxURX";
            this.textBoxURX.Size = new System.Drawing.Size(100, 20);
            this.textBoxURX.TabIndex = 4;
            // 
            // textBoxLLX
            // 
            this.textBoxLLX.Location = new System.Drawing.Point(463, 40);
            this.textBoxLLX.Name = "textBoxLLX";
            this.textBoxLLX.Size = new System.Drawing.Size(100, 20);
            this.textBoxLLX.TabIndex = 5;
            // 
            // textBoxURY
            // 
            this.textBoxURY.Location = new System.Drawing.Point(585, 12);
            this.textBoxURY.Name = "textBoxURY";
            this.textBoxURY.Size = new System.Drawing.Size(100, 20);
            this.textBoxURY.TabIndex = 6;
            // 
            // textBoxLLY
            // 
            this.textBoxLLY.Location = new System.Drawing.Point(585, 40);
            this.textBoxLLY.Name = "textBoxLLY";
            this.textBoxLLY.Size = new System.Drawing.Size(100, 20);
            this.textBoxLLY.TabIndex = 7;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(285, 8);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 8;
            this.button3.Text = "button3";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // TestingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(888, 346);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.textBoxLLY);
            this.Controls.Add(this.textBoxURY);
            this.Controls.Add(this.textBoxLLX);
            this.Controls.Add(this.textBoxURX);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.richTextBox1);
            this.Controls.Add(this.button1);
            this.KeyPreview = true;
            this.Name = "TestingForm";
            this.Text = "TestingForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox textBoxURX;
        private System.Windows.Forms.TextBox textBoxLLX;
        private System.Windows.Forms.TextBox textBoxURY;
        private System.Windows.Forms.TextBox textBoxLLY;
        private System.Windows.Forms.Button button3;
    }
}