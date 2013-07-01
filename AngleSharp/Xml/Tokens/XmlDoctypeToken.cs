using System;

namespace AngleSharp.Xml
{
    sealed class XmlDoctypeToken : XmlToken
    {
        public Boolean IsQuirksForced 
        {
            get; 
            set; 
        }

        public String Name
        { 
            get; 
            set;
        }

        public String SystemIdentifier 
        { 
            get; 
            set;
        }

        public String PublicIdentifier 
        { 
            get; 
            set;
        }
    }
}
