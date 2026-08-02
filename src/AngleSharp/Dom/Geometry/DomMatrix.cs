namespace AngleSharp.Dom.Geometry;

using AngleSharp.Attributes;
using System;
using System.Collections.Generic;
using System.Numerics;

/// <summary>
/// Represents the DomMatrix class.
/// </summary>
[DomName("DOMMatrix")]
[DomName("SVGMatrix")]
[DomName("WebKitCSSMatrix")]
[DomExposed("Window")]
[DomExposed("Worker")]
public class DomMatrix : DomMatrixReadOnly, IDomMatrix
{
    /// <summary>
    /// Initializes a new instance of the DomMatrix class.
    /// </summary>
    [DomConstructor]
    public DomMatrix()
    {
    }

    /// <summary>
    /// Initializes a new instance of the DomMatrix class.
    /// </summary>
    public DomMatrix(IEnumerable<Double> init)
        : base(init)
    {
    }

    /// <summary>
    /// Initializes a new instance of the DomMatrix class.
    /// </summary>
    public DomMatrix(String transformList)
        : base(transformList)
    {
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    public static DomMatrix FromSequence(IEnumerable<Double> init, Boolean? is2D = null)
    {
        var matrix = new DomMatrix(init);

        if (is2D.HasValue)
        {
            matrix.Set2dFlag(is2D.Value);
        }

        return matrix;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromMatrix")]
    public static new DomMatrix FromMatrix(DomMatrixInit? other = null)
    {
        other ??= new DomMatrixInit();
        ValidateAliases(other);
        var values = new Double[16];

        values[0] = other.M11 ?? other.A ?? 1.0;
        values[1] = other.M12 ?? other.B ?? 0.0;
        values[2] = other.M13 ?? 0.0;
        values[3] = other.M14 ?? 0.0;
        values[4] = other.M21 ?? other.C ?? 0.0;
        values[5] = other.M22 ?? other.D ?? 1.0;
        values[6] = other.M23 ?? 0.0;
        values[7] = other.M24 ?? 0.0;
        values[8] = other.M31 ?? 0.0;
        values[9] = other.M32 ?? 0.0;
        values[10] = other.M33 ?? 1.0;
        values[11] = other.M34 ?? 0.0;
        values[12] = other.M41 ?? other.E ?? 0.0;
        values[13] = other.M42 ?? other.F ?? 0.0;
        values[14] = other.M43 ?? 0.0;
        values[15] = other.M44 ?? 1.0;
        var matrix = new DomMatrix(values);

        if (other.Is2D.HasValue)
        {
            matrix.Set2dFlag(other.Is2D.Value);
        }

        return matrix;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromFloat32Array")]
    public static new DomMatrix FromFloat32Array(Single[] array32)
    {
        var values = new Double[array32.Length];

        for (var i = 0; i < array32.Length; i++)
        {
            values[i] = array32[i];
        }

        return new DomMatrix(values);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("fromFloat64Array")]
    public static new DomMatrix FromFloat64Array(Double[] array64)
    {
        return new DomMatrix(array64);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("a")]
    public new Double A
    {
        get => base.A;
        set => M11 = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("b")]
    public new Double B
    {
        get => base.B;
        set => M12 = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("c")]
    public new Double C
    {
        get => base.C;
        set => M21 = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("d")]
    public new Double D
    {
        get => base.D;
        set => M22 = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("e")]
    public new Double E
    {
        get => base.E;
        set => M41 = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("f")]
    public new Double F
    {
        get => base.F;
        set => M42 = value;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m11")]
    public new Double M11
    {
        get => base.M11;
        set => SetAt(0, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m12")]
    public new Double M12
    {
        get => base.M12;
        set => SetAt(1, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m13")]
    public new Double M13
    {
        get => base.M13;
        set => SetAt(2, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m14")]
    public new Double M14
    {
        get => base.M14;
        set => SetAt(3, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m21")]
    public new Double M21
    {
        get => base.M21;
        set => SetAt(4, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m22")]
    public new Double M22
    {
        get => base.M22;
        set => SetAt(5, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m23")]
    public new Double M23
    {
        get => base.M23;
        set => SetAt(6, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m24")]
    public new Double M24
    {
        get => base.M24;
        set => SetAt(7, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m31")]
    public new Double M31
    {
        get => base.M31;
        set => SetAt(8, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m32")]
    public new Double M32
    {
        get => base.M32;
        set => SetAt(9, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m33")]
    public new Double M33
    {
        get => base.M33;
        set => SetAt(10, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m34")]
    public new Double M34
    {
        get => base.M34;
        set => SetAt(11, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m41")]
    public new Double M41
    {
        get => base.M41;
        set => SetAt(12, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m42")]
    public new Double M42
    {
        get => base.M42;
        set => SetAt(13, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m43")]
    public new Double M43
    {
        get => base.M43;
        set => SetAt(14, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("m44")]
    public new Double M44
    {
        get => base.M44;
        set => SetAt(15, value);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("multiplySelf")]
    public IDomMatrix MultiplySelf(DomMatrixInit? other = null)
    {
        var right = FromMatrix(other);
        var result = MultiplyCore(ToRows(), right.ToRows());
        SetElements(ToElements(result));
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("preMultiplySelf")]
    public IDomMatrix PreMultiplySelf(DomMatrixInit? other = null)
    {
        var left = FromMatrix(other);
        var result = MultiplyCore(left.ToRows(), ToRows());
        SetElements(ToElements(result));
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("translateSelf")]
    public IDomMatrix TranslateSelf(Double tx = 0.0, Double ty = 0.0, Double tz = 0.0)
    {
        var transform = new[,]
        {
            { 1.0, 0.0, 0.0, tx },
            { 0.0, 1.0, 0.0, ty },
            { 0.0, 0.0, 1.0, tz },
            { 0.0, 0.0, 0.0, 1.0 },
        };
        SetElements(ToElements(MultiplyCore(ToRows(), transform)));
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("scaleSelf")]
    public IDomMatrix ScaleSelf(Double scaleX = 1.0, Double? scaleY = null, Double scaleZ = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0)
    {
        var sy = scaleY ?? scaleX;
        TranslateSelf(originX, originY, originZ);
        var scale = new[,]
        {
            { scaleX, 0.0, 0.0, 0.0 },
            { 0.0, sy, 0.0, 0.0 },
            { 0.0, 0.0, scaleZ, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
        SetElements(ToElements(MultiplyCore(ToRows(), scale)));
        TranslateSelf(-originX, -originY, -originZ);
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("scale3dSelf")]
    public IDomMatrix Scale3dSelf(Double scale = 1.0, Double originX = 0.0, Double originY = 0.0, Double originZ = 0.0)
    {
        return ScaleSelf(scale, scale, scale, originX, originY, originZ);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("rotateSelf")]
    public IDomMatrix RotateSelf(Double rotX = 0.0, Double? rotY = null, Double? rotZ = null)
    {
        var x = rotX;
        var y = rotY;
        var z = rotZ;

        if (!y.HasValue && !z.HasValue)
        {
            z = x;
            x = 0.0;
            y = 0.0;
        }

        y ??= 0.0;
        z ??= 0.0;

        if (z.Value != 0.0)
        {
            var rz = RotationAroundZ(z.Value);
            SetElements(ToElements(MultiplyCore(ToRows(), rz)));
        }

        if (y.Value != 0.0)
        {
            var ry = RotationAroundY(y.Value);
            SetElements(ToElements(MultiplyCore(ToRows(), ry)));
        }

        if (x != 0.0)
        {
            var rx = RotationAroundX(x);
            SetElements(ToElements(MultiplyCore(ToRows(), rx)));
        }

        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("rotateFromVectorSelf")]
    public IDomMatrix RotateFromVectorSelf(Double x = 0.0, Double y = 0.0)
    {
        var angle = x == 0.0 && y == 0.0 ? 0.0 : Math.Atan2(y, x) * (180.0 / Math.PI);
        return RotateSelf(0.0, 0.0, angle);
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("rotateAxisAngleSelf")]
    public IDomMatrix RotateAxisAngleSelf(Double x = 0.0, Double y = 0.0, Double z = 0.0, Double angle = 0.0)
    {
        var axisLength = Math.Sqrt((x * x) + (y * y) + (z * z));

        if (axisLength == 0.0)
        {
            return this;
        }

        var nx = x / axisLength;
        var ny = y / axisLength;
        var nz = z / axisLength;
        var radians = angle * (Math.PI / 180.0);
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        var t = 1.0 - cos;
        var rotation = new[,]
        {
            { t * nx * nx + cos, t * nx * ny - (sin * nz), t * nx * nz + (sin * ny), 0.0 },
            { t * nx * ny + (sin * nz), t * ny * ny + cos, t * ny * nz - (sin * nx), 0.0 },
            { t * nx * nz - (sin * ny), t * ny * nz + (sin * nx), t * nz * nz + cos, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
        SetElements(ToElements(MultiplyCore(ToRows(), rotation)));
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("skewXSelf")]
    public IDomMatrix SkewXSelf(Double sx = 0.0)
    {
        var tangent = Math.Tan(sx * (Math.PI / 180.0));
        var skew = new[,]
        {
            { 1.0, tangent, 0.0, 0.0 },
            { 0.0, 1.0, 0.0, 0.0 },
            { 0.0, 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
        SetElements(ToElements(MultiplyCore(ToRows(), skew)));
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("skewYSelf")]
    public IDomMatrix SkewYSelf(Double sy = 0.0)
    {
        var tangent = Math.Tan(sy * (Math.PI / 180.0));
        var skew = new[,]
        {
            { 1.0, 0.0, 0.0, 0.0 },
            { tangent, 1.0, 0.0, 0.0 },
            { 0.0, 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
        SetElements(ToElements(MultiplyCore(ToRows(), skew)));
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("invertSelf")]
    public IDomMatrix InvertSelf()
    {
        var rows = ToRows();
        var matrix = new Matrix4x4(
            (Single)rows[0, 0], (Single)rows[0, 1], (Single)rows[0, 2], (Single)rows[0, 3],
            (Single)rows[1, 0], (Single)rows[1, 1], (Single)rows[1, 2], (Single)rows[1, 3],
            (Single)rows[2, 0], (Single)rows[2, 1], (Single)rows[2, 2], (Single)rows[2, 3],
            (Single)rows[3, 0], (Single)rows[3, 1], (Single)rows[3, 2], (Single)rows[3, 3]);

        if (Matrix4x4.Invert(matrix, out var inverted))
        {
            var invertedRows = new[,]
            {
                { (Double)inverted.M11, (Double)inverted.M12, (Double)inverted.M13, (Double)inverted.M14 },
                { (Double)inverted.M21, (Double)inverted.M22, (Double)inverted.M23, (Double)inverted.M24 },
                { (Double)inverted.M31, (Double)inverted.M32, (Double)inverted.M33, (Double)inverted.M34 },
                { (Double)inverted.M41, (Double)inverted.M42, (Double)inverted.M43, (Double)inverted.M44 },
            };
            SetElements(ToElements(invertedRows));
            return this;
        }

        SetElements(new[]
        {
            Double.NaN, Double.NaN, Double.NaN, Double.NaN,
            Double.NaN, Double.NaN, Double.NaN, Double.NaN,
            Double.NaN, Double.NaN, Double.NaN, Double.NaN,
            Double.NaN, Double.NaN, Double.NaN, Double.NaN,
        }, false);
        return this;
    }

    /// <summary>
    /// Provides the member value.
    /// </summary>
    [DomName("setMatrixValue")]
    public IDomMatrix SetMatrixValue(String transformList)
    {
        SetMatrixFromString(transformList);
        return this;
    }

    internal DomMatrixInit ToMatrixInit()
    {
        return new DomMatrixInit
        {
            M11 = M11,
            M12 = M12,
            M13 = M13,
            M14 = M14,
            M21 = M21,
            M22 = M22,
            M23 = M23,
            M24 = M24,
            M31 = M31,
            M32 = M32,
            M33 = M33,
            M34 = M34,
            M41 = M41,
            M42 = M42,
            M43 = M43,
            M44 = M44,
            Is2D = Is2D,
        };
    }

    private void Set2dFlag(Boolean is2D)
    {
        if (is2D)
        {
            SetElements(ToFloat64Array(), true);
        }
        else
        {
            SetElements(ToFloat64Array(), false);
        }
    }

    private void SetAt(Int32 index, Double value)
    {
        var values = ToFloat64Array();
        values[index] = value;
        SetElements(values);
    }

    private static void ValidateAliases(DomMatrix2DInit init)
    {
        EnsureSame(init.A, init.M11, "a", "m11");
        EnsureSame(init.B, init.M12, "b", "m12");
        EnsureSame(init.C, init.M21, "c", "m21");
        EnsureSame(init.D, init.M22, "d", "m22");
        EnsureSame(init.E, init.M41, "e", "m41");
        EnsureSame(init.F, init.M42, "f", "m42");
    }

    private static void EnsureSame(Double? first, Double? second, String firstName, String secondName)
    {
        if (first.HasValue && second.HasValue && !SameValueZero(first.Value, second.Value))
        {
            throw new ArgumentException($"{firstName} and {secondName} cannot be different.");
        }
    }

    private static Boolean SameValueZero(Double a, Double b)
    {
        return a == b || (Double.IsNaN(a) && Double.IsNaN(b));
    }

    private static Double[,] RotationAroundX(Double angle)
    {
        var radians = angle * (Math.PI / 180.0);
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        return new[,]
        {
            { 1.0, 0.0, 0.0, 0.0 },
            { 0.0, cos, -sin, 0.0 },
            { 0.0, sin, cos, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
    }

    private static Double[,] RotationAroundY(Double angle)
    {
        var radians = angle * (Math.PI / 180.0);
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        return new[,]
        {
            { cos, 0.0, sin, 0.0 },
            { 0.0, 1.0, 0.0, 0.0 },
            { -sin, 0.0, cos, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
    }

    private static Double[,] RotationAroundZ(Double angle)
    {
        var radians = angle * (Math.PI / 180.0);
        var cos = Math.Cos(radians);
        var sin = Math.Sin(radians);
        return new[,]
        {
            { cos, -sin, 0.0, 0.0 },
            { sin, cos, 0.0, 0.0 },
            { 0.0, 0.0, 1.0, 0.0 },
            { 0.0, 0.0, 0.0, 1.0 },
        };
    }
}
