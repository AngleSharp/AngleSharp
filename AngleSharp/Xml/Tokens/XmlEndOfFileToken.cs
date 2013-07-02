using System;

namespace AngleSharp.Xml
{
    /// <summary>
    /// Represents the final token to mark the EOF.
    /// </summary>
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
