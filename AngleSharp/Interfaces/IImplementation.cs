namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using AngleSharp.Dom.Xml;
    using System;

    /// <summary>
    /// The DOMImplementation interface represent an object providing methods
    /// which are not dependent on any particular document. 
    /// </summary>
    [DomName("DOMImplementation")]
    public interface IImplementation
    {
        /// <summary>
        /// Creates and returns an XMLDocument.
        /// </summary>
        /// <param name="namespaceUri">
        /// The namespace URI of the document to be created, or null if the
        /// document doesn't belong to one.
        /// </param>
        /// <param name="qualifiedName">
        /// The qualified name, that is an optional prefix and colon plus the
        /// local root element name, of the document to be created.
        /// </param>
        /// <param name="doctype">
        /// DocumentType of the document to be created. It defaults to null.
        /// </param>
        /// <returns>A new document.</returns>
        [DomName("createDocument")]
        IXmlDocument CreateDocument(String namespaceUri, String qualifiedName, IDocumentType doctype);

        /// <summary>
        /// Creates and returns an HTML Document.
        /// </summary>
        /// <param name="title">
        /// The title to give the new HTML document.
        /// </param>
        /// <returns>A new document.</returns>
        [DomName("createHTMLDocument")]
        IDocument CreateHtmlDocument(String title);

        /// <summary>
        /// Creates and returns a DocumentType.
        /// </summary>
        /// <param name="qualifiedName">
        /// The qualified name, like svg:svg.
        /// </param>
        /// <param name="publicId">
        /// The PUBLIC identifier.
        /// </param>
        /// <param name="systemId">
        /// The SYSTEM identifiers.
        /// </param>
        /// <returns>A document type with the specified attributes.</returns>
        [DomName("createDocumentType")]
        IDocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId);

        /// <summary>
        /// Returns a Boolean indicating if a given feature is supported or
        /// not. This function is unreliable and kept for compatibility purpose
        /// alone: except for SVG-related queries, it always returns true.
        /// </summary>
        /// <param name="feature">The feature name.</param>
        /// <param name="version">
        /// The version of the specification defining the feature.
        /// </param>
        /// <returns></returns>
        [DomName("hasFeature")]
        Boolean HasFeature(String feature, String version = null);
    }
}
