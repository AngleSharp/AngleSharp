namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomRectInit class.
/// </summary>
[DomName("DOMRectInit")]
public class DomRectInit
{
    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("x")]
    public Double X { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("y")]
    public Double Y { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("width")]
    public Double Width { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("height")]
    public Double Height { get; set; }
}
