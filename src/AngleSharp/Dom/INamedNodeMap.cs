namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by
    /// numeric or string index.
    /// https://dom.spec.whatwg.org/#interface-namednodemap
    /// </summary>
	[DomName("NamedNodeMap")]
    public interface INamedNodeMap : IEnumerable<IAttr>
    {
        /// <summary>
        /// Gets the node at the specified numeric index.
        /// </summary>
        /// <param name="index">The index of the element.</param>
        /// <returns>The attribute at the specified numeric index.</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        IAttr this[Int32 index] { get; }

        /// <summary>
        /// Gets the node with the specified name.
        /// </summary>
        /// <param name="name">The name of the element.</param>
        /// <returns>The attribute at the specfied name.</returns>
        [DomAccessor(Accessors.Getter)]
        IAttr this[String name] { get; }

        /// <summary>
        /// Gets the number of nodes in the NamedNodeMap.
        /// </summary>
        /// <returns>The number of nodes in the collection.</returns>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets a named item in the NamedNodeMap.
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        /// <returns>The attribute with the given name.</returns>
        [DomName("getNamedItem")]
        IAttr GetNamedItem(String name);

        /// <summary>
        /// Sets a named item in the NamedNodeMap.
        /// </summary>
        /// <param name="item">The named item to set.</param>
        /// <returns>The replaced attribute, if any.</returns>
        [DomName("setNamedItem")]
        IAttr SetNamedItem(IAttr item);

        /// <summary>
        /// Removes a named item from the NamedNodeMap
        /// </summary>
        /// <param name="name">The named item to remove.</param>
        /// <returns>The removed attribute.</returns>
        [DomName("removeNamedItem")]
        IAttr RemoveNamedItem(String name);

        /// <summary>
        /// Gets a named item in the NamedNodeMap identified by namespace and
        /// local name.
        /// </summary>
        /// <param name="namespaceUri">The namespace of the item.</param>
        /// <param name="localName">The local name of the item.</param>
        /// <returns>The attribute with the namespaceUri and name.</returns>
        [DomName("getNamedItemNS")]
        IAttr GetNamedItem(String namespaceUri, String localName);

        /// <summary>
        /// Sets a named item in the NamedNodeMap.
        /// </summary>
        /// <param name="item">The named item to set.</param>
        /// <returns>The replaced attribute, if any.</returns>
        [DomName("setNamedItemNS")]
        IAttr SetNamedItemWithNamespaceUri(IAttr item);

        /// <summary>
        /// Removes a named item from the NamedNodeMap.
        /// </summary>
        /// <param name="namespaceUri">The namespace of the item.</param>
        /// <param name="localName">The local name of the item.</param>
        /// <returns>The removed attribute.</returns>
        [DomName("removeNamedItemNS")]
        IAttr RemoveNamedItem(String namespaceUri, String localName);
    }
}
