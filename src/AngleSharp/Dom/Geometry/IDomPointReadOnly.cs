namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the IDomPointReadOnly interface.
/// </summary>
[DomName("DOMPointReadOnly")]
public interface IDomPointReadOnly
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
    [DomName("z")]
    Double Z { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("w")]
    Double W { get; }

    /// <summary>
    /// Executes MatrixTransform and returns a value.
    /// </summary>
    [DomName("matrixTransform")]
    IDomPoint MatrixTransform(DomMatrixInit? matrix = null);
}
