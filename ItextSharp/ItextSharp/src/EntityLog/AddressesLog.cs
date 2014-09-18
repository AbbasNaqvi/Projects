using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
    [Obsolete]
    class AddressesLog
    {
        /*
         * This Class is not in use
         *  
         */ 

       public Dictionary<string,SinglePdfLine> adressList = new Dictionary<string,SinglePdfLine>();
        static AddressesLog log = new AddressesLog();
        static public AddressesLog Create
        {
            get { return log; }
        }
    }
}
