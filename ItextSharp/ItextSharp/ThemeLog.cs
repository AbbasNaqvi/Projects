using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
    class ThemeLog
    {
        public Dictionary<string, Theme> ThemeList = new Dictionary<string, Theme>();
        static ThemeLog log = new ThemeLog();
        static public ThemeLog Create
        {
            get { return log; }
        }
    }
}
