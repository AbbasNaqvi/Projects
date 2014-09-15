using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
    class Theme
    {
       public  List<Adress> ThemeAdress = new List<Adress>();
        private string themeName;

        public string ThemeName
        {
            get { return themeName; }
            set { themeName = value; }
        }
        private DateTime savedDate;

        public DateTime SavedDate
        {
            get { return savedDate; }
            set { savedDate = value; }
        }

    }
}
