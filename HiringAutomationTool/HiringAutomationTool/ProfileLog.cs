using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace HiringAutomationTool
{
    static class ProfileLog
    {
        public static HashSet<Profile> profileList = new HashSet<Profile>();

        public static bool IsContainsProfile(string id)
        {
            foreach (var x in profileList)
            {
                if (x.Email.Equals(id))
                {
                    return true;
                }
            }
            return false;
        }
        public static bool Add(Profile item)
        {
            if (IsContainsProfile(item.Email))
            {
                return false;
            }
            profileList.Add(item);
            return true;
        }
        /*
         public static void Clear()
         {
             profileList.Clear();
         }

         public static Profile ContainsProfile(string id)
         {
             foreach (var x in profileList)
             {
                 if (x.Email.Equals(id))
                 {
                     return x;
                 }
             }
             return null;
         }
 
         public static bool Contains(Profile item)
         {
             return profileList.Contains(item);
         }

         /*public void CopyTo(Profile[] array, int arrayIndex)
         {
             profileList.CopyTo(array, arrayIndex);
         }
       
         public static int Count
         {
             get {
                 return profileList.Count;
             }
         }

         public static bool IsReadOnly
         {
         }

         public static bool Remove(Profile item)
         {
             return profileList.Remove(item);
         }

         public static IEnumerator<Profile> GetEnumerator()
         {
             throw new NotImplementedException();
         }

         IEnumerator IEnumerable.GetEnumerator()
         {
             throw new NotImplementedException();
         }
     */
    }
}
