using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace RightMovePropertyExtractor
{
    class Record
    {
        /*
         * These Properties can be changes as per requirement
         */ 

        private string propertyID;

        public string PropertyID
        {
            get { return propertyID; }
            set { propertyID = value; }
        }


        private string price;

        public string Price
        {
            get { return price; }
            set
            {
                try
                {
                    string temp=Regex.Match(value, "(?<data>(\\d|,)+)").Groups["data"].Value;
                    if (string.IsNullOrEmpty(temp) == false)
                    {
                        price = temp;
                    }
                    else {
                        throw new Exception("Price can not be Empty");
                    }
                }
                catch (Exception)
                {
                   price = value;
                }
            }
        }



        private string bedroomCount;

        public string BedroomCount
        {
            get { return bedroomCount; }
            set
            {
                try
                {
                    if (string.IsNullOrEmpty(value)==false)
                    {
                        string temp = Regex.Match(value, "(?<data>\\d*)").Groups["data"].Value;
                        if (string.IsNullOrEmpty(temp) == false)
                        {
                            bedroomCount = temp;
                        }
                    }
                    else
                    {
                        bedroomCount = "-1";
                    }
                }
                catch (Exception)
                {
                    bedroomCount = value;
                }
            }
        }


        private string postalCode;

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }


        private string propertyType;

        public string PropertyType
        {
            get { return propertyType; }
            set
            {
                if (string.IsNullOrEmpty(value) == true)
                {
                    propertyType = null;
                }
                else
                {
                    string bedrooms = Regex.Replace(value, "\\d+ bedroom", "");

                    if (value.Contains("semi-detached"))
                    {
                        propertyType = "semi-detached";
                    }
                    else if (value.Contains("detached house") && value.Contains("terraced house"))
                    {
                        propertyType = "detached and terraced";
                    }
                    else if (value.Contains("detached house"))
                    {
                        propertyType = "detached";
                    }
                    else if (value.Contains("terraced"))
                    {
                        propertyType = "terraced";
                    }
                    else if (value.Contains("house for sale"))
                    {
                        propertyType = "house";
                    }
                    else if (value.Contains("property"))
                    {
                        propertyType = "property";
                    }
                    else if (value.Contains("flat"))
                    {
                        propertyType = "flat";
                    }
                    else if (value.Contains("apartment"))
                    {
                        propertyType = "apartment";
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(value))
                        {
                            propertyType = "Not Found";
                        }
                        else
                        {
                            propertyType = bedrooms;
                        }
                    }
                }
            }
        }

        public string this[string i]
        {
            get
            {
                if (i.Equals("PropertyID"))
                {
                    return propertyID;
                }
                else if (i.Equals("Price"))
                {
                    return price;
                }
                else if (i.Equals("BedroomCount"))
                {
                    return bedroomCount;
                }
                else if (i.Equals("PropertyType"))
                {
                    return propertyType;
                }
                else if (i.Equals("PostalCode"))
                {
                    return postalCode;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                if (i.Equals("PropertyID"))
                {
                    PropertyID = value;
                }
                else if (i.Equals("Price"))
                {
                    Price = value;
                }
                else if (i.Equals("BedroomCount"))
                {
                    BedroomCount = value;
                }
                else if (i.Equals("PropertyType"))
                {
                    PropertyType = value;
                }
                else if (i.Equals("PostalCode"))
                {
                    PostalCode = value;
                }
            }
        }

    }
}
