using System;
using AngleSharp.DOM.Collections;

namespace AngleSharp.DOM
{
    interface INode
    {
        Node AppendChild(Node child);
        NamedNodeMap Attributes { get; }
        string BaseURI { get; }
        NodeList ChildNodes { get; }
        Node CloneNode(bool deep = true);
        DocumentPosition CompareDocumentPosition(Node otherNode);
        bool Contains(Node otherNode);
        Node FirstChild { get; }
        bool HasAttributes { get; }
        bool HasChildNodes { get; }
        Node InsertBefore(Node newElement, Node referenceElement);
        Node InsertChild(int index, Node child);
        bool IsDefaultNamespace(string namespaceURI);
        bool IsEqualNode(Node otherNode);
        Node LastChild { get; }
        string LocalName { get; }
        string LookupNamespaceURI(string prefix);
        string LookupPrefix(string namespaceURI);
        string NamespaceURI { get; set; }
        Node NextSibling { get; }
        string NodeName { get; }
        NodeType NodeType { get; }
        string NodeValue { get; set; }
        Node Normalize();
        Document OwnerDocument { get; }
        Element ParentElement { get; }
        Node ParentNode { get; }
        string Prefix { get; }
        Node PreviousSibling { get; }
        Node RemoveChild(Node child);
        Node ReplaceChild(Node newChild, Node oldChild);
        string TextContent { get; set; }
    }
}
