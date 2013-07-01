using System;

namespace AngleSharp.Xml
{
    sealed class XmlDeclaration : XmlToken
    {
        public String Version
        {
            get;
            set;
        }

        public String Encoding
        {
            get;
            set;
        }

        public Boolean Standalone
        {
            get;
            set;
        }
    }
}
