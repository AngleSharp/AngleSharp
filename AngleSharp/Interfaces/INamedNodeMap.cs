using System;
using System.Collections.Generic;
using AngleSharp.Attributes;
using AngleSharp.Dom;

namespace AngleSharp.Interfaces
{
    /// <summary>
    /// NamedNodeNap is a key/value pair of nodes that can be accessed by numeric or string index
    /// </summary>
	[DomName("NamedNodeMap")]
    public interface INamedNodeMap : IEnumerable<IAttr>
    {
        /// <summary>
        /// Gets the node at the specified numeric index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The attribute at the specified numeric index</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        IAttr this[Int32 index] { get; }

        /// <summary>
        /// Gets the node at the specified named index
        /// </summary>
        /// <param name="index"></param>
        /// <returns>The attribute at the specfied named index</returns>
        [DomAccessor(Accessors.Getter)]
        IAttr this[String index] { get; }

        /// <summary>
        /// Gets the number of nodes in the NamedNodeMap.
        /// </summary>
        /// <returns>The number of nodes in the collection</returns>
        [DomName("length")]
        Int32 Length { get; }

        /// <summary>
        /// Gets a named item in the NamedNodeMap
        /// </summary>
        /// <param name="name">The name of the item to get.</param>
        [DomName("getNamedItem")]
        IAttr GetNamedItem(String name);

        /// <summary>
        /// Sets a named item in the NamedNodeMap
        /// </summary>
        /// <param name="item">The named item to set.</param>
        [DomName("setNamedItem")]
        void SetNamedItem(IAttr item);

        /// <summary>
        /// Removes a named item from the NamedNodeMap
        /// </summary>
        /// <param name="item">The named item to remove.</param>
        [DomName("removeNamedItem")]
        void RemoveNamedItem(IAttr item);
    }
}
