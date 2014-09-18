using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using pdftron;
using pdftron.Common;
using pdftron.Filters;
using pdftron.SDF;
using pdftron.PDF;
 

namespace ItextSharp
{
    /*
     * 
     * This class contains Methods which are used to access PDF tron ..Methods are almost same  as iTextSharp .This cant be used because of accuracy problem while knowing coordinates of text.
     * 
     * 
     */ 


    class PdfTron
    {
     public string ConsoleLog = null;
     public  bool example1_basic = true;
     public bool example2_xml = true;
     public bool example3_wordlist = true;
     public bool example4_advanced = false;
     public bool example5_low_level = false;


       class LowLevelTextExtractUtils
       {
         public string ConsoleLog=null;
           // A utility method used to dump all text content in the 
           // console window.
           public void DumpAllText(ElementReader reader)
           {
               Element element;
               while ((element = reader.Next()) != null)
               {
                   switch (element.GetType())
                   {
                       case Element.Type.e_text_begin:
                           ConsoleLog+="\n--> Text Block Begin";
                           break;
                       case Element.Type.e_text_end:
                           ConsoleLog+="\n--> Text Block End";
                           break;
                       case Element.Type.e_text:
                           {
                               Rect bbox = new Rect();
                               element.GetBBox(bbox);
                               // ConsoleLog+="\n--> BBox: {0}, {1}, {2}, {3}", bbox.x1, bbox.y1, bbox.x2, bbox.y2);

                               String txt = element.GetTextString();
                               Console.Write(txt);
                               ConsoleLog+="";
                               break;
                           }
                       case Element.Type.e_text_new_line:
                           {
                               // ConsoleLog+="\n--> New Line";
                               break;
                           }
                       case Element.Type.e_form: // Process form XObjects
                           {
                               reader.FormBegin();
                               DumpAllText(reader);
                               reader.End();
                               break;
                           }
                   }
               }
           }


           private string _srch_str;

           // A helper method for ReadTextFromRect
           void RectTextSearch(ElementReader reader, Rect pos)
           {
               Element element;
               while ((element = reader.Next()) != null)
               {
                   switch (element.GetType())
                   {
                       case Element.Type.e_text:
                           {
                               Rect bbox = new Rect();
                               element.GetBBox(bbox);
                               if (bbox.IntersectRect(bbox, pos))
                               {
                                   _srch_str += element.GetTextString();
                               }
                               break;
                           }
                       case Element.Type.e_text_new_line:
                           {
                             //  _srch_str += "\n";
                               break;
                           }
                       case Element.Type.e_form: // Process form XObjects
                           {
                               reader.FormBegin();
                               RectTextSearch(reader, pos);
                               reader.End();
                               break;
                           }
                   }
               }
           }

