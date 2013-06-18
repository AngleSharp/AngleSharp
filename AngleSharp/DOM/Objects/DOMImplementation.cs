using AngleSharp.DOM.Html;
using System;

namespace AngleSharp.DOM
{
    /// <summary>
    /// Provides a number of methods for performing operations that are independent of any particular instance of the DOM.
    /// </summary>
    [DOM("DOMImplementation")]
    public sealed class DOMImplementation : IDOMImplementation
    {
        internal DOMImplementation()
        {
        }

        //TODO

        /// <summary>
        /// Creates an empty DocumentType node. Entity declarations and notations are not made available. Entity reference expansions and default attribute additions do not occur.
        /// </summary>
        /// <param name="qualifiedName">The qualified name of the document type to be created.</param>
        /// <param name="publicId">The external subset public identifier.</param>
        /// <param name="systemId">The external subset system identifier.</param>
        /// <returns>A new DocumentType node with Node.ownerDocument set to null.</returns>
        [DOM("createDocumentType")]
        public DocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId)
        {
            return new DocumentType { PublicId = publicId, SystemId = systemId, NodeName = qualifiedName };
        }

        /// <summary>
        /// Creates a DOM Document object of the specified type with its document element.
        /// </summary>
        /// <param name="namespaceURI">The namespace URI of the document element to create or null.</param>
        /// <param name="qualifiedName">The qualified name of the document element to be created or null.</param>
        /// <param name="doctype">The type of document to be created or null.</param>
        /// <returns>A new Document object with its document element.</returns>
        [DOM("createDocument")]
        public Document CreateDocument(String namespaceURI, String qualifiedName, DocumentType doctype)
        {
            Document doc = null;

            if (Namespaces.Html == namespaceURI)
                doc = new HTMLDocument();
            else
                doc = new Document();

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
        [DOM("getFeature")]
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
        [DOM("hasFeature")]
        public Boolean HasFeature(String feature, String version)
        {
            //TODO
            return false;
        }
    }
}
