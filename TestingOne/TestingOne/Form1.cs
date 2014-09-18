using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Windows.Forms;

namespace TestingOne
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
           
        }

        public string getParagraphByCoOrdinate(string filepath, int pageno, int cordinate1, int coordinate2, int coordinate3, int coordinate4, bool filter)
        {
            PdfReader reader = new PdfReader(filepath);
            if (filter == false)
            {
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(cordinate1, 1000 - coordinate2, coordinate3, 1000 - coordinate4);
                RenderFilter[] renderFilter = new RenderFilter[1];
                renderFilter[0] = new RegionTextRenderFilter(rect);
                ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);
                string text = PdfTextExtractor.GetTextFromPage(reader, pageno, textExtractionStrategy);
                return text;
            }
            else
            {
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(cordinate1, coordinate2, coordinate3, coordinate4);
                RenderFilter[] renderFilter = new RenderFilter[1];
                renderFilter[0] = new RegionTextRenderFilter(rect);
                ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);
                string text = PdfTextExtractor.GetTextFromPage(reader, pageno, textExtractionStrategy);
                return text;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string path=textBox5.Text;
            int URX=int.Parse(textBox1.Text);
            int URY=int.Parse(textBox2.Text);
            int LLX=int.Parse(textBox3.Text);
            int LLY=int.Parse(textBox4.Text);
            richTextBox1.Text += getParagraphByCoOrdinate(path, 1,URX ,URY ,LLX ,LLY , true);
        }
    }
}
