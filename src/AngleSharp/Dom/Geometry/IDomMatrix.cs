namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the IDomMatrix interface.
/// </summary>
[DomName("DOMMatrix")]
[DomName("SVGMatrix")]
[DomName("WebKitCSSMatrix")]
public interface IDomMatrix : IDomMatrixReadOnly
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("a")]
    new Double A { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("b")]
    new Double B { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("c")]
    new Double C { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("d")]
    new Double D { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("e")]
    new Double E { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("f")]
    new Double F { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m11")]
    new Double M11 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m12")]
    new Double M12 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m13")]
    new Double M13 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m14")]
    new Double M14 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m21")]
    new Double M21 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m22")]
    new Double M22 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m23")]
    new Double M23 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m24")]
    new Double M24 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m31")]
    new Double M31 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m32")]
    new Double M32 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m33")]
    new Double M33 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m34")]
    new Double M34 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m41")]
    new Double M41 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m42")]
    new Double M42 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m43")]
    new Double M43 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    [DomName("m44")]
    new Double M44 { get; set; }

    /// <summary>
    /// Executes MultiplySelf and returns a value.
    /// </summary>
    [DomName("multiplySelf")]
    IDomMatrix MultiplySelf(DomMatrixInit? other = null);

    /// <summary>
    /// Executes PreMultiplySelf and returns a value.
    /// </summary>
    [DomName("preMultiplySelf")]
    IDomMatrix PreMultiplySelf(DomMatrixInit? other = null);

    /// <summary>
    /// Executes TranslateSelf and returns a value.
    /// </summary>
    [DomName("translateSelf")]
    IDomMatrix TranslateSelf(Double tx = 0.0, Double ty = 0.0, Double tz = 0.0);

    /// <summary>
    /// Executes ScaleSelf and returns a value.
    /// </summary>
    [DomName("scaleSelf")]
    IDomMatrix ScaleSelf(Double scaleX = 1.0, Double? scaleY = null, Double scaleZ = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes Scale3dSelf and returns a value.
    /// </summary>
    [DomName("scale3dSelf")]
    IDomMatrix Scale3dSelf(Double scale = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes RotateSelf and returns a value.
    /// </summary>
    [DomName("rotateSelf")]
    IDomMatrix RotateSelf(Double rotX = 0.0, Double? rotY = null, Double? rotZ = null);

    /// <summary>
    /// Executes RotateFromVectorSelf and returns a value.
    /// </summary>
    [DomName("rotateFromVectorSelf")]
    IDomMatrix RotateFromVectorSelf(Double x = 0.0, Double y = 0.0);

    /// <summary>
    /// Executes RotateAxisAngleSelf and returns a value.
    /// </summary>
    [DomName("rotateAxisAngleSelf")]
    IDomMatrix RotateAxisAngleSelf(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double angle = 0.0);

    /// <summary>
    /// Executes SkewXSelf and returns a value.
    /// </summary>
    [DomName("skewXSelf")]
    IDomMatrix SkewXSelf(Double sx = 0.0);

    /// <summary>
    /// Executes SkewYSelf and returns a value.
    /// </summary>
    [DomName("skewYSelf")]
    IDomMatrix SkewYSelf(Double sy = 0.0);

    /// <summary>
    /// Executes InvertSelf and returns a value.
    /// </summary>
    [DomName("invertSelf")]
    IDomMatrix InvertSelf();

    /// <summary>
    /// Executes SetMatrixValue and returns a value.
    /// </summary>
    [DomName("setMatrixValue")]
    IDomMatrix SetMatrixValue(String transformList);
}
