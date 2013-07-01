using System;

namespace AngleSharp.Xml
{
    sealed class XmlPIToken : XmlToken
    {
        public String Target 
        { 
            get; 
            set; 
        }

        public String Content
        {
            get;
            set;
        }
    }
}
