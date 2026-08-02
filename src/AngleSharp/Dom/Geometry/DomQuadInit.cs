namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomQuadInit class.
/// </summary>
[DomName("DOMQuadInit")]
public class DomQuadInit
{
    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p1")]
    public DomPointInit? P1 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p2")]
    public DomPointInit? P2 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p3")]
    public DomPointInit? P3 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p4")]
    public DomPointInit? P4 { get; set; }
}
