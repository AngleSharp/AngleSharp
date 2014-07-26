namespace AngleSharp.DOM.Xml
{
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
        /// Loads the given URL as an XmlDocument. Clears the current document
        /// structure and re-fills it with the contents from the provided URL.
        /// </summary>
        /// <param name="url">The URL to get the new DOM from.</param>
        /// <returns>True if the URL could be loaded successfully, otherwise false.</returns>
        [DomName("load")]
        Boolean LoadXml(String url);
    }
}
