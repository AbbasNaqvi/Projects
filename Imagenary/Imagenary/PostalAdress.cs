using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imagenary
{
    [Serializable()]
    class PostalAdress
    {
        private string pId;

        public string PID
        {
            get { return pId; }
            set
            {
                if (value.Contains("www"))
                {
                    try { pId = value.Remove(0, 41); }
                    catch (Exception)
                    {
                        throw new Exception("Property ID not identified");

                    }
                }
                else {

                    pId = value;
                }
            }
        }

        private string postalCode;

        public string PCD
        {
            get { return postalCode; }
            set { postalCode = value.Replace(",", "").ToLower(); }
        }

        public List<string> Propertykeys = new List<string>();
        private DateTime searchedProperties;

        public DateTime SearchedProperties
        {
            get { return searchedProperties; }
            set { searchedProperties = value; }
        }

        public bool AddPropertyKey(string key)
        {
            bool Result = false;
            if (Propertykeys.Contains(key))
            {
                Result = false;
            }
            else
            {
                Propertykeys.Add(key);
                Result = true;            
            }
            return Result;
        }
        private DateTime searchedLandMark;

        public DateTime SearchedLandMark
        {
            get { return searchedLandMark; }
            set { searchedLandMark = value; }
        }

    }
}
