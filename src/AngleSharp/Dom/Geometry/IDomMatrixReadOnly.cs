namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the IDomMatrixReadOnly interface.
/// </summary>
[DomName("DOMMatrixReadOnly")]
public interface IDomMatrixReadOnly
{
    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("a")]
    Double A { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("b")]
    Double B { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("c")]
    Double C { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("d")]
    Double D { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("e")]
    Double E { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("f")]
    Double F { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m11")]
    Double M11 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m12")]
    Double M12 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m13")]
    Double M13 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m14")]
    Double M14 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m21")]
    Double M21 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m22")]
    Double M22 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m23")]
    Double M23 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m24")]
    Double M24 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m31")]
    Double M31 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m32")]
    Double M32 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m33")]
    Double M33 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m34")]
    Double M34 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m41")]
    Double M41 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m42")]
    Double M42 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m43")]
    Double M43 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("m44")]
    Double M44 { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("is2D")]
    Boolean Is2D { get; }

    /// <summary>
    /// Gets the value.
    /// </summary>
    [DomName("isIdentity")]
    Boolean IsIdentity { get; }

    /// <summary>
    /// Executes Translate and returns a value.
    /// </summary>
    [DomName("translate")]
    IDomMatrix Translate(Double tx = 0.0, Double ty = 0.0, Double tz = 0.0);

    /// <summary>
    /// Executes Scale and returns a value.
    /// </summary>
    [DomName("scale")]
    IDomMatrix Scale(Double scaleX = 1.0, Double? scaleY = null, Double scaleZ = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes ScaleNonUniform and returns a value.
    /// </summary>
    [DomName("scaleNonUniform")]
    IDomMatrix ScaleNonUniform(Double scaleX = 1.0, Double scaleY = 1.0);

    /// <summary>
    /// Executes Scale3d and returns a value.
    /// </summary>
    [DomName("scale3d")]
    IDomMatrix Scale3d(Double scale = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes Rotate and returns a value.
    /// </summary>
    [DomName("rotate")]
    IDomMatrix Rotate(Double rotX = 0.0, Double? rotY = null, Double? rotZ = null);

    /// <summary>
    /// Executes RotateFromVector and returns a value.
    /// </summary>
    [DomName("rotateFromVector")]
    IDomMatrix RotateFromVector(Double x = 0.0, Double y = 0.0);

    /// <summary>
    /// Executes RotateAxisAngle and returns a value.
    /// </summary>
    [DomName("rotateAxisAngle")]
    IDomMatrix RotateAxisAngle(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double angle = 0.0);

    /// <summary>
    /// Executes SkewX and returns a value.
    /// </summary>
    [DomName("skewX")]
    IDomMatrix SkewX(Double sx = 0.0);

    /// <summary>
    /// Executes SkewY and returns a value.
    /// </summary>
    [DomName("skewY")]
    IDomMatrix SkewY(Double sy = 0.0);

    /// <summary>
    /// Executes Multiply and returns a value.
    /// </summary>
    [DomName("multiply")]
    IDomMatrix Multiply(DomMatrixInit? other = null);

    /// <summary>
    /// Executes FlipX and returns a value.
    /// </summary>
    [DomName("flipX")]
    IDomMatrix FlipX();

    /// <summary>
    /// Executes FlipY and returns a value.
    /// </summary>
    [DomName("flipY")]
    IDomMatrix FlipY();

    /// <summary>
    /// Executes Inverse and returns a value.
    /// </summary>
    [DomName("inverse")]
    IDomMatrix Inverse();

    /// <summary>
    /// Executes TransformPoint and returns a value.
    /// </summary>
    [DomName("transformPoint")]
    IDomPoint TransformPoint(DomPointInit? point = null);

    /// <summary>
    /// Executes ToFloat32Array and returns a value.
    /// </summary>
    [DomName("toFloat32Array")]
    Single[] ToFloat32Array();

    /// <summary>
    /// Executes ToFloat64Array and returns a value.
    /// </summary>
    [DomName("toFloat64Array")]
    Double[] ToFloat64Array();
}
