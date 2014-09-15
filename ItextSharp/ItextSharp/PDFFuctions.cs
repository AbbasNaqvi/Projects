using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.IO;

namespace ItextSharp
{
    class PDFFuctions
    {

        public string getParagraphByCoOrdinate(string filepath, int pageno, int cordinate1, int coordinate2, int coordinate3, int coordinate4,bool filter)
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
            else {
                iTextSharp.text.Rectangle rect = new iTextSharp.text.Rectangle(cordinate1, coordinate2, coordinate3, coordinate4);
                RenderFilter[] renderFilter = new RenderFilter[1];
                renderFilter[0] = new RegionTextRenderFilter(rect);
                ITextExtractionStrategy textExtractionStrategy = new FilteredTextRenderListener(new LocationTextExtractionStrategy(), renderFilter);
                string text = PdfTextExtractor.GetTextFromPage(reader, pageno, textExtractionStrategy);
                return text;            
            }
        }
        public string ReadPdfFile(string fileName)
        {
            StringBuilder text = new StringBuilder();

            if (File.Exists(fileName))
            {
                PdfReader pdfReader = new PdfReader(fileName);

                for (int page = 1; page <= pdfReader.NumberOfPages; page++)
                {
                    ITextExtractionStrategy strategy = new SimpleTextExtractionStrategy();
                    string currentText = PdfTextExtractor.GetTextFromPage(pdfReader, page, strategy);

                    currentText = Encoding.UTF8.GetString(ASCIIEncoding.Convert(Encoding.Default, Encoding.UTF8, Encoding.Default.GetBytes(currentText)));
                    text.Append(currentText);
                }
                pdfReader.Close();
            }
            return text.ToString();
        }

    }
}
