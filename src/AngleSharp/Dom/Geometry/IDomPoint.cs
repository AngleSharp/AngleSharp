namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the IDomPoint interface.
/// </summary>
[DomName("DOMPoint")]
[DomName("SVGPoint")]
public interface IDomPoint : IDomPointReadOnly
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
    [DomName("z")]
    new Double Z { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("w")]
    new Double W { get; set; }
}
