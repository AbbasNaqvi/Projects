namespace HiringAutomationTool
{
    partial class Form1
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
            this.LabelHeading = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.TabControl1 = new System.Windows.Forms.TabControl();
            this.TCEmail = new System.Windows.Forms.TabPage();
            this.TCEmailTemplate = new System.Windows.Forms.TabPage();
            this.ButtonTabBrowseEmailTemplate = new System.Windows.Forms.Button();
            this.LabelSelectedTemplate = new System.Windows.Forms.Label();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.labelTabEmailTemplates = new System.Windows.Forms.Label();
            this.TabbuttonSave = new System.Windows.Forms.Button();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.TCSettings = new System.Windows.Forms.TabPage();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.labelEmail = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.buttonLoginReset = new System.Windows.Forms.Button();
            this.buttonLoginSave = new System.Windows.Forms.Button();
            this.textBoxpassword = new System.Windows.Forms.TextBox();
            this.labelPassword = new System.Windows.Forms.Label();
            this.textBoxuserID = new System.Windows.Forms.TextBox();
            this.LabelErrorMessage = new System.Windows.Forms.Label();
            this.labelColorAlert3 = new System.Windows.Forms.Label();
            this.button6 = new System.Windows.Forms.Button();
            this.labelTopColor = new System.Windows.Forms.Label();
            this.textBoxThemeName = new System.Windows.Forms.TextBox();
            this.labelThemeName = new System.Windows.Forms.Label();
            this.labelColorAlert2 = new System.Windows.Forms.Label();
            this.labelColorAlert1 = new System.Windows.Forms.Label();
            this.buttonSetColor = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.labelBackColor = new System.Windows.Forms.Label();
            this.comboBoxColors = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.radioButtonThemes = new System.Windows.Forms.RadioButton();
            this.radioButtonCustom = new System.Windows.Forms.RadioButton();
            this.TCDataFiltering = new System.Windows.Forms.TabPage();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.webBrowser1 = new System.Windows.Forms.WebBrowser();
            this.button3 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.dateTimePicker2 = new System.Windows.Forms.DateTimePicker();
            this.labelTo = new System.Windows.Forms.Label();
            this.labelFrom = new System.Windows.Forms.Label();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.radioButtonSpecificDate = new System.Windows.Forms.RadioButton();
            this.radioButtonRecent = new System.Windows.Forms.RadioButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.colorDialog1 = new System.Windows.Forms.ColorDialog();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.TabControl1.SuspendLayout();
            this.TCEmailTemplate.SuspendLayout();
            this.TCSettings.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.TCDataFiltering.SuspendLayout();
            this.SuspendLayout();
            // 
            // LabelHeading
            // 
            this.LabelHeading.AutoSize = true;
            this.LabelHeading.Font = new System.Drawing.Font("Showcard Gothic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.LabelHeading.ForeColor = System.Drawing.Color.Maroon;
            this.LabelHeading.Location = new System.Drawing.Point(75, 21);
            this.LabelHeading.Name = "LabelHeading";
            this.LabelHeading.Size = new System.Drawing.Size(423, 36);
            this.LabelHeading.TabIndex = 0;
            this.LabelHeading.Text = "Hiring Automation Tool";
            // 
            // panel1
            // 
            this.panel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel1.AutoSize = true;
            this.panel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.panel1.BackColor = System.Drawing.SystemColors.ControlLight;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Controls.Add(this.TabControl1);
            this.panel1.Controls.Add(this.LabelHeading);
            this.panel1.Location = new System.Drawing.Point(0, 2);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(986, 539);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::HiringAutomationTool.Properties.Resources.Untitled;
            this.pictureBox1.Location = new System.Drawing.Point(3, 3);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(75, 63);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // TabControl1
            // 
            this.TabControl1.AllowDrop = true;
            this.TabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TabControl1.Controls.Add(this.TCEmail);
            this.TabControl1.Controls.Add(this.TCEmailTemplate);
            this.TabControl1.Controls.Add(this.TCSettings);
            this.TabControl1.Controls.Add(this.TCDataFiltering);
            this.TabControl1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TabControl1.Location = new System.Drawing.Point(12, 76);
            this.TabControl1.Name = "TabControl1";
            this.TabControl1.SelectedIndex = 0;
            this.TabControl1.ShowToolTips = true;
            this.TabControl1.Size = new System.Drawing.Size(962, 449);
            this.TabControl1.SizeMode = System.Windows.Forms.TabSizeMode.FillToRight;
            this.TabControl1.TabIndex = 1;
            // 
            // TCEmail
            // 
            this.TCEmail.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TCEmail.Location = new System.Drawing.Point(4, 22);
            this.TCEmail.Name = "TCEmail";
            this.TCEmail.Padding = new System.Windows.Forms.Padding(3);
            this.TCEmail.Size = new System.Drawing.Size(954, 423);
            this.TCEmail.TabIndex = 1;
            this.TCEmail.Text = "Email";
            this.TCEmail.UseVisualStyleBackColor = true;
            // 
            // TCEmailTemplate
            // 
            this.TCEmailTemplate.Controls.Add(this.ButtonTabBrowseEmailTemplate);
            this.TCEmailTemplate.Controls.Add(this.LabelSelectedTemplate);
            this.TCEmailTemplate.Controls.Add(this.textBox3);
            this.TCEmailTemplate.Controls.Add(this.labelTabEmailTemplates);
            this.TCEmailTemplate.Controls.Add(this.TabbuttonSave);
            this.TCEmailTemplate.Controls.Add(this.richTextBox1);
            this.TCEmailTemplate.Controls.Add(this.comboBox1);
            this.TCEmailTemplate.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TCEmailTemplate.Location = new System.Drawing.Point(4, 22);
            this.TCEmailTemplate.Name = "TCEmailTemplate";
            this.TCEmailTemplate.Padding = new System.Windows.Forms.Padding(3);
            this.TCEmailTemplate.Size = new System.Drawing.Size(954, 423);
            this.TCEmailTemplate.TabIndex = 2;
            this.TCEmailTemplate.Text = "Email Template";
            this.TCEmailTemplate.UseVisualStyleBackColor = true;
            // 
            // ButtonTabBrowseEmailTemplate
            // 
            this.ButtonTabBrowseEmailTemplate.Location = new System.Drawing.Point(407, 37);
            this.ButtonTabBrowseEmailTemplate.Name = "ButtonTabBrowseEmailTemplate";
            this.ButtonTabBrowseEmailTemplate.Size = new System.Drawing.Size(75, 23);
            this.ButtonTabBrowseEmailTemplate.TabIndex = 6;
            this.ButtonTabBrowseEmailTemplate.Text = "Browse";
            this.ButtonTabBrowseEmailTemplate.UseVisualStyleBackColor = true;
            this.ButtonTabBrowseEmailTemplate.Click += new System.EventHandler(this.ButtonTabBrowseEmailTemplate_Click);
            // 
            // LabelSelectedTemplate
            // 
            this.LabelSelectedTemplate.AutoSize = true;
            this.LabelSelectedTemplate.Location = new System.Drawing.Point(46, 42);
            this.LabelSelectedTemplate.Name = "LabelSelectedTemplate";
            this.LabelSelectedTemplate.Size = new System.Drawing.Size(113, 13);
            this.LabelSelectedTemplate.TabIndex = 5;
            this.LabelSelectedTemplate.Text = "Selected Template";
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(165, 39);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(199, 20);
            this.textBox3.TabIndex = 4;
            // 
            // labelTabEmailTemplates
            // 
            this.labelTabEmailTemplates.AutoSize = true;
            this.labelTabEmailTemplates.Location = new System.Drawing.Point(589, 37);
            this.labelTabEmailTemplates.Name = "labelTabEmailTemplates";
            this.labelTabEmailTemplates.Size = new System.Drawing.Size(96, 13);
            this.labelTabEmailTemplates.TabIndex = 3;
            this.labelTabEmailTemplates.Text = "Email Tempates";
            // 
            // TabbuttonSave
            // 
            this.TabbuttonSave.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TabbuttonSave.Location = new System.Drawing.Point(872, 396);
            this.TabbuttonSave.Name = "TabbuttonSave";
            this.TabbuttonSave.Size = new System.Drawing.Size(75, 23);
            this.TabbuttonSave.TabIndex = 2;
            this.TabbuttonSave.Text = "Save";
            this.TabbuttonSave.UseVisualStyleBackColor = true;
            // 
            // richTextBox1
            // 
            this.richTextBox1.Location = new System.Drawing.Point(18, 82);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(929, 297);
            this.richTextBox1.TabIndex = 1;
            this.richTextBox1.Text = "";
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(716, 29);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(199, 21);
            this.comboBox1.Sorted = true;
            this.comboBox1.TabIndex = 2;
            // 
            // TCSettings
            // 
            this.TCSettings.Controls.Add(this.splitContainer1);
            this.TCSettings.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TCSettings.Location = new System.Drawing.Point(4, 22);
            this.TCSettings.Name = "TCSettings";
            this.TCSettings.Padding = new System.Windows.Forms.Padding(3);
            this.TCSettings.Size = new System.Drawing.Size(954, 423);
            this.TCSettings.TabIndex = 3;
            this.TCSettings.Text = "Setting";
            this.TCSettings.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(32, 27);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.AllowDrop = true;
            this.splitContainer1.Panel1.Controls.Add(this.labelEmail);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.buttonLoginReset);
            this.splitContainer1.Panel1.Controls.Add(this.buttonLoginSave);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxpassword);
            this.splitContainer1.Panel1.Controls.Add(this.labelPassword);
            this.splitContainer1.Panel1.Controls.Add(this.textBoxuserID);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.LabelErrorMessage);
            this.splitContainer1.Panel2.Controls.Add(this.labelColorAlert3);
            this.splitContainer1.Panel2.Controls.Add(this.button6);
            this.splitContainer1.Panel2.Controls.Add(this.labelTopColor);
            this.splitContainer1.Panel2.Controls.Add(this.textBoxThemeName);
            this.splitContainer1.Panel2.Controls.Add(this.labelThemeName);
            this.splitContainer1.Panel2.Controls.Add(this.labelColorAlert2);
            this.splitContainer1.Panel2.Controls.Add(this.labelColorAlert1);
            this.splitContainer1.Panel2.Controls.Add(this.buttonSetColor);
            this.splitContainer1.Panel2.Controls.Add(this.button5);
            this.splitContainer1.Panel2.Controls.Add(this.button4);
            this.splitContainer1.Panel2.Controls.Add(this.label3);
            this.splitContainer1.Panel2.Controls.Add(this.labelBackColor);
            this.splitContainer1.Panel2.Controls.Add(this.comboBoxColors);
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonThemes);
            this.splitContainer1.Panel2.Controls.Add(this.radioButtonCustom);
            this.splitContainer1.Size = new System.Drawing.Size(919, 297);
            this.splitContainer1.SplitterDistance = 306;
            this.splitContainer1.TabIndex = 4;
            // 
            // labelEmail
            // 
            this.labelEmail.AutoSize = true;
            this.labelEmail.Location = new System.Drawing.Point(56, 90);
            this.labelEmail.Name = "labelEmail";
            this.labelEmail.Size = new System.Drawing.Size(37, 13);
            this.labelEmail.TabIndex = 7;
            this.labelEmail.Text = "Email";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(85, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 6;
            this.label1.Text = "Profile";
            // 
            // buttonLoginReset
            // 
            this.buttonLoginReset.Location = new System.Drawing.Point(156, 180);
            this.buttonLoginReset.Name = "buttonLoginReset";
            this.buttonLoginReset.Size = new System.Drawing.Size(75, 23);
            this.buttonLoginReset.TabIndex = 5;
            this.buttonLoginReset.Text = "Reset";
            this.buttonLoginReset.UseVisualStyleBackColor = true;
            this.buttonLoginReset.Click += new System.EventHandler(this.buttonLoginReset_Click);
            // 
            // buttonLoginSave
            // 
            this.buttonLoginSave.Location = new System.Drawing.Point(59, 180);
            this.buttonLoginSave.Name = "buttonLoginSave";
            this.buttonLoginSave.Size = new System.Drawing.Size(75, 23);
            this.buttonLoginSave.TabIndex = 4;
            this.buttonLoginSave.Text = "Save";
            this.buttonLoginSave.UseVisualStyleBackColor = true;
            this.buttonLoginSave.Click += new System.EventHandler(this.buttonLoginSave_Click);
            // 
            // textBoxpassword
            // 
            this.textBoxpassword.Location = new System.Drawing.Point(147, 122);
            this.textBoxpassword.Name = "textBoxpassword";
            this.textBoxpassword.Size = new System.Drawing.Size(100, 20);
            this.textBoxpassword.TabIndex = 3;
            this.textBoxpassword.UseSystemPasswordChar = true;
            // 
            // labelPassword
            // 
            this.labelPassword.AutoSize = true;
            this.labelPassword.Location = new System.Drawing.Point(56, 129);
            this.labelPassword.Name = "labelPassword";
            this.labelPassword.Size = new System.Drawing.Size(61, 13);
            this.labelPassword.TabIndex = 1;
            this.labelPassword.Text = "Password";
            // 
            // textBoxuserID
            // 
            this.textBoxuserID.Location = new System.Drawing.Point(147, 83);
            this.textBoxuserID.Name = "textBoxuserID";
            this.textBoxuserID.Size = new System.Drawing.Size(100, 20);
            this.textBoxuserID.TabIndex = 2;
            // 
            // LabelErrorMessage
            // 
            this.LabelErrorMessage.AutoSize = true;
            this.LabelErrorMessage.ForeColor = System.Drawing.Color.Red;
            this.LabelErrorMessage.Location = new System.Drawing.Point(288, 237);
            this.LabelErrorMessage.Name = "LabelErrorMessage";
            this.LabelErrorMessage.Size = new System.Drawing.Size(88, 13);
            this.LabelErrorMessage.TabIndex = 18;
            this.LabelErrorMessage.Text = "Error Message";
            // 
            // labelColorAlert3
            // 
            this.labelColorAlert3.AutoSize = true;
            this.labelColorAlert3.Location = new System.Drawing.Point(288, 185);
            this.labelColorAlert3.Name = "labelColorAlert3";
            this.labelColorAlert3.Size = new System.Drawing.Size(80, 13);
            this.labelColorAlert3.TabIndex = 17;
            this.labelColorAlert3.Text = "Not Choosen";
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(195, 175);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 16;
            this.button6.Text = "Choose";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // labelTopColor
            // 
            this.labelTopColor.AutoSize = true;
            this.labelTopColor.Location = new System.Drawing.Point(102, 185);
            this.labelTopColor.Name = "labelTopColor";
            this.labelTopColor.Size = new System.Drawing.Size(62, 13);
            this.labelTopColor.TabIndex = 15;
            this.labelTopColor.Text = "Top Color";
            // 
            // textBoxThemeName
            // 
            this.textBoxThemeName.Location = new System.Drawing.Point(195, 100);
            this.textBoxThemeName.Name = "textBoxThemeName";
            this.textBoxThemeName.Size = new System.Drawing.Size(100, 20);
            this.textBoxThemeName.TabIndex = 14;
            // 
            // labelThemeName
            // 
            this.labelThemeName.AutoSize = true;
            this.labelThemeName.Location = new System.Drawing.Point(102, 103);
            this.labelThemeName.Name = "labelThemeName";
            this.labelThemeName.Size = new System.Drawing.Size(81, 13);
            this.labelThemeName.TabIndex = 13;
            this.labelThemeName.Text = "Theme Name";
            // 
            // labelColorAlert2
            // 
            this.labelColorAlert2.AutoSize = true;
            this.labelColorAlert2.Location = new System.Drawing.Point(288, 159);
            this.labelColorAlert2.Name = "labelColorAlert2";
            this.labelColorAlert2.Size = new System.Drawing.Size(76, 13);
            this.labelColorAlert2.TabIndex = 12;
            this.labelColorAlert2.Text = "NotChoosen";
            // 
            // labelColorAlert1
            // 
            this.labelColorAlert1.AutoSize = true;
            this.labelColorAlert1.Location = new System.Drawing.Point(288, 134);
            this.labelColorAlert1.Name = "labelColorAlert1";
            this.labelColorAlert1.Size = new System.Drawing.Size(80, 13);
            this.labelColorAlert1.TabIndex = 11;
            this.labelColorAlert1.Text = "Not Choosen";
            // 
            // buttonSetColor
            // 
            this.buttonSetColor.Location = new System.Drawing.Point(368, 211);
            this.buttonSetColor.Name = "buttonSetColor";
            this.buttonSetColor.Size = new System.Drawing.Size(75, 23);
            this.buttonSetColor.TabIndex = 10;
            this.buttonSetColor.Text = "Save";
            this.buttonSetColor.UseVisualStyleBackColor = true;
            this.buttonSetColor.Click += new System.EventHandler(this.buttonSetColor_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(195, 149);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 8;
            this.button5.Text = "Choose";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(195, 124);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 7;
            this.button4.Text = "Choose";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(102, 159);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(61, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "TextColor";
            // 
            // labelBackColor
            // 
            this.labelBackColor.AutoSize = true;
            this.labelBackColor.Location = new System.Drawing.Point(102, 129);
            this.labelBackColor.Name = "labelBackColor";
            this.labelBackColor.Size = new System.Drawing.Size(65, 13);
            this.labelBackColor.TabIndex = 4;
            this.labelBackColor.Text = "BackColor";
            // 
            // comboBoxColors
            // 
            this.comboBoxColors.FormattingEnabled = true;
            this.comboBoxColors.Items.AddRange(new object[] {
            "Dark",
            "Default",
            "Green",
            "Blue",
            "Red"});
            this.comboBoxColors.Location = new System.Drawing.Point(195, 256);
            this.comboBoxColors.Name = "comboBoxColors";
            this.comboBoxColors.Size = new System.Drawing.Size(273, 21);
            this.comboBoxColors.TabIndex = 3;
            this.comboBoxColors.SelectedIndexChanged += new System.EventHandler(this.comboBoxColors_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(115, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Color Scheme";
            // 
            // radioButtonThemes
            // 
            this.radioButtonThemes.AutoSize = true;
            this.radioButtonThemes.Location = new System.Drawing.Point(71, 260);
            this.radioButtonThemes.Name = "radioButtonThemes";
            this.radioButtonThemes.Size = new System.Drawing.Size(69, 17);
            this.radioButtonThemes.TabIndex = 1;
            this.radioButtonThemes.TabStop = true;
            this.radioButtonThemes.Text = "Themes";
            this.radioButtonThemes.UseVisualStyleBackColor = true;
            this.radioButtonThemes.CheckedChanged += new System.EventHandler(this.radioButtonThemes_CheckedChanged);
            // 
            // radioButtonCustom
            // 
            this.radioButtonCustom.AutoSize = true;
            this.radioButtonCustom.Location = new System.Drawing.Point(63, 83);
            this.radioButtonCustom.Name = "radioButtonCustom";
            this.radioButtonCustom.Size = new System.Drawing.Size(66, 17);
            this.radioButtonCustom.TabIndex = 0;
            this.radioButtonCustom.TabStop = true;
            this.radioButtonCustom.Text = "Custom";
            this.radioButtonCustom.UseVisualStyleBackColor = true;
            this.radioButtonCustom.CheckedChanged += new System.EventHandler(this.radioButtonCustom_CheckedChanged);
            // 
            // TCDataFiltering
            // 
            this.TCDataFiltering.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TCDataFiltering.Controls.Add(this.progressBar1);
            this.TCDataFiltering.Controls.Add(this.webBrowser1);
            this.TCDataFiltering.Controls.Add(this.button3);
            this.TCDataFiltering.Controls.Add(this.button2);
            this.TCDataFiltering.Controls.Add(this.button1);
            this.TCDataFiltering.Controls.Add(this.dateTimePicker2);
            this.TCDataFiltering.Controls.Add(this.labelTo);
            this.TCDataFiltering.Controls.Add(this.labelFrom);
            this.TCDataFiltering.Controls.Add(this.dateTimePicker1);
            this.TCDataFiltering.Controls.Add(this.radioButtonSpecificDate);
            this.TCDataFiltering.Controls.Add(this.radioButtonRecent);
            this.TCDataFiltering.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.TCDataFiltering.Location = new System.Drawing.Point(4, 22);
            this.TCDataFiltering.Name = "TCDataFiltering";
            this.TCDataFiltering.Padding = new System.Windows.Forms.Padding(3);
            this.TCDataFiltering.Size = new System.Drawing.Size(954, 423);
            this.TCDataFiltering.TabIndex = 0;
            this.TCDataFiltering.Text = "Data Filtering";
            this.TCDataFiltering.UseVisualStyleBackColor = true;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(15, 94);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(929, 36);
            this.progressBar1.TabIndex = 13;
            // 
            // webBrowser1
            // 
            this.webBrowser1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.webBrowser1.Location = new System.Drawing.Point(111, 136);
            this.webBrowser1.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBrowser1.Name = "webBrowser1";
            this.webBrowser1.Size = new System.Drawing.Size(833, 272);
            this.webBrowser1.TabIndex = 12;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(6, 234);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(99, 88);
            this.button3.TabIndex = 11;
            this.button3.Text = "Pause";
            this.button3.UseVisualStyleBackColor = true;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(6, 319);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(99, 89);
            this.button2.TabIndex = 10;
            this.button2.Text = "Stop";
            this.button2.UseVisualStyleBackColor = true;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(6, 153);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(99, 86);
            this.button1.TabIndex = 9;
            this.button1.Text = "Start";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // dateTimePicker2
            // 
            this.dateTimePicker2.Location = new System.Drawing.Point(529, 60);
            this.dateTimePicker2.Name = "dateTimePicker2";
            this.dateTimePicker2.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker2.TabIndex = 8;
            // 
            // labelTo
            // 
            this.labelTo.AutoSize = true;
            this.labelTo.Location = new System.Drawing.Point(469, 65);
            this.labelTo.Name = "labelTo";
            this.labelTo.Size = new System.Drawing.Size(22, 13);
            this.labelTo.TabIndex = 7;
            this.labelTo.Text = "To";
            // 
            // labelFrom
            // 
            this.labelFrom.AutoSize = true;
            this.labelFrom.Location = new System.Drawing.Point(192, 65);
            this.labelFrom.Name = "labelFrom";
            this.labelFrom.Size = new System.Drawing.Size(34, 13);
            this.labelFrom.TabIndex = 6;
            this.labelFrom.Text = "From";
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Location = new System.Drawing.Point(251, 61);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(200, 20);
            this.dateTimePicker1.TabIndex = 5;
            // 
            // radioButtonSpecificDate
            // 
            this.radioButtonSpecificDate.AutoSize = true;
            this.radioButtonSpecificDate.Location = new System.Drawing.Point(38, 63);
            this.radioButtonSpecificDate.Name = "radioButtonSpecificDate";
            this.radioButtonSpecificDate.Size = new System.Drawing.Size(102, 17);
            this.radioButtonSpecificDate.TabIndex = 4;
            this.radioButtonSpecificDate.TabStop = true;
            this.radioButtonSpecificDate.Text = "Specific Date";
            this.radioButtonSpecificDate.UseVisualStyleBackColor = true;
            this.radioButtonSpecificDate.CheckedChanged += new System.EventHandler(this.radioButtonSpecificDate_CheckedChanged);
            // 
            // radioButtonRecent
            // 
            this.radioButtonRecent.AutoSize = true;
            this.radioButtonRecent.Location = new System.Drawing.Point(38, 19);
            this.radioButtonRecent.Name = "radioButtonRecent";
            this.radioButtonRecent.Size = new System.Drawing.Size(66, 17);
            this.radioButtonRecent.TabIndex = 3;
            this.radioButtonRecent.TabStop = true;
            this.radioButtonRecent.Text = "Recent";
            this.radioButtonRecent.UseVisualStyleBackColor = true;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // colorDialog1
            // 
            this.colorDialog1.FullOpen = true;
            this.colorDialog1.ShowHelp = true;
            this.colorDialog1.SolidColorOnly = true;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnablePreventFocusChange;
            this.ClientSize = new System.Drawing.Size(995, 550);
            this.Controls.Add(this.panel1);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.HelpButton = true;
            this.Name = "Form1";
            this.Text = "HAT (Prototype)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.TabControl1.ResumeLayout(false);
            this.TCEmailTemplate.ResumeLayout(false);
            this.TCEmailTemplate.PerformLayout();
            this.TCSettings.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.TCDataFiltering.ResumeLayout(false);
            this.TCDataFiltering.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label LabelHeading;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.TabControl TabControl1;
        private System.Windows.Forms.TabPage TCDataFiltering;
        private System.Windows.Forms.TabPage TCEmail;
        private System.Windows.Forms.TabPage TCEmailTemplate;
        private System.Windows.Forms.TabPage TCSettings;
        private System.Windows.Forms.TextBox textBoxpassword;
        private System.Windows.Forms.TextBox textBoxuserID;
        private System.Windows.Forms.Label labelPassword;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.WebBrowser webBrowser1;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.DateTimePicker dateTimePicker2;
        private System.Windows.Forms.Label labelTo;
        private System.Windows.Forms.Label labelFrom;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.RadioButton radioButtonSpecificDate;
        private System.Windows.Forms.RadioButton radioButtonRecent;
        private System.Windows.Forms.Button ButtonTabBrowseEmailTemplate;
        private System.Windows.Forms.Label LabelSelectedTemplate;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label labelTabEmailTemplates;
        private System.Windows.Forms.Button TabbuttonSave;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Button buttonLoginReset;
        private System.Windows.Forms.Button buttonLoginSave;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBoxColors;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.RadioButton radioButtonThemes;
        private System.Windows.Forms.RadioButton radioButtonCustom;
        private System.Windows.Forms.Label labelEmail;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelBackColor;
        private System.Windows.Forms.ColorDialog colorDialog1;
        private System.Windows.Forms.Button buttonSetColor;
        private System.Windows.Forms.Label labelColorAlert2;
        private System.Windows.Forms.Label labelColorAlert1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.TextBox textBoxThemeName;
        private System.Windows.Forms.Label labelThemeName;
        private System.Windows.Forms.Label labelColorAlert3;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label labelTopColor;
        private System.Windows.Forms.Label LabelErrorMessage;
    }
}

