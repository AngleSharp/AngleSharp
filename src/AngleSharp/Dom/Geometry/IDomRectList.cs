namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;
using System.Collections.Generic;

/// <summary>
/// Represents the IDomRectList interface.
/// </summary>
[DomName("DOMRectList")]
public interface IDomRectList : IEnumerable<IDomRect>
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("length")]
    Int32 Length { get; }

    /// <summary>
    /// Executes Item and returns a value.
    /// </summary>
    [DomName("item")]
    IDomRect? Item(Int32 index);
}
