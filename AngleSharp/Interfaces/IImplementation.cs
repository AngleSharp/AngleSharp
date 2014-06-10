namespace AngleSharp.DOM
{
    using System;

    [DomName("DOMImplementation")]
    public interface IImplementation
    {
        [DomName("createDocument")]
        IXmlDocument CreateDocument(String namespaceUri, String qualifiedName, IDocumentType doctype);

        [DomName("createHTMLDocument")]
        IHtmlDocument CreateHtmlDocument(String title);

        [DomName("createDocumentType")]
        IDocumentType CreateDocumentType(String qualifiedName, String publicId, String systemId);

        [DomName("hasFeature")]
        Boolean HasFeature(String feature, String version = null);
    }
}
