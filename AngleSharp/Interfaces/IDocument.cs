namespace AngleSharp.DOM
{
    using AngleSharp.DOM.Collections;
    using System;

    interface IDocument : INode, IQueryElements
    {
        Element ActiveElement { get; }
        Document Append(params Node[] nodes);
        String CharacterSet { get; set; }
        Attr CreateAttribute(String name);
        Attr CreateAttributeNS(String namespaceURI, String name);
        IComment CreateComment(String data);
        IDocumentFragment CreateDocumentFragment();
        Element CreateElement(String tagName);
        Element CreateElementNS(String namespaceURI, String tagName);
        IEvent CreateEvent(String type);
        EntityReference CreateEntityReference(String name);
        IProcessingInstruction CreateProcessingInstruction(String target, String data);
        IText CreateTextNode(String data);
        IRange CreateRange();
        IWindow DefaultView { get; }
        IWindow ParentWindow { get; }
        DocumentType Doctype { get; }
        Element DocumentElement { get; }
        String DocumentUri { get; }
        Element GetElementById(String elementId);
        DOMImplementation Implementation { get; }
        String InputEncoding { get; }
        DateTime LastModified { get; }
        Location Location { get; set; }
        Readiness ReadyState { get; set; }
        event EventHandler OnReadyStateChange;
        String Referrer { get; }
        Document Prepend(params Node[] nodes);
        DOMStringList StyleSheetSets { get; }
    }
}
