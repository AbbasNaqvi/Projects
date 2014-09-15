using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
    class AddressesLog
    {
       public Dictionary<string,Adress> adressList = new Dictionary<string,Adress>();
        static AddressesLog log = new AddressesLog();
        static public AddressesLog Create
        {
            get { return log; }
        }
    }
}
