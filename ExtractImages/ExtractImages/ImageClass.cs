using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using AForge.Imaging;
using ImageMatchingLibrary;

namespace ExtractImages
{
   public class ImageClass
    {
        public static List<System.Drawing.Image> ExtractImagesFromPDF(String PDFSourcePath)
        {
            List<System.Drawing.Image> ImgList = new List<System.Drawing.Image>();

            iTextSharp.text.pdf.RandomAccessFileOrArray RAFObj = null;
            iTextSharp.text.pdf.PdfReader PDFReaderObj = null;
            iTextSharp.text.pdf.PdfObject PDFObj = null;
            iTextSharp.text.pdf.PdfStream PDFStremObj = null;

            try
            {
                RAFObj = new iTextSharp.text.pdf.RandomAccessFileOrArray(PDFSourcePath);
                PDFReaderObj = new iTextSharp.text.pdf.PdfReader(RAFObj, null);

                for (int i = 0; i <= PDFReaderObj.XrefSize - 1; i++)
                {
                    PDFObj = PDFReaderObj.GetPdfObject(i);

                    if ((PDFObj != null) && PDFObj.IsStream())
                    {
                        PDFStremObj = (iTextSharp.text.pdf.PdfStream)PDFObj;
                        iTextSharp.text.pdf.PdfObject subtype = PDFStremObj.Get(iTextSharp.text.pdf.PdfName.SUBTYPE);

                        if ((subtype != null) && subtype.ToString() == iTextSharp.text.pdf.PdfName.IMAGE.ToString())
                        {
                            try
                            {
                                iTextSharp.text.pdf.parser.PdfImageObject PdfImageObj =
                         new iTextSharp.text.pdf.parser.PdfImageObject((iTextSharp.text.pdf.PRStream)PDFStremObj);

                                System.Drawing.Image ImgPDF = PdfImageObj.GetDrawingImage();
                                ImgList.Add(ImgPDF);
                            }
                            catch (Exception)
                            {

                            }
                        }
                    }
                }
                PDFReaderObj.Close();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return ImgList;
        }
        public static double FindComparisonRatioBetweenImages(System.Drawing.Image one, System.Drawing.Image template)
        {
            Bitmap Bone = new Bitmap(one);
            Bitmap Btemplate = new Bitmap(template);


            Bitmap cloneTemplate = new Bitmap(Btemplate.Width, Btemplate.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics gr = Graphics.FromImage(cloneTemplate))
            {
                gr.DrawImage(Btemplate, new Rectangle(0, 0, cloneTemplate.Width, cloneTemplate.Height));
            }

            Bitmap clone = new Bitmap(Btemplate.Width, Btemplate.Height, System.Drawing.Imaging.PixelFormat.Format24bppRgb);
            using (Graphics gr = Graphics.FromImage(clone))
            {
                gr.DrawImage(Bone, new Rectangle(0, 0, cloneTemplate.Width, cloneTemplate.Height));
            }


            ExhaustiveTemplateMatching tm = new ExhaustiveTemplateMatching(0);
            // compare two images'
            TemplateMatch[] matchings = null;
            matchings = tm.ProcessImage(clone, cloneTemplate);

            // check similarity level

            return matchings[0].Similarity;
        }
        public static bool IsImageExistInPDF(string path, System.Drawing.Image img,out string log)
        {
            bool Result = false;
            double threshold =0.95;
            string FileName = System.IO.Path.GetFileName(path).Replace(".pdf", "");
            log = "";
            // log = "Log File\n\nPATH=" + path+"\n\nThreshold="+threshold;
            List<System.Drawing.Image> ListImage = ExtractImagesFromPDF(path);
            int imageCount=0;
            foreach (var i in ListImage)
            {
                imageCount++;
                double value = 0;
                try
                {
                    value = FindComparisonRatioBetweenImages(i, img);
                    log += "\n\nName= Image" + imageCount   +" , Threshold= "+value;
                    
                    if (value > threshold)
                    {
                        Result = true;
                        break;
                    }
                }
                catch (Exception ez)
                {
                    log += "\n\nResult=Exception";
                    log += "\n\nException=" + ez.Message;
                    throw new Exception(ez.Message);
                }
            }
            string LogFilepath = System.IO.Directory.GetCurrentDirectory() + "\\Log\\" + FileName + "\\Image.txt";
            System.IO.File.WriteAllText(LogFilepath, log, Encoding.UTF32);
            return Result;
        }
        public static void WriteImageFile(string path)
        {
            try
            {
                string FileName = System.IO.Path.GetFileName(path).Replace(".pdf","");
                // Get a List of Image
                List<System.Drawing.Image> ListImage = ExtractImagesFromPDF(path);
                
                for (int i = 0; i < ListImage.Count; i++)
                {
                    try
                    {
                        System.IO.Directory.CreateDirectory(System.IO.Directory.GetCurrentDirectory() + "\\Log\\" + FileName);
                        string img = System.IO.Directory.GetCurrentDirectory()+"\\Log\\"+FileName+"\\Image" + i + ".jpeg";
                        // Write Image File
                        ListImage[i].Save(img, System.Drawing.Imaging.ImageFormat.Jpeg);
                    }
                    catch (Exception e)
                    {
                        throw new Exception(e.Message);
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
