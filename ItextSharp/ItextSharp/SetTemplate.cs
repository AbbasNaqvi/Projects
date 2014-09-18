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
    public partial class SetTemplate : Form
    {

        public SetTemplate()
        {
            InitializeComponent();
        }


        int activeRadio;


        private void Form1_Load(object sender, EventArgs e)
        {
            panel3.Enabled = false;
            textBox1.Text = @"C:\Users\jafar.baltidynamolog\Desktop\PropertyPDF\1\1.pdf";
            //textBox2.Text = "0";
            //textBox3.Text = "220";
            //textBox4.Text = "350";
            //textBox5.Text = "200";
            textBoxDocumentType.Text = "PS";
            //radioButton2.Checked = true;
        }




        #region Events
        private void button4_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Kindly Find the adresses in PDF");
                return;
            }
            FullAddressesLog log = FullAddressesLog.Create;
            FullAdress theme = new FullAdress();
            var x = listView1.Items;
            foreach (ListViewItem i in x)
            {
                Tempadress = new ItextSharp.SinglePdfLine();
                Tempadress.Address = i.SubItems[0].Text;
                Tempadress.FontSize = float.Parse(i.SubItems[1].Text);
                Tempadress.FontFamily = i.SubItems[2].Text;
                Tempadress.Bold = bool.Parse(i.SubItems[3].Text);
                Tempadress.Italic = bool.Parse(i.SubItems[4].Text);
                Tempadress.Color = i.SubItems[5].Text;
                Tempadress.DocumentType = textBoxDocumentType.Text;
                Tempadress.PageNo =(int)numericUpDown1.Value;

                if (i.Selected == true)
                {
                    Tempadress.LLX = (int)numericUpDown4.Value;
                    Tempadress.LLY = (int)numericUpDown5.Value;
                    Tempadress.URX = (int)numericUpDown2.Value;
                    Tempadress.URY = (int)numericUpDown3.Value;
                }
                else
                {
                    Tempadress.URX = float.Parse(i.SubItems[6].Text);
                    Tempadress.URY = float.Parse(i.SubItems[7].Text);
                    Tempadress.LLX = float.Parse(i.SubItems[8].Text);
                    Tempadress.LLY = float.Parse(i.SubItems[9].Text);
                }

                theme.AdressLines.Add(Tempadress);
            }
            theme.FullAdressID = Tempadress.DocumentType;
            log.ThemeList.Add(theme.FullAdressID, theme);
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
        //private void button2_Click_1(object sender, EventArgs e)
        //{
        //    string ExtractedText = richTextBox1.Text;
        //    FindFont(ExtractedText);

        //}
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
            if (String.IsNullOrWhiteSpace(textBoxText.Text))
            {
                MessageBox.Show("Required Address  can not be null");
            }
 

                panel3.Enabled = true;
                FindFont(textBoxText.Text);
        }

        #endregion
        private void FindFont(string extractedText)
        {
            string[] ExtractedTexts = extractedText.Split('\n');

            Numeric_KeyPad_Lock = true;
            PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBox1.Text));
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
            string XmlDocument = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, Int32.Parse(numericUpDown1.Value.ToString()), S);
            foreach (var extractedtext in ExtractedTexts)
            {
                try
                {
                    string Regexe = "(?<data><span.*?" + extractedtext + ".*?span>)";
                    //        Regex RegexObj = new Regex("(?<data><span.*?"   +Regexe+ ".*?span>)");
                    
                   
                    MatchCollection collection = Regex.Matches(XmlDocument, "(?<data><span.*?" + extractedtext.Replace(" ", @"\s") + ".*?span>)");
                    
                    foreach (Match x in collection)
                    {
                        SinglePdfLine adobj = new ItextSharp.SinglePdfLine();
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
                      
                        string[] splits = coordinates.Split(',');
                            adobj.URX = float.Parse(splits[0]);
                            adobj.URY = float.Parse(splits[1]);
                            adobj.LLX = float.Parse(splits[2]);
                            adobj.LLY = float.Parse(splits[3]);

                        
                        adobj.FontSize = float.Parse(FontSize);
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
                        item.SubItems[0].Text = extractedtext;
                        item.SubItems[2].Text = adobj.FontFamily;
                        item.SubItems[1].Text = adobj.FontSize.ToString();
                        item.SubItems[3].Text = adobj.Bold.ToString();
                        item.SubItems[4].Text = adobj.Italic.ToString();
                        item.SubItems[5].Text = adobj.Color.ToString();

                        Tempadress = new SinglePdfLine();
                        try
                        {
                            numericUpDown2.Value = (int)adobj.URX;
                            item.SubItems[6].Text = adobj.URX.ToString();
                            Tempadress.URX = adobj.URX;
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Argument Exception while Getting numeric box ");

                        }
                        try
                        {
                            numericUpDown3.Value = (int)adobj.URY;
                            Tempadress.URY = adobj.URY;
                            item.SubItems[7].Text = adobj.URY.ToString();
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Argument Exception while Getting numeric box ");

                        }
                        try
                        {
                            numericUpDown4.Value = (int)adobj.LLX;
                            Tempadress.LLX = adobj.LLX;
                            item.SubItems[8].Text = adobj.LLX.ToString();
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Argument Exception while Getting numeric box ");
                        }
                        try
                        {
                            numericUpDown5.Value = (int)adobj.LLY;
                            Tempadress.LLY = adobj.LLY;
                            item.SubItems[9].Text = adobj.LLY.ToString();
                        }
                        catch (ArgumentException)
                        {
                            MessageBox.Show("Argument Exception while Getting numeric box ");
                        }

                        adobj.DocumentType = textBoxDocumentType.Text;
                        listView1.Items.Add(item);
                        listView1.Update();
                        Numeric_KeyPad_Lock = false;
                        radioButton3.Checked = true;
                    }
                }
                catch (ArgumentException)
                {
                    MessageBox.Show("Input String contains invalid characters");
                }
            }

        }
        //private string ReadParagraph()
        //{
        //    PdfReader reader = new PdfReader(textBox1.Text);
        //    AcroFields form = reader.AcroFields;
        //    var fields = form.Fields;
        //    foreach (var x in fields)
        //    {
        //        richTextBox1.Text += x.Key + "  , " + x.Value;
        //    }
        //    return null;
        //}
        private string FindAdress(SinglePdfLine ad)
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
        SinglePdfLine Tempadress;
        int SelectedIndex = 0;
        private void listView1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (Tempadress != null && Tempadress.URX != 0f && Tempadress.URY != 0f && Tempadress.LLX != 0f && Tempadress.LLY != 0f)
            {
                listView1.Items[SelectedIndex].SubItems[6].Text = Tempadress.URX.ToString();
                listView1.Items[SelectedIndex].SubItems[7].Text = Tempadress.URY.ToString();
                listView1.Items[SelectedIndex].SubItems[8].Text = Tempadress.LLX.ToString();
                listView1.Items[SelectedIndex].SubItems[9].Text = Tempadress.LLY.ToString();
            }


            //Only one item is allowed to select so selected index is also one
            var selectedCollection = listView1.SelectedItems;
            foreach (ListViewItem i in selectedCollection)
            {
                SelectedIndex = i.Index;
            }

            panel3.Enabled = true;
            Tempadress = new ItextSharp.SinglePdfLine();
            var x = listView1.SelectedItems;
            foreach (ListViewItem i in x)
            {
                Tempadress.Address = i.SubItems[0].Text;
                Tempadress.FontSize = float.Parse(i.SubItems[1].Text);
                Tempadress.FontFamily = i.SubItems[2].Text;
                Tempadress.Bold = bool.Parse(i.SubItems[3].Text);
                Tempadress.Italic = bool.Parse(i.SubItems[4].Text);
                Tempadress.Color = i.SubItems[5].Text;

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

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            //if (radioButton1.Checked == true)
            //{
            //    activeRadio = 1;
            //    textBox2.Enabled = true;
            //    textBox3.Enabled = true;
            //    textBox4.Enabled = true;
            //    textBox5.Enabled = true;
            //    label2.Enabled = true;
            //    label3.Enabled = true;
            //    label4.Enabled = true;
            //    label5.Enabled = true;
            //    textBoxText.Enabled = false;

            //    label8.Enabled = false;

            //}
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

        private void buttonRemove_Click(object sender, EventArgs e)
        {
            var x = listView1.SelectedItems;
            foreach (ListViewItem i in x)
            {
                listView1.Items.Remove(i);
            }
            listView1.Update();
        }

        private void button2_Click_1(object sender, EventArgs e)
        {

            try
            {
                PDFFuctions functions = new PDFFuctions();
                Tempadress.Address = Tempadress.Address.Trim();
                var items = listView1.SelectedItems;
                if (items.Count <= 0)
                {
                    MessageBox.Show("You must select one item");
                }
                foreach (ListViewItem x in items)
                {
                    string text = functions.getParagraphByCoOrdinate(textBox1.Text,(int)numericUpDown1.Value, (int)numericUpDown4.Value, (int)numericUpDown5.Value, (int)numericUpDown2.Value, (int)numericUpDown3.Value, true);
                    MessageBox.Show("Retreived Value is" + text);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Kindly Find the adress in PDF first");
            }
        }




    }
}
