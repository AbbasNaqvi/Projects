using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace HiringAutomationTool
{
     class CookieAwareWebClient : WebClient
    {

        private int timeout;
        public int Timeout
        {
            get
            {
                return timeout;
            }
            set
            {
                timeout = value;
            }
        }

        public CookieAwareWebClient()
            : this(new CookieContainer())
        { }
        public CookieAwareWebClient(CookieContainer c)
        {
            this.timeout = 1000000;//In Milli seconds
            this.CookieContainer = c;
        }
        public CookieContainer CookieContainer { get; set; }
        protected override WebResponse GetWebResponse(WebRequest request)
        {
            try
            {
                WebResponse RESPONSE = base.GetWebResponse(request);
                DynamoWebClient.Extension = RESPONSE.Headers["Content-Disposition"];
                return RESPONSE;
            }
            catch (Exception )
            {

                return null;
            }
        }
     
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            
            var castRequest = request as HttpWebRequest;
            request.Timeout = this.timeout;
            if (castRequest != null)
            {
                castRequest.CookieContainer = this.CookieContainer;
            }

            return request;
        }
    }
}
