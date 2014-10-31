using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using System.Text.RegularExpressions;
using System.Globalization;

namespace ItextSharp
{
    public partial class FirstPage : Form
    {
        FullAdress Tempadress;

        public FirstPage()
        {
            InitializeComponent();

            SerializationHandler handler = new SerializationHandler();
           handler.DecryptFullAddress();
            richTextBox1.Update();
            textBox1.Text = @"C:\Users\jafar.baltidynamolog\Desktop\PropertyPDF\1\1.pdf";
            FillComboBox();
        }
        private void FillComboBox()
        {
            comboBox1.Items.Clear();
            FullAddressesLog themelogobj = FullAddressesLog.Create;
            foreach (var x in themelogobj.ThemeList)
            {
                comboBox1.Items.Add(x.Key);
            }
        }
        private void SearchUsingTemplate(string text)
        {

            foreach (SinglePdfLine Tempadressobj in Tempadress.AdressLines)
            {

                float LLX = 0f, LLY = 0f, URX = 0f, URY = 0f;
                PDFFuctions fuctions = new PDFFuctions();
                if (Tempadressobj.LLX != 0f)
                {
                    LLX = Tempadressobj.LLX;
                }
                if (Tempadressobj.LLY != 0f)
                {
                    LLY = Tempadressobj.LLY;
                }
                if (Tempadressobj.URX != 0f)
                {
                    URX = Tempadressobj.URX;
                }
                if (Tempadressobj.URX != 0f)
                {
                    URY = Tempadressobj.URY;
                }
 
                string Document = fuctions.getParagraphByCoOrdinate(text, Tempadressobj.PageNo, (int)LLX, (int)LLY, (int)URX, (int)URY, true);

                richTextBox1.Text += Document + "\n";
                FindFont(Document,text);
                foreach (var x in CompareAddresses)
                {
                    //    richTextBox1.Text += x.LLX+"\n";
                    if (x.Address.Equals(Document) && x.FontSize == Tempadressobj.FontSize && x.FontFamily.Equals(Tempadressobj.FontFamily) == true)
                    {
                        richTextBox1.Text += "Adress Found  =" + x.Address + "   " + Tempadressobj.URX + "    " + x.URX + "  " + Tempadressobj.URY + "\n";
                    }
                }
            }
        
        }
        private void buttonSearch_Click(object sender, EventArgs e)
        {

            string[] filesNamess = textBox1.Text.Split(',');

            foreach (var x in filesNamess)
            {
                SearchUsingTemplate(x);
            }
         }
        List<SinglePdfLine> CompareAddresses;
        private void FindFont(string extractedText,string text)
        {
            CompareAddresses = new List<SinglePdfLine>();
            PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), text));
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
            string XmlDocument = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, S);
            try
            {
                Regex RegexObj = new Regex("(?<data><span.*?" + extractedText + ".*?span>)");


                MatchCollection collection = Regex.Matches(XmlDocument, "(?<data><span.*?" + extractedText + ".*?span>)");
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

                    adobj.Color = Regex.Match(data, "<span style=.*?color=(?<data>.*?)\">").Groups["data"].Value;
                    adobj.FontFamily = Regex.Match(data, "<span style=\"font-family:(?<data>.*?);").Groups["data"].Value;
                    string FontSize = Regex.Match(data, "<span style=.*?font-size:(?<data>.*?);.*?>").Groups["data"].Value;
                    string coordinates = Regex.Match(data, "<span style=.*?coordinates:(?<data>.*?);.*?>").Groups["data"].Value;
                    adobj.Address = Regex.Match(data, "<span.*?\">(?<data>.*?)<.span>").Groups["data"].Value;

                    string[] splits = coordinates.Split(',');
                    adobj.URX = float.Parse(splits[0]);
                    adobj.URY = float.Parse(splits[1]);
                    adobj.LLX = float.Parse(splits[2]);
                    adobj.LLY = float.Parse(splits[3]);

                    adobj.FontSize = float.Parse(FontSize);
                   
                     CompareAddresses.Add(adobj);
                }
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show("Argument Exception in Form1 Find Font  , "+ex.Message);
            }

        }
        public void Print()
        {
            richTextBox1.Text = "";
            FullAddressesLog log = FullAddressesLog.Create;
            foreach (KeyValuePair<string,FullAdress> i in log.ThemeList)
            {
                foreach (var x in i.Value.AdressLines)
                {
                    richTextBox1.Text += x.DocumentType+"\n";
                }

            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            SetTemplate newForm = new SetTemplate();
            newForm.ShowDialog();
            FillComboBox();
              Print();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializationHandler handler = new SerializationHandler();
            handler.EncryptFullAddress();
        }
       
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            FullAddressesLog log = FullAddressesLog.Create;
            Tempadress = new FullAdress();
           
            string key = comboBox1.SelectedItem.ToString();
            if (log.ThemeList.ContainsKey(key))
            {
                Tempadress=log.ThemeList[key];
            }
            else {
                MessageBox.Show("Serious Error");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            OpenFileDialog file = new OpenFileDialog();
            file.Multiselect = true;
            file.Filter = "Pdf Files (.pdf)|*.pdf";
            try
            {
                file.ShowDialog();
            }
            catch (Exception)
            {

            }
            foreach (var x in file.FileNames)
            {
                textBox1.Text += file.FileName+",";
            }
            if (file.FileNames.Count() > 0)
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1, 1);
            }
                richTextBox1.Text += textBox1.Text;
                textBox1.Update();
            
        }
    }
}
