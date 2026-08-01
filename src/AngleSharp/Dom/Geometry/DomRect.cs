namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomRect class.
/// </summary>
[DomName("DOMRect")]
[DomName("SVGRect")]
[DomExposed("Window")]
[DomExposed("Worker")]
public class DomRect : DomRectReadOnly, IDomRect
{
    /// <summary>
    /// Initializes a new instance of the DomRect class.
    /// </summary>
    [DomConstructor]
    public DomRect(Double x = 0.0, Double y = 0.0, Double width = 0.0, Double height = 0.0)
        : base(x, y, width, height)
    {
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("x")]
    public new Double X
    {
        get => base.X;
        set => base.X = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("y")]
    public new Double Y
    {
        get => base.Y;
        set => base.Y = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("width")]
    public new Double Width
    {
        get => base.Width;
        set => base.Width = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("height")]
    public new Double Height
    {
        get => base.Height;
        set => base.Height = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromRect")]
    public static new DomRect FromRect(DomRectInit? other = null)
    {
        other ??= new DomRectInit();
        return new DomRect(other.X, other.Y, other.Width, other.Height);
    }
}
