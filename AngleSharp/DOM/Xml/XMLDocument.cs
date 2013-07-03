using System;

namespace AngleSharp.DOM.Xml
{
    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    [DOM("XMLDocument")]
    public sealed class XMLDocument : Document
    {
        internal XMLDocument()
        {
            _contentType = MimeTypes.Xml;
        }
    }
}
