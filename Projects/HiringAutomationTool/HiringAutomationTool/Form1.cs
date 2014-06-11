using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace HiringAutomationTool
{
    public partial class Form1 : Form
    {
        Color CustomBackColor;
        Color CustomForeColor;
        Color CustomtOPColor;

        
        public Form1()
        {
            InitializeComponent();
            SerializationHandler shandler = new SerializationHandler();
            shandler.Deserialize();
            foreach (var x in ProfileLog.profileList)
                richTextBox2.Text += x.Email;

            xmlHandler handler = new xmlHandler();
            try
            {   comboBoxColors.Items.Clear();
                handler.ReadList();
                foreach (var x in ThemesCollection.themesList)
                comboBoxColors.Items.Add(x.ThemeName);
                comboBoxColors.Update();
            }
            catch (Exception)
            {
                ThemesCollection.InitializeList();
                LabelErrorMessage.Text = "File Not Found";
            }
            
            LabelErrorMessage.Text = "";
            buttonSetColor.Enabled = false;
            radioButtonThemes.Checked = true;
            radioButtonSpecificDate.Checked = true;
            progressBar1.Visible = false;
            comboBoxColors.SelectedItem="default";
            SetThemes("Default");
           // comboBoxColors.DataSource = ThemesCollection.themesList;
            ThemesCollection.themesList.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(themesList_CollectionChanged);
        }

        void themesList_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            int index=e.NewStartingIndex;
            comboBoxColors.Items.Add(ThemesCollection.themesList[index].ThemeName);
            comboBoxColors.Update();
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void radioButtonSpecificDate_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButtonSpecificDate.Checked == true)
            {
                labelFrom.Enabled = true;
                labelTo.Enabled = true;
                dateTimePicker1.Enabled = true;
                dateTimePicker2.Enabled = true;

            }
            else
            {

                labelFrom.Enabled = false;
                labelTo.Enabled = false;
                dateTimePicker1.Enabled = false;
                dateTimePicker2.Enabled = false;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            progressBar1.Visible = true;
        }

        private void ButtonTabBrowseEmailTemplate_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();

        }

        private void radioButtonThemes_CheckedChanged(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            if (radioButtonThemes.Checked == true)
            {
                comboBoxColors.Update();
                comboBoxColors.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
                buttonSetColor.Enabled = false;
                button6.Enabled = false;
                textBoxThemeName.Enabled = false;
            }
            else {
                comboBoxColors.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                buttonSetColor.Enabled = true;
                button6.Enabled = true;
                textBoxThemeName.Enabled = true;
            }
        }
        private string SetThemes(string Name,Color A, Color B,Color C)
        {
            if (A.IsNamedColor==false||B.IsNamedColor==false||C.IsNamedColor==false)
            {
                return "Unnamed color will not work";
            }

            Themes theme = new Themes();
            theme.ThemeName = Name;
            theme.TextColor = B.Name;
            theme.BackColor = A.Name;
            theme.TopColor = C.Name;
            TCDataFiltering.ForeColor = Color.FromName(theme.TextColor);
            TCDataFiltering.BackColor = Color.FromName(theme.BackColor);
            TCSettings.BackColor = Color.FromName(theme.BackColor);
            TCSettings.ForeColor = Color.FromName(theme.TextColor);
            TCEmailTemplate.BackColor = Color.FromName(theme.BackColor);
            TCEmailTemplate.ForeColor = Color.FromName(theme.TextColor);
            TCEmail.BackColor = Color.FromName(theme.BackColor);
            TCEmail.ForeColor = Color.FromName(theme.TextColor);
            panel1.BackColor = Color.FromName(theme.TopColor);
            panel1.ForeColor = Color.FromName(theme.TextColor);
            TabControl1.Update();
            var x = ThemesCollection.Contains(theme);
                ThemesCollection.themesList.Remove(x);
                ThemesCollection.themesList.Add(theme);
            
            return null;

        }
        private string SetThemes(string themeName)
        {

            Themes theme=ThemesCollection.Contains(themeName);
            if (theme == null)
            {
                return "This theme is not specified";
            }


            TCDataFiltering.ForeColor = Color.FromName(theme.TextColor);
            TCDataFiltering.BackColor = Color.FromName(theme.BackColor);
            TCSettings.BackColor = Color.FromName(theme.BackColor);
            TCSettings.ForeColor = Color.FromName(theme.TextColor);
            TCEmailTemplate.BackColor = Color.FromName(theme.BackColor);
            TCEmailTemplate.ForeColor = Color.FromName(theme.TextColor);
            TCEmail.BackColor = Color.FromName(theme.BackColor);
            TCEmail.ForeColor = Color.FromName(theme.TextColor);
            panel1.BackColor = Color.FromName(theme.TopColor);
            panel1.ForeColor = Color.FromName(theme.TextColor);

            TabControl1.Update();
            return "Theme is updated";

        }
        private void comboBoxColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            string message=SetThemes(comboBoxColors.SelectedItem.ToString());
            LabelErrorMessage.Text = message;
        }

        private void buttonLoginReset_Click(object sender, EventArgs e)
        {
            textBoxuserID.Text = "";
            textBoxpassword.Text = "";
        }

        private void button4_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            colorDialog1.ShowDialog();
            CustomBackColor = colorDialog1.Color;
            labelColorAlert1.Text = colorDialog1.Color.Name;
            if (labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert3.Text.Equals("NotChoosen") == false)
            {
                buttonSetColor.Enabled = true;
            }
            else {
                buttonSetColor.Enabled = false;            
            }
        }

        private void buttonSetColor_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            string ThemeName = textBoxThemeName.Text.Trim();
            if (ThemeName.Length > 0)
            {
                string message;
                if ((message = SetThemes(ThemeName, CustomBackColor, CustomForeColor, CustomtOPColor)) != null)
                {
                    LabelErrorMessage.Text = message.ToString();
                }
                else {
                    LabelErrorMessage.Text ="";
                
                }
            }
            else {

                LabelErrorMessage.Text = "Please Enter the Theme Name";
            }
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            colorDialog1.ShowDialog();
            CustomForeColor = colorDialog1.Color;
            labelColorAlert2.Text = colorDialog1.Color.Name;
            if (labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert3.Text.Equals("NotChoosen") == false)
            {

                buttonSetColor.Enabled = true;
            }
            else
            {
                buttonSetColor.Enabled = false;
            }

        }

        private void radioButtonCustom_CheckedChanged(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            if (radioButtonThemes.Checked == true)
            {
                comboBoxColors.Enabled = true;
                button4.Enabled = false;
                button5.Enabled = false;
                buttonSetColor.Enabled = false;
                button6.Enabled = false;
                textBoxThemeName.Enabled = false;

            }
            else
            {
                comboBoxColors.Enabled = false;
                button4.Enabled = true;
                button5.Enabled = true;
                buttonSetColor.Enabled = true;
                button6.Enabled = true;
                textBoxThemeName.Enabled = true;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LabelErrorMessage.Text = "";
            colorDialog1.ShowDialog();
            CustomtOPColor = colorDialog1.Color;
            labelColorAlert3.Text = colorDialog1.Color.Name;
            if (labelColorAlert2.Text.Equals("NotChoosen") == false && labelColorAlert2.Text.Equals("NotChoosen") == false&& labelColorAlert3.Text.Equals("NotChoosen")==false)
            {

                buttonSetColor.Enabled = true;
            }
            else
            {
                buttonSetColor.Enabled = false;
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            xmlHandler handler = new xmlHandler();
            handler.WriteList();
            SerializationHandler shandler = new SerializationHandler();
            shandler.Serialize();


        }

        private void buttonLoginSave_Click(object sender, EventArgs e)
        {
            Profile p = new Profile();
            p.Email = textBoxuserID.Text;
            p.Password = textBoxpassword.Text;
            ProfileLog.Add(p);
        }
    }
}
