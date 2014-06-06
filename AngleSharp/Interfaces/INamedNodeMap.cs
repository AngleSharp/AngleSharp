namespace AngleSharp.DOM
{
    using System;

    [DOM("NamedNodeMap")]
    public interface INamedNodeMap
    {
        [DOM("getNamedItem")]
        INode this[String name] { get; }

        [DOM("setNamedItem")]
        INode Add(INode item); // raises(DOMException)

        [DOM("removeNamedItem")]
        INode Remove(String name); // raises(DOMException)

        [DOM("item")]
        INode this[Int32 index] { get; }

        [DOM("length")]
        Int32 Length { get; }

        [DOM("getNamedItemNS")]
        INode this[String namespaceUri, String localName] { get; } // raises(DOMException)

        [DOM("setNamedItemNS")]
        INode AddWithNamespace(INode item); // raises(DOMException)

        [DOM("removeNamedItemNS")]
        INode RemoveWithNamespace(String namespaceUri, String localName); // raises(DOMException)
    }
}
