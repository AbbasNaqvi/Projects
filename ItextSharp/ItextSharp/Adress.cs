using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
   [Serializable]
    class Adress
    {
        private string documentType;

        public string DocumentType
        {
            get { return documentType; }
            set { documentType = value; }
        }
        

        private string adress;

        public string Address
        {
            get { return adress; }
            set { adress = value; }
        }

        private float fontSize;

        public float FontSize
        {
            get { return fontSize; }
            set { fontSize = value; }
        }


        private float llx;

        public float LLX
        {
            get { return llx; }
            set { llx = value; }
        }
        private float lly;

        public float LLY
        {
            get { return lly; }
            set { lly = value; }
        }


        private float urx;

        public float URX
        {
            get { return urx; }
            set { urx = value; }
        }

        private float ury;

        public float URY
        {
            get { return ury; }
            set { ury = value; }
        }
        



        private string fontFamily;

        public string FontFamily
        {
            get { return fontFamily; }
            set { fontFamily = value; }
        }

        private bool italic;

        public bool Italic
        {
            get { return italic; }
            set { italic = value; }
        }


        private bool bold;

        public bool Bold
        {
            get { return bold; }
            set { bold = value; }
        }

        private bool underLined;

        public bool UnderLined
        {
            get { return underLined; }
            set { underLined = value; }
        }

        private string color;

        public string Color
        {
            get { return color; }
            set { color = value; }
        }
        

    }
}
