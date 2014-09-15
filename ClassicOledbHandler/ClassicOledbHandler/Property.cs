using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ClassicOledbHandler
{
    [Serializable()]
    class Property
    {
#region input file variables
        public bool IsLandMarkCertified = false;
        public bool IsDownloaded = false;
        public bool IsLandMarkSearched = false;


        public bool IsSphSearched = false;
        public bool IsSphCertified = false;


        private DateTime dateAdded;

        public DateTime DateAdded
        {
            get { return dateAdded; }
            set { dateAdded = value; }
        }

        private string department;

        public string ORD
        {
            get { return department; }
            set { department = value; }
        }


        private string organizationName;

        public string ORC
        {
            get { return organizationName; }
            set { organizationName = value; }
        }

        private string subBuildingName;

        public string SBN
        {
            get { return subBuildingName; }
            set { subBuildingName = value; }
        }

        private string buildingName;

        public string BNA
        {
            get { return buildingName; }
            set { buildingName = value; }
        }

        private string poBoxNo;

        public string POB
        {
            get { return poBoxNo; }
            set { poBoxNo = value; }
        }

        private string buildingNumber;

        public string NUM
        {
            get { return buildingNumber; }
            set { buildingNumber = value; }
        }
        private string streetName;

        public string STM
        {
            get { return streetName; }
            set { streetName = value; }
        }

        private string doubleDependencyLocality;

        public string DDL
        {
            get { return doubleDependencyLocality; }
            set { doubleDependencyLocality = value; }
        }


        private string dependentLocality;

        public string DLO
        {
            get { return dependentLocality; }
            set { dependentLocality = value; }
        }

        private string postTown;

        public string PTN
        {
            get { return postTown; }
            set { postTown = value; }
        }


        private string administrativeCountry;

        public string CTA
        {
            get { return administrativeCountry; }
            set { administrativeCountry = value; }
        }

        private string formerPostalCountry;

        public string CTP
        {
            get { return formerPostalCountry; }
            set { formerPostalCountry = value; }
        }

        private string traditionalCountry;

        public string CTT
        {
            get { return traditionalCountry; }
            set { traditionalCountry = value; }
        }

        private string sortCode;

        public string SCD
        {
            get { return sortCode; }
            set { sortCode = value; }
        }

        private string userCategory;

        public string CAT
        {
            get { return userCategory; }
            set { userCategory = value; }
        }

        private string numberDeliveryPoint;

        public string NDP
        {
            get { return numberDeliveryPoint; }
            set { numberDeliveryPoint = value; }
        }

        private string deliveryPointSuffix;

        public string DPX
        {
            get { return deliveryPointSuffix; }
            set { deliveryPointSuffix = value; }
        }


        private string uniqueDeliveryPoint;

        public string URN
        {
            get { return uniqueDeliveryPoint; }
            set { uniqueDeliveryPoint = value; }
        }

        private string landMarkAdress;

        public string LMA
        {
            get { return landMarkAdress; }
            set { landMarkAdress = value; }
        }


        private string simpleAdress;

        public string SimpleAdress
        {
            get { return simpleAdress; }
            set { simpleAdress = value; }
        }

        /*
                private string propertyId;

                public string PropertyID
                {
                    get { return propertyId; }
                    set { propertyId = value; }
                }
                */
        private string urlAdress;

        public string UrlAdress
        {
            get { return urlAdress; }
            set { urlAdress = value; }
        }
        private string dependentStreet;

        public string DST
        {
            get { return dependentStreet; }
            set { dependentStreet = value; }
        }


        private string longitude;

        public string Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        private string latitude;

        public string Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        private string adress;

        public string Adress
        {
            get { return adress; }
            set { adress = value; }
        }
     /*These are SPH file Properties
         * 
         * 
         * 
         */
#endregion
        private string propertyID;

        public string PropertyID
        {
            get { return propertyID; }
            set { propertyID = value; }
        }


        private string source;

        public string Source
        {
            get { return source; }
            set { source = value; }
        }

        private string fullAdress;

        public string FullAddress
        {
            get { return fullAdress; }
            set { fullAdress = value; }
        }

        private string streetAddress;

        public string StreetAddress
        {
            get { return streetAddress; }
            set { streetAddress = value; }
        }

        private string postalCode;

        public string PostalCode
        {
            get { return postalCode; }
            set { postalCode = value; }
        }

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        private string marketedBy;

        public string MarketedBy
        {
            get { return marketedBy; }
            set { marketedBy = value; }
        }

        private string marketedDate;

        public string MarketedDate
        {
            get { return marketedDate; }
            set { marketedDate = value; }
        }
        private string successURL;

        public string SuccessURL
        {
            get { return successURL; }
            set { successURL = value; }
        }

        private string addedOn;

        public string AddedOn
        {
            get { return addedOn; }
            set { addedOn = value; }
        }
        private string lastSaleDate;

        public string LastSaleDate
        {
            get { return lastSaleDate; }
            set { lastSaleDate = value; }
        }

        private string lastSalePrice;

        public string LastSalePrice
        {
            get { return lastSalePrice; }
            set { lastSalePrice = value; }
        }
    }
}
