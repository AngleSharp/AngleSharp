namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Xml;
    using AngleSharp.Extensions;
    using AngleSharp.Html;
    using AngleSharp.Network;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a number of methods for performing operations that are independent of any particular instance of the DOM.
    /// </summary>
    sealed class DomImplementation : IImplementation
    {
        #region Features

        static readonly Dictionary<String, String[]> _features = new Dictionary<String, String[]>(StringComparer.OrdinalIgnoreCase);

        static DomImplementation()
        {
            AddFeature("XML", "1.0", "2.0");
            AddFeature("HTML", "1.0", "2.0");
            AddFeature("Core", "2.0");
            AddFeature("Views", "2.0");
            AddFeature("StyleSheets", "2.0");
            AddFeature("CSS", "2.0");
            AddFeature("CSS2", "2.0");
            AddFeature("Traversal", "2.0");
            AddFeature("Events", "2.0");
            AddFeature("UIEvents", "2.0");
            AddFeature("HTMLEvents", "2.0");
            AddFeature("Range", "2.0");
            AddFeature("MutationEvents", "2.0");
        }

        static void AddFeature(String feature, params String[] versions)
        {
            _features.Add(feature, versions);
        }

        #endregion

        #region Fields

        readonly Document _owner;

        #endregion

        #region ctor

        public DomImplementation(Document owner)
        {
            _owner = owner;
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an empty DocumentType node. Entity declarations and notations are not made available.
        /// Entity reference expansions and default attribute additions do not occur.
        /// </summary>
        /// <param name="qualifiedName">The qualified name of the document type to be created.</param>
        /// <param name="publicId">The external subset public identifier.</param>
        /// <param name="systemId">The external subset system identifier.</param>
        /// <returns>A new DocumentType node with the owner document set to null.</returns>
        public IDocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId)
        {
            if (!qualifiedName.IsXmlName())
                throw new DomException(ErrorCode.InvalidCharacter);
            else if (!qualifiedName.IsQualifiedName())
                throw new DomException(ErrorCode.Namespace);

            return new DocumentType(qualifiedName) { PublicIdentifier = publicId, SystemIdentifier = systemId, Owner = _owner };
        }

        /// <summary>
        /// Creates a DOM Document object of the specified type with its document element.
        /// </summary>
        /// <param name="namespaceUri">Optional: The namespace URI of the document element to create.</param>
        /// <param name="qualifiedName">Optional: The qualified name of the document element to be created.</param>
        /// <param name="doctype">Optional: The type of document to be created.</param>
        /// <returns>A new Document object with its document element.</returns>
        public IXmlDocument CreateDocument(String namespaceUri = null, String qualifiedName = null, IDocumentType doctype = null)
        {
            var doc = new XmlDocument();

            if (doctype != null)
                doc.AppendChild(doctype);

            if (!String.IsNullOrEmpty(qualifiedName))
            {
                var element = doc.CreateElement(namespaceUri, qualifiedName);

                if (element != null)
                    doc.AppendChild(element);
            }

            doc.BaseUri = _owner.BaseUri;
            return doc;
        }

        /// <summary>
        /// Creates a DOM HTML Document object of the specified type with its document element.
        /// </summary>
        /// <param name="title">The title of the HTML document.</param>
        /// <returns>A new Document object with its document element.</returns>
        public IDocument CreateHtmlDocument(String title)
        {
            var doc = new Document();
            doc.ContentType = MimeTypes.Html;
            doc.AppendChild(new DocumentType(Tags.Html) { Owner = doc });
            doc.AppendChild(doc.CreateElement(Tags.Html));
            doc.DocumentElement.AppendChild(doc.CreateElement(Tags.Head));

            if (!String.IsNullOrEmpty(title))
            {
                var titleElement = doc.CreateElement(Tags.Title);
                titleElement.AppendChild(doc.CreateTextNode(title));
                doc.Head.AppendChild(titleElement);
            }

            doc.DocumentElement.AppendChild(doc.CreateElement(Tags.Body));
            doc.BaseUri = _owner.BaseUri;
            return doc;
        }

        /// <summary>
        /// Test if the DOM implementation implements a specific feature and version, as specified in DOM Features.
        /// </summary>
        /// <param name="feature">The name of the feature requested. Note that any plus sign "+" prepended to the name
        /// of the feature will be ignored since it is not significant in the context of this method.</param>
        /// <param name="version">This is the version number of the feature to test.</param>
        /// <returns>True if the feature is implemented in the specified version, false otherwise.</returns>
        public Boolean HasFeature(String feature, String version = null)
        {
            version = version ?? String.Empty;
            String[] versions;

            if (_features.TryGetValue(feature, out versions))
                return versions.Contains(version, StringComparison.OrdinalIgnoreCase);

            return false;
        }

        #endregion
    }
}