           // A utility method used to extract all text content from
           // a given selection rectangle. The rectangle coordinates are
           // expressed in PDF user/page coordinate system.
           public string ReadTextFromRect(Page page, Rect pos, ElementReader reader)
           {
               _srch_str = "";
               reader.Begin(page);
               RectTextSearch(reader, pos);
               reader.End();
               return _srch_str;
           }

       }
        public void ReadTextFromCoordinates(string input_path,int pagenumber,int urx,int ury,int llx,int lly)
        {
             PDFDoc doc = new PDFDoc(input_path);
               doc.InitSecurityHandler();
               Page page = doc.GetPage(pagenumber);
               ElementReader reader = new ElementReader();
               PageIterator itr = doc.GetPageIterator();
                   reader.Begin(itr.Current());

                   LowLevelTextExtractUtils u = new LowLevelTextExtractUtils();
                   //u.DumpAllText(reader);
                   //ConsoleLog += u.ConsoleLog;
                   //reader.End();
   
        string field3 = u.ReadTextFromRect(page, new Rect(urx, ury, llx, lly), reader);
            ConsoleLog=field3;

            reader.Dispose();
            doc.Close();
        }
       public void ReadAdvanced(string input_path)
       {
           PDFNet.Initialize();

           try
           {
               PDFDoc doc = new PDFDoc(input_path);
               doc.InitSecurityHandler();

               Page page = doc.GetPage(1);
               if (page == null)
               {
                   ConsoleLog+="Page not found.";
                   return;
               }

               TextExtractor txt = new TextExtractor();
               txt.Begin(page);  // Read the page.
               // Other options you may want to consider...
               // txt.Begin(page, null, TextExtractor.ProcessingFlags.e_no_dup_remove);
               // txt.Begin(page, null, TextExtractor.ProcessingFlags.e_remove_hidden_text);
               // ...

               // Example 1. Get all text on the page in a single string.
               // Words will be separated with space or new line characters.
               if (example1_basic)
               {
                   // Get the word count.
                   ConsoleLog+="Word Count: {0}"+ txt.GetWordCount();

                   ConsoleLog+="\n\n- GetAsText --------------------------\n{0}"+ txt.GetAsText();
                   ConsoleLog+="-----------------------------------------------------------";
               }

               // Example 2. Get XML logical structure for the page.
               if (example2_xml)
               {
                   String text = txt.GetAsXML(TextExtractor.XMLOutputFlags.e_words_as_elements | TextExtractor.XMLOutputFlags.e_output_bbox | TextExtractor.XMLOutputFlags.e_output_style_info);
                   ConsoleLog+="\n\n- GetAsXML  --------------------------\n{0}"+ text;
                   ConsoleLog+="-----------------------------------------------------------";
               }

               // Example 3. Extract words one by one.
               if (example3_wordlist)
               {
                   TextExtractor.Word word;
                   for (TextExtractor.Line line = txt.GetFirstLine(); line.IsValid(); line = line.GetNextLine())
                   {
                       for (word = line.GetFirstWord(); word.IsValid(); word = word.GetNextWord())
                       {
                           ConsoleLog+=word.GetString();
                       }
                   }
                   ConsoleLog+="-----------------------------------------------------------";
               }

               // Example 3. A more advanced text extraction example. 
               // The output is XML structure containing paragraphs, lines, words, 
               // as well as style and positioning information.
               if (example4_advanced)
               {
                   Rect bbox;
                   int cur_flow_id = -1, cur_para_id = -1;

                   TextExtractor.Line line;
                   TextExtractor.Word word;
                   TextExtractor.Style s, line_style;

                   // For each line on the page...
                   for (line = txt.GetFirstLine(); line.IsValid(); line = line.GetNextLine())
                   {
                       if (line.GetNumWords() == 0)
                       {
                           continue;
                       }

                       if (cur_flow_id != line.GetFlowID())
                       {
                           if (cur_flow_id != -1)
                           {
                               if (cur_para_id != -1)
                               {
                                   cur_para_id = -1;
                                   ConsoleLog+="</Para>";
                               }
                               ConsoleLog+="</Flow>";
                           }
                           cur_flow_id = line.GetFlowID();
                           ConsoleLog+="<Flow id=\"{0}\">"+ cur_flow_id;
                       }

                       if (cur_para_id != line.GetParagraphID())
                       {
                           if (cur_para_id != -1)
                               ConsoleLog+="</Para>";
                           cur_para_id = line.GetParagraphID();
                           ConsoleLog+="<Para id=\"{0}\">"+ cur_para_id;
                       }

                       bbox = line.GetBBox();
                       line_style = line.GetStyle();
                       Console.Write("<Line box=\""+bbox.y1+","+bbox.y2+","+bbox.x1+","+bbox.x2+">");
                       PrintStyle(line_style);
                       ConsoleLog+="";

                       // For each word in the line...
                       for (word = line.GetFirstWord(); word.IsValid(); word = word.GetNextWord())
                       {
                           // Output the bounding box for the word.
                           bbox = word.GetBBox();
                           ConsoleLog+= "<Word box=\"{0}, {1}, {2}, {3}\""+ bbox.x1+ bbox.y1+ bbox.x2+ bbox.y2;

                           int sz = word.GetStringLen();
                           if (sz == 0) continue;

                           // If the word style is different from the parent style, output the new style.
                           s = word.GetStyle();
                           if (s != line_style)
                           {
                               PrintStyle(s);
                           }

                           ConsoleLog+=">\n"+word.GetString();
                           ConsoleLog+="</Word>";
                       }
                       ConsoleLog+="</Line>";
                   }

                   if (cur_flow_id != -1)
                   {
                       if (cur_para_id != -1)
                       {
                           cur_para_id = -1;
                           ConsoleLog+="</Para>";
                       }
                       ConsoleLog+="</Flow>";
                   }
               }

               // Note: Calling Dispose() on TextExtractor when it is not anymore in use can result in increased performance and lower memory consumption.
               txt.Dispose();
               doc.Close();
               ConsoleLog+="Done.";
           }
           catch (PDFNetException e)
           {
               ConsoleLog+=e.Message;
           }

           // Sample code showing how to use low-level text extraction APIs.
           if (example5_low_level)
           {
               try
               {
                   LowLevelTextExtractUtils util = new LowLevelTextExtractUtils();
                   PDFDoc doc = new PDFDoc(input_path);
                   doc.InitSecurityHandler();

                   // Example 1. Extract all text content from the document
                   ElementReader reader = new ElementReader();
                   PageIterator itr = doc.GetPageIterator();
                   //for (; itr.HasNext(); itr.Next()) //  Read every page
                   {
                       reader.Begin(itr.Current());

                       LowLevelTextExtractUtils u = new LowLevelTextExtractUtils();
                          u.DumpAllText(reader);
                          ConsoleLog += u.ConsoleLog;
                       reader.End();
                   }

                   // Example 2. Extract text based on the selection rectangle.
                   ConsoleLog+="----------------------------------------------------";
                   ConsoleLog+="Extract text based on the selection rectangle.";
                   ConsoleLog+="----------------------------------------------------";

                   Page first_page = doc.GetPage(1);
                   string field1 = util.ReadTextFromRect(first_page, new Rect(27, 392, 563, 534), reader);
                   string field2 = util.ReadTextFromRect(first_page, new Rect(28, 551, 106, 623), reader);
                   string field3 = util.ReadTextFromRect(first_page, new Rect(208, 550, 387, 621), reader);

                   ConsoleLog+="Field 1: {0}"+ field1;
                   ConsoleLog+="Field 2: {0}"+ field2;
                   ConsoleLog+="Field 3: {0}"+ field3;
                   // ... 

                   reader.Dispose();
                   doc.Close();
                   ConsoleLog+="Done.";
               }
               catch (PDFNetException e)
               {
                   ConsoleLog+=e.Message;
               }
           }

           PDFNet.Terminate();
    
       }
       static void PrintStyle(TextExtractor.Style s)
       {
           Console.Write(" style=\"font-family: {0}; font-size: {1}; {2}\"", s.GetFontName(), s.GetFontSize(), (s.IsSerif() ? " sans-serif; " : " "));
       }


