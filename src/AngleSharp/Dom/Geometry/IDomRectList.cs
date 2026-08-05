namespace AngleSharp.Dom.Geometry;

using System;
using System.Collections.Generic;

/// <summary>
/// Represents the IDomRectList interface.
/// </summary>
public interface IDomRectList : IEnumerable<IDomRect>
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    Int32 Length { get; }

    /// <summary>
    /// Executes Item and returns a value.
    /// </summary>
    IDomRect? Item(Int32 index);
}
