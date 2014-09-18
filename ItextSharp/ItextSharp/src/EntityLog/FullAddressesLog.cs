using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
    class FullAddressesLog
    {
        public Dictionary<string, FullAdress> ThemeList = new Dictionary<string, FullAdress>();
        static FullAddressesLog log = new FullAddressesLog();
        static public FullAddressesLog Create
        {
            get { return log; }
        }
    }
}
