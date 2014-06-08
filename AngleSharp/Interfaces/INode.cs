namespace AngleSharp.DOM
{
    using System;
    using AngleSharp.DOM.Collections;

    [DomName("Node")]
    public interface INode
    {
        Node AppendChild(Node child);
        String BaseURI { get; }
        NodeList ChildNodes { get; }
        Node CloneNode(Boolean deep = true);
        DocumentPosition CompareDocumentPosition(Node otherNode);
        Boolean Contains(Node otherNode);
        Node FirstChild { get; }
        Boolean HasChildNodes { get; }
        Node InsertBefore(Node newElement, Node referenceElement);
        Node InsertChild(Int32 index, Node child);
        Boolean IsDefaultNamespace(String namespaceURI);
        Boolean IsEqualNode(Node otherNode);
        Node LastChild { get; }
        String LookupNamespaceURI(String prefix);
        String LookupPrefix(String namespaceURI);
        Node NextSibling { get; }
        String NodeName { get; }
        NodeType NodeType { get; }
        String NodeValue { get; set; }
        Node Normalize();
        Document OwnerDocument { get; }
        Element ParentElement { get; }
        Node ParentNode { get; }
        Node PreviousSibling { get; }
        Node RemoveChild(Node child);
        Node ReplaceChild(Node newChild, Node oldChild);
        String TextContent { get; set; }
    }
}
