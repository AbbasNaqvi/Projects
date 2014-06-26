using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitandMerge
{
    class BrochuresCollection
    {
      //  public SortedList<string, Brochures> BList = new SortedList<string, Brochures>();
        public List<Brochures> BList = new List< Brochures>();
        
        static BrochuresCollection BCobject=new BrochuresCollection();


        public List<Brochures> BListSpecific = new List<Brochures>();
        private string folderName;

        public string FolderName
        {
            get { return folderName; }
            set { folderName = value; }
        }
        
        public static BrochuresCollection Create()
        {
            return BCobject;
        }
       

    }
}
