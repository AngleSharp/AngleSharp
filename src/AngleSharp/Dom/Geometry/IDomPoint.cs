namespace AngleSharp.Dom.Geometry;

using System;

/// <summary>
/// Represents the IDomPoint interface.
/// </summary>
public interface IDomPoint : IDomPointReadOnly
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
    new Double Z { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double W { get; set; }
}
