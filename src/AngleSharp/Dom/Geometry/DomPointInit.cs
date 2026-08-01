namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomPointInit class.
/// </summary>
[DomName("DOMPointInit")]
public class DomPointInit
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
    [DomName("z")]
    public Double Z { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("w")]
    public Double W { get; set; } = 1.0;
}
