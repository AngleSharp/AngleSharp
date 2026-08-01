namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the IDomRectReadOnly interface.
/// </summary>
[DomName("DOMRectReadOnly")]
public interface IDomRectReadOnly
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("x")]
    Double X { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("y")]
    Double Y { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("width")]
    Double Width { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("height")]
    Double Height { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("top")]
    Double Top { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("right")]
    Double Right { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("bottom")]
    Double Bottom { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("left")]
    Double Left { get; }
}

