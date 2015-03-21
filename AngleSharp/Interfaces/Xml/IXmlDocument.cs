namespace AngleSharp.Dom.Xml
{
    using AngleSharp.Attributes;
    using System;

    /// <summary>
    /// The interface represent an XML document.
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
