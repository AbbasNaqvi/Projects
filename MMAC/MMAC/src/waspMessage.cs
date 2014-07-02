// POP3 manage class
// Karavaev Denis karavaev_denis@hotmail.com
// http://wasp.elcat.kg
///////////////////////////////////////////////////
///

using System;

namespace MMAC
{
    /// <summary>
    /// Summary description for waspMessage.
    /// </summary>
    class waspMessage
    {
        public string GetFrom(string messTop)
        {
            messTop = messTop.Remove(0, (messTop.IndexOf("\r\nFrom:") + 7));
            messTop = messTop.Remove(messTop.IndexOf('\r'), ((messTop.Length - messTop.IndexOf('\r')) - 1));
            return messTop;
        }
        public string GetDate(string messTop)
        {
            messTop = messTop.Remove(0, (messTop.IndexOf("\r\nDate:") + 7));
            messTop = messTop.Remove(messTop.IndexOf('\r'), (messTop.Length - messTop.IndexOf('\r')));
            return messTop;
        }
        public string GetMessID(string messTop)
        {
            messTop = messTop.Remove(0, (messTop.IndexOf("\r\nMessage-ID: ") + 13));
            messTop = messTop.Remove(messTop.IndexOf('\r'), (messTop.Length - messTop.IndexOf('\r')));
            return messTop;
        }
        public string GetTo(string messTop)
        {
            messTop = messTop.Remove(0, (messTop.IndexOf("\r\nTo:") + 6));
            messTop = messTop.Remove(messTop.IndexOf('\r'), (messTop.Length - messTop.IndexOf('\r')));
            return messTop;
        }
        public string GetSubject(string messTop)
        {
            messTop = messTop.Remove(0, (messTop.IndexOf("\r\nSubject:") + 10));
            messTop = messTop.Remove(messTop.IndexOf('\r'), (messTop.Length - messTop.IndexOf('\r')));
            return messTop;
        }
        public string GetBody(string AllMessage)
        {
            AllMessage = AllMessage.Remove(0, (AllMessage.IndexOf("\r\n\r\n")));
            return AllMessage;
        }
    }
}
