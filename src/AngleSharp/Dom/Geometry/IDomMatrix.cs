namespace AngleSharp.Dom.Geometry;

using System;

/// <summary>
/// Represents the IDomMatrix interface.
/// </summary>
public interface IDomMatrix : IDomMatrixReadOnly
{
    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double A { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double B { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double C { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double D { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double E { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double F { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M11 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M12 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M13 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M14 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M21 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M22 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M23 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M24 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M31 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M32 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M33 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M34 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M41 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M42 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M43 { get; set; }

    /// <summary>
    /// Gets or sets the value.
    /// </summary>
    new Double M44 { get; set; }

    /// <summary>
    /// Executes MultiplySelf and returns a value.
    /// </summary>
    IDomMatrix MultiplySelf(DomMatrixInit? other = null);

    /// <summary>
    /// Executes PreMultiplySelf and returns a value.
    /// </summary>
    IDomMatrix PreMultiplySelf(DomMatrixInit? other = null);

    /// <summary>
    /// Executes TranslateSelf and returns a value.
    /// </summary>
    IDomMatrix TranslateSelf(Double tx = 0.0, Double ty = 0.0, Double tz = 0.0);

    /// <summary>
    /// Executes ScaleSelf and returns a value.
    /// </summary>
    IDomMatrix ScaleSelf(Double scaleX = 1.0, Double? scaleY = null, Double scaleZ = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes Scale3dSelf and returns a value.
    /// </summary>
    IDomMatrix Scale3dSelf(Double scale = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0);

    /// <summary>
    /// Executes RotateSelf and returns a value.
    /// </summary>
    IDomMatrix RotateSelf(Double rotX = 0.0, Double? rotY = null, Double? rotZ = null);

    /// <summary>
    /// Executes RotateFromVectorSelf and returns a value.
    /// </summary>
    IDomMatrix RotateFromVectorSelf(Double x = 0.0, Double y = 0.0);

    /// <summary>
    /// Executes RotateAxisAngleSelf and returns a value.
    /// </summary>
    IDomMatrix RotateAxisAngleSelf(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double angle = 0.0);

    /// <summary>
    /// Executes SkewXSelf and returns a value.
    /// </summary>
    IDomMatrix SkewXSelf(Double sx = 0.0);

    /// <summary>
    /// Executes SkewYSelf and returns a value.
    /// </summary>
    IDomMatrix SkewYSelf(Double sy = 0.0);

    /// <summary>
    /// Executes InvertSelf and returns a value.
    /// </summary>
    IDomMatrix InvertSelf();

    /// <summary>
    /// Executes SetMatrixValue and returns a value.
    /// </summary>
    IDomMatrix SetMatrixValue(String transformList);
}
