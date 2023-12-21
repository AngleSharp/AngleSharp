namespace AngleSharp.ReadOnly;

using System;
using System.Collections.Generic;

/// <summary>
/// NodeList objects are collections of nodes.
/// </summary>
public interface IReadOnlyNodeList : IEnumerable<IReadOnlyNode>, IMarkupFormattable
{
    IReadOnlyNode this[Int32 index] { get; }
    Int32 Length { get; }
    void Add(IReadOnlyNode node);
}