namespace AngleSharp.Dom.Geometry;

using System;

/// <summary>
/// Represents the IDomPointReadOnly interface.
/// </summary>
public interface IDomPointReadOnly
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    Double X { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double Y { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double Z { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double W { get; }

    /// <summary>
    /// Executes MatrixTransform and returns a value.
    /// </summary>
    IDomPoint MatrixTransform(DomMatrixInit? matrix = null);
}
