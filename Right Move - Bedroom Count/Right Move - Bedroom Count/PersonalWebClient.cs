using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Right_Move___Bedroom_Count
{
    class PersonalWebClient:WebClient
    {  /// <summary>
    /// Time in milliseconds
    /// </summary>
    public int Timeout { get; set; }

    public PersonalWebClient() : this(100000) { }

    public PersonalWebClient(int timeout)
    {
        this.Timeout = timeout;
    }

    protected override WebRequest GetWebRequest(Uri address)
    {
        var request = base.GetWebRequest(address);
        if (request != null)
        {
            request.Timeout = this.Timeout;
        }
        return request;
    }
    }
}
