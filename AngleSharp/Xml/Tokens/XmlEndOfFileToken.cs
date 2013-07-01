using System;

namespace AngleSharp.Xml
{
    sealed class XmlEndOfFileToken : XmlToken
    {
        /// <summary>
        /// Creates a new EOF token.
        /// </summary>
        public XmlEndOfFileToken()
        {
            _type = XmlTokenType.EOF;
        }
    }
}
