using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace SplitandMerge
{
    class Brochures
    {

        private string folderName;

        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }
        

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        private string postCode;

        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }



        private string propertyType;

        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        public void FindCode()
        {
            string pattern = "^(?<data>[A-Za-z]*)\\d*";
            try
            {
                FolderName = Regex.Match(PostCode, pattern, RegexOptions.Multiline).Groups["data"].Value;
            }
            catch (Exception)
            {
                FolderName = "NOT FOUND";            
            }        
        }
    }
}
