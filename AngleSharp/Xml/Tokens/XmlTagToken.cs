using System;
using System.Collections.Generic;

namespace AngleSharp.Xml
{
    sealed class XmlTagToken : XmlToken
    {
        List<KeyValuePair<String, String>> attributes;

        public XmlTagToken()
        {
            attributes = new List<KeyValuePair<String, String>>();
        }

        public List<KeyValuePair<String, String>> Attributes
        {
            get { return attributes; }
        }

        public String Name 
        { 
            get; 
            set; 
        }

        public Boolean IsSelfClosing
        {
            get; 
            set; 
        }

        public void AddAttribute(String name)
        {
            throw new NotImplementedException();
        }

        public void SetAttributeValue(String value)
        {
            throw new NotImplementedException();
        }
    }
}
