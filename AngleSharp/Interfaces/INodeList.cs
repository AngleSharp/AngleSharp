namespace AngleSharp.Dom
{
    using AngleSharp.Attributes;
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NodeList objects are collections of nodes.
    /// </summary>
    [DomName("NodeList")]
    public interface INodeList : IEnumerable<INode>, IMarkupFormattable
    {
        /// <summary>
        /// Returns an item in the list by its index, or null if out-of-bounds.
        /// </summary>
        /// <param name="index">The 0-based index.</param>
        /// <returns>The element if it exists, otherwise false.</returns>
        [DomName("item")]
        [DomAccessor(Accessors.Getter)]
        INode this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of nodes in the NodeList.
        /// </summary>
        [DomName("length")]
        Int32 Length { get; }
    }
}
