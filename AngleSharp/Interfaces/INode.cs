namespace AngleSharp.DOM
{
    using System;
    using AngleSharp.DOM.Collections;

    /// <summary>
    /// A Node is an interface from which a number of DOM types inherit, 
    /// and allows these various types to be treated similarly.
    /// </summary>
    [DomName("Node")]
    public interface INode
    {
        [DomName("baseURI")]
        String BaseURI { get; }

        [DomName("nodeName")]
        String NodeName { get; }

        [DomName("childNodes")]
        NodeList ChildNodes { get; }

        [DomName("cloneNode")]
        Node CloneNode(Boolean deep = true);

        [DomName("isEqualNode")]
        Boolean IsEqualNode(Node otherNode);

        [DomName("compareDocumentPosition")]
        DocumentPosition CompareDocumentPosition(Node otherNode);

        [DomName("normalize")]
        Node Normalize();

        [DomName("ownerDocument")]
        Document OwnerDocument { get; }

        [DomName("parentElement")]
        Element ParentElement { get; }

        [DomName("parentNode")]
        Node ParentNode { get; }

        [DomName("contains")]
        Boolean Contains(Node otherNode);

        [DomName("firstChild")]
        Node FirstChild { get; }

        [DomName("lastChild")]
        Node LastChild { get; }

        [DomName("nextSibling")]
        Node NextSibling { get; }

        [DomName("previousSibling")]
        Node PreviousSibling { get; }

        [DomName("isDefaultNamespace")]
        Boolean IsDefaultNamespace(String namespaceURI);

        [DomName("lookupNamespaceURI")]
        String LookupNamespaceURI(String prefix);

        [DomName("lookupPrefix")]
        String LookupPrefix(String namespaceURI);

        [DomName("nodeType")]
        NodeType NodeType { get; }

        [DomName("nodeValue")]
        String NodeValue { get; set; }

        [DomName("textContent")]
        String TextContent { get; set; }

        [DomName("hasChildNodes")]
        Boolean HasChildNodes { get; }

        [DomName("appendChild")]
        Node AppendChild(Node child);

        [DomName("insertBefore")]
        Node InsertBefore(Node newElement, Node referenceElement);

        [DomName("removeChild")]
        Node RemoveChild(Node child);

        [DomName("replaceChild")]
        Node ReplaceChild(Node newChild, Node oldChild);
    }
}
