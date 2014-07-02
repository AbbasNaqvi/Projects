using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MMAC
{
    class EmailCollection
    {
        private static EmailCollection emailCollectionObj = new EmailCollection();

        Dictionary<string, Email> EmailDictionary = new Dictionary<string, Email>();

        public static  EmailCollection Create{
        get{return emailCollectionObj;}        
        }

        
    }
}
