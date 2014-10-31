using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Listing_Date_Updater
{
    class AgentUrlDictionaryLog
    {
        public AgentUrlDictionaryLog()
        {
            AgentLog.Add("TSPC", new AgentUrlDictionary() { DirectUrl = "http://www.sspc.co.uk/40-Marine-Parade-Dundee-DD1-3BN-3-Bedroom-Flat-TSPC ", InDirectUrl = "http://www.tspc.co.uk/details.asp?id=112740 " });
            AgentLog.Add("SSPC Moray", new AgentUrlDictionary() { DirectUrl = "", InDirectUrl = "http://www.spcmoray.com/property.php?id=5975 " });
            AgentLog.Add("HSPC", new AgentUrlDictionary() { DirectUrl = "http://www.sspc.co.uk/Kenilworth-Glenlivet-Ballindalloch-AB37-9DB-4-Bedroom-Semi-Detached-Villa-HSPC ", InDirectUrl = "" });
            AgentLog.Add("ESPC", new AgentUrlDictionary() { DirectUrl = "http://www.sspc.co.uk/79-Riverside-Road-Wormit-NEWPORT-ON-TAY-DD6-8LG-4-Bedroom-Detached-House-ESPC ", InDirectUrl = "http://www.espc.com/properties/details.aspx?pid=339790 " });
            AgentLog.Add("FSPC", new AgentUrlDictionary() { DirectUrl = "", InDirectUrl = "http://www.fifespc.co.uk/property-search.cfm?Action=Results&ID=13732&T=1 " });
            AgentLog.Add("BSPC", new AgentUrlDictionary() { DirectUrl = "", InDirectUrl = "http://www.bspc.co.uk/propertydetails.asp?pRef=19110&pPostCode=EH43%206AN " });
            AgentLog.Add("GSPC", new AgentUrlDictionary() { DirectUrl = "http://www.sspc.co.uk/The-Hume-Wester-Grove-at-Bathgate-Bathgate-EH48-2GF-4-Bedroom-Detached-Villa-GSPC ", InDirectUrl = "http://www.gspc.co.uk/property/west-lothian/bathgate/the-hume-wester-grove-at-bathgate/203782/ " });
            AgentLog.Add("PSPC", new AgentUrlDictionary() { DirectUrl = "http://www.sspc.co.uk/Tormore-15-High-Street-Kinross-KY13-8AN-4-Bedroom-Detached-Villa-PSPC ", InDirectUrl = "http://www.pspc.co.uk/property/52812/#location=&beds=0&budget=0&page=1 " });
            AgentLog.Add("DGSPC", new AgentUrlDictionary() { DirectUrl = "http://www.sspc.co.uk/4-Old-Library-Row-Wanlockhead-ML12-6XL-ML12-6XL-3-Bedroom-Cottage-End-Terrace-DGSPC ", InDirectUrl = "http://www.dgspc.co.uk/4_Old_Library_Row_Wanlockhead_ML12_6XL_25573 " });

        }
        static Dictionary<string, AgentUrlDictionary> AgentLog = new Dictionary<string, AgentUrlDictionary>();

    }
}
