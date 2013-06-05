using System;

namespace AngleSharp.DOM
{
    interface IDOMImplementation
    {
        Document CreateDocument(string namespaceURI, string qualifiedName, DocumentType doctype);
        DocumentType CreateDocumentType(string qualifiedName, string publicId, string systemId);
        object GetFeature(string feature, string version);
        bool HasFeature(string feature, string version);
    }
}
