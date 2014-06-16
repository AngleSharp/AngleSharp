namespace AngleSharp.DOM.Xml
{
    using System;

    /// <summary>
    /// Represents a document node that contains only XML nodes.
    /// </summary>
    sealed class XmlDocument : Document, IXmlDocument
    {
        internal XmlDocument()
        {
            _contentType = MimeTypes.Xml;
        }
    }
}