        public void RunPdfTron(string input_path)
        { 
            PDFNet.Initialize();

            // string output_path = "../../../../TestFiles/Output/";

            try
            {
                // Open the test file
                PDFDoc doc = new PDFDoc(input_path);
                doc.InitSecurityHandler();

                PageIterator itr;
                ElementReader page_reader = new ElementReader();

                for (itr = doc.GetPageIterator(); itr.HasNext(); itr.Next())        //  Read every page
                {
                        int pageno=itr.GetPageNumber();
                    

                    page_reader.Begin(itr.Current());
                    ProcessElements(page_reader);
                    page_reader.End();
                }

                page_reader.Dispose(); // Calling Dispose() on ElementReader/Writer/Builder can result in increased performance and lower memory consumption.
                doc.Close();
            }
            catch (PDFNetException e)
            {
                ConsoleLog+=e.Message;
            }

            PDFNet.Terminate();
        }
        
        
        
        

        private void ProcessElements(ElementReader reader)
        {
            Element element;
            while ((element = reader.Next()) != null)   // Read page contents
            {
                switch (element.GetType())
                {
                    case Element.Type.e_path:                       // Process path data...
                        {
                            PathData data = element.GetPathData();
                            double[] points = data.points;
                            ConsoleLog+="Process Element.Type.e_path";
                            break;
                        }
                    case Element.Type.e_image:
                    case Element.Type.e_inline_image:
                        {
                            // Process images...
                            ConsoleLog+="Process Element.Type.e_image";
                            break;
                        }
                    case Element.Type.e_text:               // Process text strings...
                        {
                            ConsoleLog+="Process Element.Type.e_text";
                            String txt = element.GetTextString();
                            ConsoleLog +=  txt;

                            break;
                        }
                    case Element.Type.e_form:               // Process form XObjects
                        {
                            ConsoleLog+="Process Element.Type.e_form";
                            reader.FormBegin();
                            ProcessElements(reader);
                            reader.End();
                            break;
                        }
                }
            }
        }



    }
}
