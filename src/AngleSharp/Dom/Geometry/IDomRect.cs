namespace AngleSharp.Dom.Geometry;

using System;

/// <summary>
/// Represents the IDomRect interface.
/// </summary>
public interface IDomRect : IDomRectReadOnly
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double X { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double Y { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double Width { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double Height { get; set; }
}
