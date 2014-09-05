using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imagenary
{
    class PostalAdressLog
    {
      public Dictionary<string,PostalAdress> adressList = new Dictionary<string,PostalAdress>();


        static PostalAdressLog log = new PostalAdressLog();
        static public PostalAdressLog Create
        {
            get { return log; }
        }

         public PostalAdress GetObject(string p)
        {
            PostalAdress ResultObj;
            if (adressList.ContainsKey(p))
            {
                ResultObj = adressList[p];
            }
            else {
                ResultObj = new PostalAdress();
                ResultObj.PID = p;
            }
            return ResultObj;
        }


         public bool Add(PostalAdress p)
         {
             bool Result;
             if (this.adressList.ContainsKey(p.PID))
             {
             //    adressList[p.PID] = p;
                 Result = false;
             }
             else
             {
                 this.adressList.Add(p.PID, p);
                 Result = true;
             }
             return Result;
         }


    }
}
