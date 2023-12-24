namespace AngleSharp.Html.Construction;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents a constructable node list. (Children)
/// </summary>
public interface IConstructableNodeList : IEnumerable<IConstructableNode>
{
    /// <summary>
    /// Returns an item in the list by its index, or throws an exception.
    /// </summary>
    IConstructableNode this[Int32 index] { get; }

    /// <summary>
    /// Length of the list.
    /// </summary>
    Int32 Length { get; }

    /// <summary>
    /// Clears the list.
    /// </summary>
    void Clear();
}