namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomRectReadOnly class.
/// </summary>
[DomName("DOMRectReadOnly")]
[DomExposed("Window")]
[DomExposed("Worker")]
public class DomRectReadOnly : IDomRectReadOnly
{
    /// <summary>
    /// Initializes a new instance of the DomRectReadOnly class.
    /// </summary>
    [DomConstructor]
    public DomRectReadOnly(Double x = 0.0, Double y = 0.0, Double width = 0.0, Double height = 0.0)
    {
        X = x;
        Y = y;
        Width = width;
        Height = height;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("x")]
    public virtual Double X { get; protected set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("y")]
    public virtual Double Y { get; protected set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("width")]
    public virtual Double Width { get; protected set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("height")]
    public virtual Double Height { get; protected set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("top")]
    public Double Top => GeometryMath.NaNSafeMinimum(Y, Y + Height);

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("right")]
    public Double Right => GeometryMath.NaNSafeMaximum(X, X + Width);

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("bottom")]
    public Double Bottom => GeometryMath.NaNSafeMaximum(Y, Y + Height);

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("left")]
    public Double Left => GeometryMath.NaNSafeMinimum(X, X + Width);

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromRect")]
    public static DomRectReadOnly FromRect(DomRectInit? other = null)
    {
        other ??= new DomRectInit();
        return new DomRectReadOnly(other.X, other.Y, other.Width, other.Height);
    }
}
