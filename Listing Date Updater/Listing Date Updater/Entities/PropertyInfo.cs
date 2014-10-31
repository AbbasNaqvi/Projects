using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listing_Date_Updater
{
    class PropertyInfo
    {
        private string postCode;

        public string PostCode
        {
            get { return postCode; }
            set { postCode = value; }
        }

        private string inputSource;

        public string InputSource
        {
            get { return inputSource; }
            set { inputSource = value; }
        }
        

        private string price;

        public string Price
        {
            get { return price; }
            set { price = value; }
        }

        private string soldPriceHistory;

        public string SoldPriceHistory
        {
            get { return soldPriceHistory; }
            set { soldPriceHistory = value; }
        }

        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }

        private string marketedFrom;

        public string MarketedFrom
        {
            get { return marketedFrom; }
            set { marketedFrom = value; }
        }

        private string marketedBy;

        public string MarketedBy
        {
            get { return marketedBy; }
            set { marketedBy = value; }
        }

        private string propertyType;

        public string PropertyType
        {
            get { return propertyType; }
            set { propertyType = value; }
        }

        private string propertyID;

        public string PropertyID
        {
            get { return propertyID; }
            set { propertyID = value; }
        }

        private string marketedDateEstimate;

        public string MarketedDateEstimate
        {
            get { return marketedDateEstimate; }
            set { marketedDateEstimate = value; }
        }


        private string numberOfDays;
                    
        public string NumberOfDays
        {
            get { return numberOfDays; }
            set { numberOfDays = value; }
        }


        private string successurl;

        public string SuccessUrl
        {
            get { return successurl; }
            set { successurl = value; }
        }

        private string homeUrl;

        public string HomeUrl
        {
            get { return homeUrl; }
            set { homeUrl = value; }
        }


        private string foundSource;

        public string FoundSource
        {
            get { return foundSource; }
            set { foundSource = value; }
        }
        


    }
}
