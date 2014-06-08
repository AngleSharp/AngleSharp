namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Html;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Provides a number of methods for performing operations that are independent of any particular instance of the DOM.
    /// </summary>
    [DomName("DOMImplementation")]
    public sealed class DOMImplementation : IDOMImplementation
    {
        #region Features

        static readonly List<KeyValuePair<String, String>> _features;

        static DOMImplementation()
        {
            _features = new List<KeyValuePair<String, String>>();
            AddFeature("XML", "1.0");
            AddFeature("HTML", "1.0");
            AddFeature("Core", "2.0");
            AddFeature("XML", "2.0");
            AddFeature("HTML", "2.0");
            AddFeature("Views", "2.0");
            AddFeature("StyleSheets", "2.0");
            AddFeature("CSS", "2.0");
            AddFeature("CSS2", "2.0");
            //Events 2.0
            //UIEvents 2.0
            //MutationEvents 2.0
            //HTMLEvents 2.0
            //Range 2.0
            //Traversal 2.0
        }

        static void AddFeature(String feature, String version)
        {
            _features.Add(new KeyValuePair<String, String>(feature, version));
        }

        #endregion

        #region ctor

        internal DOMImplementation()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Creates an empty DocumentType node. Entity declarations and notations are not made available. Entity reference expansions and default attribute additions do not occur.
        /// </summary>
        /// <param name="qualifiedName">The qualified name of the document type to be created.</param>
        /// <param name="publicId">The external subset public identifier.</param>
        /// <param name="systemId">The external subset system identifier.</param>
        /// <returns>A new DocumentType node with Node.ownerDocument set to null.</returns>
        [DomName("createDocumentType")]
        public DocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId)
        {
            return new DocumentType { PublicId = publicId, SystemId = systemId, NodeName = qualifiedName };
        }

        /// <summary>
        /// Creates a DOM Document object of the specified type with its document element.
        /// </summary>
        /// <param name="namespaceURI">Optional: The namespace URI of the document element to create.</param>
        /// <param name="qualifiedName">Optional: The qualified name of the document element to be created.</param>
        /// <param name="doctype">Optional: The type of document to be created.</param>
        /// <returns>A new Document object with its document element.</returns>
        [DomName("createDocument")]
        public Document CreateDocument(String namespaceURI = null, String qualifiedName = null, DocumentType doctype = null)
        {
            Document doc = null;

            if (Namespaces.Html == namespaceURI)
                doc = new HTMLDocument();
            else
                doc = new Document();

            if(doctype != null)
                doc.AppendChild(doctype);

            doc.NodeName = qualifiedName ?? doc.NodeName;
            return doc;
        }

        /// <summary>
        /// This method returns a specialized object which implements the specialized APIs of the specified feature and version, as specified in DOM Features. 
        /// </summary>
        /// <param name="feature">The name of the feature requested. Note that any plus sign "+" prepended to the name of the feature will be ignored since it is not significant in the context of this method.</param>
        /// <param name="version">This is the version number of the feature to test.</param>
        /// <returns>Returns an object which implements the specialized APIs of the specified feature and version, if any, or null if there is no object which implements interfaces associated with that feature.</returns>
        [DomName("getFeature")]
        public Object GetFeature(String feature, String version)
        {
            //TODO
            return null;
        }

        /// <summary>
        /// Test if the DOM implementation implements a specific feature and version, as specified in DOM Features.
        /// </summary>
        /// <param name="feature">The name of the feature requested. Note that any plus sign "+" prepended to the name of the feature will be ignored since it is not significant in the context of this method.</param>
        /// <param name="version">This is the version number of the feature to test.</param>
        /// <returns>True if the feature is implemented in the specified version, false otherwise.</returns>
        [DomName("hasFeature")]
        public Boolean HasFeature(String feature, String version)
        {
            foreach (var _feature in _features)
                if (_feature.Key == feature && _feature.Value == version)
                    return true;

            return false;
        }

        #endregion
    }
}
