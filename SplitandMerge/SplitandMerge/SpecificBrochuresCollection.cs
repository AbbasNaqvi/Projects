using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SplitandMerge
{
    class SpecificBrochuresCollection
    {
        public List<BrochuresCollection> sbcList = new List<BrochuresCollection>();
        static SpecificBrochuresCollection list = new SpecificBrochuresCollection();



        public static SpecificBrochuresCollection Create()
        {
            return list;
        }
        public bool Contains(string FolderName)
        {
            foreach (BrochuresCollection bc in sbcList)
            {
                if (bc.FolderName.Equals(FolderName))
                {

                    return true;
                }
            }

            return false;
        }
        public void Add(Brochures b)
        {
            foreach (BrochuresCollection bc in sbcList)
            {
                if (bc.FolderName.Equals(b.FolderName))
                {

                    bc.BListSpecific.Add(b);
                    return;
                }
            }
            BrochuresCollection collection = new BrochuresCollection();
            collection.FolderName = b.FolderName;
            collection.BListSpecific.Add(b);
            sbcList.Add(collection);
            

        }
    }
}
