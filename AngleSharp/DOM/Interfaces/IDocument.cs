using AngleSharp.DOM.Collections;
using System;

namespace AngleSharp.DOM
{
    interface IDocument : INode, IQueryElements
    {
        Element ActiveElement { get; }
        Document Append(params Node[] nodes);
        string CharacterSet { get; set; }
        Attr CreateAttribute(string name);
        Attr CreateAttributeNS(string namespaceURI, string name);
        CDATASection CreateCDATASection(string data);
        Comment CreateComment(string data);
        DocumentFragment CreateDocumentFragment();
        Element CreateElement(string tagName);
        Element CreateElementNS(string namespaceURI, string tagName);
        Event CreateEvent(string type);
        EntityReference CreateEntityReference(string name);
        ProcessingInstruction CreateProcessingInstruction(string target, string data);
        TextNode CreateTextNode(string data);
        Range CreateRange();
        IWindow DefaultView { get; }
        IWindow ParentWindow { get; }
        DocumentType Doctype { get; }
        Element DocumentElement { get; }
        string DocumentURI { get; }
        Element GetElementById(string elementId);
        DOMImplementation Implementation { get; }
        string InputEncoding { get; }
        DateTime LastModified { get; }
        Location Location { get; set; }
        Readiness ReadyState { get; set; }
        event EventHandler OnReadyStateChange;
        string Referrer { get; }
        Document Prepend(params Node[] nodes);
        DOMStringList StyleSheetSets { get; }
    }
}
