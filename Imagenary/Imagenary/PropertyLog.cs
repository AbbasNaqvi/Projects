using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Imagenary
{
    class PropertyLog
    {
        public Dictionary<string, Property> propertyList = new Dictionary<string, Property>();

        static PropertyLog log = new PropertyLog();
        static public PropertyLog Create
        {
            get { return log; }
        }

        public bool Add(Property property)
        {
            bool Result = false;
            if (propertyList.ContainsKey(property.PCD+property.URN) == false)
            {
                propertyList.Add(property.PCD+property.URN, property);
                Result = true;

            }
            else
            {
                propertyList[property.PCD + property.URN] = property;
                Result = false;
            }
            return Result;
        }


        [Obsolete]
        public bool Add(Property property, PostalAdress postaladress)
        {
            bool Result = false;
            if (propertyList.ContainsKey(property.PCD+property.URN) == false)
            {
                property.PCD = postaladress.PCD;
                property.PID = postaladress.PID;
                property.DateAdded = DateTime.Now;
                propertyList.Add(property.PCD, property);
                Result = false;
            }
            else
            {
                Result = true;
            }
            return Result;
        }
        public bool Contains(string adress)
        {
            if (propertyList.ContainsKey(adress))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
