namespace AngleSharp.DOM
{
    using System;

    interface IDOMImplementation
    {
        Document CreateDocument(String namespaceURI, String qualifiedName, DocumentType doctype);
        DocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId);
        Object GetFeature(String feature, String version);
        Boolean HasFeature(String feature, String version);
    }
}
