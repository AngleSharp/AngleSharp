namespace AngleSharp.Dom.Geometry;

using System;

/// <summary>
/// Represents the IDomMatrixReadOnly interface.
/// </summary>
public interface IDomMatrixReadOnly
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    Double A { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double B { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double C { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double D { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double E { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double F { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M11 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M12 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M13 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M14 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M21 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M22 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M23 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M24 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M31 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M32 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M33 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M34 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M41 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M42 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M43 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Double M44 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Boolean Is2D { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    Boolean IsIdentity { get; }

    /// <summary>
    /// Executes Translate and returns a value.
    /// </summary>
    IDomMatrix Translate(Double tx = 0.0, Double ty = 0.0, Double tz = 0.0);

    /// <summary>
    /// Executes Scale and returns a value.
    /// </summary>
    IDomMatrix Scale(Double scaleX = 1.0, Double? scaleY = null, Double scaleZ = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes ScaleNonUniform and returns a value.
    /// </summary>
    IDomMatrix ScaleNonUniform(Double scaleX = 1.0, Double scaleY = 1.0);

    /// <summary>
    /// Executes Scale3d and returns a value.
    /// </summary>
    IDomMatrix Scale3d(Double scale = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes Rotate and returns a value.
    /// </summary>
    IDomMatrix Rotate(Double rotX = 0.0, Double? rotY = null, Double? rotZ = null);

    /// <summary>
    /// Executes RotateFromVector and returns a value.
    /// </summary>
    IDomMatrix RotateFromVector(Double x = 0.0, Double y = 0.0);

    /// <summary>
    /// Executes RotateAxisAngle and returns a value.
    /// </summary>
    IDomMatrix RotateAxisAngle(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double angle = 0.0);

    /// <summary>
    /// Executes SkewX and returns a value.
    /// </summary>
    IDomMatrix SkewX(Double sx = 0.0);

    /// <summary>
    /// Executes SkewY and returns a value.
    /// </summary>
    IDomMatrix SkewY(Double sy = 0.0);

    /// <summary>
    /// Executes Multiply and returns a value.
    /// </summary>
    IDomMatrix Multiply(DomMatrixInit? other = null);

    /// <summary>
    /// Executes FlipX and returns a value.
    /// </summary>
    IDomMatrix FlipX();

    /// <summary>
    /// Executes FlipY and returns a value.
    /// </summary>
    IDomMatrix FlipY();

    /// <summary>
    /// Executes Inverse and returns a value.
    /// </summary>
    IDomMatrix Inverse();

    /// <summary>
    /// Executes TransformPoint and returns a value.
    /// </summary>
    IDomPoint TransformPoint(DomPointInit? point = null);

    /// <summary>
    /// Executes ToFloat32Array and returns a value.
    /// </summary>
    Single[] ToFloat32Array();

    /// <summary>
    /// Executes ToFloat64Array and returns a value.
    /// </summary>
    Double[] ToFloat64Array();
}
