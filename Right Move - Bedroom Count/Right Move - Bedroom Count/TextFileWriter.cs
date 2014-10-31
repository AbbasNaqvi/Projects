using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Right_Move___Bedroom_Count
    {
    class TextFileWriter
        {

            /*
             * usage:
             * var handler=new TextFileWriter(filename);
             * handler.WriteRecordInCSV("abc,123,321");
             * handler.Dispose();
             */

            string FileName;
        StreamWriter writer=null;
        public TextFileWriter(string filename)
        {
            SetFileName(filename);
        writer=new StreamWriter(FileName,true);
        }
        public void WriteRecordInCSV(string row)
        {
            writer.WriteLine(row);
        }
        public void Dispose()
        {
            writer.Dispose();
        }
        private void SetFileName(string filename)
        {
            FileName = filename;
        }

        }
    }
