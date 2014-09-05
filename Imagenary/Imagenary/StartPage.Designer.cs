namespace Imagenary
{
    partial class StartPage
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.buttonStart = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panel1 = new System.Windows.Forms.Panel();
            this.buttonRichBox = new System.Windows.Forms.Button();
            this.buttonPrint = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.labelProgress = new System.Windows.Forms.Label();
            this.buttonExport = new System.Windows.Forms.Button();
            this.buttonStartInspection = new System.Windows.Forms.Button();
            this.labelUrl = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonSettings = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.backgroundWorker2 = new System.ComponentModel.BackgroundWorker();
            this.button1 = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.textBox1.Location = new System.Drawing.Point(223, 53);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(304, 33);
            this.textBox1.TabIndex = 0;
            // 
            // buttonStart
            // 
            this.buttonStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonStart.Location = new System.Drawing.Point(533, 53);
            this.buttonStart.Name = "buttonStart";
            this.buttonStart.Size = new System.Drawing.Size(94, 33);
            this.buttonStart.TabIndex = 1;
            this.buttonStart.Text = "Start Processing";
            this.buttonStart.UseVisualStyleBackColor = true;
            this.buttonStart.Click += new System.EventHandler(this.buttonStart_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.buttonRichBox);
            this.panel1.Controls.Add(this.buttonPrint);
            this.panel1.Controls.Add(this.richTextBox1);
            this.panel1.Controls.Add(this.labelProgress);
            this.panel1.Controls.Add(this.buttonExport);
            this.panel1.Controls.Add(this.buttonStartInspection);
            this.panel1.Location = new System.Drawing.Point(21, 114);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(710, 285);
            this.panel1.TabIndex = 3;
            // 
            // buttonRichBox
            // 
            this.buttonRichBox.Location = new System.Drawing.Point(610, 3);
            this.buttonRichBox.Name = "buttonRichBox";
            this.buttonRichBox.Size = new System.Drawing.Size(93, 36);
            this.buttonRichBox.TabIndex = 5;
            this.buttonRichBox.Text = "Hide";
            this.buttonRichBox.UseVisualStyleBackColor = true;
            this.buttonRichBox.Click += new System.EventHandler(this.buttonRichBox_Click);
            // 
            // buttonPrint
            // 
            this.buttonPrint.Location = new System.Drawing.Point(150, 3);
            this.buttonPrint.Name = "buttonPrint";
            this.buttonPrint.Size = new System.Drawing.Size(87, 36);
            this.buttonPrint.TabIndex = 4;
            this.buttonPrint.Text = "Print Properties";
            this.buttonPrint.UseVisualStyleBackColor = true;
            this.buttonPrint.Click += new System.EventHandler(this.buttonPrint_Click);
            // 
            // richTextBox1
            // 
            this.richTextBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.richTextBox1.BackColor = System.Drawing.Color.Gray;
            this.richTextBox1.Font = new System.Drawing.Font("Modern No. 20", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richTextBox1.ForeColor = System.Drawing.SystemColors.Window;
            this.richTextBox1.Location = new System.Drawing.Point(3, 45);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.ReadOnly = true;
            this.richTextBox1.Size = new System.Drawing.Size(700, 233);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // labelProgress
            // 
            this.labelProgress.AutoSize = true;
            this.labelProgress.Location = new System.Drawing.Point(577, 15);
            this.labelProgress.Name = "labelProgress";
            this.labelProgress.Size = new System.Drawing.Size(27, 13);
            this.labelProgress.TabIndex = 2;
            this.labelProgress.Text = "50%";
            // 
            // buttonExport
            // 
            this.buttonExport.Location = new System.Drawing.Point(95, 3);
            this.buttonExport.Name = "buttonExport";
            this.buttonExport.Size = new System.Drawing.Size(49, 36);
            this.buttonExport.TabIndex = 1;
            this.buttonExport.Text = "Export";
            this.buttonExport.UseVisualStyleBackColor = true;
            // 
            // buttonStartInspection
            // 
            this.buttonStartInspection.Location = new System.Drawing.Point(3, 3);
            this.buttonStartInspection.Name = "buttonStartInspection";
            this.buttonStartInspection.Size = new System.Drawing.Size(86, 36);
            this.buttonStartInspection.TabIndex = 0;
            this.buttonStartInspection.Text = "StartInspection";
            this.buttonStartInspection.UseVisualStyleBackColor = true;
            this.buttonStartInspection.Click += new System.EventHandler(this.buttonStartInspection_Click);
            // 
            // labelUrl
            // 
            this.labelUrl.AutoSize = true;
            this.labelUrl.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelUrl.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.labelUrl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelUrl.ForeColor = System.Drawing.Color.White;
            this.labelUrl.Location = new System.Drawing.Point(123, 58);
            this.labelUrl.Name = "labelUrl";
            this.labelUrl.Size = new System.Drawing.Size(85, 20);
            this.labelUrl.TabIndex = 3;
            this.labelUrl.Text = "Input File";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(96, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = " 2343423";
            // 
            // buttonSettings
            // 
            this.buttonSettings.Location = new System.Drawing.Point(676, 9);
            this.buttonSettings.Name = "buttonSettings";
            this.buttonSettings.Size = new System.Drawing.Size(65, 21);
            this.buttonSettings.TabIndex = 6;
            this.buttonSettings.Text = "Settings";
            this.buttonSettings.UseVisualStyleBackColor = true;
            this.buttonSettings.Click += new System.EventHandler(this.buttonSettings_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.label3.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.label3.Font = new System.Drawing.Font("Algerian", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.DarkGray;
            this.label3.Location = new System.Drawing.Point(17, 9);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(248, 24);
            this.label3.TabIndex = 7;
            this.label3.Tag = "342343";
            this.label3.Text = "ZOOPLA IMAGE MATCH";
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork_1);
            // 
            // backgroundWorker2
            // 
            this.backgroundWorker2.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker2_DoWork);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(243, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(103, 36);
            this.button1.TabIndex = 6;
            this.button1.Text = "Add Record In DB";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // StartPage
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.ClientSize = new System.Drawing.Size(753, 411);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.buttonSettings);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.labelUrl);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.buttonStart);
            this.Controls.Add(this.textBox1);
            this.Name = "StartPage";
            this.Text = "Zoopla Image Match";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.StartPage_FormClosed);
            this.Load += new System.EventHandler(this.StartPage_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonStart;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label labelProgress;
        private System.Windows.Forms.Button buttonExport;
        private System.Windows.Forms.Button buttonStartInspection;
        private System.Windows.Forms.Label labelUrl;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button buttonSettings;
        private System.Windows.Forms.Button buttonPrint;
        private System.Windows.Forms.Button buttonRichBox;
        private System.Windows.Forms.Label label3;
        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.ComponentModel.BackgroundWorker backgroundWorker2;
        private System.Windows.Forms.Button button1;
    }
}