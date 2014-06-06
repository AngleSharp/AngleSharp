namespace AngleSharp.DOM
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// NodeList objects are collections of nodes.
    /// </summary>
    [DOM("NodeList")]
    public interface INodeList : IEnumerable<INode>
    {
        /// <summary>
        /// Returns an item in the list by its index, or null if out-of-bounds. 
        /// </summary>
        /// <param name="index">The 0-based index.</param>
        /// <returns>The element if it exists, otherwise false.</returns>
        [DOM("item")]
        INode this[Int32 index] { get; }

        /// <summary>
        /// Gets the number of nodes in the NodeList.
        /// </summary>
        [DOM("length")]
        Int32 Length { get; }
    }
}
