namespace Right_Move___Bedroom_Count
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
                this.buttonRead = new System.Windows.Forms.Button();
                this.textBox1 = new System.Windows.Forms.TextBox();
                this.buttonBrowse = new System.Windows.Forms.Button();
                this.progressBar1 = new System.Windows.Forms.ProgressBar();
                this.labelNotify = new System.Windows.Forms.Label();
                this.progressBar2 = new System.Windows.Forms.ProgressBar();
                this.labelNotify2 = new System.Windows.Forms.Label();
                this.SuspendLayout();
                // 
                // buttonRead
                // 
                this.buttonRead.Location = new System.Drawing.Point(383, 34);
                this.buttonRead.Name = "buttonRead";
                this.buttonRead.Size = new System.Drawing.Size(75, 23);
                this.buttonRead.TabIndex = 0;
                this.buttonRead.Text = "Read";
                this.buttonRead.UseVisualStyleBackColor = true;
                this.buttonRead.Click += new System.EventHandler(this.buttonRead_Click);
                // 
                // textBox1
                // 
                this.textBox1.Location = new System.Drawing.Point(65, 33);
                this.textBox1.Name = "textBox1";
                this.textBox1.Size = new System.Drawing.Size(231, 20);
                this.textBox1.TabIndex = 1;
                // 
                // buttonBrowse
                // 
                this.buttonBrowse.Location = new System.Drawing.Point(302, 33);
                this.buttonBrowse.Name = "buttonBrowse";
                this.buttonBrowse.Size = new System.Drawing.Size(75, 23);
                this.buttonBrowse.TabIndex = 2;
                this.buttonBrowse.Text = "Browse";
                this.buttonBrowse.UseVisualStyleBackColor = true;
                this.buttonBrowse.Click += new System.EventHandler(this.buttonBrowse_Click);
                // 
                // progressBar1
                // 
                this.progressBar1.Location = new System.Drawing.Point(15, 114);
                this.progressBar1.Name = "progressBar1";
                this.progressBar1.Size = new System.Drawing.Size(519, 23);
                this.progressBar1.TabIndex = 5;
                // 
                // labelNotify
                // 
                this.labelNotify.AutoSize = true;
                this.labelNotify.Location = new System.Drawing.Point(12, 98);
                this.labelNotify.Name = "labelNotify";
                this.labelNotify.Size = new System.Drawing.Size(0, 13);
                this.labelNotify.TabIndex = 6;
                // 
                // progressBar2
                // 
                this.progressBar2.Location = new System.Drawing.Point(15, 168);
                this.progressBar2.Name = "progressBar2";
                this.progressBar2.Size = new System.Drawing.Size(519, 23);
                this.progressBar2.TabIndex = 7;
                // 
                // labelNotify2
                // 
                this.labelNotify2.AutoSize = true;
                this.labelNotify2.Location = new System.Drawing.Point(21, 152);
                this.labelNotify2.Name = "labelNotify2";
                this.labelNotify2.Size = new System.Drawing.Size(0, 13);
                this.labelNotify2.TabIndex = 8;
                // 
                // Form1
                // 
                this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
                this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
                this.ClientSize = new System.Drawing.Size(553, 245);
                this.Controls.Add(this.labelNotify2);
                this.Controls.Add(this.progressBar2);
                this.Controls.Add(this.labelNotify);
                this.Controls.Add(this.progressBar1);
                this.Controls.Add(this.buttonBrowse);
                this.Controls.Add(this.textBox1);
                this.Controls.Add(this.buttonRead);
                this.Name = "Form1";
                this.Text = "Right Move -Bedroom Count(1.2)";
                this.ResumeLayout(false);
                this.PerformLayout();

            }

        #endregion

        private System.Windows.Forms.Button buttonRead;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button buttonBrowse;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label labelNotify;
        private System.Windows.Forms.ProgressBar progressBar2;
        private System.Windows.Forms.Label labelNotify2;
        }
    }

