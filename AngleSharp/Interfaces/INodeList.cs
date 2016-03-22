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
        /// Returns an item in the list by its index, or throws an exception.
        /// </summary>
        /// <param name="index">The 0-based index.</param>
        /// <returns>The element at the given index.</returns>
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
