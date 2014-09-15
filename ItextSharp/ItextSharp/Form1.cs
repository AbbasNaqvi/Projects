using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;
using System.Text.RegularExpressions;

namespace ItextSharp
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }


        int activeRadio;


        private void Form1_Load(object sender, EventArgs e)
        {
            panel3.Enabled = false;
            textBox1.Text = @"C:\Users\jafar.baltidynamolog\Desktop\PropertyPDF\1\1.pdf";
            textBox2.Text = "0";
            textBox3.Text = "220";
            textBox4.Text = "350";
            textBox5.Text = "200";
            textBoxDocumentType.Text = "PS";
            radioButton2.Checked = true;
        }




        #region Events
        private void button4_Click(object sender, EventArgs e)
        {
            ThemeLog log = ThemeLog.Create;
            Theme theme = new Theme();

            Tempadress = new ItextSharp.Adress();
            var x = listView1.Items;
                foreach (ListViewItem i in x)
                {
                    Tempadress.Address = i.SubItems[0].Text;
                    Tempadress.FontSize = float.Parse(i.SubItems[1].Text);
                    Tempadress.FontFamily = i.SubItems[2].Text;
                    Tempadress.Bold = bool.Parse(i.SubItems[3].Text);
                    Tempadress.Italic = bool.Parse(i.SubItems[4].Text);
                    Tempadress.Color = i.SubItems[5].Text;
                    Tempadress.DocumentType = textBoxDocumentType.Text;
                  
                    if (activeRadio==1)
                    {
                        Tempadress.LLX = Int32.Parse(textBox2.Text);
                        Tempadress.LLY = Int32.Parse(textBox3.Text);
                        Tempadress.URX = Int32.Parse(textBox4.Text);
                        Tempadress.URY = Int32.Parse(textBox5.Text);
                    }
                    else if(activeRadio==2)
                    {
                        try
                        {
                            Tempadress.URX = (float)numericUpDown2.Value;

                        }
                        catch (ArgumentException)
                        {

                        }
                        try
                        {
                            Tempadress.URY = (float)numericUpDown3.Value;
                        }
                        catch (ArgumentException)
                        {

                        }
                        try
                        {
                            Tempadress.LLX = (float)numericUpDown4.Value;
                        }
                        catch (ArgumentException)
                        {

                        }
                        try
                        {
                            Tempadress.LLY = (float)numericUpDown5.Value;
                        }
                        catch (ArgumentException)
                        {

                        }

                    }
                    theme.ThemeAdress.Add(Tempadress);
                    log.ThemeList.Add(theme.ThemeName, theme);
                //AddressesLog Adlog = AddressesLog.Create;
                //Adlog.adressList.Add(Tempadress.DocumentType, Tempadress);
                 
       
            }
                MessageBox.Show("Saved Successfully");
        }
        private void button5_Click(object sender, EventArgs e)
        {
            //string Document = getParagraphByCoOrdinate(textBox1.Text, 1, Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox5.Text));
            //richTextBox1.Text = Document;

            string FontName = "ABCDEE+Cambria";
            string Address = "House # 1711/A Street # 33/4 Allama Iqbal Colony Rawalpindi";
            //<span style="font-family:ABCDEE+Cambria;font-size:8.948792">House # 1711/A Street # 33/4 Allama Iqbal Colony Rawalpindi </span>
        }

        //Open
        private void button3_Click(object sender, EventArgs e)
        {
            //webBrowser1.Navigate(textBox1.Text);
            //webBrowser1.ShowPageSetupDialog();
        }
        private void button2_Click_1(object sender, EventArgs e)
        {
            string ExtractedText = richTextBox1.Text;
            FindFont(ExtractedText);

        }
        [STAThread]
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            file.Filter = "Pdf Files (.pdf)|*.pdf";
            try
            {
                file.ShowDialog();
            }
            catch (Exception)
            {

            }
            textBox1.Text = file.FileName;


        }
        bool IsOnlyText = false;
        private void button2_Click(object sender, EventArgs e)
        {

            if (activeRadio == 1)
            {
                PDFFuctions functions = new PDFFuctions();
                string Document = functions.getParagraphByCoOrdinate(textBox1.Text, Int32.Parse(numericUpDown1.Value.ToString()), Int32.Parse(textBox2.Text), Int32.Parse(textBox3.Text), Int32.Parse(textBox4.Text), Int32.Parse(textBox5.Text), false);
                richTextBox1.Text = Document;
            }
            else if (activeRadio == 2)
            {


                panel3.Enabled = true;
                ///  IsOnlyText = true;
                FindFont(textBoxText.Text);
            }
        }

        #endregion
        private void FindFont(string extractedText)
        {
            string[] ExtractedTexts = extractedText.Split('\n');

            Numeric_KeyPad_Lock = true;
            PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBox1.Text));
            //      richTextBox1.Text = "CheckPoint" + 1;
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
            //      richTextBox1.Text = "CheckPoint" + 2;
            string XmlDocument = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, Int32.Parse(numericUpDown1.Value.ToString()), S);
            //    richTextBox1.Text = "CheckPoint" + 3;



            foreach (var extractedtext in ExtractedTexts)
            {
                try
                {
                    //        richTextBox1.Text = "CheckPoint" + 4;
                    string Regexe = "(?<data><span.*?" + extractedtext + ".*?span>)";
                    //        Regex RegexObj = new Regex("(?<data><span.*?"   +Regexe+ ".*?span>)");
                    //      Regex RegexObj = new Regex(Regexe);
                    MatchCollection collection = Regex.Matches(XmlDocument, "(?<data><span.*?" + extractedtext.Replace(" ", @"\s") + ".*?span>)");
                    foreach (Match x in collection)
                    {
                        Adress adobj = new ItextSharp.Adress();
                        string data = x.Groups["data"].Value;

                        if (data.Contains("NOTBOLD"))
                        {
                            adobj.Bold = false;
                            data = data.Replace("NOTBOLD", "");
                        }
                        else
                        {
                            adobj.Bold = true;
                            data = data.Replace("BOLD", "");
                        }
                        adobj.Color = Regex.Match(data, "<span style=.*?color=(?<data>.*?);.*?>").Groups["data"].Value;
                        adobj.FontFamily = Regex.Match(data, "<span style=\"font-family:(?<data>.*?);.*?").Groups["data"].Value;
                        string FontSize = Regex.Match(data, "<span style=.*?font-size:(?<data>.*?);.*?>").Groups["data"].Value;
                        string coordinates = Regex.Match(data, "<span style=.*?coordinates:(?<data>.*?);.*?>").Groups["data"].Value;
                        if (activeRadio == 2)
                        {
                            string[] splits = coordinates.Split(',');
                            adobj.URX = float.Parse(splits[0]);
                            adobj.URY = float.Parse(splits[1]);
                            adobj.LLX = float.Parse(splits[2]);
                            adobj.LLY = float.Parse(splits[3]);

                        }
                        adobj.FontSize = float.Parse(FontSize);
                        //                    ListViewItem item = new ListViewItem(new[]{extractedText,adobj.FontFamily,adobj.FontSize});
                        richTextBox1.Text = "CheckPoint" + 5;
                        ListViewItem item = new ListViewItem();
                        item.SubItems.Add("Address");
                        item.SubItems.Add("Size");
                        item.SubItems.Add("Font");
                        item.SubItems.Add("Bold");
                        item.SubItems.Add("Italic");
                        item.SubItems.Add("Color");
                        item.SubItems.Add("URX");
                        item.SubItems.Add("URY");
                        item.SubItems.Add("LLX");
                        item.SubItems.Add("LLY");
                        item.SubItems[0].Text = extractedText;
                        item.SubItems[2].Text = adobj.FontFamily;
                        item.SubItems[1].Text = adobj.FontSize.ToString();
                        item.SubItems[3].Text = adobj.Bold.ToString();
                        item.SubItems[4].Text = adobj.Italic.ToString();
                        item.SubItems[5].Text = adobj.Color.ToString();


                        richTextBox1.Text = "CheckPoint" + 6;
                        if (activeRadio == 1)
                        {
                            adobj.LLX = float.Parse(textBox2.Text);
                            adobj.LLY = float.Parse(textBox3.Text);
                            adobj.URX = float.Parse(textBox4.Text);
                            adobj.URY = float.Parse(textBox5.Text);

                        }
                        else if (activeRadio == 2)
                        {
                            Tempadress = new Adress();
                            try
                            {
                                numericUpDown2.Value = (int)adobj.URX;
                                item.SubItems[6].Text = adobj.URX.ToString();
                                Tempadress.URX = adobj.URX;
                            }
                            catch (ArgumentException)
                            {

                            }
                            try
                            {
                                numericUpDown3.Value = (int)adobj.URY;
                                Tempadress.URY = adobj.URY;
                                item.SubItems[7].Text = adobj.URY.ToString();
                            }
                            catch (ArgumentException)
                            {

                            }
                            try
                            {
                                numericUpDown4.Value = (int)adobj.LLX;
                                Tempadress.LLX = adobj.LLX;
                                item.SubItems[8].Text = adobj.LLX.ToString();
                            }
                            catch (ArgumentException)
                            {

                            }
                            try
                            {
                                numericUpDown5.Value = (int)adobj.LLY;
                                Tempadress.LLY = adobj.LLY;
                                item.SubItems[9].Text = adobj.LLY.ToString();
                            }
                            catch (ArgumentException)
                            {

                            }

                        }
                        adobj.DocumentType = textBoxDocumentType.Text;
                        richTextBox1.Text = "CheckPoint" + 7;

                        listView1.Items.Add(item);
                        listView1.Update();
                        richTextBox1.Text = XmlDocument;
                        Numeric_KeyPad_Lock = false;
                        radioButton3.Checked = true;

                    }
                    //                richTextBox1.Text += XmlDocument;
                }
                catch (ArgumentException ex)
                {
                    MessageBox.Show("Argument Exception in Regex");
                    // Syntax error in the regular expression
                }
            }

        }
        private string ReadParagraph()
        {
            PdfReader reader = new PdfReader(textBox1.Text);
            AcroFields form = reader.AcroFields;
            var fields = form.Fields;
            foreach (var x in fields)
            {
                richTextBox1.Text += x.Key + "  , " + x.Value;
            }
            return null;
        }
        private string FindAdress(Adress ad)
        {
            string ResultAddress = null;
            PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBox1.Text));
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
            string XmlDocument = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, Int32.Parse(numericUpDown1.Value.ToString()), S);
            return ResultAddress;
        }
        private bool IsFontFound(string address, string fontName)
        {
            bool Result = false;
            PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBox1.Text));
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
            string r = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, S);
            string Target = @"<span style=""font-family:" + fontName.Replace(" ", "+") + @""">" + address + "</span>";
            if (r.Contains(Target))
            {
                Result = true;
            }
            else
            {
                Result = false;
            }
            return Result;
        }
        Adress Tempadress;

        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            panel3.Enabled = true;
            Tempadress = new ItextSharp.Adress();
            var x = listView1.SelectedItems;
            foreach (ListViewItem i in x)
            {
                Tempadress.Address = i.SubItems[0].Text;
                Tempadress.FontSize = float.Parse(i.SubItems[1].Text);
                Tempadress.FontFamily = i.SubItems[2].Text;
                Tempadress.Bold = bool.Parse(i.SubItems[3].Text);
                Tempadress.Italic = bool.Parse(i.SubItems[4].Text);
                Tempadress.Color = i.SubItems[5].Text;
                if (activeRadio == 1)
                {
                    Tempadress.LLX = Int32.Parse(textBox2.Text);
                    Tempadress.LLY = Int32.Parse(textBox3.Text);
                    Tempadress.URX = Int32.Parse(textBox4.Text);
                    Tempadress.URY = Int32.Parse(textBox5.Text);
                }
                else if (activeRadio == 2)
                {
                    try
                    {
                        Tempadress.URX = float.Parse(i.SubItems[6].Text);
                    }
                    catch (ArgumentException)
                    { }
                    try
                    {
                        Tempadress.URY = float.Parse(i.SubItems[7].Text);
                    }
                    catch (ArgumentException)
                    { }
                    try
                    {
                        Tempadress.LLX = float.Parse(i.SubItems[8].Text);
                    }
                    catch (ArgumentException)
                    { }
                    try
                    {
                        Tempadress.LLY = float.Parse(i.SubItems[9].Text);
                    }
                    catch (ArgumentException)
                    { }

                }
                Tempadress.DocumentType = textBoxDocumentType.Text;

                Numeric_KeyPad_Lock = true;
                numericUpDown2.Value = (int)Tempadress.URX;
                numericUpDown3.Value = (int)Tempadress.URY;
                numericUpDown4.Value = (int)Tempadress.LLX;
                numericUpDown5.Value = (int)Tempadress.LLY;
                Numeric_KeyPad_Lock = false;
                listView1.Select();
                listView1.HideSelection = false;
            }
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                activeRadio = 1;
                textBox2.Enabled = true;
                textBox3.Enabled = true;
                textBox4.Enabled = true;
                textBox5.Enabled = true;
                label2.Enabled = true;
                label3.Enabled = true;
                label4.Enabled = true;
                label5.Enabled = true;
                textBoxText.Enabled = false;
                textBoxAgent.Enabled = false;
                label8.Enabled = false;
                label9.Enabled = false;
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                activeRadio = 2;
                textBox2.Enabled = false;
                textBox3.Enabled = false;
                textBox4.Enabled = false;
                textBox5.Enabled = false;
                label2.Enabled = false;
                label3.Enabled = false;
                label4.Enabled = false;
                label5.Enabled = false;
                textBoxText.Enabled = true;
                textBoxAgent.Enabled = true;
                label8.Enabled = true;
                label9.Enabled = true;

            }
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {

            if (radioButton3.Checked == true)
            {
                var x = listView1.SelectedItems;
                foreach (ListViewItem i in x)
                {

                    Numeric_KeyPad_Lock = true;
                    //numericUpDown3.Value = int.Parse(i.SubItems["URX"].Text.ToString());
                    //numericUpDown5.Value = int.Parse(i.SubItems["LLX"].Text.ToString());
                    Numeric_KeyPad_Lock = false;

                }

                numericUpDown2.Enabled = true;
                numericUpDown4.Enabled = true;
                label10.Enabled = true;
                label12.Enabled = true;

                numericUpDown3.Enabled = false;
                numericUpDown5.Enabled = false;
                label11.Enabled = false;
                label13.Enabled = false;
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            Numeric_KeyPad_Lock = true;
            var x = listView1.SelectedItems;
            if (radioButton4.Checked == true && x != null)
            {

                foreach (ListViewItem i in x)
                {

                    //numericUpDown2.Value = int.Parse(i.SubItems["URY"].Text.ToString());
                    //numericUpDown4.Value = int.Parse(i.SubItems["LLY"].Text.ToString());

                    Numeric_KeyPad_Lock = false;

                }
                Numeric_KeyPad_Lock = true;
                Numeric_KeyPad_Lock = false;
                numericUpDown2.Enabled = false;
                numericUpDown4.Enabled = false;
                label10.Enabled = false;
                label12.Enabled = false;
                numericUpDown3.Enabled = true;
                numericUpDown5.Enabled = true;
                label11.Enabled = true;
                label13.Enabled = true;
            }
        }
        bool Numeric_KeyPad_Lock = false;
        private void numericUpDown2_ValueChanged(object sender, EventArgs e)
        {
            if (Numeric_KeyPad_Lock == false)
                Tempadress.URX = (float)numericUpDown2.Value;
        }

        private void numericUpDown3_ValueChanged(object sender, EventArgs e)
        {
            if (Numeric_KeyPad_Lock == false)
                Tempadress.URY = (float)numericUpDown3.Value;
        }

        private void numericUpDown4_ValueChanged(object sender, EventArgs e)
        {
            if (Numeric_KeyPad_Lock == false)
                Tempadress.LLX = (float)numericUpDown4.Value;
        }

        private void numericUpDown5_ValueChanged(object sender, EventArgs e)
        {
            if (Numeric_KeyPad_Lock == false)
            {
                if (Tempadress != null)
                {
                    Tempadress.LLY = (float)numericUpDown5.Value;
                }
            }
        }
    }
}
