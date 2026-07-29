namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomQuad class.
/// </summary>
[DomName("DOMQuad")]
[DomExposed("Window")]
[DomExposed("Worker")]
public sealed class DomQuad : IDomQuad
{
    /// <summary>
    /// Initializes a new instance of the DomQuad class.
    /// </summary>
    [DomConstructor]
    public DomQuad(DomPointInit? p1 = null, DomPointInit? p2 = null, DomPointInit? p3 = null, DomPointInit? p4 = null)
    {
        P1 = DomPoint.FromPoint(p1);
        P2 = DomPoint.FromPoint(p2);
        P3 = DomPoint.FromPoint(p3);
        P4 = DomPoint.FromPoint(p4);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p1")]
    public IDomPoint P1 { get; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p2")]
    public IDomPoint P2 { get; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p3")]
    public IDomPoint P3 { get; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("p4")]
    public IDomPoint P4 { get; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromRect")]
    public static DomQuad FromRect(DomRectInit? other = null)
    {
        other ??= new DomRectInit();
        var x = other.X;
        var y = other.Y;
        var width = other.Width;
        var height = other.Height;
        return new DomQuad(
            new DomPointInit { X = x, Y = y, Z = 0.0, W = 1.0 },
            new DomPointInit { X = x + width, Y = y, Z = 0.0, W = 1.0 },
            new DomPointInit { X = x + width, Y = y + height, Z = 0.0, W = 1.0 },
            new DomPointInit { X = x, Y = y + height, Z = 0.0, W = 1.0 });
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromQuad")]
    public static DomQuad FromQuad(DomQuadInit? other = null)
    {
        other ??= new DomQuadInit();
        return new DomQuad(other.P1, other.P2, other.P3, other.P4);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("getBounds")]
    public IDomRect GetBounds()
    {
        var left = GeometryMath.NaNSafeMinimum(P1.X, P2.X, P3.X, P4.X);
        var top = GeometryMath.NaNSafeMinimum(P1.Y, P2.Y, P3.Y, P4.Y);
        var right = GeometryMath.NaNSafeMaximum(P1.X, P2.X, P3.X, P4.X);
        var bottom = GeometryMath.NaNSafeMaximum(P1.Y, P2.Y, P3.Y, P4.Y);
        return new DomRect(left, top, right - left, bottom - top);
    }
}
