namespace AngleSharp.DOM.Xml
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The XMLDocument interface represent an XML document. It inherits from
    /// the generic Document and do not add any specific methods or properties
    /// to it: nevertheless, several algorithms behave differently with the two
    /// types of documents.
    /// </summary>
    [DomName("XMLDocument")]
    public interface IXmlDocument : IDocument
    {
        /// <summary>
        /// Loads the given url as an XmlDocument. Clears the current document
        /// structure and re-fills it with the contents from the provided url.
        /// </summary>
        /// <param name="url">The url to get the new DOM from.</param>
        [DomName("load")]
        void LoadXml(String url);
    }
}
