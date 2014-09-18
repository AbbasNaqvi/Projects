using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ItextSharp
{
    /*
     * Theme may contains many lines of adresses . 
     */ 
    [Serializable]
    class FullAdress
    {
       public  List<SinglePdfLine> AdressLines = new List<SinglePdfLine>();
      
        
        private string fullAdressID;
        public string FullAdressID
        {
            get { return fullAdressID; }
            set { fullAdressID = value; }
        }




        /*
         * Saved Date is not used in project ,it can be later used when introducing bound on FullAdress
         * 
         */ 
        private DateTime savedDate;

        public DateTime SavedDate
        {
            get { return savedDate; }
            set { savedDate = value; }
        }

    }
}
