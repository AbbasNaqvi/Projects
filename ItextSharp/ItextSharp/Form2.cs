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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

            SerializationHandler handler = new SerializationHandler();
          //  handler.DeserializeAddressesLog();
            Print();
            richTextBox1.Update();
            textBox1.Text = @"C:\Users\jafar.baltidynamolog\Desktop\PropertyPDF\1\1.pdf";
            FillComboBox();
        }
        private void FillComboBox()
        {
            comboBox1.Items.Clear();
            AddressesLog log = AddressesLog.Create;
            foreach (KeyValuePair<string, Adress> i in log.adressList)
            {
                comboBox1.Items.Add(i.Key);
            }
        }
   
        private void buttonSearch_Click(object sender, EventArgs e)
        {
           //AddressesLog log = AddressesLog.Create;
           //Adress adobj= log.adressList[comboBox1.];

            float LLX = 0f, LLY = 0f, URX = 0f, URY = 0f;
            PDFFuctions fuctions = new PDFFuctions();
           if (Tempadress.LLX != 0f)
           {
                LLX = Tempadress.LLX;
           }
           if (Tempadress.LLY != 0f)
           {
               LLY = Tempadress.LLY;
           }
           if (Tempadress.URX != 0f)
           {
               URX = Tempadress.URX;
           }
           if (Tempadress.URX != 0f)
           {
               URY = Tempadress.URY;
           }


           string Document = fuctions.getParagraphByCoOrdinate(textBox1.Text, 1, (int)LLX, (int)LLY, (int)URX, (int)URY,true);
           richTextBox1.Text = Document+"\n";
           FindFont(Document);
       //    richTextBox1.Text = "";
           foreach (var x in CompareAddresses)
           {
           //    richTextBox1.Text += x.LLX+"\n";
               if (x.Address.Equals(Document)&&x.FontSize==Tempadress.FontSize&&x.FontFamily.Equals(Tempadress.FontFamily)==true)
               {
                   richTextBox1.Text += "Adress Found  =" + x.Address + "   " + Tempadress.URX + "    " + x.URX + "  " + Tempadress.URY + "\n";
               }
           }
        }
        List<Adress> CompareAddresses;
        private void FindFont(string extractedText)
        {
            CompareAddresses = new List<Adress>();
            PdfReader reader = new PdfReader(System.IO.Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), textBox1.Text));
            TextWithFontExtractionStategy S = new TextWithFontExtractionStategy();
            string XmlDocument = iTextSharp.text.pdf.parser.PdfTextExtractor.GetTextFromPage(reader, 1, S);
            try
            {
                Regex RegexObj = new Regex("(?<data><span.*?" + extractedText + ".*?span>)");


                MatchCollection collection = Regex.Matches(XmlDocument, "(?<data><span.*?" + extractedText + ".*?span>)");
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
                MessageBox.Show("Argument Exception in Form1 Find Font");
            }

        }
        public void Print()
        {
            richTextBox1.Text = "";
            AddressesLog log = AddressesLog.Create;
            foreach (KeyValuePair<string, Adress> i in log.adressList)
            {
                richTextBox1.Text += i.Value.Address + "   -   " + i.Value.DocumentType + "   -   " + i.Value.FontFamily + "   -   " + i.Value.FontSize + "   -   " + i.Value.Bold;
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form1 newForm = new Form1();
            newForm.ShowDialog();
            FillComboBox();
            Print();
        }
        private void Form2_FormClosing(object sender, FormClosingEventArgs e)
        {
            SerializationHandler handler = new SerializationHandler();
            handler.SerializeAddress();
        }
        Adress Tempadress;
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            AddressesLog log = AddressesLog.Create;
            Tempadress = new Adress();
           
            string key = comboBox1.SelectedItem.ToString();
            if (log.adressList.ContainsKey(key))
            {
                Tempadress.DocumentType = comboBox1.SelectedText;
                Tempadress.Address = log.adressList[key].Address;
                Tempadress.Bold = log.adressList[key].Bold;
                Tempadress.Color = log.adressList[key].Color;
                Tempadress.FontFamily = log.adressList[key].FontFamily;
                Tempadress.FontSize = log.adressList[key].FontSize;
                Tempadress.Italic = log.adressList[key].Italic;
                Tempadress.LLX = log.adressList[key].LLX;
                Tempadress.LLY = log.adressList[key].LLY;
                Tempadress.URX = log.adressList[key].URX;
                Tempadress.URY = log.adressList[key].URY;
            }
            else {
                MessageBox.Show("Serious Error");
            }
        }

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
    }
}
