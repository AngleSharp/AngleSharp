namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;
using System.Collections.Generic;
using System.Globalization;

/// <summary>
/// Represents the DomMatrixReadOnly class.
/// </summary>
[DomName("DOMMatrixReadOnly")]
[DomExposed("Window")]
[DomExposed("Worker")]
public class DomMatrixReadOnly : IDomMatrixReadOnly
{
    private Double _m11;
    private Double _m12;
    private Double _m13;
    private Double _m14;
    private Double _m21;
    private Double _m22;
    private Double _m23;
    private Double _m24;
    private Double _m31;
    private Double _m32;
    private Double _m33;
    private Double _m34;
    private Double _m41;
    private Double _m42;
    private Double _m43;
    private Double _m44;
    private Boolean _is2D;

    /// <summary>
    /// Initializes a new instance of the DomMatrixReadOnly class.
    /// </summary>
    [DomConstructor]
    public DomMatrixReadOnly()
    {
        SetIdentity();
    }

    /// <summary>
    /// Initializes a new instance of the DomMatrixReadOnly class.
    /// </summary>
    public DomMatrixReadOnly(IEnumerable<Double> init)
    {
        ApplySequence(init);
    }

    /// <summary>
    /// Initializes a new instance of the DomMatrixReadOnly class.
    /// </summary>
    public DomMatrixReadOnly(String transformList)
    {
        SetMatrixFromString(transformList);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromMatrix")]
    public static DomMatrixReadOnly FromMatrix(DomMatrixInit? other = null)
    {
        var matrix = DomMatrix.FromMatrix(other);
        return new DomMatrixReadOnly(matrix.ToFloat64Array()) { _is2D = matrix.Is2D };
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromFloat32Array")]
    public static DomMatrixReadOnly FromFloat32Array(Single[] array32)
    {
        var array64 = new Double[array32.Length];

        for (var i = 0; i < array32.Length; i++)
        {
            array64[i] = array32[i];
        }

        return new DomMatrixReadOnly(array64);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromFloat64Array")]
    public static DomMatrixReadOnly FromFloat64Array(Double[] array64)
    {
        return new DomMatrixReadOnly(array64);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("a")]
    public virtual Double A => _m11;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("b")]
    public virtual Double B => _m12;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("c")]
    public virtual Double C => _m21;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("d")]
    public virtual Double D => _m22;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("e")]
    public virtual Double E => _m41;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("f")]
    public virtual Double F => _m42;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m11")]
    public virtual Double M11 => _m11;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m12")]
    public virtual Double M12 => _m12;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m13")]
    public virtual Double M13 => _m13;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m14")]
    public virtual Double M14 => _m14;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m21")]
    public virtual Double M21 => _m21;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m22")]
    public virtual Double M22 => _m22;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m23")]
    public virtual Double M23 => _m23;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m24")]
    public virtual Double M24 => _m24;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m31")]
    public virtual Double M31 => _m31;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m32")]
    public virtual Double M32 => _m32;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m33")]
    public virtual Double M33 => _m33;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m34")]
    public virtual Double M34 => _m34;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m41")]
    public virtual Double M41 => _m41;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m42")]
    public virtual Double M42 => _m42;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m43")]
    public virtual Double M43 => _m43;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m44")]
    public virtual Double M44 => _m44;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("is2D")]
    public virtual Boolean Is2D => _is2D;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("isIdentity")]
    public virtual Boolean IsIdentity =>
        _m11 == 1.0 && _m12 == 0.0 && _m13 == 0.0 && _m14 == 0.0 &&
        _m21 == 0.0 && _m22 == 1.0 && _m23 == 0.0 && _m24 == 0.0 &&
        _m31 == 0.0 && _m32 == 0.0 && _m33 == 1.0 && _m34 == 0.0 &&
        _m41 == 0.0 && _m42 == 0.0 && _m43 == 0.0 && _m44 == 1.0;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("translate")]
    public virtual IDomMatrix Translate(Double tx = 0.0, Double ty = 0.0, Double tz = 0.0)
    {
        return AsMutableCopy().TranslateSelf(tx, ty, tz);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("scale")]
    public virtual IDomMatrix Scale(Double scaleX = 1.0, Double? scaleY = null, Double scaleZ = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0)
    {
        return AsMutableCopy().ScaleSelf(scaleX, scaleY, scaleZ, originX, originY, originZ);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("scaleNonUniform")]
    public virtual IDomMatrix ScaleNonUniform(Double scaleX = 1.0, Double scaleY = 1.0)
    {
        return AsMutableCopy().ScaleSelf(scaleX, scaleY, 1.0, 0.0, 0.0, 0.0);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("scale3d")]
    public virtual IDomMatrix Scale3d(Double scale = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0)
    {
        return AsMutableCopy().Scale3dSelf(scale, originX, originY, originZ);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("rotate")]
    public virtual IDomMatrix Rotate(Double rotX = 0.0, Double? rotY = null, Double? rotZ = null)
    {
        return AsMutableCopy().RotateSelf(rotX, rotY, rotZ);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("rotateFromVector")]
    public virtual IDomMatrix RotateFromVector(Double x = 0.0, Double y = 0.0)
    {
        return AsMutableCopy().RotateFromVectorSelf(x, y);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("rotateAxisAngle")]
    public virtual IDomMatrix RotateAxisAngle(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double angle = 0.0)
    {
        return AsMutableCopy().RotateAxisAngleSelf(x, y, z, angle);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("skewX")]
    public virtual IDomMatrix SkewX(Double sx = 0.0)
    {
        return AsMutableCopy().SkewXSelf(sx);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("skewY")]
    public virtual IDomMatrix SkewY(Double sy = 0.0)
    {
        return AsMutableCopy().SkewYSelf(sy);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("multiply")]
    public virtual IDomMatrix Multiply(DomMatrixInit? other = null)
    {
        return AsMutableCopy().MultiplySelf(other);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("flipX")]
    public virtual IDomMatrix FlipX()
    {
        return Multiply(DomMatrix.FromSequence(new[] { -1.0, 0.0, 0.0, 1.0, 0.0, 0.0 }).ToMatrixInit());
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("flipY")]
    public virtual IDomMatrix FlipY()
    {
        return Multiply(DomMatrix.FromSequence(new[] { 1.0, 0.0, 0.0, -1.0, 0.0, 0.0 }).ToMatrixInit());
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("inverse")]
    public virtual IDomMatrix Inverse()
    {
        return AsMutableCopy().InvertSelf();
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("transformPoint")]
    public virtual IDomPoint TransformPoint(DomPointInit? point = null)
    {
        point ??= new DomPointInit();
        var x = point.X;
        var y = point.Y;
        var z = point.Z;
        var w = point.W;
        var tx = (_m11 * x) + (_m21 * y) + (_m31 * z) + (_m41 * w);
        var ty = (_m12 * x) + (_m22 * y) + (_m32 * z) + (_m42 * w);
        var tz = (_m13 * x) + (_m23 * y) + (_m33 * z) + (_m43 * w);
        var tw = (_m14 * x) + (_m24 * y) + (_m34 * z) + (_m44 * w);
        return new DomPoint(tx, ty, tz, tw);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("toFloat32Array")]
    public virtual Single[] ToFloat32Array()
    {
        var raw = ToFloat64Array();
        var result = new Single[raw.Length];

        for (var i = 0; i < raw.Length; i++)
        {
            result[i] = (Single)raw[i];
        }

        return result;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("toFloat64Array")]
    public virtual Double[] ToFloat64Array()
    {
        return new[]
        {
            _m11, _m12, _m13, _m14,
            _m21, _m22, _m23, _m24,
            _m31, _m32, _m33, _m34,
            _m41, _m42, _m43, _m44,
        };
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    public override String ToString()
    {
        EnsureFinite();

        if (_is2D)
        {
            return String.Concat(
                "matrix(",
                Format(_m11), ", ",
                Format(_m12), ", ",
                Format(_m21), ", ",
                Format(_m22), ", ",
                Format(_m41), ", ",
                Format(_m42),
                ")");
        }

        return String.Concat(
            "matrix3d(",
            Format(_m11), ", ", Format(_m12), ", ", Format(_m13), ", ", Format(_m14), ", ",
            Format(_m21), ", ", Format(_m22), ", ", Format(_m23), ", ", Format(_m24), ", ",
            Format(_m31), ", ", Format(_m32), ", ", Format(_m33), ", ", Format(_m34), ", ",
            Format(_m41), ", ", Format(_m42), ", ", Format(_m43), ", ", Format(_m44),
            ")");
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected DomMatrix AsMutableCopy()
    {
        return DomMatrix.FromSequence(ToFloat64Array(), _is2D);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected void SetIdentity()
    {
        _m11 = 1.0;
        _m12 = 0.0;
        _m13 = 0.0;
        _m14 = 0.0;
        _m21 = 0.0;
        _m22 = 1.0;
        _m23 = 0.0;
        _m24 = 0.0;
        _m31 = 0.0;
        _m32 = 0.0;
        _m33 = 1.0;
        _m34 = 0.0;
        _m41 = 0.0;
        _m42 = 0.0;
        _m43 = 0.0;
        _m44 = 1.0;
        _is2D = true;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected void SetElements(Double[] values, Boolean? is2D = null)
    {
        _m11 = values[0];
        _m12 = values[1];
        _m13 = values[2];
        _m14 = values[3];
        _m21 = values[4];
        _m22 = values[5];
        _m23 = values[6];
        _m24 = values[7];
        _m31 = values[8];
        _m32 = values[9];
        _m33 = values[10];
        _m34 = values[11];
        _m41 = values[12];
        _m42 = values[13];
        _m43 = values[14];
        _m44 = values[15];
        _is2D = is2D ?? ComputeIs2D();
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected void SetMatrixFromString(String transformList)
    {
        if (String.IsNullOrWhiteSpace(transformList))
        {
            SetIdentity();
            return;
        }

        var value = transformList.Trim();

        if (value.StartsWith("matrix3d(", StringComparison.OrdinalIgnoreCase) && value.EndsWith(")", StringComparison.Ordinal))
        {
            var payload = value.Substring(9, value.Length - 10);
            var values = ParseCsvDoubles(payload, 16);
            SetElements(values, false);
            return;
        }

        if (value.StartsWith("matrix(", StringComparison.OrdinalIgnoreCase) && value.EndsWith(")", StringComparison.Ordinal))
        {
            var payload = value.Substring(7, value.Length - 8);
            var values = ParseCsvDoubles(payload, 6);
            ApplySequence(values);
            return;
        }

        throw new DomException(DomError.Syntax);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected void ApplySequence(IEnumerable<Double> init)
    {
        var values = new List<Double>();

        foreach (var entry in init)
        {
            values.Add(entry);
        }

        if (values.Count == 6)
        {
            SetElements(new[]
            {
                values[0], values[1], 0.0, 0.0,
                values[2], values[3], 0.0, 0.0,
                0.0, 0.0, 1.0, 0.0,
                values[4], values[5], 0.0, 1.0,
            }, true);
            return;
        }

        if (values.Count == 16)
        {
            SetElements(values.ToArray(), false);
            return;
        }

        throw new ArgumentException("DOMMatrix sequence needs 6 or 16 values.", nameof(init));
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected static Double[] ParseCsvDoubles(String payload, Int32 expected)
    {
        var pieces = payload.Split(',');

        if (pieces.Length != expected)
        {
            throw new DomException(DomError.Syntax);
        }

        var values = new Double[expected];

        for (var i = 0; i < pieces.Length; i++)
        {
            if (!Double.TryParse(pieces[i], NumberStyles.Float, CultureInfo.InvariantCulture, out var parsed))
            {
                throw new DomException(DomError.Syntax);
            }

            values[i] = parsed;
        }

        return values;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected Boolean ComputeIs2D()
    {
        return IsZero(_m13) && IsZero(_m14) && IsZero(_m23) && IsZero(_m24) &&
                IsZero(_m31) && IsZero(_m32) && IsZero(_m34) && IsZero(_m43) &&
                _m33 == 1.0 && _m44 == 1.0;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected static Boolean IsZero(Double value) => value == 0.0;

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected static String Format(Double value)
    {
        return Convert.ToString(value, CultureInfo.InvariantCulture) ?? String.Empty;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected void EnsureFinite()
    {
        var values = ToFloat64Array();

        for (var i = 0; i < values.Length; i++)
        {
            if (Double.IsNaN(values[i]) || Double.IsInfinity(values[i]))
            {
                throw new DomException(DomError.InvalidState);
            }
        }
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected static Double[,] MultiplyCore(Double[,] left, Double[,] right)
    {
        var result = new Double[4, 4];

        for (var row = 0; row < 4; row++)
        {
            for (var col = 0; col < 4; col++)
            {
                var sum = 0.0;

                for (var i = 0; i < 4; i++)
                {
                    sum += left[row, i] * right[i, col];
                }

                result[row, col] = sum;
            }
        }

        return result;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected Double[,] ToRows()
    {
        return new[,]
        {
            { _m11, _m21, _m31, _m41 },
            { _m12, _m22, _m32, _m42 },
            { _m13, _m23, _m33, _m43 },
            { _m14, _m24, _m34, _m44 },
        };
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    protected static Double[] ToElements(Double[,] rows)
    {
        return new[]
        {
            rows[0, 0], rows[1, 0], rows[2, 0], rows[3, 0],
            rows[0, 1], rows[1, 1], rows[2, 1], rows[3, 1],
            rows[0, 2], rows[1, 2], rows[2, 2], rows[3, 2],
            rows[0, 3], rows[1, 3], rows[2, 3], rows[3, 3],
        };
    }
}
