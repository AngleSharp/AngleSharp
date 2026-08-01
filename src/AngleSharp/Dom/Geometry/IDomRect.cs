namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the IDomRect interface.
/// </summary>
[DomName("DOMRect")]
[DomName("SVGRect")]
public interface IDomRect : IDomRectReadOnly
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("x")]
    new Double X { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("y")]
    new Double Y { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("width")]
    new Double Width { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("height")]
    new Double Height { get; set; }
}
