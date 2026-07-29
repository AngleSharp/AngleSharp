namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomPointReadOnly class.
/// </summary>
[DomName("DOMPointReadOnly")]
[DomExposed("Window")]
[DomExposed("Worker")]
public class DomPointReadOnly : IDomPointReadOnly
{
    /// <summary>
    /// Initializes a new instance of the DomPointReadOnly class.
    /// </summary>
    [DomConstructor]
    public DomPointReadOnly(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double w = 1.0)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
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
    [DomName("z")]
    public virtual Double Z { get; protected set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("w")]
    public virtual Double W { get; protected set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromPoint")]
    public static DomPointReadOnly FromPoint(DomPointInit? other = null)
    {
        other ??= new DomPointInit();
        return new DomPointReadOnly(other.X, other.Y, other.Z, other.W);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("matrixTransform")]
    public virtual IDomPoint MatrixTransform(DomMatrixInit? matrix = null)
    {
        var matrixObject = DomMatrix.FromMatrix(matrix);
        return matrixObject.TransformPoint(new DomPointInit { X = X, Y = Y, Z = Z, W = W });
    }
}
