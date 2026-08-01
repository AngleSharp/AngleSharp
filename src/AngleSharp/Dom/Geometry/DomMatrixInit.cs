namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;

/// <summary>
/// Represents the DomMatrixInit class.
/// </summary>
[DomName("DOMMatrixInit")]
public class DomMatrixInit : DomMatrix2DInit
{
    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m13")]
    public Double? M13 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m14")]
    public Double? M14 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m23")]
    public Double? M23 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m24")]
    public Double? M24 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m31")]
    public Double? M31 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m32")]
    public Double? M32 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m33")]
    public Double? M33 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m34")]
    public Double? M34 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m43")]
    public Double? M43 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m44")]
    public Double? M44 { get; set; }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("is2D")]
    public Boolean? Is2D { get; set; }
}
