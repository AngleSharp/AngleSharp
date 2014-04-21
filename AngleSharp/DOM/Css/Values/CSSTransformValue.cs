namespace AngleSharp.DOM.Css
{
    using System;
    using System.Globalization;

    /// <summary>
    /// Represents a transformation in CSS.
    /// https://developer.mozilla.org/en-US/docs/Web/CSS/transform#CSS_transform_functions
    /// </summary>
    public abstract class CSSTransformValue : CSSValue
    {
        #region Properties

        public abstract TransformMatrix ComputeMatrix();

        #endregion

        #region Classes

        public sealed class Matrix : CSSTransformValue
        {
            TransformMatrix _matrix;

            public Matrix(Single m11, Single m12, Single m21, Single m22, Single tx, Single ty)
            {
                _matrix = new TransformMatrix(m11, m12, 0f, m21, m22, 0f, 0f, 0f, 1f, tx, ty, 0f);
            }

            public override TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Matrix, _matrix.M11.ToString(CultureInfo.InvariantCulture), _matrix.M12.ToString(CultureInfo.InvariantCulture), _matrix.M21.ToString(CultureInfo.InvariantCulture), _matrix.M22.ToString(CultureInfo.InvariantCulture), _matrix.Tx.ToString(CultureInfo.InvariantCulture), _matrix.Ty.ToString(CultureInfo.InvariantCulture));
            }
        }

        public sealed class Matrix3D : CSSTransformValue
        {
            TransformMatrix _matrix;

            public Matrix3D(Single m11, Single m12, Single m13, Single m21, Single m22, Single m23, Single m31, Single m32, Single m33, Single tx, Single ty, Single tz)
            {
                _matrix = new TransformMatrix(m11, m12, m13, m21, m22, m23, m31, m32, m33, tx, ty, tz);
            }

            public override TransformMatrix ComputeMatrix()
            {
                return _matrix;
            }

            public override String ToCss()
            {
                return _matrix.ToCss();
            }
        }

        public sealed class Translate : CSSTransformValue
        {
            CSSCalcValue _x;
            CSSCalcValue _y;

            public Translate(CSSCalcValue x, CSSCalcValue y)
            {
                _x = x;
                _y = y;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate, _x.CssText, _y.CssText);
            }
        }

        public sealed class TranslateX : CSSTransformValue
        {
            CSSCalcValue _x;

            public TranslateX(CSSCalcValue x)
            {
                _x = x;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateX, _x.CssText);
            }
        }

        public sealed class TranslateY : CSSTransformValue
        {
            CSSCalcValue _y;

            public TranslateY(CSSCalcValue y)
            {
                _y = y;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dy = _y.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, dy, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateY, _y.CssText);
            }
        }

        public sealed class TranslateZ : CSSTransformValue
        {
            CSSCalcValue _z;

            public TranslateZ(CSSCalcValue z)
            {
                _z = z;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, dz);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.TranslateZ, _z.CssText);
            }
        }

        public sealed class Translate3D : CSSTransformValue
        {
            CSSCalcValue _x;
            CSSCalcValue _y;
            CSSCalcValue _z;

            public Translate3D(CSSCalcValue x, CSSCalcValue y, CSSCalcValue z)
            {
                _x = x;
                _y = y;
                _z = z;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var dx = _x.ToPixel();
                var dy = _y.ToPixel();
                var dz = _z.ToPixel();
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, dx, dy, dz);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Translate3d, _x.CssText, _y.CssText, _z.CssText);
            }
        }

        public sealed class Rotate : CSSTransformValue
        {
            Angle _angle;

            public Rotate(Angle angle)
            {
                _angle = angle;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                return new TransformMatrix(cosa, sina, 0f, -sina, cosa, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate, _angle.ToCss());
            }
        }

        public sealed class Rotate3D : CSSTransformValue
        {
            Single _x;
            Single _y;
            Single _z;
            Angle _angle;

            public Rotate3D(Single x, Single y, Single z, Angle angle)
            {
                _x = x;
                _y = y;
                _z = z;
                _angle = angle;
            }

            public static Rotate3D RotateX(Angle angle)
            {
                return new Rotate3D(1f, 0f, 0f, angle);
            }

            public static Rotate3D RotateY(Angle angle)
            {
                return new Rotate3D(0f, 1f, 0f, angle);
            }

            public static Rotate3D RotateZ(Angle angle)
            {
                return new Rotate3D(0f, 0f, 1f, angle);
            }

            public override TransformMatrix ComputeMatrix()
            {
                var norm = 1f / (Single)Math.Sqrt(_x * _x + _y * _y + _z * _z);
                var sina = _angle.Sin();
                var cosa = _angle.Cos();
                var l = _x * norm;
                var m = _y * norm;
                var n = _z * norm;
                var omc = (1f - cosa);
                return new TransformMatrix(
                    l * l * omc + cosa, m * l * omc - n * sina, n * l * omc + m * sina,
                    l * m * omc + n * sina, m * m * omc + cosa, n * m * omc - l * sina,
                    l * n * omc - m * sina, m * n * omc + l * sina, n * n * omc + cosa,
                    0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Rotate3d, _x.ToString(CultureInfo.InvariantCulture), _y.ToString(CultureInfo.InvariantCulture), _z.ToString(CultureInfo.InvariantCulture), _angle.ToCss());
            }
        }

        public sealed class Scale : CSSTransformValue
        {
            Single _sx;
            Single _sy;

            public Scale(Single scale)
            {
                _sx = scale;
                _sy = scale;
            }

            public Scale(Single sx, Single sy)
            {
                _sx = sx;
                _sy = sy;
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                if (_sx == _sy)
                    return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture));

                return FunctionNames.Build(FunctionNames.Scale, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture));
            }
        }

        public sealed class ScaleX : CSSTransformValue
        {
            Single _scale;

            public ScaleX(Single scale)
            {
                _scale = scale;
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleX, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        public sealed class ScaleY : CSSTransformValue
        {
            Single _scale;

            public ScaleY(Single scale)
            {
                _scale = scale;
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleY, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        public sealed class ScaleZ : CSSTransformValue
        {
            Single _scale;

            public ScaleZ(Single scale)
            {
                _scale = scale;
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f, _scale, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.ScaleZ, _scale.ToString(CultureInfo.InvariantCulture));
            }
        }

        public sealed class Scale3D : CSSTransformValue
        {
            Single _sx;
            Single _sy;
            Single _sz;

            public Scale3D(Single sx, Single sy, Single sz)
            {
                _sx = sx;
                _sy = sy;
                _sz = sz;
            }

            public override TransformMatrix ComputeMatrix()
            {
                return new TransformMatrix(_sx, 0f, 0f, 0f, _sy, 0f, 0f, 0f, _sz, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Scale3d, _sx.ToString(CultureInfo.InvariantCulture), _sy.ToString(CultureInfo.InvariantCulture), _sz.ToString(CultureInfo.InvariantCulture));
            }
        }

        public sealed class Skew : CSSTransformValue
        {
            Angle _alpha;
            Angle _beta;

            public Skew(Angle alpha, Angle beta)
            {
                _alpha = alpha;
                _beta = beta;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var a = _alpha.Tan();
                var b = _beta.Tan();
                return new TransformMatrix(1f, a, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.Skew, _alpha.ToCss(), _beta.ToCss());
            }
        }

        public sealed class SkewX : CSSTransformValue
        {
            Angle _angle;

            public SkewX(Angle alpha)
            {
                _angle = alpha;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var a = _angle.Tan();
                return new TransformMatrix(1f, a, 0f, 0f, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewX, _angle.ToCss());
            }
        }

        public sealed class SkewY : CSSTransformValue
        {
            Angle _angle;

            public SkewY(Angle beta)
            {
                _angle = beta;
            }

            public override TransformMatrix ComputeMatrix()
            {
                var b = _angle.Tan();
                return new TransformMatrix(1f, 0f, 0f, b, 1f, 0f, 0f, 0f, 1f, 0f, 0f, 0f);
            }

            public override String ToCss()
            {
                return FunctionNames.Build(FunctionNames.SkewY, _angle.ToCss());
            }
        }

        #endregion
    }
}
